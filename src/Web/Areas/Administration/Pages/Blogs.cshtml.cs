using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Areas.Administration.Pages
{
    public class BlogsModel : PageModel
    {
        public BlogsModel(IBlogAdminService service, ILogger<BlogsModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private IBlogAdminService _service;
        private ILogger<BlogsModel> _logger;

        [BindProperty]
        public IEnumerable<BlogViewModel> Blogs { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Blogs = await _service.GetBlogListAsync() ?? new List<BlogViewModel>();
            return Page();
        }

        public async Task<IActionResult> OnGetRemoveAsync(int id)
        {
            var result = await _service.RemoveBlogByIdAsync(id);
            TempData["Result"] = result;
            Blogs = await _service.GetBlogListAsync() ?? new List<BlogViewModel>();
            return Page();
        }
    }
}
