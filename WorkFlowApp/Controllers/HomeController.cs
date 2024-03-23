using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Diagnostics;
using System.Xml.Linq;
using WorkFlowApp.Models;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using WorkFlowApp.ViewModels;

namespace WorkFlowApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Contact> _contact;
        private readonly IUnitOfWork<Features> _features;
        public HomeController(
             IUnitOfWork<Contact> contact,
             IUnitOfWork<Features> features

             )
        {
            _features = features;
            _contact = contact;

        }


        [ViewLayout("_MainLayout")]
        [HttpGet]
        public async Task<IActionResult> IndexAsync()

        {

            var model = new LandingViewModel
            {
                contact =await _contact.Entity.GetAll().FirstOrDefaultAsync(),
                Features =await _features.Entity.GetAll().FirstOrDefaultAsync(),


            };

            return View(model);
        }




    }
}

