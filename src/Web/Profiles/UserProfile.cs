using AutoMapper;
using Core.Entities;
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
        }
    }
}
