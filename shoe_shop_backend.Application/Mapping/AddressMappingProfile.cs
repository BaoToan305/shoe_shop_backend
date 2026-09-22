using AutoMapper;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Mapping
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
