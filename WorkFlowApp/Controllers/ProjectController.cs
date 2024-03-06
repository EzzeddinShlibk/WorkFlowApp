using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NToastNotify;
using static System.Collections.Specialized.BitVector32;
using static WorkFlowApp.Classes.Helper;
using WorkFlowApp.Classes;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkFlowApp.Controllers
{
    [ViewLayout("_Layout")]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectTask> _projectTask;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(
                     IUnitOfWork<Project> project,
                     IUnitOfWork<ProjectTask> projectTask,
                     IUnitOfWork<ProjectsUser> projectsUser,
                     IUnitOfWork<TeamUser> teamUser,
                        UserManager<ApplicationUser> userManager,
                      IWebHostEnvironment hostEnvironment,
                       IToastNotification toastNotification
                     )
        {
            _project = project;
            _teamuser = teamUser;
            _projectsUser = projectsUser;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _projectTask = projectTask;
        }

        public Guid getTeamID(string UserID)
        {
            var team = _teamuser.Entity.GetAll().Where(a => a.userId == UserID).Include(k => k.team).FirstOrDefault();

            Guid teamID = Guid.Empty;
            if (team != null)
            {
                teamID = team.team.Id;
            }

            return teamID;
        }
        private async Task PopulateUsersDropDownList(string UserId, object selected = null)
        {
            Guid teamID = getTeamID(UserId);

            var teamUsers = await _teamuser.Entity.GetAll()
                .Where(a => a.teamId == teamID && a.isDeleted == false && a.isApproved == true)
                .Include(a => a.user)
                .ToListAsync();
            var usersList = teamUsers.Select(a => new SelectListItem
            {
                Value = a.userId.ToString(),
                Text = a.user.UserName
            });

            ViewBag.UsersList = new SelectList(usersList, "Value", "Text", selected);

        }



        public async Task getDataAsync( string UserId)
        {
            await PopulateUsersDropDownList(UserId);
            var projects = await _project.Entity.GetAll()
           .Include(a => a.ProjectsUsers)
           .ThenInclude(i => i.user)
           .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId))
           .ToListAsync();
            var projectlinelist = new List<ProjectLine> { };
            foreach (var item in projects)
            {
                TimeSpan difference = item.EndDate - item.StartDate;
                int daysDifference = Math.Abs(difference.Days);

                int taskscount = _projectTask.Entity.GetAll().Where(a => a.projectId == item.Id).Count();

                var projectLine = new ProjectLine
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    EndDate = item.EndDate,
                    TaskCount = taskscount,
                    DaysLeft = daysDifference,
                    Percent = 0
                };
                projectlinelist.Add(projectLine);
            }
        }
        public async Task<IActionResult> Projects(string UserId, string message)
        {
            ViewBag.Message = message;


            await PopulateUsersDropDownList(UserId);


            var projects = await _project.Entity.GetAll()
           .Include(a => a.ProjectsUsers)
           .ThenInclude(i => i.user)
           .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId))
           .ToListAsync();


            var projectlinelist = new List<ProjectLine> { };

            foreach (var item in projects)
            {
                TimeSpan difference = item.EndDate - item.StartDate;
                int daysDifference = Math.Abs(difference.Days);

                int taskscount = _projectTask.Entity.GetAll().Where(a => a.projectId == item.Id).Count();

                var projectLine = new ProjectLine
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    EndDate = item.EndDate,
                    TaskCount = taskscount,
                    DaysLeft = daysDifference,
                    Percent = 0
                };
                projectlinelist.Add(projectLine);
            }




            return View(projectlinelist);
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateOrEditProject(Guid id)
        {

            if (id == Guid.Empty)
                return View(new Project());
            else
            {
                var model = await _project.Entity.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateOrEditProject(Guid id, Section model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string ImageName, CoverName;
        //        if (id == Guid.Empty)
        //        {
        //            var ExistName = await _Section.Entity.GetAll().Where(a => a.NameAr.ToUpper() == model.NameAr.ToUpper() || a.NameEn.ToUpper() == model.NameEn.ToUpper()).FirstOrDefaultAsync();

        //            if (ExistName == null)
        //            {
        //                ImageName = UploadedFile(model.Image);
        //                CoverName = UploadedFile(model.CoverImage);
        //                Section newsection = new Section();
        //                newsection.NameAr = model.NameAr;
        //                newsection.NameEn = model.NameEn;
        //                newsection.Pic = ImageName;
        //                newsection.CoverPic = CoverName;
        //                newsection.CreatedDate = DateTime.Now;
        //                _Section.Entity.Insert(newsection);
        //                await _Section.SaveAsync();
        //                _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });

        //            }
        //            else
        //            {
        //                _toastNotification.AddErrorToastMessage(_localizer["Alreadyexists"].Value, new ToastrOptions() { Title = "" });

        //            }
        //        }
        //        else
        //        {
        //            var ExistSecName = await _Section.Entity.GetAll().Where(a => a.NameAr.ToUpper() == model.NameAr.ToUpper() && a.Id != model.Id || a.NameEn.ToUpper() == model.NameEn.ToUpper() && a.Id != model.Id).FirstOrDefaultAsync();
        //            if (ExistSecName == null)
        //            {
        //                if (model.Image != null)
        //                {
        //                    DeleteImg(model.Pic);
        //                    ImageName = UploadedFile(model.Image);
        //                }
        //                else
        //                {
        //                    ImageName = model.Pic;
        //                }
        //                if (model.CoverImage != null)
        //                {
        //                    DeleteImg(model.CoverPic);
        //                    CoverName = UploadedFile(model.CoverImage);
        //                }
        //                else
        //                {
        //                    CoverName = model.CoverPic;
        //                }
        //                var sections = await _Section.Entity.GetByIdAsync(model.Id);
        //                sections.NameAr = model.NameAr;
        //                sections.NameEn = model.NameEn;
        //                sections.Pic = ImageName;
        //                sections.CoverPic = CoverName;

        //                sections.ModifiedDate = DateTime.Now;
        //                _Section.Entity.Update(sections);
        //                await _Section.SaveAsync();
        //                _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });

        //                id = Guid.Empty;
        //            }
        //            else
        //            {
        //                _toastNotification.AddErrorToastMessage(_localizer["Alreadyexists"].Value, new ToastrOptions() { Title = "" });


        //            }
        //        }
        //        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllSections", await _Section.Entity.GetAll().AsNoTracking().ToListAsync()) });
        //    }
        //    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditSection", model) });
        //}
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                var model = await _project.Entity.GetByIdAsync(id);
                model.IsDeleted = true;
                model.ModifiedDate = DateTime.Now;

                _project.Entity.Update(model);
                await _project.SaveAsync();

                _toastNotification.AddSuccessToastMessage("تم الحذف بنجاح", new ToastrOptions() { Title = "" });


                return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllSections", await _Section.Entity.GetAll().AsNoTracking().ToListAsync()) });
            }
            catch
            {
                throw;
            }
        }

    }
}
