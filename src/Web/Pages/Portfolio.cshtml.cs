using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class PortfolioModel : PageModel
    {
        public PortfolioModel(IProjectService service, ILogger<PortfolioModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private readonly IProjectService _service;
        private readonly ILogger<PortfolioModel> _logger;

        public new int Page { get; set; }
        public string Category { get; set; }
        public int MaxProjectsOnPage { get; set; }
        public int CountProjects { get; set; }
        public IEnumerable<CategoryProjectViewModel> Categories { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public async Task<IActionResult> OnGetAsync(string category = "Все", int pg = 1, int count = 10)
        {
            Category = category;
            Page = pg;
            MaxProjectsOnPage = count;
            CountProjects = await _service.GetProjectCountAllAsync();
            Categories = await _service.GetAllCategoriesAsync();
            Projects = await _service.GetProjectsByCategoryNameAndPageAsync(category, pg, count);
            return Page();
        }
    }
}
