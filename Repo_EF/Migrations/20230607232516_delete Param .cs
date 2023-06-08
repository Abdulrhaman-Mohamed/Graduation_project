using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class deleteParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParamValues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParamValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SubSystemID = table.Column<int>(type: "int", nullable: false),
                    CommandID = table.Column<int>(type: "int", nullable: false),
                    CommandParamID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Device = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamValues", x => new { x.Id, x.SubSystemID, x.CommandID, x.CommandParamID });
                    table.ForeignKey(
                        name: "FK_ParamValues_CommandParams_CommandParamID_CommandID_SubSystemID",
                        columns: x => new { x.CommandParamID, x.CommandID, x.SubSystemID },
                        principalTable: "CommandParams",
                        principalColumns: new[] { "Id", "CommandId", "SubSystemId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParamValues_CommandParamID_CommandID_SubSystemID",
                table: "ParamValues",
                columns: new[] { "CommandParamID", "CommandID", "SubSystemID" });
        }
    }
}
