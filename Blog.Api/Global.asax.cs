using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {

                string username = Session["Username"]?.ToString();
                string roles = Session["Roles"]?.ToString();

                HttpContext.Current.User = new GenericPrincipal(
                  new GenericIdentity(username, "Forms"), roles.Split(';'));
            }
        }

    }
}