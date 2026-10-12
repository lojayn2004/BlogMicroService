using BlogService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogService.Database.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.AuthorId)
                   .HasMaxLength(450);

            builder.Property(e => e.AuthorName)
                   .HasMaxLength(256);

            builder.Property(e => e.AuthorAvatarUrl)
                   .HasMaxLength(2048);

            builder.Property(e => e.Title)
                   .HasMaxLength(500);


            builder.Property(e => e.CreatedAt)
                   .HasComputedColumnSql("GETDATE()")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.UpdatedAt)
                   .HasComputedColumnSql("GETDATE()")
                   .ValueGeneratedOnAddOrUpdate();

        }
    }
}
