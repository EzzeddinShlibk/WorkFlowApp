
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowApp.Models.Entities;

namespace WorkFlowApp.Models
{
    public class Contact : BaseEntity
    {

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Facebook { get; set; }


    }
}
