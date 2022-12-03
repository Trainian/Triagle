using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Administration
{
    public class WebUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Пользователь")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public string Avatar { get; set; }
        public string? Role { get; set; } = "";
    }
}
