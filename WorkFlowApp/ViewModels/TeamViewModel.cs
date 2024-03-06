using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.ViewModels
{
    public class TeamViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pic { get; set; }
        public string phone { get; set; }
        public bool isAdmin { get; set; }
        public bool isApproved { get; set; }



    }
}
