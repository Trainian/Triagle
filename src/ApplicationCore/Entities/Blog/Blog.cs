using ApplicationCore.Entities.Base;
using ApplicationCore.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Blog
{
    public class Blog : BaseObject
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreateDateTime { get; set; }

        [Required]
        public string CreateWebUser { get; set; }

        [Required]
        public Category Category { get; set; }

        public List<BlogMessage> BlogMessages { get; set; }
        //TODO: Настроить Лайки
        public List<WebUser> Likes { get; set; }
        //TODO: Настроить Твиты
        public int Twits { get; set; }
    }
}
