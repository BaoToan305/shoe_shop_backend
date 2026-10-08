using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service
{
    public interface IProductService
    {
        Task<ProductResponse> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(string productId);
        Task<ProductDTO> CreateProductAsync(ProductRequest request);
        Task<bool> UpdateProductAsync(ProductRequest request);
        Task<bool> DeleteProductAsync(string productId);
    }
}
