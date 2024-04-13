using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using WorkFlowApp.Models.Entities;
using static System.Collections.Specialized.BitVector32;
using WorkFlowApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.ViewModels;
using WorkFlowApp.Models;

namespace WorkFlowApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork<SiteState> _SiteState;
        private readonly IUnitOfWork<Team> _team;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IUnitOfWork<Project> _project;
        private readonly IUnitOfWork<ProjectTask> _projecttask;
        private readonly IUnitOfWork<Contact> _contact;
        private readonly IUnitOfWork<Features> _features;

        public AdminController(
                      IWebHostEnvironment hostEnvironment,
                      IUnitOfWork<SiteState> SiteState,
                      IUnitOfWork<Team> team,
                      IUnitOfWork<Contact> contact,
                      IUnitOfWork<ProjectTask> projecttask,
                      IUnitOfWork<TeamUser> teamuser,
                      IUnitOfWork<Project> project,
                      IUnitOfWork<Features> features,
                      IToastNotification toastNotification
                     )
        {

            _SiteState = SiteState;
            _projecttask=projecttask;
            _webHostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _team = team;
            _features = features;
            _contact = contact;
            _teamuser = teamuser;
            _project = project;
        }


        [HttpGet]
        public async Task<IActionResult> Contacts()
        {

            var model = await _contact.Entity.GetAll().AsNoTracking().FirstOrDefaultAsync();

            if (model == null)
                return View(new Contact());
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacts(Contact model, Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var Contact = await _contact.Entity.GetByIdAsync(id);
                    Contact.Phone1 = model.Phone1;
                    Contact.Phone2 = model.Phone2;
                    Contact.Address = model.Address;
                    Contact.Email1 = model.Email1;
                    Contact.Email2 = model.Email2;
                    Contact.Facebook = model.Facebook;
                    Contact.CreatedDate = model.CreatedDate;
                    Contact.ModifiedDate = DateTime.Now;
                    _contact.Entity.Update(Contact);
                    await _contact.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم حفظ البيانات بنجاح", new ToastrOptions() { Title = "" });

                    return RedirectToAction("Contacts");
                }
                else
                {

                    Guid ContentId = Guid.NewGuid();

                    Contact newContact = new Contact
                    {
                        Id = ContentId,
                        CreatedDate = DateTime.Now,
                        Phone1 = model.Phone1,
                        Phone2 = model.Phone2,
                        Address = model.Address,
                        Email1 = model.Email1,
                        Email2 = model.Email2,
                        Facebook = model.Facebook,

                    };
                    _contact.Entity.Insert(newContact);
                    await _contact.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم حفظ البيانات بنجاح", new ToastrOptions() { Title = "" });


                    return RedirectToAction("Contacts");

                }
            }
            catch
            {
                throw;
            }

        }



        private string UploadedFile(IFormFile Img, String Type)
        {
            string uniqueFileName = null;

            if (Img != null)
            {
                string uploadsFolder;
                if (Type == "Features")
                {
                    uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Features");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Img.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Img.CopyTo(fileStream);
                    }
                }

            }
            return uniqueFileName;
        }
        private void DeleteImg(string Img, String Type)
        {
            if (Img != "" && Img != null)
            {
                string DeletedImagePath;
                if (Type == "Features")
                {
                    DeletedImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Features", Img);
                    if (System.IO.File.Exists(DeletedImagePath))
                    {
                        System.IO.File.Delete(DeletedImagePath);
                    }
                }
       

            }
        }

        [HttpGet]
        public async Task<IActionResult> Features()
        {

            var model = await _features.Entity.GetAll().AsNoTracking().FirstOrDefaultAsync();

            if (model == null)
                return View(new Features());
            else
            {
                return View(model);
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Features(Features model, Guid id)
        {
            try
            {
                string ImageName1, ImageName2, ImageName3;
                if (id != Guid.Empty)
                {


                    if (model.Image1 != null)
                    {
                        DeleteImg(model.Pic1, "Features");
                        ImageName1 = UploadedFile(model.Image1, "Features");
                    }
                    else
                    {
                        ImageName1 = model.Pic1;
                    }
                    if (model.Image2 != null)
                    {
                        DeleteImg(model.Pic2, "Features");
                        ImageName2 = UploadedFile(model.Image2, "Features");
                    }
                    else
                    {
                        ImageName2 = model.Pic2;
                    }
                    if (model.Image3 != null)
                    {
                        DeleteImg(model.Pic3, "Features");
                        ImageName3 = UploadedFile(model.Image3, "Features");
                    }
                    else
                    {
                        ImageName3 = model.Pic3;
                    }




                    var Features = await _features.Entity.GetByIdAsync(id);
                    Features.Title1 = model.Title1;
                    Features.Content1 = model.Content1;
                    Features.Pic1 = ImageName1;

                    Features.Title2 = model.Title2;
                    Features.Content2 = model.Content2;
                    Features.Pic2 = ImageName2;

                    Features.Title3 = model.Title3;
                    Features.Content3 = model.Content3;
                    Features.Pic3 = ImageName3;

                    _features.Entity.Update(Features);
                    await _features.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                    return RedirectToAction("Features");
                }
                else
                {


                    ImageName1 = UploadedFile(model.Image1, "Features");
                    ImageName2 = UploadedFile(model.Image2, "Features");
                    ImageName3 = UploadedFile(model.Image3, "Features");

                    Guid StoreId = Guid.NewGuid();

                    Features newFeatures = new Features
                    {
                        Id = StoreId,
                        Title1 = model.Title1,
                        Content1 = model.Content1,
                        Pic1 = ImageName1,

                        Title2 = model.Title2,
                        Content2 = model.Content2,
                        Pic2 = ImageName2,

                        Title3 = model.Title3,
                        Content3 = model.Content3,
                        Pic3 = ImageName3,

                    };
                    _features.Entity.Insert(newFeatures);
                    await _features.SaveAsync();
                    _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                    return RedirectToAction("Features");

                }
            }
            catch
            {
                throw;
            }

        }










        public async Task<IActionResult> TeamsAsync()
        {
            var teams = await _team.Entity.GetAll().Include(a => a.TeamUsers).ThenInclude(k=>k.user)
                .Where(p => p.TeamUsers.Any(pu => pu.isDeleted == false)).ToListAsync();
            var model = new List<TeamsListViewModel> { };
            foreach (var item in teams)
            {

                var admin =_teamuser.Entity.GetAll().Include(a=>a.user).Where(a=>a.teamId==item.Id && a.isAdmin==1).FirstOrDefault();
                var projects = _project.Entity.GetAll().Include(k => k.ProjectsUsers).Where(p => p.ProjectsUsers.Any(pu => pu.user.Id == admin.user.Id) && p.IsDeleted == false && p.IsArchived == false).Count();
                var projectlist = new TeamsListViewModel
                {

                    code = item.Code,
                    Date = item.CreatedDate,
                    Admin = admin.user.UserName,
                    Users=item.TeamUsers.Count(),
                    Projects= projects,

              
                };
                model.Add(projectlist);
            }
            return View(model);
        }



        [HttpGet]
        [Authorize]
        public IActionResult SiteState(string Message)
        {
            ViewBag.Message = Message;

            var model = _SiteState.Entity.GetAll().FirstOrDefault();
            if (model == null)
            {
                return View("SiteState", new SiteState());
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteState(string save, SiteState model)
        {
            try
            {

                var siteState = _SiteState.Entity.GetAll().FirstOrDefault();
                if (save == "تفعيل الموقع")
                {
                    siteState.State = true;
                }
                else if (save == "إلغاء تفعيل الموقع")
                {
                    siteState.State = false;
                    _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });
                                                        }
                else if (save == "تحديث رسالة الإغلاق")
                {
                    siteState.ClosingMessage = model.ClosingMessage;
                    _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                }
                siteState.ModifiedDate = DateTime.Now;
                _SiteState.Entity.Update(siteState);
                await _SiteState.SaveAsync();
                _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("SiteState");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = "The basic data not found in the database ";
                ViewBag.ErrorMessage = "Missing data row- " + ex;
                return View("Error");
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteStateUpdate(SiteState model)
        {
            try
            {
                var siteState = _SiteState.Entity.GetAll().FirstOrDefault();
                if (siteState != null)
                {
                    siteState.ClosingMessage = model.ClosingMessage;
                    siteState.CreatedDate = model.CreatedDate;
                    siteState.ModifiedDate = DateTime.Now;
                    _SiteState.Entity.Update(siteState);
                    await _SiteState.SaveAsync();
                }
                else
                {
                    var newSiteState = new SiteState
                    {
                        ClosingMessage = model.ClosingMessage,
                        State = true,
                        CreatedDate = DateTime.Now,
                    };
                    _SiteState.Entity.Insert(newSiteState);
                    await _SiteState.SaveAsync();
                }
                _toastNotification.AddSuccessToastMessage("تم الحفظ بنجاح", new ToastrOptions() { Title = "" });

                return RedirectToAction("SiteState");
            }
            catch
            {
                throw;
            }
        }

        [ViewLayout("_MainLayout")]
        [AllowAnonymous]
        [Route("Closing")]
        [HttpGet]
        public IActionResult Closing()
        {
      
            var siteState = _SiteState.Entity.GetAll().FirstOrDefault();
            if (siteState == null)
            {
                return View("Closing", new SiteState());
            }

            if (siteState.State == false)
            {

                return View("Closing", siteState);
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Index()
        {
            var teams=_team.Entity.GetAll().Count();
            var users=_teamuser.Entity.GetAll().Where(a => a.isDeleted==false).Count();
            var projects=_project.Entity.GetAll().Where(a => a.IsDeleted==false).Count();
            var tasks=_projecttask.Entity.GetAll().Where(a => a.isDeleted==false).Count();




            ViewBag.Teams = teams;  
            ViewBag.Projects = projects;
            ViewBag.Tasks = tasks;
            ViewBag.users = users;
            return View();
        }
    }
}
