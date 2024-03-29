﻿using System.ComponentModel.DataAnnotations;

namespace WorkFlowApp.Models.Entities
{
    public class Team:BaseEntity
    {
        [Required(ErrorMessage = "الرجاء إدخال الكود")]
        public string Code { get; set; } = string.Empty;
        public List<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();

    }
}
