using Microsoft.EntityFrameworkCore;


namespace Blog.CoreApi.Models
{
    public class ApplicationContext : DbContext
    {
        const string connectionString = "Server=NOMAN-EJAZ001\\MSSQLSERVER2016;Database=BlogApplication;Trusted_Connection=True;";


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<User> Users { get; set; }        
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserBlog> UserBlogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
    }
}
