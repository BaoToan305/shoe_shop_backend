using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Mapping
{
    public class BrandsMappingProfile : Profile
    {
        public BrandsMappingProfile()
        {
            CreateMap<Brands, BrandsDTO>();
            CreateMap<BrandsDTO, Brands>();
        }
    }
}
