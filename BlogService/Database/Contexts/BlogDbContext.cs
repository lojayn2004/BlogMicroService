using BlogService.Database.Configurations;
using BlogService.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Database.Contexts
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions options): base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            
        }

        public DbSet<Blog> Blogs {  get; set; }
    }
}
