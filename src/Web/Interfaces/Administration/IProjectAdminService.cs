using ApplicationCore.Entities.Project;
using Web.ViewModels;

namespace Web.Interfaces.Administration
{
    public interface IProjectAdminService
    {
        Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync();
        Task<ProjectViewModel> GetProjectByIdAsync(int id);
        Task<string> CreateProjectAsync(ProjectViewModel project);
        Task<string> UpdateProjectAsync(ProjectViewModel project);
        Task<string> RemoveProjectByIdAsync(int id);
        Task<IEnumerable<SkillViewModel>> GetAllSkillsAsync();
        Task<IEnumerable<SkillViewModel>> GetListSkillsByIdAsync(IEnumerable<string> idList);
        Task<IEnumerable<CategoryProjectViewModel>> GetAllCategoriesAsync();
        Task<CategoryProjectViewModel> GetCategoryByIdAsync(int id);
        IEnumerable<string> GetAllImages();
    }
}
