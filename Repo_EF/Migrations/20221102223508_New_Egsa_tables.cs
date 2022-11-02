using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo_EF.Migrations
{
    public partial class New_Egsa_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Command",
                columns: table => new
                {
                    com_id = table.Column<int>(type: "int", nullable: false),
                    sub_ID = table.Column<int>(type: "int", nullable: false),
                    com_description = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    sensor_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.sub_ID);
                });

            migrationBuilder.CreateTable(
                name: "param_TB_type",
                columns: table => new
                {
                    param_ID = table.Column<int>(type: "int", nullable: false),
                    param_type = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_param_TB_type", x => x.param_ID);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Nat_ID = table.Column<int>(type: "int", nullable: false),
                    Station_name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Station_Type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Nat_ID);
                });

            migrationBuilder.CreateTable(
                name: "CoM_Param",
                columns: table => new
                {
                    param_ID = table.Column<int>(type: "int", nullable: false),
                    com_id = table.Column<int>(type: "int", nullable: false),
                    sub_Id = table.Column<int>(type: "int", nullable: false),
                    param_type = table.Column<int>(type: "int", nullable: true),
                    Commandsub_ID = table.Column<int>(type: "int", nullable: false),
                    param_TB_typeparam_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoM_Param", x => x.sub_Id);
                    table.UniqueConstraint("AK_CoM_Param_param_ID_com_id_sub_Id", x => new { x.param_ID, x.com_id, x.sub_Id });
                    table.ForeignKey(
                        name: "FK_CoM_Param_Command_Commandsub_ID",
                        column: x => x.Commandsub_ID,
                        principalTable: "Command",
                        principalColumn: "sub_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoM_Param_param_TB_type_param_TB_typeparam_ID",
                        column: x => x.param_TB_typeparam_ID,
                        principalTable: "param_TB_type",
                        principalColumn: "param_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Satellite",
                columns: table => new
                {
                    Sat_ID = table.Column<int>(type: "int", nullable: false),
                    Nat_ID = table.Column<int>(type: "int", nullable: true),
                    Sat_name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Launch_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mass = table.Column<int>(type: "int", nullable: true),
                    Sat_type = table.Column<short>(type: "smallint", nullable: true),
                    Orbit_Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    StationNat_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellite", x => x.Sat_ID);
                    table.ForeignKey(
                        name: "FK_Satellite_Station_StationNat_ID",
                        column: x => x.StationNat_ID,
                        principalTable: "Station",
                        principalColumn: "Nat_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Param_Value",
                columns: table => new
                {
                    sub_ID = table.Column<int>(type: "int", nullable: false),
                    com_id = table.Column<int>(type: "int", nullable: false),
                    parm_ID = table.Column<int>(type: "int", nullable: false),
                    device = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Param_Value", x => new { x.com_id, x.parm_ID });
                    table.ForeignKey(
                        name: "FK_Param_Value_CoM_Param_parm_ID_com_id_sub_ID",
                        columns: x => new { x.parm_ID, x.com_id, x.sub_ID },
                        principalTable: "CoM_Param",
                        principalColumns: new[] { "param_ID", "com_id", "sub_Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sat_Station",
                columns: table => new
                {
                    Sat_Sta_ID = table.Column<int>(type: "int", nullable: false),
                    Sat_ID = table.Column<int>(type: "int", nullable: true),
                    Station_ID = table.Column<int>(type: "int", nullable: true),
                    SatelliteSat_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sat_Station", x => x.Sat_Sta_ID);
                    table.ForeignKey(
                        name: "FK_Sat_Station_Satellite_SatelliteSat_ID",
                        column: x => x.SatelliteSat_ID,
                        principalTable: "Satellite",
                        principalColumn: "Sat_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sat_Station_Station_Station_ID",
                        column: x => x.Station_ID,
                        principalTable: "Station",
                        principalColumn: "Nat_ID");
                });

            migrationBuilder.CreateTable(
                name: "Subsystem",
                columns: table => new
                {
                    Sub_ID = table.Column<int>(type: "int", nullable: false),
                    Sat_ID = table.Column<int>(type: "int", nullable: true),
                    Sub_name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Sub_type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    SatelliteSat_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsystem", x => x.Sub_ID);
                    table.ForeignKey(
                        name: "FK_Subsystem_Satellite_SatelliteSat_ID",
                        column: x => x.SatelliteSat_ID,
                        principalTable: "Satellite",
                        principalColumn: "Sat_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoM_Param_Commandsub_ID",
                table: "CoM_Param",
                column: "Commandsub_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CoM_Param_param_TB_typeparam_ID",
                table: "CoM_Param",
                column: "param_TB_typeparam_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Param_Value_parm_ID_com_id_sub_ID",
                table: "Param_Value",
                columns: new[] { "parm_ID", "com_id", "sub_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_Sat_Station_SatelliteSat_ID",
                table: "Sat_Station",
                column: "SatelliteSat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sat_Station_Station_ID",
                table: "Sat_Station",
                column: "Station_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Satellite_StationNat_ID",
                table: "Satellite",
                column: "StationNat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subsystem_SatelliteSat_ID",
                table: "Subsystem",
                column: "SatelliteSat_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Param_Value");

            migrationBuilder.DropTable(
                name: "Sat_Station");

            migrationBuilder.DropTable(
                name: "Subsystem");

            migrationBuilder.DropTable(
                name: "CoM_Param");

            migrationBuilder.DropTable(
                name: "Satellite");

            migrationBuilder.DropTable(
                name: "Command");

            migrationBuilder.DropTable(
                name: "param_TB_type");

            migrationBuilder.DropTable(
                name: "Station");
        }
    }
}
