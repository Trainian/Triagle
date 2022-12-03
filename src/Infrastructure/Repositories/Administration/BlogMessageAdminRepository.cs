using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories.Administration;
using Infrastructure.Repositories.Base;
using Infrastructure.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Administration
{
    public class BlogMessageAdminRepository : Repository<BlogMessage>, IBlogMessageAdminRepository
    {
        public BlogMessageAdminRepository(WebContext context) : base(context)
        {
            _context = context;
        }

        private WebContext _context;

        public async Task<IReadOnlyList<BlogMessage>> GetBlogMessagesByBlogIdAsync(int blogId)
        {
            return await _context.Set<BlogMessage>()
                .Include(bm => bm.Blog)
                .Include(bm => bm.ResponseToBlogMessage)
                .Where(bm => bm.Blog.Id == blogId)
                .ToListAsync();
        }

        public override async Task<IReadOnlyList<BlogMessage>> GetAllAsync()
        {
            return await _context.Set<BlogMessage>()
                .Include(bm => bm.Blog)
                .Include(bm => bm.ResponseToBlogMessage)
                .ToListAsync();
        }

        public override async Task<BlogMessage> GetByIdAsync(int id)
        {
            return await _context.Set<BlogMessage>()
                .Include(bm => bm.Blog)
                .Include(bm => bm.ResponseToBlogMessage)
                .FirstAsync(bm => bm.Id == id);
        }
    }
}
