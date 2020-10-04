using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}
