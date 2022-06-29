using AutoMapper;
using FishMarket.Dto;
using FishMarket.Entities.Concrete;
using Microsoft.AspNetCore.DataProtection;

namespace FishMarket.Api.Mappings
{
    public class FishMarketMapper : Profile
    {
        public FishMarketMapper(IDataProtector provider)
        {
            CreateMap<User, UserRegisterDto>().
              ForMember(u => u.Password, opt => opt.MapFrom(u2 => u2.Password));
            CreateMap<UserRegisterDto, User>().
              ForMember(u => u.Password, opt => opt.MapFrom(u2 => u2.Password));

            CreateMap<FishPrice, FishPriceUpdateDto>().
             ForMember(u => u.Price, opt => opt.MapFrom(u2 => u2.Price));
            CreateMap<FishPriceUpdateDto, FishPrice>();

        }
    }
}
