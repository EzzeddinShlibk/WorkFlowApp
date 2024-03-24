using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using WorkFlowApp.Models.UnitOfWork;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using WorkFlowApp.Classes;





var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("testContextConnection") ?? throw new InvalidOperationException("Connection string 'testContextConnection' not found.");

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
var config = builder.Configuration;


builder.Services.AddScoped<EmailService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    //options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
})
     .AddDefaultUI()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//           .AddEntityFrameworkStores<DbContext>();

//builder.Services.AddScoped<SignInManager<IdentityUser>>();


builder.Services.AddScoped<AsyncActionFilter>();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(AsyncActionFilter));

    //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //options.Filters.Add(new AuthorizeFilter(policy));

}).AddXmlSerializerFormatters();
builder.Services.AddHttpContextAccessor();







// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
{
    ProgressBar = false,
    PositionClass = ToastPositions.BottomCenter
});
builder.Services.AddRazorPages();
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}








//NOTE this line must be above .UseMvc() line.



app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
