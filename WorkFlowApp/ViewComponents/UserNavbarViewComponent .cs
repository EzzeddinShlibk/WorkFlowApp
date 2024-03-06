using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.ViewComponents
{
    public class UserNavbarViewComponent  : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<Profile> _Profile;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserNavbarViewComponent (UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,
          IUnitOfWork<Profile> Profile)
        {
            _userManager = userManager;
            _Profile = Profile;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var profile = _Profile.Entity.GetAll().Where(a => a.UserId == user.Id).FirstOrDefault();
                if (profile == null)
                {
                    ViewBag.UserName = user.Email;
                    ViewBag.UserImg = null;
                }
                else
                {
                    ViewBag.UserName = profile.DisplayName;
                    ViewBag.UserImg = profile.Pic;

                }


            }


            return View();
        }
    }
}
