using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WorkFlowApp.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<ProjectTask> _ProjectTask;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NotificationViewComponent(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,
          IUnitOfWork<ProjectTask> projectTask)
        {
            _userManager = userManager;
            _ProjectTask = projectTask;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tasks = await _ProjectTask.Entity.GetAll()
                .Where(a => a.userId == userId && a.isRead == false && a.isDeleted == false)
                .Include(a => a.statues)
                .Include(a => a.priority)
                .ToListAsync();

            ViewBag.NotificationsCount = tasks.Count;
            return View(tasks);
        }
    }
}
