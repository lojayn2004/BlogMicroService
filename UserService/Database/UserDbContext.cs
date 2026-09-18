using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersService.Database.Configurations;
using UsersService.Models;

namespace UsersService.Database
{
    public class UserDbContext: IdentityDbContext<ApplicationUser>
    {
        
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new UserConfigurations());
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
