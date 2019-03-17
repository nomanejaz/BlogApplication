﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Models
{
    public class UserRole
    {
       [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
