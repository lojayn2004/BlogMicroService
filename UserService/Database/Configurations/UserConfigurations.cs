using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersService.Models;

namespace UsersService.Database.Configurations
{
    public class UserConfigurations: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
       

            builder.Property(e => e.DisplayName)
                   .HasMaxLength(100);
            builder.Property(e => e.Bio)
                   .HasMaxLength(500);
            builder.Property(e => e.AvatarUrl)
                   .HasMaxLength(200);
            builder.Property(e => e.CreatedAt)
                   .HasComputedColumnSql("GETDATE()")
                   .ValueGeneratedOnAdd()
                   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    
    }
}
