using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DTO;
using Blog.Entity.Models;
using CorApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CorApplication.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(IConfiguration configuration)
            : base(configuration)
        {
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new LoginResponse
                {
                    Success = false
                });
            }

            var user = db.Users.Where(x => x.UserName == model.Username).FirstOrDefault();

            if (user == null)
            {
                return Ok(new LoginResponse
                {
                    Success = false
                });
            }

            if (!SecurePasswordHasherHelper.Verify(model.Password, user.Password))
            {
                return Ok(new LoginResponse
                {
                    Success = false
                });
            }

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            user.Token = token;
            db.SaveChanges();

            return Ok(new LoginResponse
            {
                Roles = db.UserRoles.Where(u => u.UserId == user.Id).Select(r => r.Role.RoleName).ToList(),
                UserId = user.Id,
                UserName = user.UserName,
                Success = true,
                Token = token
            });
        }

        [HttpGet]
        [Route("List")]
        public ActionResult List()
        {
            var users = db.Users.ToList();

            return Ok(PrepareUserOutput(users));
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public ActionResult<bool> Delete(string id)
        {
            if (!ValidateRequest(1))
            {
                return Unauthorized();
            }

            var user = db.Users.FirstOrDefault(b => b.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var blogs = db.UserBlogs.Where(b => b.AuthorId == id);
            var comments = db.BlogComments.Where(c => c.PostById == id);
            var roles = db.UserRoles.Where(r => r.UserId == id);

            db.UserRoles.RemoveRange(roles);
            db.BlogComments.RemoveRange(comments);
            db.UserBlogs.RemoveRange(blogs);

            db.SaveChanges();

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(true);
        }

        [HttpGet]
        [Route("LogOff")]
        public ActionResult<bool> LogOff()
        {
            if (!ValidateRequest())
            {
                return Unauthorized();
            }

            var user = db.Users.FirstOrDefault(b => b.Token == GetToken());

            if (user != null)
            {
                user.Token = "";
                db.SaveChanges();
            }

            return Ok(true);
        }

        [HttpGet]
        [Route("MakeAdmin/{id}")]
        public ActionResult<bool> MakeAdmin(string id)
        {
            if (!ValidateRequest(1))
            {
                return Unauthorized();
            }

            var user = db.Users.FirstOrDefault(b => b.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.UserRole.Add(new UserRole
            {
                RoleId = 1
            });

            db.SaveChanges();

            return Ok(true);
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register([FromBody]UserDTO model)
        {
            var userId = Guid.NewGuid().ToString();

            if (db.Users.Any(u => u.UserName == model.UserName))
            {
                return Ok(false);
            }

            var user = new User()
            {

                UserName = model.UserName,
                Email = model.Email,
                Password = SecurePasswordHasherHelper.Hash(model.Password),
                Id = userId,
                UserRole = new List<UserRole>
                    {
                        new UserRole { UserId = userId , RoleId = 2 }
                    }
            };
            if (!db.Users.Any())
            {
                user.UserRole.Add(new UserRole
                {
                    RoleId = 1
                });
            }

            db.Users.Add(user);
            db.SaveChanges();

            return Ok(true);
        }

        private List<UserDTO> PrepareUserOutput(List<User> users)
        {
            var result = new List<UserDTO>();
            foreach (var user in users)
            {
                var roles = db.UserRoles.Where(r => r.UserId == user.Id).ToList();

                result.Add(new UserDTO
                {
                    Email = user.Email,
                    IsAdmin = roles.Any(r => r.RoleId == 1),
                    Id = user.Id,
                    UserName = user.UserName
                });
            }
            return result;
        }
    }
}