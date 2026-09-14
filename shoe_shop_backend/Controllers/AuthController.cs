using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService loginService;
        public AuthController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await loginService.Login(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistRequest request)
        {
            await loginService.RegistAccount(request);
            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            var result = await loginService.Logout(refreshToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest request)
        {
            var response = await loginService.RefreshToken(request);
            return Ok(response);
        }
    }
}