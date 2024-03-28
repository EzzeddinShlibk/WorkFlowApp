using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ViewLayout("_Layout")]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectTask> _projectTask;
        private readonly IUnitOfWork<Comment> _comment;
        private readonly IUnitOfWork<Profile> _profile;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(
                     IUnitOfWork<Project> project,
                     IUnitOfWork<ProjectTask> projectTask,
                     IUnitOfWork<Profile> profile,
                     IUnitOfWork<ProjectsUser> projectsUser,
                     IUnitOfWork<TeamUser> teamUser,
                     IUnitOfWork<Comment> comment,
                        UserManager<ApplicationUser> userManager,
                      IWebHostEnvironment hostEnvironment,
                       IToastNotification toastNotification
                     )
        {
            _comment = comment;
            _project = project;
            _teamuser = teamUser;
            _profile = profile;
            _projectsUser = projectsUser;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _projectTask = projectTask;
        }

        public Guid getTeamID(string UserID)
        {
            var data = _teamuser.Entity.GetAll().Where(a => a.userId == UserID).FirstOrDefault();

            Guid teamID = Guid.Empty;
            if (data != null)
            {
                teamID = data.teamId;
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

            var usersList = teamUsers.Select(a =>
            {
                var profile = _profile.Entity.GetAll().FirstOrDefault(k => k.UserId == a.userId);

                return new SelectListItem
                {
                    Value = a.userId.ToString(),
                    Text = profile != null ? profile.DisplayName : a.user.UserName
                };

            }).ToList();



            ViewBag.UsersList = new SelectList(usersList, "Value", "Text", null);

        }



        public async Task<List<ProjectViewModel>> getDataAsync(string UserId)
        {
            var projects = await _project.Entity.GetAll()
           .Include(a => a.ProjectsUsers)
           .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId) && p.IsDeleted == false && p.IsArchived == false)
           .ToListAsync();


            var model = new List<ProjectViewModel> { };

            foreach (var item in projects)
            {

                TimeSpan difference = DateTime.Now - item.EndDate;
                int daysDifference = Math.Abs(difference.Days);




                var tasks = await _projectTask.Entity.GetAll().Include(a => a.statues).Where(a => a.projectId == item.Id && a.isDeleted == false).ToListAsync();
                int all = tasks.Count;
                int completed = tasks.Where(k => k.statues.Num == 5).Count();

                double percent = (double)completed / all * 100;

                var projectViewModel = new ProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    EndDate = item.EndDate,
                    TaskCount = all,
                    DaysLeft = daysDifference,
                    Percent = Math.Round(percent,3),
                };
                model.Add(projectViewModel);
            }

            return model;
        }


        public async Task<IActionResult> Projects(string message)
        {
            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var model = await getDataAsync(UserId.ToString());
            return View(model);
        }






        [NoDirectAccess]
        public async Task<IActionResult> CreateOrEditProject(Guid id, string message)
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

            var adminteamusers = await _teamuser.Entity.GetAll().Where(a => a.teamId == teamID && a.isAdmin == 1 || a.isAdmin == 2 && a.isDeleted == false).ToListAsync();

            if (ModelState.IsValid)
            {

                if (model.EndDate < model.StartDate)
                {
                    await PopulateUsersDropDownList(userID.ToString());
                    ViewBag.erroredDate = "لايمكن ان يكون تاريخ النتهاء اقدم من تاريخ البدء";
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditProject", model) });
                }
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
                                _projectsUser.Entity.Insert(projectuser);
                                await _projectsUser.SaveAsync();
                            }
                        }



                        _toastNotification.AddSuccessToastMessage("تم حفظ المشروع بنجاح", new ToastrOptions() { Title = "" });


                    }
                    catch
                    {

                    }


                }
                else
                {
                    try
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
                                _projectsUser.Entity.Insert(projectuser);
                                await _projectsUser.SaveAsync();
                            }
                        }

                        _toastNotification.AddSuccessToastMessage("تم حفظ المشروع بنجاح", new ToastrOptions() { Title = "" });


                        id = Guid.Empty;
                    }
                    catch (Exception)
                    {

                        throw;
                    }


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
                var projectTasks = await _projectTask.Entity.GetAll().Where(a => a.projectId == id).ToListAsync();


                foreach (var item in projectTasks)
                {
                    var task = await _projectTask.Entity.GetByIdAsync(item.Id);
                    task.isDeleted = true;
                    task.ModifiedDate = DateTime.Now;
                    _projectTask.Entity.Update(task);
                    await _projectTask.SaveAsync();
                }
                var model = await _project.Entity.GetByIdAsync(id);
                model.IsDeleted = true;
                model.ModifiedDate = DateTime.Now;

                _project.Entity.Update(model);
                await _project.SaveAsync();


                _toastNotification.AddSuccessToastMessage("تم الحذف بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("Projects", new { UserId = userID.ToString() });

            }
            catch
            {
                return View("projects");
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


                return RedirectToAction("Projects", new { UserId = userID.ToString() });

            }
            catch
            {
                return View("projects");


                throw;
            }

        }



        [HttpGet]
        public async Task<IActionResult> DeletedProjects(string message)
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var projects = await _project.Entity.GetAll()
            .Include(a => a.ProjectsUsers)
            .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == userID) && p.IsDeleted == true)
            .ToListAsync();

            var model = new List<DelArchProjectViewModel> { };

            foreach (var item in projects)
            {

                var tasks = await _projectTask.Entity.GetAll().Include(a => a.statues).Where(a => a.projectId == item.Id).ToListAsync();
                int all = tasks.Count;
                int completed = tasks.Where(k => k.statues.Num == 5).Count();

                double percent = (double)completed / all * 100;

                var deletedProjectViewModel = new DelArchProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                    Percent = percent
                };
                model.Add(deletedProjectViewModel);
            }


            return View(model);
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RestroreDeleted(Guid id)
        {
            try
            {
                var projectTasks = await _projectTask.Entity.GetAll().Where(a => a.projectId == id).ToListAsync();


                foreach (var item in projectTasks)
                {
                    var task = await _projectTask.Entity.GetByIdAsync(item.Id);
                    task.isDeleted = false;
                    task.ModifiedDate = DateTime.Now;
                    _projectTask.Entity.Update(task);
                    await _projectTask.SaveAsync();
                }
                var model = await _project.Entity.GetByIdAsync(id);
                model.IsDeleted = false;
                model.ModifiedDate = DateTime.Now;

                _project.Entity.Update(model);
                await _project.SaveAsync();


                _toastNotification.AddSuccessToastMessage("تم الاستعادة بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("DeletedProjects");



            }
            catch
            {
                return RedirectToAction("DeletedProjects");



                throw;
            }

        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteForEver(Guid id)
        {
            try
            {
                // Delete project tasks
                var projectTasks = await _projectTask.Entity.GetAll().Where(a => a.projectId == id).ToListAsync();
                foreach (var task in projectTasks)
                {
                    var comments =await _comment.Entity.GetAll().Where(a=>a.projectTaskId==task.Id).ToListAsync();
                    foreach (var com in comments)
                    {
                        _comment.Entity.Delete(com.Id);
                    }

                    await _comment.SaveAsync();
                    _projectTask.Entity.Delete(task.Id);

                }
                await _projectTask.SaveAsync();

                // Delete project users
                var projectUsers = await _projectsUser.Entity.GetAll().Where(a => a.projectId == id).ToListAsync();
                foreach (var user in projectUsers)
                {
                    _projectsUser.Entity.Delete(user.Id);
                }
                await _projectsUser.SaveAsync();

                // Delete the project itself
                var projectModel = await _project.Entity.GetByIdAsync(id);
                _project.Entity.Delete(projectModel.Id);
                await _project.SaveAsync();

                // Notify success and redirect
                _toastNotification.AddSuccessToastMessage("تم الحذف النهائي بنجاح", new ToastrOptions() { Title = "" });
                return RedirectToAction("DeletedProjects");
            }
            catch (Exception ex)
            {
                throw;
                // Log the exception or handle it appropriately
                //_toastNotification.AddErrorToastMessage("حدث خطأ أثناء الحذف", new ToastrOptions() { Title = "" });
                //return RedirectToAction("DeletedProjects");
            }
        }









        [HttpGet]
        public async Task<IActionResult> ArchivedProjects(string message)
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var projects = await _project.Entity.GetAll()
            .Include(a => a.ProjectsUsers)
            .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == userID) && p.IsArchived == true)
            .ToListAsync();


            var model = new List<DelArchProjectViewModel> { };

            foreach (var item in projects)
            {

                var tasks = await _projectTask.Entity.GetAll().Include(a => a.statues).Where(a => a.projectId == item.Id).ToListAsync();
                int all = tasks.Count;
                int completed = tasks.Where(k => k.statues.Num == 5).Count();

                double percent = (double)completed / all * 100;

                var deletedProjectViewModel = new DelArchProjectViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                    Percent = percent
                };
                model.Add(deletedProjectViewModel);
            }


            return View(model);
        }





        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RestroreArchived(Guid id)
        {
            try
            {

                var model = await _project.Entity.GetByIdAsync(id);
                model.IsArchived = false;
                model.ModifiedDate = DateTime.Now;

                _project.Entity.Update(model);
                await _project.SaveAsync();


                _toastNotification.AddSuccessToastMessage("تم الاستعادة بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("ArchivedProjects");



            }
            catch
            {
                return RedirectToAction("ArchivedProjects");



                throw;
            }

        }

    }
}
