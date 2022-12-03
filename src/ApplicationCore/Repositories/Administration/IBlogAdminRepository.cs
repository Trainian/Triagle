using ApplicationCore.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Administration
{
    public interface IBlogAdminRepository
    {
        Task<IReadOnlyList<Blog>?> GetBlogListAsync();
        Task<Blog?> GetBlogByIdAsync(int id);
        Task<bool> AddBlogAsync(Blog blog);
        Task<bool> UpdateBlogAsync(Blog blog);
        Task<bool> RemoveBlogByIdAsync(int id);
        Task<IReadOnlyList<Category>?> GetAllCategoryListAsync();
        Task<Category?> GetCategoryById(int id);
    }
}
