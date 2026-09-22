using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;
namespace shoe_shop_backend.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
           CreateMap<Product, ProductDTO>();
           CreateMap<ProductDTO, Product>();
        }
    }
}
