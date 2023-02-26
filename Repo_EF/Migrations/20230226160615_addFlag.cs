using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class addFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WaittingPlans");

            migrationBuilder.DropColumn(
                name: "namePlan",
                table: "Plans");

            migrationBuilder.AddColumn<bool>(
                name: "FlagWatting",
                table: "Plans",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagWatting",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "namePlan",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WaittingPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    AcknowledgeId = table.Column<int>(type: "int", nullable: true),
                    commandID = table.Column<int>(type: "int", nullable: false),
                    SubSystemId = table.Column<int>(type: "int", nullable: false),
                    Delay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Divces = table.Column<int>(type: "int", nullable: true),
                    Repeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    inputParamter = table.Column<int>(type: "int", nullable: true),
                    namePlan = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
