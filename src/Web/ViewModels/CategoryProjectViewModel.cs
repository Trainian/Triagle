using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CategoryProjectViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Information { get; set; }
        public List<ProjectViewModel>? Projects { get; set; }
        public int? ProjectsCount => Projects?.Count();
    }
}
