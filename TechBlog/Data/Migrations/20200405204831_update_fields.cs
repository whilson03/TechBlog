using Microsoft.EntityFrameworkCore.Migrations;

namespace TechBlog.Data.Migrations
{
    public partial class update_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewsCount",
                table: "BlogPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewsCount",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
