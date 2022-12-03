using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories;
using AutoMapper;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ProjectService : IProjectService
    {
        public ProjectService(IProjectRepository repository, IIdentityRepository identity, IMapper mapper, ILogger<BlogPageService> logger)
        {
            _repository = repository;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
        }

        private readonly IProjectRepository _repository;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogPageService> _logger;

        public async Task<IEnumerable<CategoryProjectViewModel>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            var categoriesView = _mapper.Map<IEnumerable<CategoryProjectViewModel>>(categories);
            return categoriesView;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync()
        {
            var projects = await _repository.GetAllProjectsAsync();
            var projectsView = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);
            return projectsView;
        }

        public async Task<CategoryProjectViewModel> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            var categoryView = _mapper.Map<CategoryProjectViewModel>(category);
            return categoryView;
        }

        public async Task<CategoryProjectViewModel> GetCategoryByNameAsync(string category)
        {
            var categorys = await _repository.GetCategoryByNameAsync(category);
            var categoryView = _mapper.Map<CategoryProjectViewModel>(categorys);
            return categoryView;
        }

        public async Task<ProjectViewModel> GetProjectByIdAsync(int id)
        {
            var project = await _repository.GetProjectByIdAsync(id);
            var projectView = _mapper.Map<ProjectViewModel>(project);
            return projectView;
        }

        public async Task<int> GetProjectCountByCategoryIdAsync(int id)
        {
            var count = await _repository.GetProjectCountByCategoryIdAsync(id);
            return count;
        }

        public async Task<int> GetProjectCountByCategoryNameAsync(string category)
        {
            var count = await _repository.GetProjectCountByCategoryNameAsync(category);
            return count;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetProjectsByCategoryId(int id)
        {
            var projects = await _repository.GetProjectsByCategoryId(id);
            var projectsView = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);
            return projectsView;
        }

        public async Task<int> GetProjectCountAllAsync()
        {
            var count = await _repository.GetProjectCountAllAsync();
            return count;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetProjectsByCategoryNameAndPageAsync(string category, int page, int maxProjectsOnPage)
        {
            CategoryP categoryP = await _repository.GetCategoryByNameAsync(category);
            List<Project> projects = new List<Project>();
            if (categoryP == null || category == "Все")
                projects = (List<Project>)await _repository.GetAllProjectsAsync();
            else
                projects = (List<Project>)await _repository.GetProjectsByCategoryId(categoryP.Id);

            List<Project> sortedProjects = new List<Project>();
            for (int i = (page - 1) * maxProjectsOnPage; i < page * maxProjectsOnPage; i++)
                if (i < projects.Count())
                    sortedProjects.Add(projects[i]);

            var projectsView = _mapper.Map<IEnumerable<ProjectViewModel>>(sortedProjects);
            return projectsView;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetProjectsByCategoryIdSomeCount(int id, int count, int projectId)
        {
            var projects = (List<Project>)await _repository.GetProjectsByCategoryId(id);
            var projectRemove = await _repository.GetProjectByIdAsync(projectId);
            projects.Remove(projectRemove);
            if (projects == null || projects.Count == 0)
                return new List<ProjectViewModel>();
            else if (projects?.Count < count)
            {
                for (int i = projects.Count; i < count; i++)
                {
                    projects.Add(new Project());
                }
            }
            projects = projects?.Take(count).ToList();
            var projectsView = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);
            return projectsView;
        }
    }
}
