using System;
using System.Collections.Generic;


namespace Blog.DTO
{
    public class UserBlogDTO
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }        
        public UserDTO Author { get; set; }
        public string AuthorId { get; set; }
        public List<BlogCommentDTO> BlogComments { get; set; }
        public int CommentCount { get; set; }
        public string TitleImage { get; set; }
    }
}