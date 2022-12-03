using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class BlogDetailsModel : PageModel
    {
        public BlogDetailsModel(IBlogPageService service, ILogger<BlogDetailsModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private IBlogPageService _service { get; set; }
        private ILogger<BlogDetailsModel> _logger { get; set; }

        public string CategoryName { get; set; }
        public BlogViewModel Blog { get; set; }
        public IEnumerable<BlogMessageViewModel> MessagesSorted { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Blog = await _service.GetBlogByIdAsync(id);
            if (Blog == null)
                return NotFound();
            CategoryName = Blog.Category.Name;
            MessagesSorted = await _service.GetBlogMessagesSortedAsync(Blog);
            return Page();
        }
    }
}
