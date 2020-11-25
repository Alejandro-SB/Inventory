using Inventory.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetToken()
        {
            return Session["JWTtoken"] as string;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception.GetType() == typeof(ApiAuthenticationException))
            {
                var currentUrl = filterContext.HttpContext.Request.Url;

                filterContext.Result = RedirectToAction("Login", "Account", new { returnUrl = currentUrl });

                filterContext.ExceptionHandled = true;

                return;
            }
        }
    }
}