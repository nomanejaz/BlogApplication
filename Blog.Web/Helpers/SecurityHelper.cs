using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.DTO;
using Newtonsoft.Json;

namespace Blog.Web.Helpers
{
    public class SecurityHelper
    {
        public static bool HasRole(string role, HttpContextBase httpContext)
        {
            var v = HttpContext.Current.Request.Cookies.Get("user");
            var roles = (List<string>)httpContext.Session["Roles"];
            return HasRequiredRole(role, roles);
        }

        public static bool HasRole(string role)
        {
            var userCookie = HttpContext.Current.Request.Cookies.Get("user");
            if (userCookie == null)
            {
                return false;
            }
            var user = JsonConvert.DeserializeObject<LoginResponse>(userCookie.Value);

            return HasRequiredRole(role, user.Roles);
        }

        public static string GetToken()
        {
            var userCookie = HttpContext.Current.Request.Cookies.Get("user");
            if (userCookie == null)
            {
                return "";
            }
            var user = JsonConvert.DeserializeObject<LoginResponse>(userCookie.Value);

            return user.Token;
        }

        public static string GetUserId()
        {
            var userCookie = HttpContext.Current.Request.Cookies.Get("user");
            if (userCookie == null)
            {
                return "";
            }
            var user = JsonConvert.DeserializeObject<LoginResponse>(userCookie.Value);

            return user.UserId;
        }

        private static bool HasRequiredRole(string requiredRole, List<string> roles)
        {
            if (string.IsNullOrEmpty(requiredRole))
            {
                return true;
            }

            if (roles == null)
            {
                return false;
            }

            if (roles.Any(r => r == requiredRole))
            {
                return true;
            }

            return false;
        }
    }
}