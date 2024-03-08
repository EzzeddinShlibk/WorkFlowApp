using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.CodeAnalysis;
namespace WorkFlowApp.ViewModels
{

    public class TaskViewModel
    {
        public Guid ProjectId { get; set; }

        public List<Statues> statues;

        public List<Priority> Priorities;
        public ProjectTask projectTask { get; set; }


    }
}
