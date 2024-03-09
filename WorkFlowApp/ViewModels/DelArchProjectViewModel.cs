using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace WorkFlowApp.ViewModels
{
   public class DelArchProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public double Percent { get; set; }


    }

}
