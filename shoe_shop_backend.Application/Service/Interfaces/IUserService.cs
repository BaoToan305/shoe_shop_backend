

using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(string userId);
        Task<UserResponse> CreateUserAsync(UserRequest request);
        Task<bool> UpdateUserAsync(UserRequest request);
        Task<bool> DeleteUserAsync(string userId);
    }
}
