using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces.Administration;
using Web.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.Areas.Administration.Pages
{
    public class ProjectAddOrEditModel : PageModel
    {
        public ProjectAddOrEditModel(IProjectAdminService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        private IProjectAdminService _service;
        private IWebHostEnvironment _environment;

        [BindProperty]
        public ProjectViewModel Project { get; set; }
        public IEnumerable<SkillViewModel> Skills { get; set; }
        public IEnumerable<CategoryProjectViewModel> Categories { get; set; }
        public IEnumerable<string> Images { get; set; }
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Загрузить изображение")]
            public IFormFile? Image { get; set; }
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            await GetElements();
            if(id == null)
            {
                Project = new ProjectViewModel() { Category = new CategoryProjectViewModel()};
            }
            else
            {
                var project = await _service.GetProjectByIdAsync((int)id);
                if(project == null)
                {
                    TempData["Result"] = "Проекты с такми ID не существует";
                    return RedirectToPage("./Projects");
                }
                Project = project;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            if (Project.Id == 0)
            {
                Project.CreateWebUser = User.Identity?.Name ?? "Hacked";
                Project.CreateDateTime = DateTime.Now;
            }
            var skills = Request.Form["Project.Skills"].ToList();
            if(skills != null && skills?.Count != 0)
            {
                var skillsView = await _service.GetListSkillsByIdAsync(skills);
                Project.Skills.AddRange(skillsView);
            }
            if (Project.Category?.Id != null)
            {
                Project.Category = await _service.GetCategoryByIdAsync((int)Project.Category.Id);
            }

            ModelState.ClearValidationState(nameof(Project));
            if (TryValidateModel(Project, nameof(Project)))
            {
                if (Project.Id == 0)
                    TempData["Result"] = await _service.CreateProjectAsync(Project);
                else
                    TempData["Result"] = await _service.UpdateProjectAsync(Project);
                return RedirectToPage("./Projects");
            }
            await GetElements();
            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync ()
        {
            if(Input.Image != null)
            {
                using(var fileStream = new FileStream(_environment.WebRootPath + "/images/portfolio/" + Input.Image.FileName, FileMode.Create))
                {
                    await Input.Image.CopyToAsync(fileStream);
                }
                await GetElements();
                Project.Image = "/images/portfolio/" + Input.Image.FileName;
                TempData["Result"] = "Успешная загрузка файла, на сервер";
                return Page();
            }
            await GetElements();
            return Page();
        }

        public async Task GetElements ()
        {
            Skills = await _service.GetAllSkillsAsync();
            Categories = await _service.GetAllCategoriesAsync();
            Images = _service.GetAllImages();
        }

        //TODO: Проверка перед отправкой формы, по средствам GET запроса
        public JsonResult OnGetTest(string ProjectName)
        {
            return new JsonResult(true);
        }
    }
}