using Web.ViewModels;

namespace Web.Interfaces.Administration
{
    public interface IBlogAdminService
    {
        Task<IEnumerable<BlogViewModel>?> GetBlogListAsync();
        Task<BlogViewModel?> GetBlogByIdAsync(int id);
        Task<string> AddBlogAsync(BlogViewModel blog);
        Task<string> UpdateBlogAsync(BlogViewModel blog);
        Task<string> RemoveBlogByIdAsync(int id);
        Task<IEnumerable<CategoryViewModel>?> GetAllCategoriesAsync();
        Task<CategoryViewModel?> GetCategoryByIdAsync(int id);
    }
}
