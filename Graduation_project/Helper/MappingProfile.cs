using AutoMapper;
using Graduation_project.ViewModel;
using Repo_Core.Models;

namespace Graduation_project.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlanDots, Plan>();
                
        }
    }
}
