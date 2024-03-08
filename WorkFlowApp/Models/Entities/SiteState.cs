using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.Models.Entities
{
    public class SiteState : BaseEntity
    {
        public bool State { get; set; }
        public string ClosingMessage { get; set; }
    }
}
