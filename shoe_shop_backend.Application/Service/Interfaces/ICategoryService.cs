using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetAllCategorysAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(string categoryId);
        Task<CategoryDTO> CreateCategoryAsync(CategoryRequest request);
        Task<bool> UpdateCategoryAsync(CategoryRequest request);
        Task<bool> DeleteCategoryAsync(string categoryId);
    }
}
