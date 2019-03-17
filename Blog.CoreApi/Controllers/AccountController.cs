using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.DTO;
using Blog.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginModel model)
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
