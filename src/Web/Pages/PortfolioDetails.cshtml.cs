using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class PortfolioDetailsModel : PageModel
    {
        public PortfolioDetailsModel(IProjectService service, ILogger<PortfolioDetailsModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private readonly IProjectService _service;
        private readonly ILogger<PortfolioDetailsModel> _logger;

        public ProjectViewModel Project { get; set; }
        public IEnumerable<ProjectViewModel> RelatedProjects { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _service.GetProjectByIdAsync(id);
            if (Project == null)
            {
                _logger.LogWarning("Проект не найден {Project}", Project);
                return RedirectToPage("Portfolio");
            }
            RelatedProjects = await _service.GetProjectsByCategoryIdSomeCount(Project.Category.Id, 4, Project.Id);
            return Page();
        }
    }
}
