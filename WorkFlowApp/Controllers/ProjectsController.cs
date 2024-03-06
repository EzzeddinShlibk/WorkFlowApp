using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using static System.Collections.Specialized.BitVector32;
using WorkFlowApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models.Entities;
using NToastNotify;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using WorkFlowApp.ViewModels;
using System.Security.Policy;
using WorkFlowApp.Classes;

namespace WorkFlowApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectTask> _projectTask;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectsController(
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



        public async Task<IActionResult> Projects(string UserId)
        {
            await PopulateUsersDropDownList(UserId);
            Guid teamID = getTeamID(UserId);
            var user = await _userManager.FindByIdAsync(UserId);
            //if (await _userManager.IsInRoleAsync(user, "Admin"))
            //{

            //}
            //else if (await _userManager.IsInRoleAsync(user, "User"))
            //{

            //}
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
                    Name = item.Name,
                    Description = item.Description,
                    EndDate = item.EndDate,
                    TaskCount = taskscount,
                    DaysLeft = daysDifference,
                    Percent = 0
                };
                projectlinelist.Add(projectLine);
            }


            var model = new ProjectViewModel
            {
                Project = new Project(),
                projects = await _project.Entity.GetAll().ToListAsync(),
                projectList = projectlinelist

            };




            TempData["UserId"] = UserId;

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project model)
        {

            Guid userID = (Guid)TempData["UserId"];

            if (ModelState.IsValid)
            {
                try
                {
                    Guid teamID = getTeamID(userID.ToString());

                    var adminteamusers = await _teamuser.Entity.GetAll().Where(a => a.teamId == teamID && a.isAdmin == true && a.isApproved == true && a.isDeleted == false).ToListAsync();


                    Project newProject = new Project();
                    Guid projectId = Guid.NewGuid();
                    newProject.Id = projectId;
                    newProject.Name = model.Name;
                    newProject.Description = model.Description;
                    newProject.StartDate = model.StartDate;
                    newProject.EndDate = model.EndDate;
                    newProject.CreatedDate = DateTime.Now;
                    newProject.IsDeleted = false;
                    newProject.IsArchived = false;
                    _project.Entity.Insert(newProject);
                    await _project.SaveAsync();




                    foreach (var item in model.SelectedUserIds)
                    {
                        ProjectsUser projectuser = new ProjectsUser();
                        projectuser.Id = Guid.NewGuid();
                        projectuser.userId = item;
                        projectuser.projectId = projectId;
                        projectuser.CreatedDate = DateTime.Now; ;
                        projectuser.isManger = true;
                        _projectsUser.Entity.Insert(projectuser);
                        await _projectsUser.SaveAsync();
                    }
                    foreach (var item in adminteamusers)
                    {
                        foreach (var user in model.SelectedUserIds)
                        {
                            if (item.userId != user)
                            {
                                ProjectsUser projectuser = new ProjectsUser();
                                projectuser.Id = Guid.NewGuid();
                                projectuser.userId = item.userId;
                                projectuser.projectId = projectId;
                                projectuser.CreatedDate = DateTime.Now; ;
                                projectuser.isManger = true;
                                _projectsUser.Entity.Insert(projectuser);
                                await _projectsUser.SaveAsync();
                            }

                        }


                    }



                    _toastNotification.AddSuccessToastMessage("تم حقظ المشروع بنجاح", new ToastrOptions() { Title = "" });
                    return RedirectToAction("Projects", new { UserId = userID.ToString() });

                }
                catch
                {
                    //return PartialView("_Create", model);
                    return PartialView("~/Views/Projects/_Create.cshtml", model);
                }
            }
            ModelState.AddModelError("", "1111Please fill all required fields");
            //return PartialView("_Create", model);
            //return PartialView("~/Views/Projects/_Create.cshtml", model);

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_Create", model) });






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

            // Assuming user.Name is the property you want to display in the dropdown
            var usersList = teamUsers.Select(a => new SelectListItem
            {
                Value = a.userId.ToString(),
                Text = a.user.UserName
            });

            ViewBag.UsersList = new SelectList(usersList, "Value", "Text", selected);

        }
    }
}
