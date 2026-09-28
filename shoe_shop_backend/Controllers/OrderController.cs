using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByIdAsync([FromQuery] string orderId)
        {
            return Ok(await _orderService.GetOrderByIdAsync(orderId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequest request)
        {
            return Ok(await _orderService.CreateOrderAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] OrderRequest request)
        {
            return Ok(await _orderService.UpdateOrderAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderAsync([FromQuery] string orderId)
        {
            return Ok(await _orderService.DeleteOrderAsync(orderId));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToOrderAsync([FromBody] OrderItemRequest request)
        {
            return Ok(await _orderService.AddItemToOrderAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromOrderAsync([FromQuery] string orderItemId)
        {
            return Ok(await _orderService.RemoveItemFromOrderAsync(orderItemId));
        }
    }
}
