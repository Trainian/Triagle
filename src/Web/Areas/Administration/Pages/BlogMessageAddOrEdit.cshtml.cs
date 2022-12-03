using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Areas.Administration.Pages
{
    public class BlogMessageAddOrEditModel : PageModel
    {
        public BlogMessageAddOrEditModel(IBlogMessageAdminService service, IBlogAdminService blogService, IWebUserService webUserService)
        {
            _service = service;
            _blogService = blogService;
            _webUserService = webUserService;
        }

        private IBlogMessageAdminService _service;
        private IBlogAdminService _blogService;
        private IWebUserService _webUserService;

        [BindProperty]
        public BlogMessageViewModel BlogMessage { get; set; }

        public IEnumerable<BlogViewModel> BlogsList { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, int? responseMessageId)
        {
            var blogsList = await _blogService.GetBlogListAsync();
            BlogsList = blogsList ?? new List<BlogViewModel>();

            if (id == null)
            {
                BlogMessage = new BlogMessageViewModel();
                if(responseMessageId != null)
                {
                    var response = await _service.GetBlogMessageByIdAsync((int)responseMessageId);
                    if(response != null)
                    {
                        BlogMessage.ResponseToBlogMessage = response;
                        BlogsList = new List<BlogViewModel>() { response.Blog };
                    }
                }
            }
            else
                BlogMessage = await _service.GetBlogMessageByIdAsync((int)id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            if(BlogMessage.Id == 0)
                BlogMessage.CreateWebUser = User?.Identity?.Name ?? "Hacked";
            if (BlogMessage.Blog?.Id != null)
            {
#pragma warning disable CS8602 // –азыменование веро€тной пустой ссылки.
                ModelState.GetValueOrDefault("BlogMessage.Blog.Text").ValidationState = ModelValidationState.Valid;
                ModelState.GetValueOrDefault("BlogMessage.Blog.Image").ValidationState = ModelValidationState.Valid;
                ModelState.GetValueOrDefault("BlogMessage.Blog.Title").ValidationState = ModelValidationState.Valid;
                ModelState.GetValueOrDefault("BlogMessage.Blog.Category").ValidationState = ModelValidationState.Valid;
                ModelState.GetValueOrDefault("BlogMessage.Blog.CreateWebUser").ValidationState = ModelValidationState.Valid;
            }
            if(BlogMessage.ResponseToBlogMessage?.Id != null)
            {
                ModelState.GetValueOrDefault("BlogMessage.ResponseToBlogMessage.Blog").ValidationState = ModelValidationState.Valid;
                ModelState.GetValueOrDefault("BlogMessage.ResponseToBlogMessage.Text").ValidationState = ModelValidationState.Valid;
            }
#pragma warning restore CS8602 // –азыменование веро€тной пустой ссылки.
            if (ModelState.IsValid)
            {
                string result;
                if (BlogMessage.Id == 0)
                    result = await _service.CreateBlogMessageAsync(BlogMessage);
                else
                    result = await _service.UpdateBlogMessageAsync(BlogMessage);

                TempData["Result"] = result;
                return RedirectToPage("./BlogMessages");
            }

            var blogsList = await _blogService.GetBlogListAsync();
            BlogsList = blogsList ?? new List<BlogViewModel>();
            return Page();
        }
    }
}
