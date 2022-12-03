using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        [Required]
        [PageRemote(HttpMethod = "GET", PageHandler = "Test", PageName = "ProjectAddOrEdit", ErrorMessage = "Test Error")]
        [Display(Name = "Название")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }
        [Required]
        [Display(Name = "Картинка")]
        public string Image { get; set; }
        [Required]
        public string CreateWebUser { get; set; }
        [Required]
        [Display(Name = "Ссылка на представление")]
        public string PreviewLink { get; set; }
        [Required]
        [Display(Name = "Скиллы")]
        public List<SkillViewModel> Skills { get; set; }
        [Required]
        [Display(Name = "Категория")]
        public CategoryProjectViewModel? Category { get; set; }
    }
}
