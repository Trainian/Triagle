using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Areas.Administration.Pages
{
    public class BlogMessagesModel : PageModel
    {
        public BlogMessagesModel(IBlogMessageAdminService service)
        {
            _service = service;
        }

        private IBlogMessageAdminService _service;

        [BindProperty]
        public IEnumerable<BlogMessageViewModel> BlogMessages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id != null)
                BlogMessages = await _service.GetBlogMessagesByBlogIdAsync((int)id);
            else
                BlogMessages = await _service.GetAllBlogMessagesAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetChangeVisibleAsync(int bmId, string returnUrl)
        {
            var result = await _service.ChangeVisibleAsync(bmId);
            TempData["Result"] = result;
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> OnGetRemoveAsync(int id, string returnUrl)
        {
            var result = await _service.RemoveBlogMessageByIdAsync(id);
            TempData["Result"] = result;
            return Redirect(returnUrl);
        }
    }
}
