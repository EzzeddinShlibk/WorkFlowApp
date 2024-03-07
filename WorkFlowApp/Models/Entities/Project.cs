using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowApp.Models.Entities
{
    public class Project : BaseEntity
    {
        [Required(ErrorMessage = "الرجاء ادخال اسم المشروع")]
        [StringLength(50, MinimumLength = 5)]

        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "الرجاء ادخال وصف المشروع")]
        [StringLength(50, MinimumLength = 5)]
        public string Description { get; set; } = string.Empty;

 
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "الرجاء ادخال تاريخ الانتهاء")]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsArchived { get; set; }


        public List<ProjectsUser> ProjectsUsers { get; set; } = new List<ProjectsUser>();


        [NotMapped]
        public List<string> SelectedUserIds { get; set; } = new List<string>();

    }
}
