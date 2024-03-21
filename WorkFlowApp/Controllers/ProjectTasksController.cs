

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
        private readonly IUnitOfWork<Comment> _Comment;
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
                     IUnitOfWork<Comment> comment,
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
            _Comment = comment;
            _statues = statues;
            _teamuser = teamUser;
            _projectsUser = projectsUser;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _projectTask = projectTask;
        }

        public IActionResult DownloadFile(string filePath)
        {
            // Ensure that the file path is within the wwwroot directory
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var fullPath = Path.Combine(wwwRootPath, filePath);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            // Return the file for download
            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            var fileName = Path.GetFileName(fullPath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
        public async Task<IActionResult> timeline(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = await _projectTask.Entity.GetAll()
                .Include(a => a.statues)
            .Where(k => k.userId == userId && k.isDeleted == false)

            .ToListAsync();

            // Convert events to a format suitable for JavaScript
            var eventsForJs = query.Select(e => new
            {
                title = e.Name,
                start = e.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = e.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                className = $"bg-soft-{e.statues.Color}" // Assuming you have a property named Color in your Event model
            });

            ViewBag.Events = Newtonsoft.Json.JsonConvert.SerializeObject(eventsForJs);

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> MyTasks(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = await _projectTask.Entity.GetAll()
            .Where(k => k.userId == userId && k.isDeleted == false)
            .Include(k => k.statues)
            .Include(s => s.priority).
            Include(k => k.User).
            Include(k => k.Comments).
            ToListAsync();
            return View(query);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(string comment, Guid Id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Comment newcommet = new Comment();
            newcommet.Id = Guid.NewGuid();
            newcommet.userId = userId;
            newcommet.comment = comment;
            newcommet.DateTime = DateTime.Now;
            newcommet.CreatedDate = DateTime.Now;
            newcommet.projectTaskId = Id;

            _Comment.Entity.Insert(newcommet);
            _Comment.SaveAsync();
            return RedirectToAction("EditTask", new { id = Id.ToString() });

            //return View("EditTask", new { id = Id });

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

        public async Task<List<TaskListViewModel>> getDataAsync(Guid projectId)
        {


            var query = _projectTask.Entity.GetAll()
            .Where(k => k.projectId == projectId && k.isDeleted == false)

        .Include(k => k.statues)
        .Include(s => s.priority).Include(k => k.User);

            var data = query.ToList();


            if (User.IsInRole("User"))
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                data = query.Where(a => a.userId == userId).ToList();

            }



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


            return model;

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
        public async Task<IActionResult> TeamPerformance(Guid projectId, string message)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid teamID = getTeamID(userId);

            var teamUsers = await _teamuser.Entity.GetAll()
                .Where(a => a.teamId == teamID && a.isDeleted == false && a.isApproved == true)
                .Include(a => a.user)
                .ToListAsync();

            var model = new List<UsersTasksViewModel> { };
            var taskslist = new List<ProjectTask> { };
            foreach (var item in teamUsers)
            {
                var tasks = await _projectTask.Entity.GetAll().Include(a => a.statues).Where(a => a.userId == item.userId).ToListAsync();
                int Notcompleted = tasks.Where(k => k.statues.Num != 5).Count();
                int completed = tasks.Where(k => k.statues.Num == 5).Count();


                var prof = _profile.Entity.GetAll().FirstOrDefault(k => k.UserId == item.userId);

                string name;
                string pic;
                if (prof != null)
                {
                    name = prof.DisplayName;
                    pic = prof.Pic;
                }
                else
                {
                    name = item.user.UserName;
                    pic = "-";
                }

                var usersTasksViewModel = new UsersTasksViewModel
                {

                    Name = name,
                    Pic = pic,
                    CompletedTask = completed,
                    UnCompletedTask = Notcompleted,

                };



                model.Add(usersTasksViewModel);





                foreach (var items in tasks)
                {
                    taskslist.Add(items);

                }


            }





            //Convert events to a format suitable for JavaScript
           var eventsForJs = taskslist.Select(e => new
           {
               title = e.Name,
               start = e.StartDate.ToString("yyyy-MM-dd"), // Format the start date without time
               end = e.EndDate.ToString("yyyy-MM-dd"), // Format the end date without time
               className = $"bg-soft-{e.statues.Color}"
           });







           ViewBag.Events = Newtonsoft.Json.JsonConvert.SerializeObject(eventsForJs);
            return View(model);
        }
        public async Task<IActionResult> Tasks(Guid projectId, string message)
        {


            ViewBag.projectId = projectId;
            var model = await getDataAsync(projectId);
            return View(model);
        }


        [HttpGet]

        public async Task<IActionResult> EditTask(Guid id)
        {
            var OldTask = await _projectTask.Entity.GetAll().Where(a => a.Id == id).Include(p => p.Comments).ThenInclude(a => a.user).FirstOrDefaultAsync();




            var comments = new List<commentList> { };


            foreach (var item in OldTask.Comments)
            {
                var prof = _profile.Entity.GetAll().FirstOrDefault(k => k.UserId == item.userId);

                string name;
                string pic;
                if (prof != null)
                {
                    name = prof.DisplayName;
                    pic = prof.Pic;
                }
                else
                {
                    name = item.user.UserName;
                    pic = "-";
                }


                var commentList = new commentList
                {

                    Name = name,
                    Pic = pic,
                    Comment = item.comment

                };
                comments.Add(commentList);
            }



            await PopulateUsersDropDownList(OldTask.projectId);


            var model = new TaskViewModel
            {
                Id = id,
                ProjectId = OldTask.projectId,
                StatuesId = OldTask.statuesId,
                ProirityId = OldTask.priorityId,
                Name = OldTask.Name,
                Description = OldTask.Name,
                StartDate = OldTask.StartDate,
                EndDate = OldTask.EndDate,
                userId = OldTask.userId,
                FilePath = OldTask.FilePath,
                Comments = comments,
                statues = await _statues.Entity.GetAll().OrderBy(a => a.Num).ToListAsync(),
                Priorities = await _priority.Entity.GetAll().OrderBy(a => a.Num).ToListAsync(),



            };
            //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "TaskAttach");
            //var filepath = "wwwroot/Files/TaskAttach/";
            //var filePath = Path.Combine("wwwroot/Files/TaskAttach/", model.FilePath);
            //var path= "wwwroot/Files/TaskAttach/"+model.FilePath;
            //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "TaskAttach" , model.FilePath);
            //var ww = _webHostEnvironment.WebRootPath;
            //var filePath =ww+ "/Files" + "TaskAttach/" + model.FilePath;
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    model.File.CopyTo(stream);
            //}
            //string ff = "C:\\Users\\nusai\\source\\repos\\WorkFlowApp1\\WorkFlowApp\\wwwroot\\Files\\TaskAttach\\e2620494-_test.docx";
            //using (var stream = new FileStream(ff, FileMode.Create))
            //{
            //    model.File.CopyTo(stream);
            //}
            return View(model);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTask(TaskViewModel model, Guid TaskId)
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
                    if (model.File != null)
                    {
                        DeleteFile(model.FilePath);
                        FileName = UploadedFile(model.File);
                    }
                    else
                    {
                        FileName = model.FilePath;
                    }
                    var ExistTask = _projectTask.Entity.GetAll().Where(a => a.Id == model.Id).FirstOrDefault();



                    ExistTask.ModifiedDate = DateTime.Now;
                    ExistTask.FilePath = FileName;
                    ExistTask.Name = model.Name;
                    ExistTask.Description = model.Description;
                    ExistTask.priorityId = model.ProirityId;
                    ExistTask.statuesId = model.StatuesId;
                    ExistTask.userId = model.userId.ToString();
                    ExistTask.StartDate = model.StartDate;
                    ExistTask.EndDate = model.EndDate;
                    ExistTask.projectId = model.ProjectId;
                    ExistTask.isDeleted = false;


                    _projectTask.Entity.Update(ExistTask);
                    await _projectTask.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم حفظ المهمة بنجاح", new ToastrOptions() { Title = "" });
                }
                catch (Exception)
                {

                    throw;
                }


            }
            //return RedirectToAction("EditTask", new { id = model.Id.ToString() });
            return View("EditTask", new { id = model.Id.ToString() });


        }

        [NoDirectAccess]
        public async Task<IActionResult> CreateOrEditTask(Guid projectId)
        {

            await PopulateUsersDropDownList(projectId);
            var statues = _statues.Entity.GetAll().Where(a => a.Num == 1).FirstOrDefault();

            var model = new TaskViewModel
            {
                statues = await _statues.Entity.GetAll().OrderBy(a => a.Num).ToListAsync(),
                Priorities = await _priority.Entity.GetAll().OrderBy(a => a.Num).ToListAsync(),
                ProjectId = projectId,
                StatuesId = statues.Id
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
                    if (model.EndDate < model.StartDate)
                    {

                        ViewBag.erroredDate = "لايمكن ان يكون تاريخ النتهاء اقدم من تاريخ البدء";
                        await PopulateUsersDropDownList(model.ProjectId);
                        model.statues = await _statues.Entity.GetAll().ToListAsync();
                        model.Priorities = await _priority.Entity.GetAll().ToListAsync();
                        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditTask", model) });
                    }
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
                        projectId = model.ProjectId,
                        isDeleted = false,

                    };
                    _projectTask.Entity.Insert(newTask);
                    await _projectTask.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم حفظ المهمة بنجاح", new ToastrOptions() { Title = "" });
                }
                catch (Exception)
                {

                    throw;
                }
                var returnmodel = await getDataAsync(model.ProjectId);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllTasks", returnmodel) });

            }
            await PopulateUsersDropDownList(model.ProjectId);
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
                uniqueFileName = Guid.NewGuid() + "_" + Img.FileName;
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
            var model = await _projectTask.Entity.GetByIdAsync(id);
            model.isDeleted = true;
            model.ModifiedDate = DateTime.Now;

            _projectTask.Entity.Update(model);
            await _projectTask.SaveAsync();

            _toastNotification.AddSuccessToastMessage("تم حفظ المهمة بنجاح", new ToastrOptions() { Title = "" });



            return RedirectToAction("Tasks", new { projectId = model.projectId.ToString() });
        }





    }
}
