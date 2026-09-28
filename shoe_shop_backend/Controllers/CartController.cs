using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartsAsync()
        {
            return Ok(await _cartService.GetAllCartsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetCartByIdAsync([FromQuery] string cartId)
        {
            return Ok(await _cartService.GetCartByIdAsync(cartId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartAsync([FromBody] CartRequest request)
        {
            return Ok(await _cartService.CreateCartAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartAsync([FromBody] CartRequest request)
        {
            return Ok(await _cartService.UpdateCartAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartAsync([FromQuery] string cartId)
        {
            return Ok(await _cartService.DeleteCartAsync(cartId));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCartAsync([FromBody] CartItemRequest request)
        {
            return Ok(await _cartService.AddItemToCartAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromCartAsync([FromQuery] string cartItemId)
        {
            return Ok(await _cartService.RemoveItemFromCartAsync(cartItemId));
        }
    }
}
