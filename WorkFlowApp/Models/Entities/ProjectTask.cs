using System.ComponentModel.DataAnnotations.Schema;
using WorkFlowApp.ViewModels;

namespace WorkFlowApp.Models.Entities
{
    public class ProjectTask:BaseEntity
    {
        public Guid projectId { get; set; }
        public Project project { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid statuesId { get; set; }
        public Statues statues { get; set; }

        public Guid priorityId { get; set; }
        public Priority priority { get; set; }

        public ApplicationUser User { get; set; }
        public string userId { get; set; }

        public string FilePath { get; set; } = string.Empty;

   

   
    }
}
