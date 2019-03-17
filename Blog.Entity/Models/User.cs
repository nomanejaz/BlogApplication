using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entity.Models
{
    public class User
    {
        public User()
        {
            UserRole = new List<UserRole>();
        }

        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string Email { get; set; }
        public List<UserRole> UserRole { get; set; }
    }
}