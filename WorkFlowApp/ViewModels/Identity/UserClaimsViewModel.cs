using System.Collections.Generic;

namespace WorkFlowApp.ViewModels.Identity
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<ClaimClass>();
        }

        public string UserId { get; set; }
        public List<ClaimClass> Claims { get; set; }
    }
}

