using AutoMapper;
using Graduation_project.ViewModel;
using Repo_Core.Identity_Models;
using Repo_Core.Models;

namespace Graduation_project.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlanDots ,Plan >();
            CreateMap<FeedbackView, Feedback>()
                .ForMember(dist => dist.PostId, src => src.MapFrom(src => src.PostId))
                .ForMember(dist => dist.UserId, src => src.MapFrom(src => src.UserId))
                .ForMember(dist => dist.comment, src => src.MapFrom(src => src.comment))
                .ForMember(dist => dist.feedbacktime, src => src.MapFrom(src => src.feedbacktime));
            CreateMap<Editting, ApplicationUser>();




        }
    }
}
