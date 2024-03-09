using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;

namespace WorkFlowApp.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IUnitOfWork<CalendarEvent> _CalendarEvent;
        public CalendarController(
            IUnitOfWork<CalendarEvent> CalendarEvent


            )

        {

 
            _CalendarEvent = CalendarEvent;
     
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetEvents")]
        public IActionResult GetEvents()
        {
            // Retrieve events from the database or any other data source
            var events =_CalendarEvent.Entity.GetAll().Select(e => new
            {
                title = e.Title,
                start = e.Start,
                end = e.End
                // Add other properties as needed
            }).ToList();

            return Ok(events);
        }

        [HttpPost]
        [Route("SaveEvent")]
        public IActionResult SaveEvent([FromBody] CalendarEvent eventData)
        {
            // Save the event data to the database or perform any necessary processing
            // You can use _context to save the event to the database

            // Example: Save to database
            // _context.CalendarEvents.Add(new CalendarEvent { Title = eventData.Title, Start = eventData.Start, End = eventData.End });
            // _context.SaveChanges();

            return Ok("Event saved successfully");
        }
    }
}
