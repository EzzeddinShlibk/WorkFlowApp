using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using WorkFlowApp.Classes;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using WorkFlowApp.ViewModels;
using WorkFlowApp.ViewModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static WorkFlowApp.Classes.Helper;

namespace WorkFlowApp.Controllers
{
    [Authorize(Roles = "Prog")] // يسمح لإحدى الصلاحيات المكتوبة
    [ViewLayout("_Layout")]
    public class UsersRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<Profile> _profile;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersRolesController(RoleManager<IdentityRole> roleManager,
                               UserManager<ApplicationUser> userManager,
                                   IWebHostEnvironment hostEnvironment,
                       IToastNotification toastNotification,
                               IUnitOfWork<Profile> profile)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _profile = profile;
            _webHostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
        }




        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Users(string submit, string textSearch)
        {
            List<ApplicationUser> users = null;

                users = await _userManager.Users.OrderByDescending(u => u.ModifiedDate)
                                                .AsNoTracking().ToListAsync();
            return View(users);

        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "غير موجود";
                return View("NotFound");
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                LockoutEnd = user.LockoutEnd,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate,
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "غير موجود";
                return View("NotFound");
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.LockoutEnd = model.LockoutEnd;
            user.CreatedDate = model.CreatedDate;
            user.ModifiedDate = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Users));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Prog")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "غير موجود";
                return View("NotFound");
            }

            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (CurrentUser.Id == id)
            {
                return RedirectToAction("AccessDenied", "Account", new { Message = "لايمكن الحذف"});
            }

            var UserId = user.Id;

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var profile = _profile.Entity.GetAll().Where(p => p.UserId == UserId).FirstOrDefault();
                if (profile != null)
                {
                    _profile.Entity.Delete(profile.Id );
                    await _profile.SaveAsync();
                }

                return RedirectToAction(nameof(Users));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(nameof(Users));
        }
        [HttpPost]
        public async Task<IActionResult> LockoutUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage ="غير موجود";

                return View("NotFound");
            }
            IdentityResult result;
            if (user.LockoutEnd == null)
            {
                result = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(10));
            }
            else
            {
                result = await _userManager.SetLockoutEndDateAsync(user, null);
            }

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(EditUser), new { id = user.Id });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction(nameof(EditUser), new { id = user.Id });
        }

    }
}
