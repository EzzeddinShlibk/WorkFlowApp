namespace WorkFlowApp.Models.Entities
{
    public class TeamUser:BaseEntity 
    { 
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
        public Guid teamId { get; set; }
        public Team team { get; set; }

        public bool isAdmin { get; set; }
        public bool isApproved { get; set; }
        public bool isDeleted { get; set; }

    }
}
