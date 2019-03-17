using Blog.Entity.Models;
using CorApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorApplication.Services
{
    public class BlogService
    {
        private readonly string contentText = @"It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
        private List<UserBlog> blogs;

        public BlogService()
        {
            blogs = new List<UserBlog>();

            for (int i = 1; i <= 20; i++)
            {
                blogs.Add(
                new UserBlog
                {
                    Author = new User
                    {
                        Id = "1",
                        UserName = $"Author {i}"
                    },
                    Id = 1,
                    Content = contentText,
                    PublishDate = DateTime.Now.AddDays(-i),
                    Title = $"Title {i}"
                });
            }
        }

        public List<UserBlog> List()
        {
            return blogs;
        }
    }
}
