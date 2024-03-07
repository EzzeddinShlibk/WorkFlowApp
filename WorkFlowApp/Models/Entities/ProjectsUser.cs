namespace WorkFlowApp.Models.Entities
{
    public class ProjectsUser:BaseEntity 
    {
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
        public Guid projectId { get; set; }
        public Project project { get; set; }




    }
}
