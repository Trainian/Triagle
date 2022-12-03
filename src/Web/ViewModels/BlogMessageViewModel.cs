using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BlogMessageViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Сообщение")]
        public string Text { get; set; }
        [Display(Name = "Показывать?")]
        public bool Visible { get; set; } = false;
        [Required]
        [Display(Name = "Дата создания")]
        public DateTime CreateDateTime { get; set; }
        [Required]
        [Display(Name = "Блог")]
        public BlogViewModel Blog { get; set; }

        [Display(Name = "Создал")]
        public string? CreateWebUser { get; set; }

        [Display(Name = "Аватар пользователя")]
        public string? CreateWebUserAvatar { get; set; }
        public BlogMessageViewModel? ResponseToBlogMessage { get; set; }
        public List<BlogMessageViewModel>? AnswerMessage { get; set; } = new List<BlogMessageViewModel>();
    }
}
