using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;
using System.Security.Claims;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;

namespace WorkFlowApp.Controllers
{
    public class ChartsController : Controller
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

        public ChartsController(
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
        public async Task<IActionResult> IndexAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid teamID = getTeamID(userId);

            int teamUserscount = _teamuser.Entity.GetAll()
          .Where(a => a.teamId == teamID && a.isDeleted == false && a.isApproved == true).Count();



            int projectscount = _project.Entity.GetAll()
          .Include(a => a.ProjectsUsers)
          .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == userId) && p.IsDeleted == false && p.IsArchived == false)
          .Count();



            int taskscount = _projectTask.Entity.GetAll().Include(k => k.project).ThenInclude(a => a.ProjectsUsers)
          .Where(p => p.project.ProjectsUsers.Any(pu => pu.user.Id == userId) && p.project.IsDeleted == false && p.project.IsArchived == false).Count();




            ViewBag.TeamUserscount = teamUserscount;
            ViewBag.Projectscount = projectscount;
            ViewBag.Taskscount = taskscount;

            return View();
        }
        public Dictionary<string, int> GetProjectsPerStatus()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tasks = _projectTask.Entity.GetAll().Include(k => k.project).ThenInclude(a => a.ProjectsUsers)
                        .Where(p => p.project.ProjectsUsers.Any(pu => pu.user.Id == userId) && p.project.IsDeleted == false && p.project.IsArchived == false) 
                        .GroupBy(p => p.statues.Name)
                        .ToDictionary(g => g.Key, g => g.Count());
            return tasks;
        }
        public Dictionary<string, int> GetProjectsPerPrioritys()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var tasks = _projectTask.Entity.GetAll().Include(k => k.project).ThenInclude(a => a.ProjectsUsers)
                     .Where(p => p.project.ProjectsUsers.Any(pu => pu.user.Id == userId) && p.project.IsDeleted == false && p.project.IsArchived == false)
                     .GroupBy(p => p.priority.Name)
                     .ToDictionary(g => g.Key, g => g.Count());
            return tasks;
        }




        public JsonResult ProjectsPerStatusChart()
        {
            var data = GetProjectsPerStatus();
            return Json(data);

            //return Json(chartData);
        }

        public JsonResult ProjectsPerPriorityCharts()
        {
            var data = GetProjectsPerPrioritys();

            return Json(data);


        }
        public JsonResult TasksPerProjectDonutChart()
        {
            var data = GetTasksPerProject();

            return Json(data);
        }

        public Dictionary<string, int> GetTasksPerProject()
        {
            var tasksPerProject = _projectTask.Entity.GetAll()
                .GroupBy(p => p.project.Name)
                .ToDictionary(g => g.Key, g => g.Count());

            return tasksPerProject;
        }
        public JsonResult TasksPerUserBarChart()
        {
            var data = GetTasksPerUser();

            return Json(data);
        }

        public Dictionary<string, int> GetTasksPerUser()
        {
            var tasksPerUser = _projectTask.Entity.GetAll()
                .GroupBy(p => p.User.UserName)
                .ToDictionary(g => g.Key, g => g.Count());

            return tasksPerUser;
        }
    }
}
