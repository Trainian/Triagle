using ApplicationCore.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Project
{
    public class Skill : BaseObject
    {
        [Required]
        public string Name { get; set; }
        public string? Information { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
