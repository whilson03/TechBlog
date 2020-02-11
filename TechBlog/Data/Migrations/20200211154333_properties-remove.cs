using Microsoft.EntityFrameworkCore.Migrations;

namespace TechBlog.Data.Migrations
{
    public partial class propertiesremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "BlogPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
