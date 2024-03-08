

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;
using WorkFlowApp.Classes;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using WorkFlowApp.ViewModels;
using static WorkFlowApp.Classes.Helper;

namespace WorkFlowApp.Controllers
{
    [ViewLayout("_Layout")]
    public class ProjectTasksController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<Profile> _profile;
        private readonly IUnitOfWork<Priority> _priority;
        private readonly IUnitOfWork<Statues> _statues;
        private readonly IUnitOfWork<ProjectTask> _projectTask;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectTasksController(
                     IUnitOfWork<Project> project,
                     IUnitOfWork<Priority> priority,
                     IUnitOfWork<Statues> statues,
                     IUnitOfWork<Profile> profile,
                     IUnitOfWork<ProjectTask> projectTask,
                     IUnitOfWork<ProjectsUser> projectsUser,
                     IUnitOfWork<TeamUser> teamUser,
                        UserManager<ApplicationUser> userManager,
                      IWebHostEnvironment hostEnvironment,
                       IToastNotification toastNotification
                     )
        {
            _project = project;
            _priority = priority;
            _profile = profile;
            _statues = statues;
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
        private async Task PopulateUsersDropDownList(Guid ProjectId, object selected = null)
        {
            var projectUsers = await _projectsUser.Entity.GetAll()
                .Where(a => a.projectId == ProjectId)
                .Include(a => a.user)
                .ToListAsync();

            var usersList = projectUsers.Select(a =>
            {
                var profile = _profile.Entity.GetAll().FirstOrDefault(k => k.UserId == a.userId);

                return new SelectListItem
                {
                    Value = a.userId.ToString(),
                    Text = profile != null ? profile.DisplayName : a.user.UserName
                };

            }).ToList();

            ViewBag.UsersList = new SelectList(usersList, "Value", "Text", selected);

        }




        public async Task<IActionResult> Tasks(Guid projectId, string message)
        {
            ViewBag.Message = message;



            await PopulateUsersDropDownList(projectId);

            var model = await _projectTask.Entity.GetAll().Where(k => k.projectId == projectId).Include(s => s.project).Include(k => k.statues).Include(s => s.statues).ToListAsync();


            return View(model);
        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateOrEditTask(Guid projectId)
        {


            var model = new TaskViewModel
            {
                projectTask = new ProjectTask(),
                statues = await _statues.Entity.GetAll().ToListAsync(),
                Priorities = await _priority.Entity.GetAll().ToListAsync(),
                ProjectId = projectId,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEditTask(TaskViewModel model)
        {
            //var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid teamID = getTeamID(userID.ToString());
            //var adminteamusers = await _teamuser.Entity.GetAll().Where(a => a.teamId == teamID && a.isAdmin == true && a.isApproved == true && a.isDeleted == false).ToListAsync();

            //if (ModelState.IsValid)
            //{
            //    if (id == Guid.Empty)
            //    {
            //        try
            //        {
            //            Project newProject = new Project();
            //            Guid projectId = Guid.NewGuid();
            //            newProject.Id = projectId;
            //            newProject.Name = model.Name;
            //            newProject.Description = model.Description;
            //            newProject.StartDate = model.StartDate;
            //            newProject.EndDate = model.EndDate;
            //            newProject.CreatedDate = DateTime.Now;
            //            newProject.IsDeleted = false;
            //            newProject.IsArchived = false;
            //            _project.Entity.Insert(newProject);
            //            await _project.SaveAsync();
            //            foreach (var item in model.SelectedUserIds)
            //            {
            //                ProjectsUser projectuser = new ProjectsUser();
            //                projectuser.Id = Guid.NewGuid();
            //                projectuser.userId = item;
            //                projectuser.projectId = projectId;
            //                projectuser.CreatedDate = DateTime.Now; ;
            //                _projectsUser.Entity.Insert(projectuser);
            //                await _projectsUser.SaveAsync();
            //            }


            //            foreach (var item in adminteamusers)
            //            {
            //                if (!model.SelectedUserIds.Contains(item.userId))
            //                {
            //                    ProjectsUser projectuser = new ProjectsUser();
            //                    projectuser.Id = Guid.NewGuid();
            //                    projectuser.userId = item.userId;
            //                    projectuser.projectId = projectId;
            //                    projectuser.CreatedDate = DateTime.Now; ;
            //                    _projectsUser.Entity.Insert(projectuser);
            //                    await _projectsUser.SaveAsync();
            //                }
            //            }



            //            _toastNotification.AddSuccessToastMessage("تم حقظ المشروع بنجاح", new ToastrOptions() { Title = "" });


            //        }
            //        catch
            //        {

            //        }


            //    }
            //    else
            //    {
            //        try
            //        {
            //            var oldproject = await _project.Entity.GetByIdAsync(model.Id);
            //            oldproject.Name = model.Name;
            //            oldproject.Description = model.Description;
            //            oldproject.StartDate = model.StartDate;
            //            oldproject.EndDate = model.EndDate;
            //            oldproject.ModifiedDate = DateTime.Now;
            //            _project.Entity.Update(oldproject);
            //            await _project.SaveAsync();


            //            var oldprojectuser = await _projectsUser.Entity.GetAll().Where(p => p.projectId == model.Id).ToListAsync();

            //            if (oldprojectuser.Any())
            //            {
            //                foreach (var item in oldprojectuser)
            //                {


            //                    Guid delId = item.Id;
            //                    _projectsUser.Entity.Delete(delId);
            //                    await _projectsUser.SaveAsync();


            //                }
            //            }
            //            foreach (var item in model.SelectedUserIds)
            //            {
            //                ProjectsUser projectuser = new ProjectsUser();
            //                projectuser.Id = Guid.NewGuid();
            //                projectuser.userId = item;
            //                projectuser.projectId = model.Id;
            //                projectuser.CreatedDate = DateTime.Now; ;
            //                _projectsUser.Entity.Insert(projectuser);
            //                await _projectsUser.SaveAsync();
            //            }


            //            foreach (var item in adminteamusers)
            //            {
            //                if (!model.SelectedUserIds.Contains(item.userId))
            //                {
            //                    ProjectsUser projectuser = new ProjectsUser();
            //                    projectuser.Id = Guid.NewGuid();
            //                    projectuser.userId = item.userId;
            //                    projectuser.projectId = model.Id;
            //                    projectuser.CreatedDate = DateTime.Now; ;
            //                    _projectsUser.Entity.Insert(projectuser);
            //                    await _projectsUser.SaveAsync();
            //                }
            //            }



            //            id = Guid.Empty;
            //        }
            //        catch (Exception)
            //        {

            //            throw;
            //        }


            //    //    }
            //    //var returnmodel = await getDataAsync(userID.ToString());
            //    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", null) });
            //}

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditTask", model) });




        }


        [ViewLayout("_Layout")]

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {

                //var model = await _project.Entity.GetByIdAsync(id);
                //model.IsDeleted = true;
                //model.ModifiedDate = DateTime.Now;

                //_project.Entity.Update(model);
                //await _project.SaveAsync();
                var model = await _project.Entity.GetByIdAsync(id);

                _project.Entity.Delete(id);
                await _project.SaveAsync();

                _toastNotification.AddSuccessToastMessage("تم الحذف بنجاح", new ToastrOptions() { Title = "" });


                //var returnmodel = await getDataAsync(userID.ToString());

                //return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });
                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });


                //return PartialView("~/Views/Project/_ViewAllProjects.cshtml", returnmodel);
                return RedirectToAction("Tasks", new { UserId = userID.ToString() });

            }
            catch
            {
                return View("projects");


                throw;
            }

        }





    }
}
