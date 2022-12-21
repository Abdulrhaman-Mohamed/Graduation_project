using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class editinplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Acknowledges_AcknowledgeId",
                table: "Plans");

            migrationBuilder.AlterColumn<int>(
                name: "AcknowledgeId",
                table: "Plans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Acknowledges_AcknowledgeId",
                table: "Plans",
                column: "AcknowledgeId",
                principalTable: "Acknowledges",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Acknowledges_AcknowledgeId",
                table: "Plans");

            migrationBuilder.AlterColumn<int>(
                name: "AcknowledgeId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Acknowledges_AcknowledgeId",
                table: "Plans",
                column: "AcknowledgeId",
                principalTable: "Acknowledges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
