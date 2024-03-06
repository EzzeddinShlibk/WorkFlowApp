using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowApp.Models.Entities
{
    public class Project : BaseEntity
    {
        [Required(ErrorMessage = "please choose Name")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsArchived { get; set; }


        public List<ProjectsUser> ProjectsUsers { get; set; } = new List<ProjectsUser>();


        [NotMapped]
        public List<string> SelectedUserIds { get; set; } = new List<string>();

    }
}
