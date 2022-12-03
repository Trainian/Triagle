using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces;

namespace Web.Pages
{
    public class BlogCommentModel : PageModel
    {
        public BlogCommentModel(IBlogPageService service, ILogger<BlogCommentModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private readonly IBlogPageService _service;
        private readonly ILogger<BlogCommentModel> _logger;

        [Required]
        [BindProperty(SupportsGet = true)]
        public int BlogId { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public int CommentId { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string UserName { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateNewBlogComment(BlogId, CommentId, UserName, Message);
                TempData["Result"] = result;
            }
            else
            {
                TempData["Result"] = "При отправке сообщения, возникла непредвиденная ошибка, пожалуйста, попробуйте позже.";
            }
            return RedirectToPage("./BlogDetails", new { id = BlogId });
        }
    }
}
