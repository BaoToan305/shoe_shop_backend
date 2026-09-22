using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service;
using shoe_shop_backend.Application.Service.Imp;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/action")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _productVariantService;
        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductVariantsAsync()
        {
            return Ok(await _productVariantService.GetAllProductVariantsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductVariantAsync([FromBody] ProductVariantRequest request)
        {
            return Ok(await _productVariantService.CreateProductVariantAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductVariantAsync([FromQuery] string productId)
        {
            return Ok(await _productVariantService.DeleteProductVariantAsync(productId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductVariantByIdAsync([FromQuery] string productId)
        {
            return Ok(await _productVariantService.GetProductVariantByIdAsync(productId));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductVariantAsync([FromBody] ProductVariantRequest request)
        {
            return Ok(await _productVariantService.UpdateProductVariantAsync(request));
        }
    }
}
