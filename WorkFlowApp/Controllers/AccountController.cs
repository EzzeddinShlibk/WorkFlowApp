﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using System.Text.Encodings.Web;
using WorkFlowApp.ViewModels.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using NToastNotify;
using Microsoft.AspNetCore.Hosting;
using System.IO;



namespace WorkFlowApp.Controllers
{
    //    _userManager: يستخدم لإدارة المستخدمين، مثل إنشاء حساب جديد وتعديل المستخدمين الحاليين.

    //_signInManager: يستخدم لإدارة عمليات تسجيل الدخول وتسجيل الخروج للمستخدمين.

    //_Profile: يستخدم للتعامل مع بيانات الملف الشخصي للمستخدمين، مثل استرجاع الملف الشخصي وتحديثه.

    //_environment: يستخدم للوصول إلى معلومات حول بيئة التطبيق، مثل مسارات الملفات والإعدادات المحيطة.

    //_urlEncoder: يستخدم لترميز وفك ترميز عناصر URL في التطبيق.

    //_roleManager: يستخدم لإدارة أدوار المستخدمين، مثل إنشاء وتعديل وحذف الأدوار.

    //_userStore: يمثل مخزن المستخدمين ويتيح الوصول إلى بيانات المستخدمين في قاعدة البيانات.

    //_emailStore: يمثل مخزن عناوين البريد الإلكتروني للمستخدمين ويسمح بالوصول إلى بيانات البريد الإلكتروني للمستخدمين.

    //_logger: يستخدم لتسجيل الأحداث والأخطاء في التطبيق.

    //_emailSender: يستخدم لإرسال رسائل البريد الإلكتروني، مثل إرسال رسائل تأكيد البريد الإلكتروني أو استعادة كلمة المرور.

    //_team: يستخدم للتعامل مع بيانات الفرق في التطبيق، مثل إنشاء وتعديل وحذف الفرق.

    //_teamuser: يستخدم لتعيين المستخدمين إلى الفرق وإدارة الاشتراكات في الفرق.
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork<Profile> _Profile;
        private readonly IWebHostEnvironment _environment;
        private readonly UrlEncoder _urlEncoder;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<AccountController> _logger;
        private readonly Microsoft.AspNetCore.Identity.UI.Services.IEmailSender _emailSender;
        private readonly IUnitOfWork<Team> _team;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;



        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IToastNotification toastNotification,
            IWebHostEnvironment environment,
            IUnitOfWork<Profile> profile,
            UrlEncoder urlEncoder,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<AccountController> logger,
            Microsoft.AspNetCore.Identity.UI.Services.IEmailSender emailSender,
            IUnitOfWork<Team> team,
            IUnitOfWork<TeamUser> teamuser

            )
        {
            _environment = environment;
            _toastNotification = toastNotification;
            _Profile = profile;
            _urlEncoder = urlEncoder;
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _team = team;
            _teamuser = teamuser;

        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [ViewLayout("_Layout")]
        [HttpGet]
        public async Task<IActionResult> Profile(string UserId)
        {
            if (await _userManager.FindByIdAsync(UserId) == null)
            {
                return View("NotFound");
            }
            var profile = _Profile.Entity.GetAll().Where(a => a.UserId == UserId).FirstOrDefault();
            var model = new Profile();
            if (profile == null)
            {

                model = new Profile();
                model.Pic = "1";    

            }
            else
            {

                model = profile;
                model.UserId = UserId;


            }


            return View(model);
        }

        [ViewLayout("_Layout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfilePost(Profile model)
        {
            if (await _userManager.FindByIdAsync(model.UserId) == null)
            {
                return View("NotFound");
            }

            try
            {
                var existprofile = _Profile.Entity.GetAll().Where(a => a.UserId == model.UserId).FirstOrDefault();
                if (existprofile != null)
                {
                    existprofile.Id = model.Id;
                    existprofile.DisplayName = model.DisplayName;
                    existprofile.bio = model.bio;
                    existprofile.PhoneNum = model.PhoneNum;
                    existprofile.Gender = model.Gender;
                    existprofile.UserId = model.UserId;
                    existprofile.CreatedDate = model.CreatedDate;
                    existprofile.ModifiedDate = DateTime.Now;
                    existprofile.Pic = model.Pic;

                    _Profile.Entity.Update(existprofile);
                    await _Profile.SaveAsync();

                }
                else
                {
                    Profile profile = new Profile
                    {
                        DisplayName = model.DisplayName,
                        bio = model.bio,
                        PhoneNum = model.PhoneNum,
                        Gender = model.Gender,
                        UserId = model.UserId,
                        CreatedDate = DateTime.Now,
                        Pic = model.Pic,
                    };
                    _Profile.Entity.Insert(profile);
                    await _Profile.SaveAsync();

                }

                _toastNotification.AddSuccessToastMessage("تم حفظ البيانات بنجاح", new ToastrOptions() { Title = "" });
                return RedirectToAction("Profile", new { UserId = model.UserId });



            }
            catch
            {
                throw;
            }
        }


        private async Task<Guid> CreateTeam()
        {

            Team team = new Team();
            team.Id = Guid.NewGuid();
            team.Code = team.Id.ToString("N").Substring(0, 5);
            team.CreatedDate = DateTime.Now;
            _team.Entity.Insert(team);
            await _team.SaveAsync();


            return team.Id;
        }


        [ViewLayout("_IdentityLayout")]
        [HttpGet]
        public IActionResult RegisterAsk()
        {
            return View();
        }




        [ViewLayout("_IdentityLayout")]
        [HttpGet]
        public IActionResult JoinTeam()
        {
            return View();
        }
        [ViewLayout("_IdentityLayout")]
        [HttpPost]
        public async Task<IActionResult> JoinTeam(Team model)
        {
            if (ModelState.IsValid)
            {
                var team = _team.Entity.GetAll().Where(a => a.Code == model.Code).FirstOrDefault();
                if (team == null)
                {
                    ViewBag.Result = "رقم الفريق غير موجود ";
                    //_toastNotification.AddErrorToastMessage("رقم الفريق غير موجود");
                    //_toastNotification.AddSuccessToastMessage("تم حفظ البيانات بنجاح", new ToastrOptions() { Title = "" });

                    return View(model);

                }
                Guid teamid = team.Id;
                TempData["TeamID"] = teamid;
                return RedirectToAction("Register");
            }


            return View(model);

        }
        [ViewLayout("_IdentityLayout")]
        [HttpGet]
        public IActionResult Register()
        {

            if (TempData["TeamID"] != null)
            {
                ViewBag.TeamID = (Guid)TempData["TeamID"];
            }
            else
            {
                ViewBag.TeamID = Guid.Empty;
            }

            return View();
        }
        [ViewLayout("_IdentityLayout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, Guid OldTeamID)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    //------------------send email-----------------------------------
                    var subject = "WorkFlow - Email Confirmation";

                    var filePath = _environment.WebRootPath + Path.DirectorySeparatorChar + "templates" + Path.DirectorySeparatorChar + "Confirm.html";
                    StreamReader str = new StreamReader(filePath);
                    string MailText = str.ReadToEnd();
                    str.Close();
                    MailText = MailText.Replace("{UserName}", model.Email);
                    MailText = MailText.Replace("{confirmationLink}", confirmationLink);

                    try
                    {
                        //await _emailSender.SendEmailAsync(message);
                        await _emailSender.SendEmailAsync(model.Email, "Confirm your email", MailText);
                    }
                    catch
                    {
                        ViewBag.ErrorTitle = "Email Confirmation Error";
                        ViewBag.ErrorMessage = "Failed to send email, please check your internet connection and try again";
                        return View("Error");
                    }
                    //-------------------------------------------------------
                    // User يجلبه من الكوكيز لهذا لا نستطيع استعماله في لوقن لأنه لم يكون الكوكي في نفس الاكشن 
                    if (_signInManager.IsSignedIn(User) && (User.IsInRole("Admin") || User.IsInRole("Prog"))) //  في حال الآدمن هو من قام بتسجيل يوزر معين من ادارة المستخدمين
                    {
                        return RedirectToAction("Users", "UsersRoles");
                    }
                    TeamUser teamUser = new TeamUser();
                    if (OldTeamID.Equals(Guid.Empty))
                    {

                        Guid TeamID = await CreateTeam();
                        if (TeamID != Guid.Empty)
                        {

                            teamUser.Id = new Guid();
                            teamUser.CreatedDate = DateTime.Now;
                            teamUser.userId = user.Id;
                            teamUser.teamId = TeamID;
                            teamUser.isAdmin = true;
                            teamUser.isApproved = true;
                            teamUser.isDeleted = false;
                            _teamuser.Entity.Insert(teamUser);
                            await _teamuser.SaveAsync();
                        }
                        await _userManager.AddToRoleAsync(user, "Admin");
                        ViewBag.SaccessTitle = "Registered Successfully";
                        ViewBag.SaccessMessage = "Please check your email, we sent confirmation link";
                    }
                    else
                    {


                        teamUser.Id = new Guid();
                        teamUser.CreatedDate = DateTime.Now;
                        teamUser.userId = user.Id;
                        teamUser.teamId = OldTeamID;
                        teamUser.isAdmin = false;
                        teamUser.isApproved = false;
                        teamUser.isDeleted = false;

                        _teamuser.Entity.Insert(teamUser);
                        await _teamuser.SaveAsync();
                        ViewBag.SaccessTitle = "Registered Successfully";
                        ViewBag.SaccessMessage = "Please check your email, we sent confirmation link and wait for admin approval";

                        await _userManager.AddToRoleAsync(user, "User");

                    }

                    return View("AccountResult");

                    //await _signInManager.SignInAsync(user, isPersistent: false); //isPersistent:false // Session cockie تنتهي عند اغلاق المتصفح مباشرة 
                    //return RedirectToAction("Index", "Home"); // Password: a123.B456
                }

                foreach (var error in result.Errors)// قائمة الأخطاء التي تظهر في summary مثلا شروط الباسوورد وغيرها من الخصائص الإفتراضية
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View(nameof(NotFound));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Cannot be found user with Id={userId}";
                return View(nameof(NotFound));
            }

            if (user.EmailConfirmed)
            {
                ViewBag.Message = "You have already verified your email address";
                return View();
            }

            if ((await _userManager.ConfirmEmailAsync(user, token)).Succeeded)
            {
                if (!(await _userManager.AddToRoleAsync(user, "User")).Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Cannot add Roles to user");
                    return View();
                }
                return View();
            }

            ViewBag.Message = "Email Confirmation failed";
            return View();


        }






        [ViewLayout("_IdentityLayout")]
        [HttpGet]
        [Route("Login")]
        [Route("Account/Login")]
        public async Task<IActionResult> Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [ViewLayout("_IdentityLayout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user) && await _userManager.CheckPasswordAsync(user, model.Password)) // بحيث انه لن يعطي رسالة الكونفيرم الا اذا كان الاسم والباسوورد صحيحين
                    {
                        ViewBag.ErrorTitle = "فشل في تسجيل الدخول";
                        ViewBag.errorMessage = "محاولة تسجيل دخول فاشلة";
                        return View("AccountResult");
                    }

                }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {

                    if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "User"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Prog"))
                    {
                        if (await CheckProgClaims(user))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Main");
                    }
                }
                //}
                if (result.IsLockedOut)
                {
                    if (user.LockoutEnd.Value.Year > DateTime.Now.Year)
                    {
                        ViewBag.ErrorTitle = "فشل في تسجيل الدخول";
                        ViewBag.errorMessage = "هذا المستخدم ممنوع من الدخول من قبل مديرر النظام";
                        return View("Errorlog");
                    }
                    else
                    {


                        ViewBag.ErrorTitle = "فشل في تسجيل الدخول";
                        ViewBag.ErrorTitle = "لقد قمت بمحاولة تسجيل الدخول 5 مرات لقد تم إغلاق الحساب الرجاء المحاولة بعد 15 دقيقة";
                        ViewBag.ResetPassword = "إعادة ضبط كلمة المرور";
                        return View("Errorlog");
                    }

                }
                ModelState.AddModelError(string.Empty, "محاولة تسجيل دخول فاشلة");
            }


            return View(model);
        }


        public async Task<bool> CheckProgClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            if (claims.Count < ClaimsAccess.ClaimsList.Count) //عند اول دخول للمبرمج يكون العدد صفر
            {
                if ((await _userManager.RemoveClaimsAsync(user, claims)).Succeeded)
                {
                    if ((await _userManager.AddClaimsAsync(user, ClaimsAccess.ClaimsList)).Succeeded)
                    {
                        var claims1 = await _userManager.GetClaimsAsync(user);
                        foreach (var claim in claims1)// في حال تخصيص صلاحية مبرمج مع عدم منحه صلاحية كل المطالبات 
                        {
                            if (claim.Value == "false")
                            {
                                if (!(await _userManager.ReplaceClaimAsync(user, claim, new Claim(claim.Type, "true"))).Succeeded)
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                }
                return false;
            }
            else
            {
                foreach (var claim in claims)// في حال تخصيص صلاحية مبرمج مع عدم منحه صلاحية كل المطالبات 
                {
                    if (claim.Value == "false")
                    {
                        if (!(await _userManager.ReplaceClaimAsync(user, claim, new Claim(claim.Type, "true"))).Succeeded)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }


    }
}