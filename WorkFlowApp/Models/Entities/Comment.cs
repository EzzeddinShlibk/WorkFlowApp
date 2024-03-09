namespace WorkFlowApp.Models.Entities
{
    public class Comment:BaseEntity 
    {
        public Guid userId { get; set; }
        public ApplicationUser user { get; set; }

        public Guid projectTaskId { get; set; }
        public ProjectTask projectTask { get; set; }
        public string comment { get; set; }=string.Empty;

        public DateTime DateTime { get; set; }
    }
}
