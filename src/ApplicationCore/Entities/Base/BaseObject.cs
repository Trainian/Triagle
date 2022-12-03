using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Base
{
    public class BaseObject
    {
        [Key]
        public int Id { get; set; }
    }
}