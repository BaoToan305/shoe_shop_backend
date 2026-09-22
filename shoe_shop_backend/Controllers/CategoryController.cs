using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;

namespace shoe_shop_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorysAsync()
        {
            return Ok(await _categoryService.GetAllCategorysAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryRequest request)
        {
            return Ok(await _categoryService.CreateCategoryAsync(request));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery] string categoryId)
        {
            return Ok(await _categoryService.DeleteCategoryAsync(categoryId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByIdAsync([FromQuery] string categoryId)
        {
            return Ok(await _categoryService.GetCategoryByIdAsync(categoryId));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] CategoryRequest request)
        {
            return Ok(await _categoryService.UpdateCategoryAsync(request));
        }
    }
}
