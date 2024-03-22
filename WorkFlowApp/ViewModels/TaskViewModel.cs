using WorkFlowApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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


        [Required(ErrorMessage = "الرجاء اختيار  الاهمية")]

        public Guid ProirityId { get; set; }
        [Required(ErrorMessage = "الرجاء ادخال اسم امهمة")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرجاء ادخال وصف المهمة")]
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string userId { get; set; }

        public string FilePath { get; set; } = string.Empty;
        public bool isRead { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }

        public List<Statues> statues;

        public List<Priority> Priorities;
        public List<commentList> Comments;


    }
}




