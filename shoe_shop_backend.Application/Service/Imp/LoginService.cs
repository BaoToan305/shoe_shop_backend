using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NUlid;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Domain.Exception;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Application.Service
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        


        public LoginService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<LoginRespponse> Login(LoginRequest request)
        {
            if (request == null)
                throw new BadRequestException("Dữ liệu đăng nhập không được để trống");

            if (string.IsNullOrWhiteSpace(request.Username))
                throw new BadRequestException("Tên đăng nhập không được để trống");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new BadRequestException("Mật khẩu không được để trống");

            var user = _unitOfWork.Repository<Users>().Find(x => x.UserName == request.Username).FirstOrDefault();
            if (user == null)
                throw new BadRequestException("Tên đăng nhập không chính xác");

            if(user.IsActive == false)
                throw new BadRequestException("Tài khoản đã bị khóa");

            if (user.Password != request.Password)
                throw new BadRequestException("Mật khẩu không đúng");

            
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();


            var refreshTokenEntity = new UserSession
            {
                Id = Ulid.NewUlid().ToString(),
                UserId = user.Id,
                TokenHash = HashToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            var response = new LoginRespponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.Phone,
                RoleId = user.RoleId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = int.Parse(_config["Jwt:AccessTokenExpireMinutes"] ?? "30") * 60
            };



            await _unitOfWork.Repository<UserSession>().AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return response;
        }

        public async Task<bool> RegistAccount(RegistRequest request)
        {
            if (request == null)
                throw new BadRequestException("Dữ liệu đăng ký không được để trống");

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new BadRequestException("Tên đăng nhập không được để trống");
            }

            if(string.IsNullOrWhiteSpace(request.Password))
            {
                throw new BadRequestException("Mật khẩu không được để trống");
            }

            if (string.IsNullOrWhiteSpace(request.ConfirmPassword))
            {
                throw new BadRequestException("Xác nhận mật khẩu không được để trống");
            }

            if(request.Password != request.ConfirmPassword)
            {
                throw new BadRequestException("Mật khẩu và xác nhận mật khẩu không khớp");
            }

            var user = _unitOfWork.Repository<Users>().Find(x => x.UserName == request.Username).FirstOrDefault();
            if(user != null)
            {
                throw new BadRequestException("Tên đăng nhập đã tồn tại");
            }

            var newUser = new Users
            {
                Id = UnityHelper.GenerateUlid(),
                UserName = request.Username,
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.PhoneNumber,
                RoleId = "user",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _unitOfWork.Repository<Users>().AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Logout(string refreshToken)
        {
            var tokenHash = HashToken(refreshToken);
            var session = await _unitOfWork.Repository<UserSession>()
                .FirstOrDefaultAsync(s => s.TokenHash == tokenHash);

            if (session != null && session.RevokedAt == null)
            {
                session.RevokedAt = DateTime.UtcNow;
                session.IsActive = false;
                await _unitOfWork.Repository<UserSession>().UpdateAsync(session);
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<RefreshResponse> RefreshToken(RefreshRequest request)
        {
            var tokenHash = HashToken(request.RefreshToken ?? string.Empty);
            var session = await _unitOfWork.Repository<UserSession>()
                .FirstOrDefaultAsync(s => s.TokenHash == tokenHash);

            if (session == null)
                throw new BusinessRuleException("Invalid refresh token");

            if (session.RevokedAt != null)
            {
                // Token cũ đã bị dùng lại => nghi ngờ bị đánh cắp
                // => revoke toàn bộ session còn active của user này để an toàn
                await _unitOfWork.Repository<UserSession>().UpdateRangeAsync([session]);
                throw new BusinessRuleException("Refresh token has been revoked. Please login again.");
            }

            if (session.ExpiresAt < DateTime.UtcNow)
                throw new BusinessRuleException("Refresh token expired");

            var user = await _unitOfWork.Repository<Users>().GetByIdAsync(session.UserId ?? string.Empty);
            if (user == null || !user.IsActive)
                throw new BusinessRuleException("User not found or inactive");

            // Rotate: thu hồi session cũ, tạo session mới
            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            var newSession = new UserSession
            {
                Id = Ulid.NewUlid().ToString(),
                UserId = user.Id,
                TokenHash = HashToken(newRefreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(double.Parse(_config["Jwt:RefreshTokenExpireDays"]!)),
                IsActive = true
            };

            session.RevokedAt = DateTime.UtcNow;
            session.ReplacedBy = newSession.Id;
            session.IsActive = false;

            await _unitOfWork.Repository<UserSession>().UpdateAsync(session);
            await _unitOfWork.Repository<UserSession>().AddAsync(newSession);

            var response = new RefreshResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = int.Parse(_config["Jwt:AccessTokenExpireMinutes"]!) * 60
            };
            return response;
        }


        public string GenerateAccessToken(Users user)
        {
            var secretKey = _config["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("Jwt:SecretKey is not configured");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpireMinutes"] ?? "15")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(bytes);
        }


    }
}
