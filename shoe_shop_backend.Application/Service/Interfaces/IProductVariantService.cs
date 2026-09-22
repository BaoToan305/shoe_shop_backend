

using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface IProductVariantService
    {
        Task<List<ProductVariantResponse>> GetAllProductVariantsAsync();
        Task<ProductVariantResponse> GetProductVariantByIdAsync(string productVariantId);
        Task<ProductVariantResponse> CreateProductVariantAsync(ProductVariantRequest request);
        Task<bool> UpdateProductVariantAsync(ProductVariantRequest request);
        Task<bool> DeleteProductVariantAsync(string productVariantId);
    }
}
