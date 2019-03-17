using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DTO
{
    public class UserRoleDTO
    {
      
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string UserId { get; set; }
        public RoleDTO Role { get; set; }
        public int RoleId { get; set; }
    }
}
