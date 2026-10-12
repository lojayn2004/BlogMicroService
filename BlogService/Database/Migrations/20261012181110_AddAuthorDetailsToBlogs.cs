using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogService.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorDetailsToBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorAvatarUrl",
                table: "Blogs",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Blogs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorAvatarUrl",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Blogs");
        }
    }
}
