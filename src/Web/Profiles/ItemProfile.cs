using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();

            CreateMap<ItemForCreationDto, Item>();

            CreateMap<ItemForUpdateDto, Item>();
        }
    }
}
