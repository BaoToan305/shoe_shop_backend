using AutoMapper;
using Microsoft.Extensions.Configuration;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Application.Service.Imp
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userEntity = _mapper.Map<Users>(request);

            await _unitOfWork.Repository<Users>().AddAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();

            var userResponse = _mapper.Map<UserResponse>(userEntity);
            return userResponse;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var user = await _unitOfWork.Repository<Users>().GetByIdAsync(userId);
                if (user != null)
                {
                    _unitOfWork.Repository<Users>().Delete(user);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var listUser = await _unitOfWork.Repository<Users>().GetAllAsync();
            if (listUser == null || !listUser.Any())
            {
                return [];
            }
            return listUser.Select(u => _mapper.Map<UserResponse>(u)).ToList();
        }

        public async Task<UserResponse> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }
            var user = await _unitOfWork.Repository<Users>().GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID '{userId}' not found.");
            }
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> UpdateUserAsync(UserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var user = await _unitOfWork.Repository<Users>().GetByIdAsync(request.Id ?? string.Empty);
                if (user != null)
                {
                    UnityHelper.CopyProperties(request, user);

                    await _unitOfWork.Repository<Users>().UpdateAsync(user);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
