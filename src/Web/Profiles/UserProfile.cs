using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserLoginDto, UserDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
