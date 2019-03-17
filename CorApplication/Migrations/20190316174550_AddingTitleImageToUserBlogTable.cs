using Microsoft.EntityFrameworkCore.Migrations;

namespace CorApplication.Migrations
{
    public partial class AddingTitleImageToUserBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleImage",
                table: "UserBlogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleImage",
                table: "UserBlogs");
        }
    }
}
