using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models;
using WorkFlowApp.Models.Entities;

namespace WorkFlowApp.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly DataContext _context;

        public ProjectTasksController(DataContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ProjectTasks.Include(p => p.User).Include(p => p.priority).Include(p => p.project).Include(p => p.statues);
            return View(await dataContext.ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .Include(p => p.User)
                .Include(p => p.priority)
                .Include(p => p.project)
                .Include(p => p.statues)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewData["userId"] = new SelectList(_context.Profile, "Id", "Id");
            ViewData["priorityId"] = new SelectList(_context.Priorities, "Id", "Name");
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["statuesId"] = new SelectList(_context.Statuess, "Id", "Id");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("projectId,Name,Description,StartDate,EndDate,statuesId,priorityId,userId,FilePath,Id,CreatedDate,ModifiedDate")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                projectTask.Id = Guid.NewGuid();
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Profile, "Id", "Id", projectTask.userId);
            ViewData["priorityId"] = new SelectList(_context.Priorities, "Id", "Name", projectTask.priorityId);
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Name", projectTask.projectId);
            ViewData["statuesId"] = new SelectList(_context.Statuess, "Id", "Id", projectTask.statuesId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Profile, "Id", "Id", projectTask.userId);
            ViewData["priorityId"] = new SelectList(_context.Priorities, "Id", "Name", projectTask.priorityId);
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Name", projectTask.projectId);
            ViewData["statuesId"] = new SelectList(_context.Statuess, "Id", "Id", projectTask.statuesId);
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("projectId,Name,Description,StartDate,EndDate,statuesId,priorityId,userId,FilePath,Id,CreatedDate,ModifiedDate")] ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Profile, "Id", "Id", projectTask.userId);
            ViewData["priorityId"] = new SelectList(_context.Priorities, "Id", "Name", projectTask.priorityId);
            ViewData["projectId"] = new SelectList(_context.Projects, "Id", "Name", projectTask.projectId);
            ViewData["statuesId"] = new SelectList(_context.Statuess, "Id", "Id", projectTask.statuesId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTasks
                .Include(p => p.User)
                .Include(p => p.priority)
                .Include(p => p.project)
                .Include(p => p.statues)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask != null)
            {
                _context.ProjectTasks.Remove(projectTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(Guid id)
        {
            return _context.ProjectTasks.Any(e => e.Id == id);
        }
    }
}
