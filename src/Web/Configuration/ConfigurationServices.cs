using ApplicationCore.Interfaces;
using ApplicationCore.Repositories;
using ApplicationCore.Repositories.Administration;
using ApplicationCore.Repositories.Base;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Administration;
using Infrastructure.Repositories.Base;
using Infrastructure.Services;
using Web.AutoMapper;
using Web.Interfaces;
using Web.Interfaces.Administration;
using Web.Services;
using Web.Services.Administration;

namespace Web.Configuration
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
            services.AddScoped(typeof(IBlogPageService), typeof(BlogPageService));
            services.AddScoped(typeof(IIdentityRepository), typeof(IdentityRepository));
            services.AddScoped(typeof(IProjectRepository), typeof(ProjectReposiroty));
            services.AddScoped(typeof(IProjectService), typeof(ProjectService));
            services.AddScoped(typeof(ISearchRepository), typeof(SearchRepository));
            services.AddScoped(typeof(ISearchService), typeof(SearchService));
            services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
            services.AddScoped(typeof(IWebUserRepository), typeof(WebUserRepository));
            services.AddScoped(typeof(IWebUserService), typeof(WebUserService));
            services.AddScoped(typeof(IBlogAdminRepository), typeof(BlogAdminRepository));
            services.AddScoped(typeof(IBlogAdminService), typeof(BlogAdminService));
            services.AddScoped(typeof(IBlogMessageAdminRepository), typeof(BlogMessageAdminRepository));
            services.AddScoped(typeof(IBlogMessageAdminService), typeof(BlogMessageAdminService));
            services.AddScoped(typeof(IProjectAdminRepository), typeof(ProjectAdminRepository));
            services.AddScoped(typeof(IProjectAdminService), typeof(ProjectAdminService));
            services.AddScoped(typeof(ICategoryProjectAdminRepository), typeof(CategoryProjectAdminRepository));
            services.AddScoped(typeof(ISkillsAdminRepository), typeof(SkillsAdminRepository));
            services.AddAutoMapper(typeof(WebMapper));
            return services;
        }
    }
}
