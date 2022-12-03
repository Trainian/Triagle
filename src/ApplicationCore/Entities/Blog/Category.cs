using ApplicationCore.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Blog
{
    public class Category : BaseObject
    {
        [Required]
        public string Name { get; set; }
        public string? Information { get; set; }
        public List<Blog>? Blogs { get; set; }
    }
}
