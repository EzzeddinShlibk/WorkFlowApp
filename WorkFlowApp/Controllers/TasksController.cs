using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using WorkFlowApp.ViewModels;

namespace WorkFlowApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectTask> _projectTask;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TasksController(
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



        public async Task<IActionResult> Tasks(Guid ProjectId, string message)
        {
           // ViewBag.Message = message;
           // var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


           // await PopulateUsersDropDownList(UserId);


           // var projects = await _project.Entity.GetAll()
           //.Include(a => a.ProjectsUsers)
           //.ThenInclude(i => i.user)
           //.Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId) && p.IsDeleted == false && p.IsArchived == false)
           //.ToListAsync();


           // var projectlinelist = new List<ProjectLine> { };

           // foreach (var item in projects)
           // {
           //     TimeSpan difference = item.EndDate - item.StartDate;
           //     int daysDifference = Math.Abs(difference.Days);

           //     int taskscount = _projectTask.Entity.GetAll().Where(a => a.projectId == item.Id).Count();

           //     var projectLine = new ProjectLine
           //     {
           //         Id = item.Id,
           //         Name = item.Name,
           //         Description = item.Description,
           //         EndDate = item.EndDate,
           //         TaskCount = taskscount,
           //         DaysLeft = daysDifference,
           //         Percent = 0
           //     };
           //     projectlinelist.Add(projectLine);
           // }




            //return View(projectlinelist);
            return View();
        }

    }
}
