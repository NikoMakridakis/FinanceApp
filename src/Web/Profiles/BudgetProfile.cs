using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Profiles
{
    public class BudgetProfile : Profile
    {
        public BudgetProfile()
        {
            CreateMap<Budget, BudgetDto>();
            CreateMap<BudgetForCreationDto, Budget>();
            CreateMap<BudgetForUpdateDto, Budget>();
        }
    }
}
