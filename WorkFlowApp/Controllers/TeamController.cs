using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models.Entities;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using WorkFlowApp.ViewModels;
using System.Net.Mail;
using System.Drawing.Printing;
using System.Diagnostics;

namespace WorkFlowApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly IUnitOfWork<Team> _team;
        private readonly IUnitOfWork<Profile> _profile;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeamController(
                        UserManager<ApplicationUser> userManager,
                        IUnitOfWork<Team> team,
                        IUnitOfWork<Profile> Profile,
                        IUnitOfWork<TeamUser> teamuser,
                        IWebHostEnvironment hostEnvironment,
                         IToastNotification toastNotification)
        {

            _userManager = userManager;
            _team = team;
            _profile = Profile;
            _teamuser = teamuser;
            _toastNotification = toastNotification;
            _webHostEnvironment = hostEnvironment;

        }
        [HttpGet]
        public async Task<ActionResult> Manage(string UserId)
        {
            if (await _userManager.FindByIdAsync(UserId) == null)
            {
                return View("NotFound");
            }

            var team = _teamuser.Entity.GetAll().Where(a => a.userId == UserId).Include(k => k.team).FirstOrDefault();

            Guid teamID = new Guid();
            if (team != null)
            {
                teamID = team.team.Id;
            }
            var teamUsers = _teamuser.Entity.GetAll().Where(a => a.teamId == teamID && a.isDeleted == false)
                .Include(a => a.user).Include(k => k.team).ToList();



            var model = new List<TeamViewModel> { };

            if (teamUsers.Any())
            {

                ViewBag.code = teamUsers.FirstOrDefault().team.Code;
                var i = 0;
                foreach (var item in teamUsers)
                {

                    var profile = _profile.Entity.GetAll().Where(a => a.UserId == item.userId).FirstOrDefault();
                    string userName;
                    string userphone;
                    string userpic;
                    if (profile != null)
                    {
                        userName = profile.DisplayName;
                        userphone = profile.PhoneNum;
                        userpic = profile.Pic;
                    }
                    else
                    {
                        int atIndex = item.user.UserName.IndexOf('@');
                        userName = (atIndex != -1) ? item.user.UserName.Substring(0, atIndex) : item.user.UserName;
                        userphone = "-";
                        userpic = "-";
                    }
                    if (item.isApproved == false)
                    {
                        i++;
                    }

                    var teamViewModel = new TeamViewModel
                    {
                        UserId = item.userId,
                        Name = userName,
                        Email = item.user.Email,
                        phone = userphone,
                        Pic = userpic,
                        isAdmin = item.isAdmin,
                        isApproved = item.isApproved,


                    };
                    ViewBag.approvedCount = i;
                    model.Add(teamViewModel);


                }
            }
            return View(model);
        }

        //[HttpGet]

        public async Task<ActionResult> AcceptUser(string UserId)
        {

            var teamuser = _teamuser.Entity.GetAll().Where(a => a.userId == UserId).FirstOrDefault();

            teamuser.isApproved = true;
            teamuser.ModifiedDate = DateTime.Now;
            _teamuser.Entity.Update(teamuser);
            _teamuser.SaveAsync();



            return RedirectToAction("Manage", "Team", new { UserId });

        }
        public async Task<ActionResult> DeleteUser(string UserId)
        {

            var teamuser = _teamuser.Entity.GetAll().Where(a => a.userId == UserId).FirstOrDefault();

            teamuser.isDeleted = true;
            teamuser.ModifiedDate = DateTime.Now;
            _teamuser.Entity.Update(teamuser);
            _teamuser.SaveAsync();
            return RedirectToAction("Manage", "Team", new { UserId });

        }


        public async Task<ActionResult> EditUser(string UserId, TeamViewModel model,int role)
        {





            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var member = await _teamuser.Entity.GetAll().Where(a => a.userId == model.UserId).FirstOrDefaultAsync();

            if (role == 2)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Manage", "Team", new { UserId });

                }
                else
                {
                
                        var userRoles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, userRoles);
                        await _userManager.AddToRoleAsync(user, "Admin");

                        member.isAdmin = 2;
                        member.ModifiedDate = DateTime.Now;
                        _teamuser.Entity.Update(member);
                        _teamuser.SaveAsync();
            
                        _toastNotification.AddAlertToastMessage("تم تغيير الصلاحية بنجاح", new ToastrOptions() { Title = "" });
                        return RedirectToAction("Manage", "Team", new { UserId });
                
                }
            }
            else if (role == 3)
            {
                if (await _userManager.IsInRoleAsync(user, "User"))
                {
                    return RedirectToAction("Manage", "Team", new { UserId });

                }
                else
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRoleAsync(user, "User");

                    member.isAdmin = 3;
                    member.ModifiedDate = DateTime.Now;
                    _teamuser.Entity.Update(member);
                    _teamuser.SaveAsync();
                    _toastNotification.AddAlertToastMessage("تم تغيير الصلاحية بنجاح", new ToastrOptions() { Title = "" });
                    return RedirectToAction("Manage", "Team", new { UserId });

                }

            }
            return RedirectToAction("Manage", "Team", new { UserId });



        }

    }
}
