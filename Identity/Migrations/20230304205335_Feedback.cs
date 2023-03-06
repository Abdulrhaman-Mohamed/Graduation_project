using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    public partial class Feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Posts_AspNetUsers_UserId",
            //    table: "Posts");

            //migrationBuilder.DropIndex(
            //    name: "IX_Posts_UserId",
            //    table: "Posts");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Posts");

            //migrationBuilder.RenameColumn(
            //    name: "userId",
            //    table: "Posts",
            //    newName: "UserId");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "Posts",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    feedbacktime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Posts_PostIdid",
                        column: x => x.PostIdid,
                        principalTable: "Posts",
                        principalColumn: "id");
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Posts_UserId",
            //    table: "Posts",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Feedbacks_PostIdid",
            //    table: "Feedbacks",
            //    column: "PostIdid");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Posts_AspNetUsers_UserId",
            //    table: "Posts",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Posts_AspNetUsers_UserId",
            //    table: "Posts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            //migrationBuilder.DropIndex(
            //    name: "IX_Posts_UserId",
            //    table: "Posts");

            //migrationBuilder.RenameColumn(
            //    name: "UserId",
            //    table: "Posts",
            //    newName: "userId");

            //migrationBuilder.AlterColumn<int>(
            //    name: "userId",
            //    table: "Posts",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "Posts",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Posts_UserId",
            //    table: "Posts",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Posts_AspNetUsers_UserId",
            //    table: "Posts",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");
        }
    }
}
