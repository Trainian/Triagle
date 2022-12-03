using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories.Administration;
using Infrastructure.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Administration
{
    public class BlogAdminRepository : IBlogAdminRepository
    {
        public BlogAdminRepository(WebContext context, ILogger<BlogAdminRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        private WebContext _context { get; }
        private ILogger<BlogAdminRepository> _logger { get; }

        public async Task<bool> AddBlogAsync(Blog blog)
        {
            var blogAdded = await _context.Blogs
                .AddAsync(blog);
            if(blogAdded == null)
            {
                _logger.LogWarning("Произошла ошибка при добавлении Блога {blog}", blog);
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<Category>?> GetAllCategoryListAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories == null)
                _logger.LogWarning("Ни одной категории не найдено");
            return categories;
        }

        public async Task<Blog?> GetBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.BlogMessages)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)
                _logger.LogWarning("Блог по Id не найден {Id}", id);
            return blog;
        }

        public async Task<IReadOnlyList<Blog>?> GetBlogListAsync()
        {
            var blogList = await _context.Blogs
                .Include(b => b.BlogMessages)
                .Include(b => b.Category)
                .ToListAsync();
            if(blogList == null)
                _logger.LogWarning("Блоги в базе отсутсвуют");
            return blogList;
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                _logger.LogWarning("Категория по Id не найдена {Id}", id);
            return category;
        }

        public async Task<bool> RemoveBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs
                .FirstOrDefaultAsync(b => b.Id == id);
            if(blog == null)
            {
                _logger.LogWarning("Не удалось найти блог, для удаления {Id}", id);
                return false;
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBlogAsync(Blog blog)
        {
            var blogUpdate = await _context.Blogs
                .FirstOrDefaultAsync(b => b.Id == blog.Id);
            if (blogUpdate == null)
            {
                _logger.LogWarning("Не удалось найти блог, для обновления {Blog}", blog);
                return false;
            }

            _context.Update(blog);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
