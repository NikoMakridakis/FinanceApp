using AutoMapper;
using Core.Entities;
using System.Security.Claims;
using Web.Models;

namespace Web.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //When registering a user, the email is used instead of the username.
            CreateMap<UserForRegisterDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<User, UserDto>();

            CreateMap<UserForExternalLoginDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
                //.ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Principal.FindFirst(ClaimTypes.GivenName).Value))
        }
    }
}
