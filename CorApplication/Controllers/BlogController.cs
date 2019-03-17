using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DTO;
using Blog.Entity.Models;
using CorApplication.Models;
using CorApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CorApplication.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {
        public BlogController(IConfiguration configuration)
            : base(configuration)
        {
        }

        [HttpGet]
        [Route("List")]
        public ActionResult<IEnumerable<UserBlog>> List()
        {
            var blogs = db.UserBlogs.Include("Author").Include("BlogComments").ToList();
            var result = new List<UserBlogDTO>();
            foreach (var blog in blogs)
            {
                result.Add(new UserBlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    AuthorId = blog.AuthorId,
                    Author = new UserDTO
                    {
                        UserName = blog.Author.UserName
                    },
                    PublishDate = blog.PublishDate,
                    CommentCount = blog.BlogComments.Count
                });
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult<IEnumerable<UserBlog>> Get(int id)
        {
            var blog = db.UserBlogs.Include("Author"). Include("BlogComments").Include("BlogComments.PostBy").FirstOrDefault(b => b.Id == id);

            return Ok(new UserBlogDTO
            {
                Title = blog.Title,
                Content = blog.Content,
                AuthorId = blog.AuthorId,
                Author = new UserDTO
                {
                    UserName = blog.Author.UserName
                },
                PublishDate = blog.PublishDate,
                BlogComments = blog.BlogComments.Select(c => new BlogCommentDTO
                {
                    Comment = c.Comment,
                    CommentPostDate = c.CommentPostDate,
                    PostBy = new UserDTO
                    {
                        UserName = c.PostBy.UserName
                    },
                    PostById = c.PostById
                }).ToList()
            });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            if (!ValidateRequest())
            {
                return Unauthorized();
            }

            var blog = db.UserBlogs.FirstOrDefault(b => b.Id == id);

            if (blog != null)
            {
                db.UserBlogs.Remove(blog);
                db.SaveChanges();
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<bool> Create([FromBody]BlogInput input)
        {
            if (!ValidateRequest())
            {
                return Unauthorized();
            }
            db.UserBlogs.Add(new UserBlog
            {
                AuthorId = input.UserId,
                Content = input.Content,
                Title = input.Title,
                PublishDate = DateTime.Now
            });

            db.SaveChanges();

            return Ok(true);
        }


        [HttpPost]
        [Route("CreateComment")]
        public ActionResult<bool> CreateComment([FromBody]BlogCommentInput input)
        {
            if (!ValidateRequest())
            {
                return Unauthorized();
            }
            var blog = db.UserBlogs.FirstOrDefault(b => b.Id == input.Id);

            if (blog != null)
            {
                blog.BlogComments.Add(new BlogComment
                {
                    Comment = input.Comment,
                    CommentPostDate = DateTime.Now,
                    PostById = input.UserId
                });

                db.SaveChanges();
            }

            return Ok(true);
        }

        [HttpGet]
        [Route("UserBlogs")]
        public ActionResult<IEnumerable<UserBlog>> UserBlogs(string UserId)
        {
            return Ok(db.UserBlogs.Include("Author").Where(x => x.AuthorId == UserId).ToList());
        }
    }
}