using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DTO
{
    public class BlogCommentDTO
    {

       
        public int Id { get; set; }
        public UserBlogDTO UserBlog { get; set; }
        public int UserBlogId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentPostDate { get; set; }
        public UserDTO PostBy { get; set; }
        public string PostById { get; set; }
    }
}
