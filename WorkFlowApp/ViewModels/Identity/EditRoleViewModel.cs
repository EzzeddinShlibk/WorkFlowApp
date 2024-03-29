﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.ViewModels
{

    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please Enter Role Name")]
        public string Name { get; set; }

    }
}
