using ApplicationCore.Entities.Base;
using ApplicationCore.Entities.Blog;
using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public interface ISearchRepository : IRepository<BaseObject>
    {
        Task<IDictionary<string,string>> GetBlogsNameByStringAsync(string fstring);
        Task<IDictionary<string, string>> GetProjectsNameByStringAsync(string fstring);
    }
}
