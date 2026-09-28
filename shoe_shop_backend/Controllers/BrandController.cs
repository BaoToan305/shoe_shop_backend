using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            return Ok(await _brandService.GetAllBrandsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandByIdAsync([FromQuery] string brandId)
        {
            return Ok(await _brandService.GetBrandByIdAsync(brandId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrandAsync([FromBody] BrandRequest request)
        {
            return Ok(await _brandService.CreateBrandAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrandAsync([FromBody] BrandRequest request)
        {
            return Ok(await _brandService.UpdateBrandAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrandAsync([FromQuery] string brandId)
        {
            return Ok(await _brandService.DeleteBrandAsync(brandId));
        }
    }
}
