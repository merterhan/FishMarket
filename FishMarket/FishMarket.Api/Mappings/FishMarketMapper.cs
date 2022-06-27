﻿using AutoMapper;
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
              ForMember(u => u.Password, opt => opt.MapFrom(u2 => provider.Unprotect(u2.Password)));
            CreateMap<UserRegisterDto, User>().
              ForMember(u => u.Password, opt => opt.MapFrom(u2 => provider.Protect(u2.Password)));
        }
    }
}
