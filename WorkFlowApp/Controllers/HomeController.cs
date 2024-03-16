using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkFlowApp.Models;
using WorkFlowApp.Models.Entities;

namespace WorkFlowApp.Controllers
{
    public class HomeController : Controller
    {
 

        [ViewLayout("_MainLayout")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

   

  
    }
}

