using System;
using System.Collections.Generic;


namespace Blog.DTO
{
    public class UserDTO
    {
        
        public string Id { get; set; }
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserRoleDTO> UserRole { get; set; }
    }
}