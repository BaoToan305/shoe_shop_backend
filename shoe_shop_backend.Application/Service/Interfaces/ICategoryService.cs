using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategorysAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(string categoryId);
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
        Task<bool> UpdateCategoryAsync(CategoryRequest request);
        Task<bool> DeleteCategoryAsync(string categoryId);
    }
}
