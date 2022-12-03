using ApplicationCore.Entities.Base;
using ApplicationCore.Entities.Blog;
using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories;
using Infrastructure.Repositories.Base;
using Infrastructure.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SearchRepository : Repository<BaseObject>, ISearchRepository
    {
        public SearchRepository(WebContext context, ILogger<SearchRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        private WebContext _context { get; set; }
        private ILogger<SearchRepository> _logger { get; set; }

        public async Task<IDictionary<string, string>> GetBlogsNameByStringAsync(string fstring)
        {
            var dict = new Dictionary<string, string>();
            var find = new List<Blog>();
            try
            {
                find = await _context.Blogs.Where(b => b.Title.Contains(fstring)).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Ошибка поиска Блогов по части строки {Blogs}", e);
            }
            foreach (var element in find)
                dict.Add("/BlogDetails/" + element.Id.ToString(), element.Title);
            return dict;
        }

        public async Task<IDictionary<string, string>> GetProjectsNameByStringAsync(string fstring)
        {
            var dict = new Dictionary<string, string>();
            var find = new List<Project>();
            try
            {
                find = await _context.Projects.Where(p => p.ProjectName.Contains(fstring)).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Ошибка поиска Проектов по части строки {Projects}", e);
            }
            foreach (var element in find)
                dict.Add("/PortfolioDetails/" + element.Id.ToString(), element.ProjectName);
            return dict;
        }
    }
}
