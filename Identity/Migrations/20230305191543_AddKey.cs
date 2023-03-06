using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    public partial class AddKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Feedbacks_Posts_PostIdid",
            //    table: "Feedbacks");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Feedbacks",
            //    table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Feedbacks_PostIdid",
            //    table: "Feedback",
            //    newName: "IX_Feedback_PostIdid");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Feedback",
                type: "nvarchar(450)",
                nullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Feedback",
            //    table: "Feedback",
            //    column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserIdId",
                table: "Feedback",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_AspNetUsers_UserIdId",
                table: "Feedback",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Feedback_Posts_PostIdid",
            //    table: "Feedback",
            //    column: "PostIdid",
            //    principalTable: "Posts",
            //    principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_AspNetUsers_UserIdId",
                table: "Feedback");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Feedback_Posts_PostIdid",
            //    table: "Feedback");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Feedback",
            //    table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_UserIdId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Feedback_PostIdid",
            //    table: "Feedbacks",
            //    newName: "IX_Feedbacks_PostIdid");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Feedbacks",
            //    table: "Feedbacks",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Feedbacks_Posts_PostIdid",
            //    table: "Feedbacks",
            //    column: "PostIdid",
            //    principalTable: "Posts",
            //    principalColumn: "id");
        }
    }
}
