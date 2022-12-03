using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories.Base;

namespace ApplicationCore.Repositories
{
    public interface IProjectRepository : IRepository<CategoryP>
    {
        Task<IReadOnlyList<CategoryP>> GetAllCategoriesAsync();
        Task<CategoryP> GetCategoryByIdAsync(int id);
        Task<CategoryP> GetCategoryByNameAsync(string category);
        Task<IReadOnlyList<Project>> GetAllProjectsAsync();
        Task<IReadOnlyList<Project>> GetProjectsByCategoryId(int id);
        Task<Project> GetProjectByIdAsync(int id);
        Task<int> GetProjectCountByCategoryIdAsync(int id);
        Task<int> GetProjectCountByCategoryNameAsync(string category);
        Task<int> GetProjectCountAllAsync();
    }
}
