using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class newDatabase22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamValues_CommandParams_CommandParamId1_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamValues",
                table: "ParamValues");

            migrationBuilder.DropIndex(
                name: "IX_ParamValues_CommandParamId1_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues");

            migrationBuilder.DropColumn(
                name: "ParamTypeId",
                table: "ParamValues");

            migrationBuilder.DropColumn(
                name: "CommandId",
                table: "ParamValues");

            migrationBuilder.DropColumn(
                name: "CommandParamId1",
                table: "ParamValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamValues",
                table: "ParamValues",
                columns: new[] { "Id", "Device" });

            migrationBuilder.CreateIndex(
                name: "IX_ParamValues_CommandParamId_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues",
                columns: new[] { "CommandParamId", "CommandParamCommandId", "CommandParamParamTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParamValues_CommandParams_CommandParamId_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues",
                columns: new[] { "CommandParamId", "CommandParamCommandId", "CommandParamParamTypeId" },
                principalTable: "CommandParams",
                principalColumns: new[] { "Id", "CommandId", "ParamTypeId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamValues_CommandParams_CommandParamId_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamValues",
                table: "ParamValues");

            migrationBuilder.DropIndex(
                name: "IX_ParamValues_CommandParamId_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues");

            migrationBuilder.AddColumn<int>(
                name: "ParamTypeId",
                table: "ParamValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommandId",
                table: "ParamValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommandParamId1",
                table: "ParamValues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamValues",
                table: "ParamValues",
                columns: new[] { "Id", "Device", "ParamTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ParamValues_CommandParamId1_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues",
                columns: new[] { "CommandParamId1", "CommandParamCommandId", "CommandParamParamTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParamValues_CommandParams_CommandParamId1_CommandParamCommandId_CommandParamParamTypeId",
                table: "ParamValues",
                columns: new[] { "CommandParamId1", "CommandParamCommandId", "CommandParamParamTypeId" },
                principalTable: "CommandParams",
                principalColumns: new[] { "Id", "CommandId", "ParamTypeId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
