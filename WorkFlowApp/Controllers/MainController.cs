using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Entities;

namespace WorkFlowApp.Controllers
{
    public class MainController : Controller
    {
        [ViewLayout("_MainLayout")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
