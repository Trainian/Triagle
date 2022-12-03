using ApplicationCore.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BlogViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название, обязательно для заполнения")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Содержание, обязательно для заполнения")]
        [Display(Name = "Содержание")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Изображение, обязательно для заполнения")]
        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Лайки")]
        public List<WebUser> Likes { get; set; }

        [Display(Name = "Лайки кол-во")]
        public int LikesCount => Likes.Count();

        [Display(Name = "Переслано")]
        public int Twits { get; set; } = 0;

        [Display(Name = "Кол-во комментариев")]
        public int Comments => BlogMessages?.Count ?? 0;

        [Display(Name = "Скрытых комментариев")]
        public int CommentsNotVisible => BlogMessages.Where(m => m.Visible == false).Count();

        [Required]
        [Display(Name = "Дата создания")]
        public DateTime CreateDateTime { get; set; }

        [Required(ErrorMessage = "Категория, обязательна для выбора")]
        [Display(Name = "Категория")]
        public CategoryViewModel Category { get; set; }

        [Display(Name = "Комментарии")]
        public List<BlogMessageViewModel> BlogMessages { get; set; } = new List<BlogMessageViewModel>();

        [Required]
        [Display(Name = "Создал")]
        public string CreateWebUser { get; set; }
    }
}
