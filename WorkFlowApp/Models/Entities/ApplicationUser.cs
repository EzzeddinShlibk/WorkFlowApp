using Microsoft.AspNetCore.Identity;

namespace WorkFlowApp.Models.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }


    }
}
