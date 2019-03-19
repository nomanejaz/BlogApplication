using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.DTO;
using Blog.Web.Helpers;
using Newtonsoft.Json;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.GetAsync<List<UserDTO>>("List", "Account");
                return View(result);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Blog");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.PostAsync<LoginResponse, LoginModel>("Login", "Account", model);
                if (result.Success)
                {
                    var cookie = new HttpCookie("user", JsonConvert.SerializeObject(result))
                    {
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return RedirectToAction("Index", "Blog");
                }
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.GetAsync<bool>("Delete", "Account", $"{id}");
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<ActionResult> MakeAdmin(string id)
        {
            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.GetAsync<bool>("MakeAdmin", "Account", $"{id}");
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Blog");
            }

            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.PostAsync<bool, RegisterModel>("Register", "Account", model);
                if (result)
                {
                    return await Login(new LoginModel
                    {
                        Username = model.UserName,
                        Password = model.Password
                    });
                }
            }
            ModelState.AddModelError("", "user already exists");
            return View(model);
        }

        public async Task<ActionResult> LogOff()
        {
            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.GetAsync<bool>("LogOff", "Account");
            }

            FormsAuthentication.SignOut();
            HttpCookie myCookie = new HttpCookie("user")
            {
                Expires = DateTime.Now.AddDays(-1d)
            };
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Index", "Blog");
        }
        
    }
}