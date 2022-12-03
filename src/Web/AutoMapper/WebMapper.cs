using ApplicationCore.Entities.Blog;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Project;
using AutoMapper;
using Web.ViewModels;
using Web.ViewModels.Administration;

namespace Web.AutoMapper
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<Blog, BlogViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<BlogMessage, BlogMessageViewModel>().ReverseMap();
            CreateMap<CategoryP, CategoryProjectViewModel>().ReverseMap();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Skill, SkillViewModel>().ReverseMap();
            CreateMap<WebUser, WebUserViewModel>().ReverseMap();
        }
    }
}
