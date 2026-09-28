using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleByIdAsync([FromQuery] string roleId)
        {
            return Ok(await _roleService.GetRoleByIdAsync(roleId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleRequest request)
        {
            return Ok(await _roleService.CreateRoleAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleRequest request)
        {
            return Ok(await _roleService.UpdateRoleAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoleAsync([FromQuery] string roleId)
        {
            return Ok(await _roleService.DeleteRoleAsync(roleId));
        }
    }
}
