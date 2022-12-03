using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Areas.Administration.Pages
{
    public class ProjectsModel : PageModel
    {
        public ProjectsModel(IProjectAdminService service)
        {
            _service = service;
        }

        private IProjectAdminService _service;

        [BindProperty]
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await GetEntities();
            return Page();
        }

        public async Task<IActionResult> OnGetRemoveAsync(int id)
        {
            TempData["Result"] = await _service.RemoveProjectByIdAsync(id);
            await GetEntities();
            return Page();
        }

        private async Task GetEntities()
        {
            Projects = await _service.GetAllProjectsAsync();
        }
    }
}
