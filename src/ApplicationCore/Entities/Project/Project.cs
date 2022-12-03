using ApplicationCore.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Project
{
    public class Project : BaseObject
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreateDateTime { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string CreateWebUser { get; set; }
        [Required]
        public string PreviewLink { get; set; }
        [Required]
        public ICollection<Skill> Skills { get; set; }
        [Required]
        public int CategoryId { get; set; }


        public CategoryP Category { get; set; }
    }
}
