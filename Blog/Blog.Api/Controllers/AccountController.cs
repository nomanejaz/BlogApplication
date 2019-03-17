using System.Collections.Generic;
using System.Web.Http;
using Blog.DTO;
using Blog.Entity.Models;

namespace Blog.Api.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new LoginResponse
                {
                    Success = false
                });
            }

            if (model.Username.ToLower() == "admin" && model.Password == "123")
            {
                return Ok(new LoginResponse
                {
                    Roles = new List<string>
                {
                    "Admin", "User"
                },
                    Success = true
                });
            }

            if (model.Username.ToLower() == "user" && model.Password == "123")
            {
                return Ok(new LoginResponse
                {
                    Roles = new List<string>
                {
                     "User"
                },
                    Success = true
                });
            }

            return Ok(new LoginResponse
            {
                Success = false
            });
        }
    }
}