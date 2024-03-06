namespace WorkFlowApp.Models.Entities
{
    public class Statues:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Num { get; set; } 
        public int Percent { get; set; }
    }
}
