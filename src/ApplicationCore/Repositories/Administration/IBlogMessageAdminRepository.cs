using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Administration
{
    public interface IBlogMessageAdminRepository : IRepository<BlogMessage>
    {
        Task<IReadOnlyList<BlogMessage>> GetBlogMessagesByBlogIdAsync (int blogId);
    }
}
