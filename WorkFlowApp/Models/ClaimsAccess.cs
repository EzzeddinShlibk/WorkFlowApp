using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WorkFlowApp.Models.Entities
{
    public static class ClaimsAccess
    {
        public static List<Claim> ClaimsList = new List<Claim>()
        {
            new Claim("Create","true"),
            new Claim("Edit","true"),
            new Claim("Delete","true"),
            new Claim("Profile","true"),
            new Claim("EditUser","true")
        };
    }
}
