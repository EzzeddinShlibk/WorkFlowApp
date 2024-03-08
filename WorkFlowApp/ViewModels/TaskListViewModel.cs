using WorkFlowApp.Models.Entities;

namespace WorkFlowApp.ViewModels
{
    public class TaskListViewModel
    {
        public Guid TaskId { get; set; }
        public string UserName { get; set; }
        public string UserPic { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime EndDate { get; set; }

        public string Statues { get; set; }
        public string priority { get; set; }
    }
}
