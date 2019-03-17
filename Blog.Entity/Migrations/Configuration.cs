namespace Blog.Entity.Migrations
{
    using Blog.Entity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Entity.Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Blog.Entity.Models.ApplicationContext context)
        {
            //using (var transaction = context.Database.BeginTransaction())
            {

                var role = new List<Role>();

                role.Add(new Role()
                {
                    RoleName = "Administrator"
                });

                role.Add(new Role()
                {
                    Id = 2,
                    RoleName = "Standard"
                });

                
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].Roles ON");
                context.Roles.AddRange(role);
                context.SaveChanges();
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].Roles OFF");
                //transaction.Commit();
            }
            
        }
    }
}
