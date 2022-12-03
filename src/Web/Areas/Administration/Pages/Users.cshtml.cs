using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces.Administration;
using Web.ViewModels.Administration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Administration.Pages
{
    public class UsersModel : PageModel
    {
        public UsersModel(IWebUserService service, ILogger<UsersModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private IWebUserService _service;
        private ILogger<UsersModel> _logger;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Id { get; set; }
            [Required]
            public string ModifiedData { get; set; }
            [Required]
            public string NewData { get; set; }
        }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<WebUserViewModel> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await GetData();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string result = "";
            if (!ModelState.IsValid)
            {
                await GetData();
                return Page();
            }
            if(Input.ModifiedData == "user.EmailConfirmed")
            {
                result = await _service.EmailConfirmedChangeAsync(Input.Id);
            }
            else if(Input.ModifiedData == "user.LockoutEnd")
            {
                var newDate = DateTime.Parse(Input.NewData);
                result = await _service.SetLockoutUserAsync(Input.Id, new DateTimeOffset(newDate));
            }
            else if(Input.ModifiedData == "user.Role")
            {
                result = await _service.SetRoleAsync(Input.Id, Input.NewData);
            }
            ViewData["Result"] = result;
            await GetData();
            return Page();
        }

        private async Task<bool> GetData ()
        {
            try
            {
                Users = await _service.UserListAsync();
                Roles = await _service.RolesListAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning("При получении данных пользователей и ролях, произошла ошибка: {e}", e);
                return false;
            }
        }
    }
}