
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface IBrandService
    {
        Task<BrandResponse> GetAllBrandsAsync();
        Task<BrandsDTO> GetBrandByIdAsync(string brandId);
        Task<BrandsDTO> CreateBrandAsync(BrandRequest request);
        Task<bool> UpdateBrandAsync(BrandRequest request);
        Task<bool> DeleteBrandAsync(string brandId);
    }
}
