using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Interfaces.Administration;
using Web.Services.Administration;
using Web.ViewModels;

namespace Web.Areas.Administration.Pages
{
    public class BlogAddOrEditModel : PageModel
    {
        public BlogAddOrEditModel(IBlogAdminService service, ILogger<BlogAddOrEditModel> logger, IWebHostEnvironment environment)
        {
            _service = service;
            _logger = logger;
            _environment = environment;
        }

        private IBlogAdminService _service;
        private ILogger<BlogAddOrEditModel> _logger;
        private IWebHostEnvironment _environment;

        [BindProperty(SupportsGet = true)]
        public BlogViewModel Blog { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public List<string> Images { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public IFormFile? Image { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await GetContentAsync();

            if (id == null)
            {
                Blog = new BlogViewModel() { CreateWebUser = User?.Identity?.Name ?? "Hacked" };
                return Page();
            }

            var blog = await _service.GetBlogByIdAsync((int)id);

            if (blog == null)
                return NotFound();

            Blog = blog;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(Blog.Category?.Id != null)
            {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                ModelState.GetValueOrDefault("Blog.Category.Name").ValidationState = ModelValidationState.Valid;
                ModelState.GetValueOrDefault("Blog.Category.Information").ValidationState = ModelValidationState.Valid;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            }
            if (ModelState.IsValid)
            {
                if(Blog.Id == 0)
                {
                    TempData["Result"] = await _service.AddBlogAsync(Blog);                    
                }
                else
                {
                    TempData["Result"] = await _service.UpdateBlogAsync(Blog);
                }
                return RedirectToPage("./Blogs");
            }
            await GetContentAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            if(Input.Image != null)
            {
                using (var fileStream = new FileStream(_environment.WebRootPath + "/images/blog/" + Input.Image.FileName, FileMode.Create))
                {
                    await Input.Image.CopyToAsync(fileStream);
                }
                await GetContentAsync();
                Blog.Image = "/images/blog/" + Input.Image.FileName;
                TempData["Result"] = "Успешная загрузка файла, на сервер";
                return Page();
            }
            await GetContentAsync();
            return Page();
        }

        private async Task GetContentAsync()
        {
            Images = _environment.WebRootFileProvider.GetDirectoryContents("images/blog").Select(e => e.Name).ToList();
            Categories = await _service.GetAllCategoriesAsync() ?? new List<CategoryViewModel>();
        }
    }
}
