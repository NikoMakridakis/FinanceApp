using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserForCreationDto, AppUser>();
            CreateMap<AppUserForUpdateDto, AppUser>();
        }
    }
}
