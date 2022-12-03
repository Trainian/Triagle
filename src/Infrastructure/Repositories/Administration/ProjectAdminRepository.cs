using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories.Administration;
using Infrastructure.Repositories.Base;
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
    public class ProjectAdminRepository : Repository<Project>, IProjectAdminRepository
    {
        public ProjectAdminRepository(WebContext context, ILogger<ProjectAdminRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        private ILogger<ProjectAdminRepository> _logger;
        private WebContext _context;
        public override async Task<IReadOnlyList<Project>> GetAllAsync()
        {
            var projectList = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Skills)
                .ToListAsync();
            if (projectList.Count == 0)
                _logger.LogWarning("Не удалось найти, ни одного проекта");
            return projectList;
        }
        public override async Task<Project> GetByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == id);
            if(project == null)
            {
                _logger.LogWarning("Не удалось найти проект по ID {Id}", id);
                return new Project();
            }
            return project;
        }
    }
}
