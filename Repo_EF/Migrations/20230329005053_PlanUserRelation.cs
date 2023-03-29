using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class PlanUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ApplicationUserId",
                table: "Plans",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserId",
                table: "Plans",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_ApplicationUserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Plans");
        }
    }
}
