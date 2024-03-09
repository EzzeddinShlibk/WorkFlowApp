namespace WorkFlowApp.Models.Entities
{
    public class CalendarEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
