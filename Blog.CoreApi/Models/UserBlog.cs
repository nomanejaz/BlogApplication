using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.CoreApi.Models
{
    public class UserBlog
    {
        public UserBlog()
        {
            BlogComments = new List<BlogComment>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }        
        public User Author { get; set; }
        public string AuthorId { get; set; }
        public List<BlogComment> BlogComments { get; set; }
    }
}