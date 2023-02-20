using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class waittingPlan_Edit_in_planTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AckId",
                table: "Plans",
                newName: "namePlan");

            migrationBuilder.AddColumn<int>(
                name: "Divces",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateTime",
                table: "Plans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "inputParamter",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WaittingPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    namePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Repeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcknowledgeId = table.Column<int>(type: "int", nullable: true),
                    SubSystemId = table.Column<int>(type: "int", nullable: false),
                    commandID = table.Column<int>(type: "int", nullable: false),
                    Divces = table.Column<int>(type: "int", nullable: true),
                    inputParamter = table.Column<int>(type: "int", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaittingPlans", x => new { x.Id, x.SequenceNumber });
                    table.ForeignKey(
                        name: "FK_WaittingPlans_Acknowledges_AcknowledgeId",
                        column: x => x.AcknowledgeId,
                        principalTable: "Acknowledges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WaittingPlans_Commands_commandID_SubSystemId",
                        columns: x => new { x.commandID, x.SubSystemId },
                        principalTable: "Commands",
                        principalColumns: new[] { "Id", "SubSystemId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaittingPlans_AcknowledgeId",
                table: "WaittingPlans",
                column: "AcknowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_WaittingPlans_commandID_SubSystemId",
                table: "WaittingPlans",
                columns: new[] { "commandID", "SubSystemId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WaittingPlans");

            migrationBuilder.DropColumn(
                name: "Divces",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "dateTime",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "inputParamter",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "namePlan",
                table: "Plans",
                newName: "AckId");
        }
    }
}
