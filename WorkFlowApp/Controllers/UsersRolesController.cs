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
    [Authorize(Roles = "Prog,Admin")] // يسمح لإحدى الصلاحيات المكتوبة



    [ViewLayout("_LayoutAdmin")]
    public class UsersRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<Profile> _profile;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHtmlLocalizer<UsersRolesController> _localizer;

        public UsersRolesController(RoleManager<IdentityRole> roleManager,
                               UserManager<ApplicationUser> userManager,
                                   IWebHostEnvironment hostEnvironment,
                         IHtmlLocalizer<UsersRolesController> localizer,
                       IToastNotification toastNotification,
                               IUnitOfWork<Profile> profile)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _profile = profile;
            _localizer = localizer;
            _webHostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
        }


        private async Task<IQueryable<IdentityRole>> GetRoles()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (await _userManager.IsInRoleAsync(user, "Prog"))
            {
                return (_roleManager.Roles);
            }
            else
            {
                return (_roleManager.Roles.Where(r => r.Name != "Prog"));
            }

        }
        [HttpGet]
        public async Task<IActionResult> Roles(string role)
        {
            ViewBag.NameInUse = role;
            return View(await GetRoles());

        }

        [NoDirectAccess]
        [Authorize(Roles = "Prog")]
        public async Task<IActionResult> CreateOrEditRole(string id)
        {
            if (id == null)
            {
                var model = new EditRoleViewModel();
                return View(model);
            }

            else
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return NotFound();
                }
                var model = new EditRoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEditRole(string id, EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (id == null)
                {
                    var ExistName = await _roleManager.Roles.Where(a => a.Name.ToUpper() == model.Name.ToUpper()).FirstOrDefaultAsync();

                    if (ExistName == null)
                    {
                        IdentityRole identityRole = new IdentityRole
                        {
                            Name = model.Name
                        };
                        IdentityResult result = await _roleManager.CreateAsync(identityRole);

                        _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });


                    }
                    else
                    {
                        _toastNotification.AddErrorToastMessage(_localizer["Alreadyexists"].Value, new ToastrOptions() { Title = "" });


                    }
                }
                else
                {

                    var role = await _roleManager.FindByIdAsync(model.Id.ToString());
                    if (role == null)
                    {
                        ViewBag.ErrorMessage = $"Cannot be found Role with Id={model.Id}";
                        return View("NotFound");
                    }
                    var ExistName = await _roleManager.Roles.Where(a => a.Name.ToUpper() == model.Name.ToUpper() && a.Id != model.Id).FirstOrDefaultAsync();
                    if (ExistName == null)
                    {
                        role.Name = model.Name;
                        var result = await _roleManager.UpdateAsync(role);
                        _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });
                        id = string.Empty;
                    }
                    else
                    {
                        _toastNotification.AddErrorToastMessage(_localizer["Alreadyexists"].Value, new ToastrOptions() { Title = "" });


                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllRoles", await GetRoles()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditRole", model) });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Prog")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Cannot be found role with Id={id}";
                    return View("NotFound");
                }

                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                if (usersInRole.Count != 0)
                {
                    _toastNotification.AddErrorToastMessage(_localizer["CantDelete"].Value, new ToastrOptions() { Title = "" });
                    return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllRoles", await GetRoles()) });

                }

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllRoles", await GetRoles() ) });

                }
                else
                {
                    return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllRoles", await GetRoles()) });

                }
            }
            catch
            {
                throw;
            }
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
        [Authorize(Policy = "EditUserPolicy")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = _localizer["CantFind"];
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
                ViewBag.ErrorMessage = _localizer["CantFind"].Value;
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
                ViewBag.ErrorMessage = _localizer["CantFind"].Value;
                return View("NotFound");
            }

            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (CurrentUser.Id == id)
            {
                return RedirectToAction("AccessDenied", "Account", new { Message = _localizer["cant'delte"].Value });
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
                ViewBag.ErrorMessage = _localizer["CantFind"].Value;

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

        [HttpPost]
        [Authorize(Policy = "EditUserPolicy")]
        public async Task<IActionResult> EditUserRoles(List<UserRolesViewModel> model, string userId) // userIdيأخذها من QieryString
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = _localizer["CantFind"].Value;
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            if ((await _userManager.RemoveFromRolesAsync(user, roles)).Succeeded)
            {
                if ((await _userManager.AddToRolesAsync(user, model.Where(s => s.IsSelected == true).Select(r => r.RoleName))).Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });
                    return RedirectToAction("EditUser", new { Id = userId});
                }
            }
            ModelState.AddModelError(string.Empty, "Failed to modify user roles");
            return RedirectToAction("EditUser", new { Id = userId });
        }
        [HttpPost]
        [Authorize(Policy = "EditPolicy")]
        public async Task<IActionResult> EditUserClaims(UserClaimsViewModel model, string userId)
        {


            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = _localizer["CantFind"].Value;

                return View("NotFound");
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims); //var result = await _userManager.RemoveFromClaimAsync(user, "User"); // لسحب صلاحية واحدة فقط
            if (result.Succeeded)
            {
                result = await _userManager.AddClaimsAsync(user, model.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });

                    return RedirectToAction("EditUser", new { Id = userId });
                }
            }
            ModelState.AddModelError(string.Empty, "Failed to modify user Claims");
            return RedirectToAction("EditUser", new { Id = userId });
        }
    }
}
