using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models.Entities
{
    public class Priority:BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
