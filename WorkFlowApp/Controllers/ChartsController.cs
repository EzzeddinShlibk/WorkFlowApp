using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;
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

        public Dictionary<string, int> GetProjectsPerStatus()
        {
            var projectsPerStatus = _projectTask.Entity.GetAll()
                .GroupBy(p => p.statues.Name)
                .ToDictionary(g => g.Key, g => g.Count());

            return projectsPerStatus;
        }
        public Dictionary<string, int> GetProjectsPerPrioritys()
        {
            var projectsPerPriority = _projectTask.Entity.GetAll()
                .GroupBy(p => p.priority.Name)
                .ToDictionary(g => g.Key, g => g.Count());

            return projectsPerPriority;
        }
        public Dictionary<Guid, int> GetProjectsPerPriority()
        {
            var projectsPerPriority = _projectTask.Entity.GetAll()
                .GroupBy(p => p.priority.Id)
                .ToDictionary(g => g.Key, g => g.Count());

            return projectsPerPriority;
        }

        public IActionResult Index()
        {
            return View();
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
