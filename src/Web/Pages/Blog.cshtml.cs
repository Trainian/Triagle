using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class BlogModel : PageModel
    {
        public BlogModel(IBlogPageService service, ILogger<BlogModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private readonly IBlogPageService _service;
        private readonly ILogger<BlogModel> _logger;

        public IEnumerable<BlogViewModel> BlogsInPage { get; set; }
        public int MaxBlogsOnPage { get; set; }
        public new int Page { get; set; }
        public string CategoryChoised { get; set; }
        public int BlogsCount { get; set; }

        public async Task<IActionResult> OnGetAsync(string category = "Все", int pg = 1, int count = 10)
        {
            Page = pg;
            CategoryChoised = category;
            MaxBlogsOnPage = count;
            BlogsCount = await _service.GetCountBlogsByCategory(category);
            BlogsInPage = await _service.GetBlogsByCategoryNameAndPageAsync(CategoryChoised, Page, MaxBlogsOnPage);
            return Page();
        }

        public async Task<JsonResult> OnGetLikeAsync(int blogId)
        {
            return new JsonResult("Успешно") { StatusCode = 200 };
        }
    }
}
