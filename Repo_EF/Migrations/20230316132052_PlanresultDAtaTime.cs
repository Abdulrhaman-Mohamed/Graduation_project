using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class PlanresultDAtaTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Commands_commandID_SubSystemId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "commandID",
                table: "Plans",
                newName: "CommandId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_commandID_SubSystemId",
                table: "Plans",
                newName: "IX_Plans_CommandId_SubSystemId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "PlanResults",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Commands_CommandId_SubSystemId",
                table: "Plans",
                columns: new[] { "CommandId", "SubSystemId" },
                principalTable: "Commands",
                principalColumns: new[] { "Id", "SubSystemId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Commands_CommandId_SubSystemId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "CommandId",
                table: "Plans",
                newName: "commandID");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_CommandId_SubSystemId",
                table: "Plans",
                newName: "IX_Plans_commandID_SubSystemId");

            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "PlanResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Commands_commandID_SubSystemId",
                table: "Plans",
                columns: new[] { "commandID", "SubSystemId" },
                principalTable: "Commands",
                principalColumns: new[] { "Id", "SubSystemId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
