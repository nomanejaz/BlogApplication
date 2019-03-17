using System.Collections.Generic;

namespace Blog.DTO
{
    public class LoginResponse
    {
        public List<string> Roles { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}