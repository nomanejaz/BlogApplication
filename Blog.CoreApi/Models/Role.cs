using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreApi.Models
{
    public class Role
    {
        public Role()
        {
            UserRole = new List<UserRole>();
        }

        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<UserRole> UserRole { get; set; }       

    }
}
