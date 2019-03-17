using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class BlogInput
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}