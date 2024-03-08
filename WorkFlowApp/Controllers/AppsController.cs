using Microsoft.AspNetCore.Mvc;

namespace WorkFlowApp.Controllers
{
    public class AppsController : Controller
    {

        public IActionResult Inbox() => View();
        public IActionResult Index() => View();

        public IActionResult ReadEmail() => View();

        public IActionResult Chart() => View();
        public IActionResult Contacts() => View();

        public IActionResult TeamPerformance() => View();

        public IActionResult DeletedProjects() => View();

        public IActionResult Invoice() => View();

        public IActionResult Tasks() => View();
        
        public IActionResult Projects() => View();

        public IActionResult Project() => View();

        [HttpGet("SiteStatus")]
        public IActionResult SiteStatus() => View();
        
        [HttpGet("UserMangment")]
        public IActionResult UserMangment() => View();

        [HttpGet("Project/Teams")]
        public IActionResult ProjectTeams() => View();

        [HttpGet("Project/Files")]
        public IActionResult ProjectFiles() => View();

        [HttpGet("MyTasks")]
        public IActionResult MyTasks() => View();

        [HttpGet("EditTask")]
        public IActionResult EditTask() => View();

        [HttpGet("Ecommerce/ProcuctDetail")]
        public IActionResult EcommerceProcuctDetail() => View();
        [HttpGet("statistics")]
        public IActionResult statistics() => View();

        [HttpGet("Ecommerce/Cart")]
        public IActionResult EcommerceCart() => View();

        [HttpGet("Ecommerce/Checkout")]
        public IActionResult EcommerceCheckout() => View();
    }
}
