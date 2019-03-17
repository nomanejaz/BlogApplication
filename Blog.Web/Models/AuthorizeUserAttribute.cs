using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Models
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public string[] Roles { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Roles"] != null)
            {
                var roles = (List<string>)httpContext.Session["Roles"];
                return HasRequiredRole(roles);
            }
            else
                return false;
        }

        private bool HasRequiredRole(List<string> roles)
        {
            if (Roles == null || Roles.Length < 1)
            {
                return true;
            }

            foreach (var role in Roles)
            {
                if (roles.Any(r => r == role))
                {
                    return true;
                }
            }

            return false;
        }
    }
}