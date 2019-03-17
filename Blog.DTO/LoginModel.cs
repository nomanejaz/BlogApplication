using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class LoginModel
    {
        
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}