using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class FeedbackPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Posts");
        }
    }
}
