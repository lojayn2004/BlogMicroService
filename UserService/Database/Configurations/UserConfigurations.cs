using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersService.Models;

namespace UsersService.Database.Configurations
{
    public class UserConfigurations: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(e => e.UserId);

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
