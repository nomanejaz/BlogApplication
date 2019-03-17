using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.DTO;
using Blog.Web.Helpers;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public async Task<ActionResult> Index()
        {
            using (var apiCaller = new ApiCaller())
                return View(await apiCaller.GetAsync<List<UserBlogDTO>>("List", "Blog"));
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            using (var apiCaller = new ApiCaller())
                return View(await apiCaller.GetAsync<UserBlogDTO>("Get", "Blog", $"{id}"));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            using (var apiCaller = new ApiCaller())
            {
                var result = await apiCaller.GetAsync<bool>("Delete", "Blog", $"{id}");
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new BlogInput());
        }

        [HttpPost]
        public async Task<ActionResult> Create(BlogInput model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var apiCaller = new ApiCaller())
            {
                model.UserId = SecurityHelper.GetUserId();
                var result = await apiCaller.PostAsync<bool, BlogInput>("Create", "Blog", model);

                if (result)
                {
                    return RedirectToAction("index");
                }
            }

            ModelState.AddModelError("", "Model not valid");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComment(BlogCommentInput model)
        {
            using (var apiCaller = new ApiCaller())
            {
                model.UserId = SecurityHelper.GetUserId();
                var result = await apiCaller.PostAsync<bool, BlogCommentInput>("CreateComment", "Blog", model);
                return RedirectToAction("view", new { id = model.Id });
            }
        }

        [AuthorizeUser(Roles = new string[] { "Admin" })]
        public ActionResult MyBlog()
        {
            return View();
        }
    }
}