using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories.Administration;
using AutoMapper;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Services.Administration
{
    public class ProjectAdminService : IProjectAdminService
    {
        public ProjectAdminService(IProjectAdminRepository repository, ICategoryProjectAdminRepository categoryRepository,
            ISkillsAdminRepository skillsRepository, IMapper mapper, ILogger<ProjectAdminService> logger, IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _categoryRepository = categoryRepository;
            _skillsRepository = skillsRepository;
            _environment = environment;
        }

        private IProjectAdminRepository _repository;
        private IMapper _mapper;
        private ILogger<ProjectAdminService> _logger;
        private ICategoryProjectAdminRepository _categoryRepository;
        private ISkillsAdminRepository _skillsRepository;
        private IWebHostEnvironment _environment;

        public async Task<string> CreateProjectAsync(ProjectViewModel project)
        {
            var projectModel = _mapper.Map<Project>(project);
            var skillList = new List<Skill>();
            foreach (var skill in projectModel.Skills)
            {
                var findSkill = await _skillsRepository.GetByIdAsync(skill.Id);
                skillList.Add(findSkill);
            }

            projectModel.Skills = skillList;
            projectModel.Category = await _categoryRepository.GetByIdAsync(projectModel.Category.Id);
            await _repository.AddAsync(projectModel);
            _logger.LogInformation("Успешное создание нового Проекта {ProjectModel}", projectModel);
            return "Успешное добавление Проекта";
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync()
        {
            var projectsListModel = await _repository.GetAllAsync();
            if (projectsListModel.Count == 0)
            {
                _logger.LogWarning("Не удалось найти ни одного Проекта");
                return new List<ProjectViewModel>();
            }
            var projectsListView = _mapper.Map<IEnumerable<ProjectViewModel>>(projectsListModel);
            return projectsListView;
        }

        public async Task<ProjectViewModel> GetProjectByIdAsync(int id)
        {
            var projectModel = await _repository.GetByIdAsync(id);
            if (projectModel?.Id == 0)
                return new ProjectViewModel();
            var projectView = _mapper.Map<ProjectViewModel>(projectModel);
            return projectView;
        }

        public async Task<string> RemoveProjectByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if(project?.Id == 0)
            {
                _logger.LogWarning("Не удалось найти Проект для удаления по Id {Id}", id);
                return "Ошибка удаления Проекта";
            }
            await _repository.DeleteAsync(project!);
            return "Успешное удаление Проекта";
        }

        public async Task<string> UpdateProjectAsync(ProjectViewModel project)
        {
            var projectModel = await _repository.GetByIdAsync(project.Id);
            if (project?.Id == 0)
            {
                _logger.LogWarning("Не удалось найти Проект для обновления по Id {Id}", project?.Id);
                return "Ошибка обновления Проекта";
            }
            var projectViewToModel = _mapper.Map<Project>(project);
            var skillList = new List<Skill>();
            foreach (var skill in projectViewToModel.Skills)
            {
                var findSkill = await _skillsRepository.GetByIdAsync(skill.Id);
                skillList.Add(findSkill);
            }

            projectModel.ProjectName = projectViewToModel.ProjectName;
            projectModel.Text = projectViewToModel.Text;
            projectModel.PreviewLink = projectViewToModel.PreviewLink;
            projectModel.Image = projectViewToModel.Image;
            projectModel.Skills = skillList;
            projectModel.Category = await _categoryRepository.GetByIdAsync(projectViewToModel.Category.Id);
            await _repository.UpdateAsync(projectModel);
            _logger.LogInformation("Успешное создание нового Проекта {ProjectModel}", projectModel);
            return "Успешное обновление Проекта";
        }

        public async Task<IEnumerable<SkillViewModel>> GetAllSkillsAsync()
        {
            var skillsModel = await _skillsRepository.GetAllAsync();
            if(skillsModel == null)
            {
                _logger.LogWarning("Не удалось найти ни одного Скилла");
                return new List<SkillViewModel>();
            }
            var skillsView = _mapper.Map<IEnumerable<SkillViewModel>>(skillsModel);
            return skillsView;
        }

        public async Task<IEnumerable<CategoryProjectViewModel>> GetAllCategoriesAsync()
        {
            var categoriesModel = await _categoryRepository.GetAllAsync();
            if(categoriesModel == null)
            {
                _logger.LogWarning("Не удалось найти ни одной Категории");
                return new List<CategoryProjectViewModel>();
            }
            var categoriesView = _mapper.Map<IEnumerable<CategoryProjectViewModel>>(categoriesModel);
            return categoriesView;
        }

        public IEnumerable<string> GetAllImages()
        {
            var images = _environment.WebRootFileProvider.GetDirectoryContents("images/portfolio").Select(p => p.Name).ToList();
            if (images == null)
            {
                _logger.LogWarning("Папка: '/images/portfolio', не содержит файлов");
                return new List<string>() { "" };
            }
            return images;
        }

        public async Task<CategoryProjectViewModel> GetCategoryByIdAsync(int id)
        {
            var categoryModel = await _categoryRepository.GetByIdAsync(id);
            if(categoryModel?.Id == 0)
            {
                _logger.LogWarning("Не удалось найти Категорию по Id {Id}", id);
                return new CategoryProjectViewModel();
            }
            var categoryView = _mapper.Map<CategoryProjectViewModel>(categoryModel);
            return categoryView;
        }

        public async Task<IEnumerable<SkillViewModel>> GetListSkillsByIdAsync(IEnumerable<string> idList)
        {
            List<Skill> skills = new List<Skill>();
            foreach(var skill in idList)
            {
                int id = 0;
                int.TryParse(skill, out id);

                if (id == 0)
                    break;

                var skillModel = await _skillsRepository.GetByIdAsync(id);

                if (skillModel.Id != 0)
                    skills.Add(skillModel);
                else
                    _logger.LogWarning("Не удалось найти Скилл по Id {Id}", id);
            }
            var skillsView = _mapper.Map<IEnumerable<SkillViewModel>>(skills);
            return skillsView;
        }
    }
}
