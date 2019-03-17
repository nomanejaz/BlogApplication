using System.Web.Http;
using Blog.Api.Services;

namespace Blog.Api.Controllers
{
    public class BlogController : ApiController
    {
        private readonly BlogService blogService;

        public BlogController()
        {
            blogService = new BlogService();
        }

        [HttpGet]
        public IHttpActionResult List()
        {
            return Ok(blogService.List());
        }
    }
}