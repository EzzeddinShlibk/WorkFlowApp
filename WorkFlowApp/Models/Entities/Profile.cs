using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowApp.Models.Entities
{
    public class Profile:BaseEntity
    {
        public string DisplayName { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;



        [Display(Name = "wrong phone number")]
        public string PhoneNum { get; set; }=string.Empty;

        public string Pic { get; set; }=string.Empty;



        public bool Gender { get; set; }

        public ApplicationUser user { get; set; }
        public string UserId { get; set; } = string.Empty;



    }
}
