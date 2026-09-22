using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(string productId);
        Task<ProductResponse> CreateProductAsync(ProductRequest request);
        Task<bool> UpdateProductAsync(ProductRequest request);
        Task<bool> DeleteProductAsync(string productId);
    }
}
