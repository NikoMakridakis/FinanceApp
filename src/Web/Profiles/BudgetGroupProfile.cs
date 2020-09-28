using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class BudgetGroupProfile : Profile
    {
        public BudgetGroupProfile()
        {
            CreateMap<BudgetGroup, BudgetGroupDto>();
            CreateMap<BudgetGroupForCreationDto, BudgetGroup>();
            CreateMap<BudgetGroupForUpdateDto, BudgetGroup>();
        }
    }
}
