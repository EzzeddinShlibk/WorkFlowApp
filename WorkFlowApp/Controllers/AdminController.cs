using Microsoft.AspNetCore.Mvc;

namespace WorkFlowApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
