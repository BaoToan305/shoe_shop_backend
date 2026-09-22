using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Mapping
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<Cart, CartDTO>();
            CreateMap<CartDTO, Cart>();
        }
    }
}
