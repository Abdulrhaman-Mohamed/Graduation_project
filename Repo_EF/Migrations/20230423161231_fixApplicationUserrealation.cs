using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class fixApplicationUserrealation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Plans",
                newName: "ApplicationUserid");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_ApplicationUserId",
                table: "Plans",
                newName: "IX_Plans_ApplicationUserid");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserid",
                table: "Plans",
                column: "ApplicationUserid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserid",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserid",
                table: "Plans",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_ApplicationUserid",
                table: "Plans",
                newName: "IX_Plans_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_ApplicationUserId",
                table: "Plans",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
