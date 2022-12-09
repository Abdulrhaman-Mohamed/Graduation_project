using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AckId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "ParamValues");

            migrationBuilder.AddColumn<DateTime>(
                name: "EX_Time",
                table: "Plans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EX_Time",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "AckId",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "ParamValues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
