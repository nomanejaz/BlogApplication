using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DTO
{
    public class RoleDTO
    {
      
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<UserRoleDTO> UserRole { get; set; }       

    }
}
