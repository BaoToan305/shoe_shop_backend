
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetAllRolesAsync();
        Task<RoleResponse> GetRoleByIdAsync(string roleId);
        Task<RoleResponse> CreateRoleAsync(RoleRequest request);
        Task<bool> UpdateRoleAsync(RoleRequest request);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}
