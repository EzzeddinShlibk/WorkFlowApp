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

namespace WorkFlowApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectsController(
                     IUnitOfWork<Project> project,
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
        }



        public async Task<IActionResult> Projects(string UserId)
        {

            //var model = GETALLAsync(UserId);

            await PopulateUsersDropDownList(UserId);




            Guid teamID = getTeamID(UserId);



            //if (await _userManager.IsInRoleAsync(user, "Admin"))
            //{ }



            var projects = await _project.Entity.GetAll()
  .Include(a => a.ProjectsUsers)
      .ThenInclude(i => i.user)
  .Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == UserId))
  .ToListAsync();

            //var model = new List<ProjectLine> { };  

            foreach (var item in projects)
            {
                //var projectLine = new ProjectLine
                //{
                //    Name = item.Name,
                //    Description = item.Description,
                //    EndDate = item.EndDate,

                //    phone = userphone,
                //    Pic = userpic,
                //    isAdmin = item.isAdmin,
                //    isApproved = item.isApproved,


                //};
                //model.Add(teamViewModel);
            }


            var model = new ProjectViewModel
            {
                Project = new Project(),
                projects = await _project.Entity.GetAll().ToListAsync(),

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
                
                }



                    _toastNotification.AddSuccessToastMessage("تم حقظ المشروع بنجاح", new ToastrOptions() { Title = "" });


            }
            else
            {
                return View("_Create", model);
            }



            //var modelL = GETALLAsync(userID.ToString());

            return RedirectToAction("Projects", new { UserId = userID.ToString() });


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
