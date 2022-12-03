using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories;
using Infrastructure.Repositories.Base;
using Infrastructure.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class ProjectReposiroty : Repository<CategoryP>, IProjectRepository
    {
        public ProjectReposiroty(WebContext context, ILogger<ProjectReposiroty> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        private readonly WebContext _context;
        private readonly ILogger<ProjectReposiroty> _logger;

        public async Task<IReadOnlyList<CategoryP>> GetAllCategoriesAsync()
        {
            var categories = await _context.CategoriesProject
                .Include(cp => cp.Projects)
                .ToListAsync();
            if (categories == null)
                _logger.LogWarning("Категории не найдены {Categories}", categories);
            return categories;
        }

        public async Task<IReadOnlyList<Project>> GetAllProjectsAsync()
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Skills)
                .ToListAsync();
            if (projects == null)
                _logger.LogWarning("Проекты не найдены {Projects}", projects);
            return projects;
        }

        public async Task<CategoryP> GetCategoryByIdAsync(int id)
        {
            var category = await _context.CategoriesProject
                .Include(cp => cp.Projects)
                .FirstOrDefaultAsync(cp => cp.Id == id);
            if (category == null)
                _logger.LogWarning("Категория по Id не найдена", category);
            return category;
        }

        public async Task<CategoryP> GetCategoryByNameAsync(string category)
        {
            var categorys = await _context.CategoriesProject
                .Include(cp => cp.Projects)
                .FirstOrDefaultAsync(cp => cp.Name == category);
            if (categorys == null)
                _logger.LogWarning("Категория по Name не найдена {Category}", categorys);
            return categorys;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Skills)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
                _logger.LogWarning("Проект по Id не найден {Project}", project);
            return project;
        }

        public async Task<int> GetProjectCountByCategoryIdAsync(int id)
        {
            var count = await _context.Projects
                .Include(p => p.Category)
                .Where(p => p.Category.Id == id)
                .CountAsync();
            return count;
        }

        public async Task<int> GetProjectCountByCategoryNameAsync(string category)
        {
            var count = await _context.Projects
                .Include(p => p.Category)
                .Where(p => p.Category.Name == category)
                .CountAsync();
            return count;
        }

        public async Task<IReadOnlyList<Project>> GetProjectsByCategoryId(int id)
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Where(p => p.Category.Id == id)
                .ToListAsync();
            return projects;
        }

        public async Task<int> GetProjectCountAllAsync()
        {
            var count = await _context.Projects
                .CountAsync();
            return count;
        }
    }
}
