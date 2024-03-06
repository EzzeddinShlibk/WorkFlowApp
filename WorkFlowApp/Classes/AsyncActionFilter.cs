
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using WorkFlowApp.Models.Entities;
using WorkFlowApp.Models.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.Classes
{
    public class AsyncActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork<SiteState> _siteState;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AsyncActionFilter(
                            IUnitOfWork<SiteState> siteState,
                            SignInManager<ApplicationUser> signInManager,
                               IHttpContextAccessor httpContextAccessor
                               )
        {
            _siteState = siteState;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes

            //var param = context.ActionArguments.SingleOrDefault();
            //string actionName = context.ActionDescriptor.DisplayName;

            var controllerName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;

            var user = _httpContextAccessor.HttpContext.User;

            var siteState = _siteState.Entity.GetAll().FirstOrDefault();
            if (siteState != null)
            {
                if (!siteState.State && actionName != "Login" && actionName != "Closing" &&
                    ((_signInManager.IsSignedIn(user) && !user.IsInRole("Prog") && !user.IsInRole("Admin"))
                    || !_signInManager.IsSignedIn(user)))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary {{ "Controller", "Admin" },
                                                                                         { "Action", "Closing" } });
                }
                else
                {
                    context.Result = null; // يجب وضع الأمر في else لأننا استعملنا RedirectToRouteResult
                    await next();
                    //// execute any code after the action executes
                }
            }
            else
            {
                context.Result = null;
                await next();
                //// execute any code after the action executes
            }
        }


        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    //Code befor action executes
        //    //throw new NotImplementedException();
        //}

        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    //Code after action executes
        //    //throw new NotImplementedException();
        //}


        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    //Code befor action executes

        //    await next();

        //    //Code after action executes
        //}


    }
}
