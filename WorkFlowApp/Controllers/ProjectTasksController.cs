

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using NToastNotify;
using System.Drawing;
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

            ViewBag.projectId = projectId;

            await PopulateUsersDropDownList(projectId);

            var data = await _projectTask.Entity.GetAll().Where(k => k.projectId == projectId).Include(s => s.project).Include(k => k.statues).Include(s => s.priority).Include(k => k.User).ToListAsync();


            var model = new List<TaskListViewModel> { };

            foreach (var item in data)
            {
                var prof = _profile.Entity.GetAll().Where(a => a.UserId == item.userId).FirstOrDefault();
                string name;
                string pic;
                if (prof != null)
                {
                    name = prof.DisplayName;
                    pic = prof.Pic;
                }
                else
                {
                    name = item.User.UserName;
                    pic = "-";
                }
                var taskLine = new TaskListViewModel
                {
                    TaskId = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    EndDate = item.EndDate,
                    UserName = name,
                    UserPic = pic,
                    Statues = item.statues.Name,
                    priority = item.priority.Name
                };
                model.Add(taskLine);

            }


            return View(model);






        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateOrEditTask(Guid projectId)
        {

            await PopulateUsersDropDownList(projectId);

            var model = new TaskViewModel
            {
                statues = await _statues.Entity.GetAll().OrderBy(a => a.Num).ToListAsync(),
                Priorities = await _priority.Entity.GetAll().OrderBy(a => a.Num).ToListAsync(),
                ProjectId = projectId,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEditTask(TaskViewModel model, Guid statuesId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (Guid.Empty == model.StatuesId)
                    {
                        ViewBag.Message = "الرجاء إختيار الحالة";
                        return View(model);
                    }
                    if (Guid.Empty == model.ProirityId)
                    {
                        ViewBag.Message = "الرجاء إختيار الأولوية";
                        return View(model);
                    }
                    var statues = await _statues.Entity.GetByIdAsync(model.StatuesId);
                    if (statues == null)
                    {
                        return View("NotFound");
                    }
                    var priority = await _priority.Entity.GetByIdAsync(model.ProirityId);
                    if (priority == null)
                    {
                        return View("NotFound");
                    }


                    string FileName;
                    FileName = UploadedFile(model.File);
                    ProjectTask newTask = new ProjectTask
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        FilePath = FileName,
                        Name = model.Name,
                        Description = model.Description,
                        priorityId = model.ProirityId,
                        statuesId = model.StatuesId,
                        userId = model.userId.ToString(),
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        projectId = model.ProjectId

                    };
                    _projectTask.Entity.Insert(newTask);
                    await _projectTask.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم حفظ المهمة بنجاح", new ToastrOptions() { Title = "" });
                }
                catch (Exception)
                {

                    throw;
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllTasks", null) });

            }
            model.statues = await _statues.Entity.GetAll().ToListAsync();
            model.Priorities = await _priority.Entity.GetAll().ToListAsync();
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditTask", model) });
        }


        private string UploadedFile(IFormFile Img)
        {
            string uniqueFileName = null;

            if (Img != null)
            {
                string uploadsFolder;

                uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "TaskAttach");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Img.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private void DeleteFile(string Img)
        {
            if (Img != "" && Img != null)
            {
                string DeletedFilePath;
                DeletedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "TaskAttach", Img);
                if (System.IO.File.Exists(DeletedFilePath))
                {
                    System.IO.File.Delete(DeletedFilePath);
                }
            }

        }



        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return View();
        }

        public async Task<IActionResult> EditTask(Guid id)
        {


            return View();
        }



    }
}
