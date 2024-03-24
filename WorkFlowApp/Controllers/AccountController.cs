
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using System.Text.Encodings.Web;
using WorkFlowApp.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using NToastNotify;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WorkFlowApp.Classes;
using Microsoft.AspNetCore.Identity.UI.Services;



namespace WorkFlowApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork<Profile> _Profile;
        private readonly IWebHostEnvironment _environment;
        private readonly UrlEncoder _urlEncoder;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork<Team> _team;
        private readonly IUnitOfWork<TeamUser> _teamuser;
        private readonly IToastNotification _toastNotification;

        private readonly EmailService _emailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IToastNotification toastNotification,
            EmailService emailService,
            IWebHostEnvironment environment,
            IUnitOfWork<Profile> profile,
            UrlEncoder urlEncoder,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<AccountController> logger,
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
            _signInManager = signInManager;
            _logger = logger;
            _team = team;
            _teamuser = teamuser;
            _emailService = emailService;

        }

        [HttpGet]
        public IActionResult AccessDenied(string message)
        {
            ViewBag.Message = message;
            return View();
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
                    EmailConfirmed = false,
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
                        await _emailService.SendEmailAsync(model.Email, subject, MailText);

                    }
                    catch (Exception)
                    {

                        throw;
                    }

              


                    if (_signInManager.IsSignedIn(User) &&  User.IsInRole("Prog")) //  في حال الآدمن هو من قام بتسجيل يوزر معين من ادارة المستخدمين
                    {
                        await _userManager.AddToRoleAsync(user, "prog");

                        return RedirectToAction("Users", "Users");
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
                            teamUser.isAdmin = 1;
                            teamUser.isApproved = true;
                            teamUser.isDeleted = false;
                            _teamuser.Entity.Insert(teamUser);
                            await _teamuser.SaveAsync();
                        }
                        await _userManager.AddToRoleAsync(user, "Admin");
                        ViewBag.SaccessTitle = "تم التسجيل بنجاح";
                        ViewBag.SaccessMessage = "الرجاء التحقق من البريد الالكتروني لقد قمنا بارسال رابط التأكيد";
                    }
                    else
                    {


                        teamUser.Id = new Guid();
                        teamUser.CreatedDate = DateTime.Now;
                        teamUser.userId = user.Id;
                        teamUser.teamId = OldTeamID;
                        teamUser.isAdmin = 3;
                        teamUser.isApproved = false;
                        teamUser.isDeleted = false;

                        _teamuser.Entity.Insert(teamUser);
                        await _teamuser.SaveAsync();
                        ViewBag.SaccessTitle = "تم التسجيل بنجاح";
                        ViewBag.SaccessMessage = "الرجاء التحقق من البريد الالكتروني لقد قمنا بارسال رابط التأكيد";

                        await _userManager.AddToRoleAsync(user, "User");

                    }

                    return View("AccountResult");

                   
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
                ViewBag.Message = "لقد قمت بتأكيد حسابك مسبقاً";
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
            //if (User.Identity.IsAuthenticated)
            //{
            //    return LocalRedirect(returnUrl ?? "/");
            //}

            //HttpContext.SignOutAsync(IdentityConstants.ExternalScheme).Wait();

            //ViewData["ReturnUrl"] = returnUrl;
            //return View();

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
            //returnUrl ??= Url.Content("~/");
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

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Projects", "Project");
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
                        var team = _teamuser.Entity.GetAll().Where(a => a.userId == user.Id).FirstOrDefault();

                        if (team != null)
                        {
                            if (team.isApproved == false)
                            {
                                ViewBag.ErrorTitle = "فشل في تسجيل الدخول";
                                ViewBag.errorMessage = "الرجاء انتظار قبول طلب الانضمام  ";
                                await _signInManager.SignOutAsync();
                                return View("Errorlog");
                            }
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result.IsLockedOut)
                {
                    if (user.LockoutEnd.Value.Year > DateTime.Now.Year)
                    {
                        ViewBag.ErrorTitle = "فشل في تسجيل الدخول";
                        ViewBag.errorMessage = "هذا المستخدم ممنوع من الدخول من قبل مدير النظام";
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







        [ViewLayout("_IdentityLayout")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (_signInManager.IsSignedIn(User))
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }
        [ViewLayout("_IdentityLayout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (await _userManager.IsEmailConfirmedAsync(user))
                    {

                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                        //------------------send email-----------------------------------
                        var subject = "Flow master - Forgot password";

                        var filePath = _environment.WebRootPath + Path.DirectorySeparatorChar + "templates" + Path.DirectorySeparatorChar + "ForgotPassword.html";
                        StreamReader str = new StreamReader(filePath);
                        string MailText = str.ReadToEnd();
                        str.Close();
                        MailText = MailText.Replace("{UserName}", model.Email);
                        MailText = MailText.Replace("{passwordResetLink}", passwordResetLink);
                        try
                        {
                            await _emailService.SendEmailAsync(model.Email, subject, MailText);

                        }
                        catch (Exception)
                        {
                            ViewBag.ErrorTitle = "Reset password Error";
                            ViewBag.ErrorMessage = "Failed to send email";
                            return View("Error");
                        }
              
                        //-------------------------------------------------------
                        ViewBag.SaccessTitle = "Reset password";
                        ViewBag.SaccessMessage = "Please check your email, The link of reset password has been sent to your email";
                        return View("AccountResult");
                    }
                    ViewBag.ErrorTitle = "Failed Reset password";
                    ViewBag.ErrorMessage = "Your account needs to be Email Confirmation";
                    return View("AccountResult");
                }
                ViewBag.ErrorTitle = "Failed Reset password";
                ViewBag.ErrorMessage = "We cannot find the email, please check your email spelling ";
                return View("AccountResult");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.SaccessTitle = "Reset password";
                        ViewBag.SaccessMessage = "Reset password successfully";
                        return View("AccountResult");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                ViewBag.ErrorTitle = "Failed Reset password";
                ViewBag.ErrorMessage = "Your account not found";
                return View("AccountResult");
            }
            return View(model);
        }
        [ViewLayout("_Layout")]
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [ViewLayout("_Layout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    ViewBag.SaccessTitle = "تغيير كلمة المرور";
                    ViewBag.SaccessMessage = "تم تغيير كلمة المرور بنجاح";
                    return View("AccountResult");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
                //----------بالإمكان تحويله الى صفحة خاصة لعرض رسالة خاصة
                //ViewBag.ErrorTitle = "Failed Reset password";
                //ViewBag.ErrorMessage = "Your account not found";
                //return View("AccountResult");
            }

            return View(model);
        }

    }
}