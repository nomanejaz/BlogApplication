using System;
using System.Linq;
using CorApplication.Models;
using CorApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CorApplication.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationContext db;
        public BaseController(IConfiguration configuration)
        {
            db = ConnectionFactory.CreateContext(configuration);
        }

        protected bool ValidateRequest(int? role = null, string userId = "")
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token) || token.Count < 1)
            {
                return false;
            }

            try
            {
                var userToken = token[0].Split(' ').Last();

                byte[] data = Convert.FromBase64String(userToken);
                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                if (when < DateTime.UtcNow.AddHours(-24))
                {
                    return false;
                }

                var user = db.Users.Include("UserRole").FirstOrDefault(u => u.Token == userToken);

                if (!string.IsNullOrEmpty(userId) && userId != user.Id)
                {
                    return false;
                }

                if (role.HasValue && user.UserRole.FirstOrDefault(r => r.RoleId == role.Value) == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        protected string GetToken()
        {
            var token = Request.Headers["Authorization"];
            var userToken = token[0].Split(' ').Last();
            return userToken;
        }
    }
}
