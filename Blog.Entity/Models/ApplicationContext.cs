using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("BlogConnection")
        {
            Database.SetInitializer<ApplicationContext>(null);
        }

        public DbSet<User> Users { get; set; }        
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserBlog> UserBlogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
    }
}
