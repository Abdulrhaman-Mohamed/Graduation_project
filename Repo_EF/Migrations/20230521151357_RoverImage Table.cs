using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class RoverImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoverImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanResultId = table.Column<int>(type: "int", nullable: false),
                    PlanSequenceNumber = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoverImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoverImages_PlanResults_PlanResultId_PlanSequenceNumber_PlanId",
                        columns: x => new { x.PlanResultId, x.PlanSequenceNumber, x.PlanId },
                        principalTable: "PlanResults",
                        principalColumns: new[] { "Id", "PlanId", "PlanSequenceNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoverImages_PlanResultId_PlanSequenceNumber_PlanId",
                table: "RoverImages",
                columns: new[] { "PlanResultId", "PlanSequenceNumber", "PlanId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoverImages");
        }
    }
}
