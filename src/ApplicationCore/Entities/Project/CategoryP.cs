using ApplicationCore.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Project
{
    public class CategoryP : BaseObject
    {
        [Required]
        public string Name { get; set; }
        public string? Information { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
