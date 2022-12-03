using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Information { get; set; }
    }
}
