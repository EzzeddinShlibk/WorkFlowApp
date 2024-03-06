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
using static System.Collections.Specialized.BitVector32;
using static WorkFlowApp.Classes.Helper;

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
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid teamID = getTeamID(userId);

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



        public async Task<List<ProjectLine>> getDataAsync(string UserId)
        {
            await PopulateUsersDropDownList(UserId);
            var projects = await _project.Entity.GetAll()
           .Include(a => a.ProjectsUsers)
           .ThenInclude(i => i.user)
               .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId) && p.IsDeleted == false && p.IsArchived == false)
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

            return projectlinelist;
        }
        public async Task<IActionResult> Projects(string message)
        {
            ViewBag.Message = message;
            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            await PopulateUsersDropDownList(UserId);


            var projects = await _project.Entity.GetAll()
           .Include(a => a.ProjectsUsers)
           .ThenInclude(i => i.user)
           .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId) && p.IsDeleted == false && p.IsArchived == false)
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
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await PopulateUsersDropDownList(userId.ToString());

            if (id == Guid.Empty)
            {
                var model = new Project();
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;
                ViewBag.edit = false;

                return View(model);
            }

            else
            {
                ViewBag.edit = true;
                var model = await _project.Entity.GetAll().Where(a => a.Id == id).Include(a => a.ProjectsUsers).FirstOrDefaultAsync();

                model.SelectedUserIds = model.ProjectsUsers.Select(pu => pu.userId.ToString()).ToList();



                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEditProject(Guid id, Project model, string name)
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid teamID = getTeamID(userID.ToString());
            var adminteamusers = await _teamuser.Entity.GetAll().Where(a => a.teamId == teamID && a.isAdmin == true && a.isApproved == true && a.isDeleted == false).ToListAsync();

            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                {
                    try
                    {
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
                            if (!model.SelectedUserIds.Contains(item.userId))
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



                        _toastNotification.AddSuccessToastMessage("تم حقظ المشروع بنجاح", new ToastrOptions() { Title = "" });


                    }
                    catch
                    {

                    }


                }
                else
                {

                    var oldproject = await _project.Entity.GetByIdAsync(model.Id);
                    oldproject.Name = model.Name;
                    oldproject.Description = model.Description;
                    oldproject.StartDate = model.StartDate;
                    oldproject.EndDate = model.EndDate;
                    oldproject.ModifiedDate = DateTime.Now;
                    _project.Entity.Update(oldproject);
                    await _project.SaveAsync();


                    var oldprojectuser = await _projectsUser.Entity.GetAll().Where(p => p.projectId == model.Id).ToListAsync();

                    if (oldprojectuser.Any())
                    {
                        foreach (var item in oldprojectuser)
                        {


                            Guid delId = item.Id;
                            _projectsUser.Entity.Delete(delId);
                            await _projectsUser.SaveAsync();

                  


                        }
                    }
                    foreach (var item in model.SelectedUserIds)
                    {
                        ProjectsUser projectuser = new ProjectsUser();
                        projectuser.Id = Guid.NewGuid();
                        projectuser.userId = item;
                        projectuser.projectId = model.Id;
                        projectuser.CreatedDate = DateTime.Now; ;
                        projectuser.isManger = true;
                        _projectsUser.Entity.Insert(projectuser);
                        await _projectsUser.SaveAsync();
                    }


                    foreach (var item in adminteamusers)
                    {
                        if (!model.SelectedUserIds.Contains(item.userId))
                        {
                            ProjectsUser projectuser = new ProjectsUser();
                            projectuser.Id = Guid.NewGuid();
                            projectuser.userId = item.userId;
                            projectuser.projectId = model.Id;
                            projectuser.CreatedDate = DateTime.Now; ;
                            projectuser.isManger = true;
                            _projectsUser.Entity.Insert(projectuser);
                            await _projectsUser.SaveAsync();
                        }
                    }



                    id = Guid.Empty;

                }
                var returnmodel = await getDataAsync(userID.ToString());
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });
            }
            await PopulateUsersDropDownList(userID.ToString());

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditProject", model) });




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


                var returnmodel = await getDataAsync(userID.ToString());

                //return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });
                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });


                //return PartialView("~/Views/Project/_ViewAllProjects.cshtml", returnmodel);
                return RedirectToAction("Projects", new { UserId = userID.ToString() });

            }
            catch
            {
                return View("projects");


                throw;
            }

        }

        [ViewLayout("_Layout")]

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Archive(Guid id)
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {

                var model = await _project.Entity.GetByIdAsync(id);
                model.IsArchived = true;
                model.ModifiedDate = DateTime.Now;

                _project.Entity.Update(model);
                await _project.SaveAsync();

                _toastNotification.AddSuccessToastMessage("تمت الارشفة  بنجاح", new ToastrOptions() { Title = "" });


                var returnmodel = await getDataAsync(userID.ToString());

                //return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });
                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllProjects", returnmodel) });


                //return PartialView("~/Views/Project/_ViewAllProjects.cshtml", returnmodel);
                return RedirectToAction("Projects", new { UserId = userID.ToString() });

            }
            catch
            {
                return View("projects");


                throw;
            }

        }




    }
}
