using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NToastNotify;
using static System.Collections.Specialized.BitVector32;
using static WorkFlowApp.Classes.Helper;
using WorkFlowApp.Classes;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WorkFlowApp.Controllers
{
    [ViewLayout("_Layout")]
    public class SectionsController : Controller

    {
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectsUser> _projectsUser;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SectionsController(
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
        //public async Task<IActionResult> Projects(string message)
        //{
        //    ViewBag.Message = message;
        //    var model = await _Section.Entity.GetAll().OrderByDescending(u => u.NameAr).AsNoTracking().ToListAsync();
        //    return View(model);
        //}

        //[NoDirectAccess]
        //public async Task<IActionResult> CreateOrEditProject(Guid id)
        //{
        //    if (id == Guid.Empty)
        //        return View(new Section());
        //    else
        //    {
        //        var model = await _Section.Entity.GetByIdAsync(id);
        //        if (model == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateOrEditProject(Guid id, Section model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string ImageName, CoverName;
        //        if (id == Guid.Empty)
        //        {
        //            var ExistName = await _Section.Entity.GetAll().Where(a => a.NameAr.ToUpper() == model.NameAr.ToUpper() || a.NameEn.ToUpper() == model.NameEn.ToUpper()).FirstOrDefaultAsync();

        //            if (ExistName == null)
        //            {
        //                ImageName = UploadedFile(model.Image);
        //                CoverName = UploadedFile(model.CoverImage);
        //                Section newsection = new Section();
        //                newsection.NameAr = model.NameAr;
        //                newsection.NameEn = model.NameEn;
        //                newsection.Pic = ImageName;
        //                newsection.CoverPic = CoverName;
        //                newsection.CreatedDate = DateTime.Now;
        //                _Section.Entity.Insert(newsection);
        //                await _Section.SaveAsync();
        //                _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });

        //            }
        //            else
        //            {
        //                _toastNotification.AddErrorToastMessage(_localizer["Alreadyexists"].Value, new ToastrOptions() { Title = "" });

        //            }
        //        }
        //        else
        //        {
        //            var ExistSecName = await _Section.Entity.GetAll().Where(a => a.NameAr.ToUpper() == model.NameAr.ToUpper() && a.Id != model.Id || a.NameEn.ToUpper() == model.NameEn.ToUpper() && a.Id != model.Id).FirstOrDefaultAsync();
        //            if (ExistSecName == null)
        //            {
        //                if (model.Image != null)
        //                {
        //                    DeleteImg(model.Pic);
        //                    ImageName = UploadedFile(model.Image);
        //                }
        //                else
        //                {
        //                    ImageName = model.Pic;
        //                }
        //                if (model.CoverImage != null)
        //                {
        //                    DeleteImg(model.CoverPic);
        //                    CoverName = UploadedFile(model.CoverImage);
        //                }
        //                else
        //                {
        //                    CoverName = model.CoverPic;
        //                }
        //                var sections = await _Section.Entity.GetByIdAsync(model.Id);
        //                sections.NameAr = model.NameAr;
        //                sections.NameEn = model.NameEn;
        //                sections.Pic = ImageName;
        //                sections.CoverPic = CoverName;

        //                sections.ModifiedDate = DateTime.Now;
        //                _Section.Entity.Update(sections);
        //                await _Section.SaveAsync();
        //                _toastNotification.AddSuccessToastMessage(_localizer["SaveMsg"].Value, new ToastrOptions() { Title = "" });

        //                id = Guid.Empty;
        //            }
        //            else
        //            {
        //                _toastNotification.AddErrorToastMessage(_localizer["Alreadyexists"].Value, new ToastrOptions() { Title = "" });


        //            }
        //        }
        //        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllSections", await _Section.Entity.GetAll().AsNoTracking().ToListAsync()) });
        //    }
        //    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEditSection", model) });
        //}
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        var ExistName = await _Categorie.Entity.GetAll().Include(n => n.Section).Where(a => a.SectionId == id).FirstOrDefaultAsync();
        //        if (ExistName == null)
        //        {
        //            var model = await _Section.Entity.GetByIdAsync(id);
        //            DeleteImg(model.Pic);
        //            _Section.Entity.Delete(id);
        //            await _Section.SaveAsync();

        //            _toastNotification.AddSuccessToastMessage(_localizer["DeleteMsg"].Value, new ToastrOptions() { Title = "" });

        //        }
        //        else
        //        {
        //            _toastNotification.AddErrorToastMessage(_localizer["CantDelete"].Value, new ToastrOptions() { Title = "" });

        //        }
        //        return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllSections", await _Section.Entity.GetAll().AsNoTracking().ToListAsync()) });
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

    }
}
