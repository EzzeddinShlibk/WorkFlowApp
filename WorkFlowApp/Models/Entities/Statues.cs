﻿namespace WorkFlowApp.Models.Entities
{
    public class Statues:BaseEntity
    {
        public int  Num { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } 
        public string Icon { get; set; }
    }
}
