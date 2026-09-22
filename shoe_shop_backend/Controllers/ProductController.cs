using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest request)
        {
            return Ok(await _productService.CreateProductAsync(request));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync([FromQuery] string productId)
        {
            return Ok(await _productService.DeleteProductAsync(productId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByIdAsync([FromQuery] string productId)
        {
            return Ok(await _productService.GetProductByIdAsync(productId));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] string productId)
        {
            return Ok(await _productService.GetProductByIdAsync(productId));
        }

    }
}
