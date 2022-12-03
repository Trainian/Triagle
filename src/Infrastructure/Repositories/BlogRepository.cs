using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories;
using Infrastructure.Repositories.Base;
using Infrastructure.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BlogRepository : Repository<Category>, IBlogRepository
    {
        public BlogRepository(WebContext context, ILogger<BlogRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        private readonly WebContext _context;
        private readonly ILogger<BlogRepository> _logger;

        public async Task<IReadOnlyList<Category>> GetAllCategorysAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Blogs.OrderByDescending(b => b.CreateDateTime))
                .ThenInclude(b => b.BlogMessages.Where(bm => bm.Visible == true))
                .ToListAsync();
            if (categories == null)
                _logger.LogWarning("Категории не найдены {Categories}", categories);
            return categories!;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Blogs.OrderByDescending(b => b.CreateDateTime))
                .ThenInclude(b => b.BlogMessages.Where(bm => bm.Visible == true))
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                _logger.LogWarning("Категория не найдена {Category}", category);
            return category;
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.BlogMessages.Where(bm => bm.Visible == true))
                .ThenInclude(bm => bm.ResponseToBlogMessage)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)
                _logger.LogWarning("Блог не найден {Blog}", blog);
            return blog;
        }

        public async Task<int> GetCountAllBlogsAsync()
        {
            var count = await _context.Blogs
                .CountAsync();
            if (count == 0)
                _logger.LogWarning("Колдичество блогов равно нулю {Count}", count);
            return count;
        }

        public async Task<IReadOnlyList<BlogMessage>> GetLastCountBlogMessagesAsync(int count)
        {
            var blogMessages = await _context.BlogMessages
                .OrderByDescending(m => m.CreateDateTime)
                .Take(count)
                .ToListAsync();
            return blogMessages;
        }

        public async Task<IReadOnlyList<Blog>> GetBlogsByCategoryIdAsync(int category)
        {
            var blogs = await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.BlogMessages.Where(bm => bm.Visible == true))
                .Where(b => b.Category.Id == category)
                .OrderByDescending(b => b.CreateDateTime)
                .ToListAsync();
            if (blogs == null)
                _logger.LogWarning("Блоги по категории не найдены {Blogs}", blogs);
            return blogs;
        }

        public async Task<IReadOnlyList<Blog>> GetAllBlogsAsync()
        {
            var blogs = await _context.Blogs
                .Include(b => b.BlogMessages.Where(bm => bm.Visible == true))
                .OrderByDescending(b => b.CreateDateTime)
                .ToListAsync();
            return blogs;
        }

        public async Task<Category> GetCategoryByNameAsync(string category)
        {
            var categorys = await _context.Categories
                .Include(c => c.Blogs)
                .FirstOrDefaultAsync(c => c.Name == category);
            if (categorys == null)
                _logger.LogWarning("Категория по Name не найдена {Category}", categorys);
            return categorys;
        }

        public async Task<int> GetCountBlogsByCategoryId(int category)
        {
            var count = await _context.Blogs
                .Include(b => b.Category)
                .Where(b => b.Category.Id == category)
                .CountAsync();
            return count;
        }

        public async Task<BlogMessage> GetBlogMessageByIdAsync(int id)
        {
            var blogMessage = await _context.BlogMessages
                .FirstOrDefaultAsync(m => m.Id == id);
            return blogMessage;
        }

        public async Task<bool> CreateNewBlogComment(int blogId, int commentId, string userName, string message)
        {
            var blog = await GetBlogByIdAsync(blogId);
            if (blog == null)
                return false;

            var blogMessage = new BlogMessage()
            {
                Blog = blog,
                CreateDateTime = DateTime.Now,
                CreateWebUser = userName,
                Text = message,
                Visible = false
            };

            BlogMessage? comment = null;
            if (commentId != 0)
                comment = await GetBlogMessageByIdAsync(commentId);
            if (commentId != 0 && comment == null)
            {
                _logger.LogWarning("Ошибка создания нового сообщения {BlogMessage}", blogMessage);
                return false;
            }

            if (comment != null)
                blogMessage.ResponseToBlogMessage = comment;

            await _context.BlogMessages.AddAsync(blogMessage);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Успешно создано новое сообщение {BlogMessage}", blogMessage);
            return true;
        }
    }
}
