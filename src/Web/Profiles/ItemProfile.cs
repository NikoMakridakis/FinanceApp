using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemReadDto>();
            CreateMap<ItemCreateDto, Item>();
        }
    }
}
