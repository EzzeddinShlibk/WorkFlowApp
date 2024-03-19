using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.Models.Entities
{
    public class Features
    {
        public Guid Id { get; set; }
        public string Pic1 { get; set; }
        public string Title1 { get; set; }
        public string Content1 { get; set; }
        [NotMapped]
        public IFormFile Image1 { get; set; }

        public string Pic2 { get; set; }
        public string Title2 { get; set; }
        public string Content2 { get; set; }
        [NotMapped]
        public IFormFile Image2 { get; set; }


        public string Pic3 { get; set; }
        public string Title3 { get; set; }
        public string Content3 { get; set; }
        [NotMapped]
        public IFormFile Image3 { get; set; }
    }
}
