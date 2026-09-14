using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service
{
    public interface ILoginService
    {
        Task<LoginRespponse> Login(LoginRequest request);

        Task<bool> RegistAccount(RegistRequest request);

        Task<bool> Logout(string refreshToken);

        Task<RefreshResponse> RefreshToken(RefreshRequest request);
    }
}
