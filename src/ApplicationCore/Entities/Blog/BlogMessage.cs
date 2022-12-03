using ApplicationCore.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Blog
{
    public class BlogMessage : BaseObject
    {
        [Required]
        public string Text { get; set; }
        public bool Visible { get; set; }
        [Required]
        public Blog Blog { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreateDateTime { get; set; }
        [Required]
        public string CreateWebUser { get; set; }
        public BlogMessage? ResponseToBlogMessage { get; set; }
    }
}
