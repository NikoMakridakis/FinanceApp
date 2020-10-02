﻿using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}
