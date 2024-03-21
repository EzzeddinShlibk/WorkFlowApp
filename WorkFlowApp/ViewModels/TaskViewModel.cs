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
    public class commentList
    {
        public string Name { get; set; } = string.Empty;
        public string Pic { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;


    }

    public class TaskViewModel
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Guid StatuesId { get; set; }
        public Guid ProirityId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string userId { get; set; }

        public string FilePath { get; set; } = string.Empty;
        public bool isRead { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public List<Statues> statues;

        public List<Priority> Priorities;
        public List<commentList> Comments;


    }
}
