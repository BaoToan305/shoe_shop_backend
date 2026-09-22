using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Mapping
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
        }
    }
}
