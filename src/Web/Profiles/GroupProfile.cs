using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupReadDto>();
            CreateMap<GroupCreateDto, Group>();
        }
    }
}
