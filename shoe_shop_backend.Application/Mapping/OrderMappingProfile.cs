using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
