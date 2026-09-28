using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] string userId)
        {
            return Ok(await _userService.GetUserByIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
        {
            return Ok(await _userService.CreateUserAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserRequest request)
        {
            return Ok(await _userService.UpdateUserAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string userId)
        {
            return Ok(await _userService.DeleteUserAsync(userId));
        }
    }
}
