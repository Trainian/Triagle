using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<CategoryProjectViewModel>> GetAllCategoriesAsync();
        Task<CategoryProjectViewModel> GetCategoryByIdAsync(int id);
        Task<CategoryProjectViewModel> GetCategoryByNameAsync(string category);
        Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync();
        Task<IEnumerable<ProjectViewModel>> GetProjectsByCategoryId(int id);
        Task<ProjectViewModel> GetProjectByIdAsync(int id);
        Task<int> GetProjectCountByCategoryIdAsync(int id);
        Task<int> GetProjectCountByCategoryNameAsync(string category);
        Task<int> GetProjectCountAllAsync();
        Task<IEnumerable<ProjectViewModel>> GetProjectsByCategoryNameAndPageAsync(string category, int page, int maxProjectsOnPage);
        Task<IEnumerable<ProjectViewModel>> GetProjectsByCategoryIdSomeCount(int id, int count, int projectId);
    }
}
