﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Models
{
    public class BlogComment
    {

        [Key]
        public int Id { get; set; }
        public UserBlog UserBlog { get; set; }
        public int UserBlogId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentPostDate { get; set; }
        public User PostBy { get; set; }
        public string PostById { get; set; }
    }
}