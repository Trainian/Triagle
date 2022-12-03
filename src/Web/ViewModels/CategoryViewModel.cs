using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Information { get; set; }
        public List<BlogViewModel> Blogs { get; set; } = new List<BlogViewModel>();
        public int BlogsCount => Blogs.Count;
    }
}
