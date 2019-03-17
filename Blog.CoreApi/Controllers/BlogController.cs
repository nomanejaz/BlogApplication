using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService blogService;

        public BlogController()
        {
            blogService = new BlogService();
        }

        [HttpGet]
        [Route("List")]
        public ActionResult<IEnumerable<UserBlog>> List()
        {
            return Ok(blogService.List());
        }       
    }
}
