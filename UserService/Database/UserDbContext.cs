using Microsoft.EntityFrameworkCore;
using UsersService.Database.Configurations;
using UsersService.Models;

namespace UsersService.Database
{
    public class UserDbContext: DbContext
    {
        
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new UserConfigurations());
        }

        public DbSet<User> Users { get; set; }

    }
}
