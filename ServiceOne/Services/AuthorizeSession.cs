using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceOne.Services
{
    public class AuthorizeSession : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["token"] != null || httpContext.Session["user_id"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult(Properties.Settings.Default.ErrorRidirect);
        }
    }
}