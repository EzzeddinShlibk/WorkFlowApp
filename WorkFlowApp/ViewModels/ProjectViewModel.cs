using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace WorkFlowApp.ViewModels
{
   public class ProjectLine
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }

        public int TaskCount { get; set; }
        public double  Percent { get; set; }

        public int DaysLeft { get; set; }


    }
    public class ProjectViewModel
    {
        public IEnumerable<Project> projects;
        public Project Project { get; set; }
        public ProjectsUser ProjectsUser { get; set; }
        public IEnumerable<ProjectLine> projectList { get; set; } 

    
    }
}
