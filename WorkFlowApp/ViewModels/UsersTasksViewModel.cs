using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
namespace WorkFlowApp.ViewModels
{

    public class UsersTasksViewModel
    {
        public string Name { get; set; }
        public string Pic { get; set; }
        public int CompletedTask { get; set; }
        public int UnCompletedTask { get; set; }



    }
}
