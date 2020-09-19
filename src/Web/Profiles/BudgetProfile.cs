using AutoMapper;
using Core.Entities;
using Web.Extensions;
using Web.Models;

namespace Web.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<Budget, BudgetDto>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Date.GetTitleFromDate())
                );
        }
    }
}
