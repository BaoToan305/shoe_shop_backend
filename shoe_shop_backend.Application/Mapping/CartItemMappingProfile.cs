using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Mapping
{
    public class CartItemMappingProfile : Profile
    {
        public CartItemMappingProfile()
        {
            CreateMap<CartItem, CartItemDTO>();
            CreateMap<CartItemDTO, CartItem>();
        }
    }
}
