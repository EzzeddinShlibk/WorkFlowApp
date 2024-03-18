using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using WorkFlowApp.Models.Entities;
using static System.Collections.Specialized.BitVector32;
using WorkFlowApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.ViewModels;

namespace WorkFlowApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork<SiteState> _SiteState;
        private readonly IUnitOfWork<Team> _team;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IUnitOfWork<Project> _project;

        public AdminController(
                      IWebHostEnvironment hostEnvironment,
                      IUnitOfWork<SiteState> SiteState,
                      IUnitOfWork<Team> team,
                      IUnitOfWork<TeamUser> teamuser,
                      IUnitOfWork<Project> project,
                      IToastNotification toastNotification
                     )
        {

            _SiteState = SiteState;
            _webHostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _team = team;
            _teamuser = teamuser;
            _project = project;
        }



        public async Task<IActionResult> TeamsAsync()
        {
            var teams = await _team.Entity.GetAll().Include(a => a.TeamUsers).ThenInclude(k=>k.user)
                .Where(p => p.TeamUsers.Any(pu => pu.isDeleted == false)).ToListAsync();
            var model = new List<TeamsListViewModel> { };
            foreach (var item in teams)
            {

                var admin =_teamuser.Entity.GetAll().Include(a=>a.user).Where(a=>a.teamId==item.Id && a.isAdmin==1).FirstOrDefault();
                var projects = _project.Entity.GetAll().Include(k => k.ProjectsUsers).Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == admin.user.Id) && p.IsDeleted == false && p.IsArchived == false).Count();
                var projectlist = new TeamsListViewModel
                {

                    code = item.Code,
                    Date = item.CreatedDate,
                    Admin = admin.user.UserName,
                    Users=item.TeamUsers.Count(),
                    Projects= projects,

              
                };
                model.Add(projectlist);
            }
            return View(model);
        }



        [HttpGet]
        [Authorize]
        public IActionResult SiteState(string Message)
        {
            ViewBag.Message = Message;

            var model = _SiteState.Entity.GetAll().FirstOrDefault();
            if (model == null)
            {
                return View("SiteState", new SiteState());
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteState(string save, SiteState model)
        {
            try
            {

                var siteState = _SiteState.Entity.GetAll().FirstOrDefault();
                if (save == "تفعيل الموقع")
                {
                    siteState.State = true;
                }
                else if (save == "إلغاء تفعيل الموقع")
                {
                    siteState.State = false;
                    _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });


                }
                else if (save == "تحديث رسالة الإغلاق")
                {
                    siteState.ClosingMessage = model.ClosingMessage;
                    _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                }
                siteState.ModifiedDate = DateTime.Now;
                _SiteState.Entity.Update(siteState);
                await _SiteState.SaveAsync();
                _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("SiteState");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = "The basic data not found in the database ";
                ViewBag.ErrorMessage = "Missing data row- " + ex;
                return View("Error");
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteStateUpdate(SiteState model)
        {
            try
            {
                var siteState = _SiteState.Entity.GetAll().FirstOrDefault();
                if (siteState != null)
                {
                    siteState.ClosingMessage = model.ClosingMessage;
                    siteState.CreatedDate = model.CreatedDate;
                    siteState.ModifiedDate = DateTime.Now;
                    _SiteState.Entity.Update(siteState);
                    await _SiteState.SaveAsync();
                }
                else
                {
                    var newSiteState = new SiteState
                    {
                        ClosingMessage = model.ClosingMessage,
                        State = true,
                        CreatedDate = DateTime.Now,
                    };
                    _SiteState.Entity.Insert(newSiteState);
                    await _SiteState.SaveAsync();
                }
                _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("SiteState");
            }
            catch
            {
                throw;
            }
        }

        [ViewLayout("_BaseLayout")]
        [AllowAnonymous]
        [Route("Closing")]
        [HttpGet]
        public IActionResult Closing()
        {
            //if (await CheckSiteState())
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            var siteState = _SiteState.Entity.GetAll().FirstOrDefault();
            if (siteState == null)
            {
                return View("Closing", new SiteState());
            }

            if (siteState.State == false)
            {

                return View("Closing", siteState);
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
