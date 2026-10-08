using Microsoft.IdentityModel.Tokens;
using shoe_shop_backend.Domain.Exception;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace shoe_shop_backend.API.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    // Các path không cần token (login, register, public API...)
    private static readonly string[] _whitelist =
    {
        "/api/auth/login",
        "/api/auth/register",
        "/swagger"
    };

    public TokenValidationMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;

        if (_whitelist.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
        {
            await _next(context);
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            throw new UnauthorizedAppException("Thiếu token xác thực");

        var token = authHeader["Bearer ".Length..].Trim();

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!);

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = handler.ValidateToken(token, validationParams, out var validatedToken);

            // Gán lại vào HttpContext.User để các tầng sau (controller) dùng User.Claims
            context.User = principal;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new UnauthorizedAppException("Token đã hết hạn  ");
        }
        catch (SecurityTokenMalformedException)
        {
            throw new UnauthorizedAppException("Token sai định dạng");
        }
        catch (Exception)
        {
            throw new UnauthorizedAppException("Token không hợp lệ");
        }

        await _next(context);
    }
}

public static class TokenValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenValidation(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TokenValidationMiddleware>();
    }
}