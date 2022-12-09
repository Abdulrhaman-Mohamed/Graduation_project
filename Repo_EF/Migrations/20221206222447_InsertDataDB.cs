using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using Repo_Core.Models;
#nullable disable

namespace Repo_EF.Migrations
{
    public partial class InsertDataDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            SqlConnection conn = new SqlConnection("SecondConnection");
            SqlConnection con = new SqlConnection("DeafultConnection");

            SqlCommand station = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[Stations]" +
                "( Id,StationName,StationType,Longitude,Latitude ) SELECT   Nat_ID ,Station_name,Station_Type,Longitude,Latitude" +
                "FROM [egsa_final].[dbo].[Station]");
            SqlCommand satellite = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[Satellites]" +
                "( Id,Mass,Date,SatelliteType,OrbitType,Name )  SELECT   Sat_ID,Mass,Launch_date,Sat_type,Orbit_Type,Sat_name  " +
                "FROM [egsa_final].[dbo].[Satellite]");
            SqlCommand SatelliteStation = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[SatelliteStation]" +
                "( SatellitesId,StationsId ) SELECT   Sat_ID ,Station_ID " +
                "FROM [egsa_final].[dbo].[Sat_Station]");
            SqlCommand subsys = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[Subsystems]" +
                "( SatelliteId,SubSystemType,SubSystemName,Id )   SELECT  Sat_ID ,Sub_type,Sub_name,Sub_ID   " +
                "FROM [egsa_final].[dbo].[Subsystem]");
            SqlCommand commands = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[Commands]" +
                "( Id,Description,SubSystemId,SensorName )   SELECT com_id ,com_description,sub_ID,sensor_name  " +
                "FROM [egsa_final].[dbo].[Command]");
            SqlCommand paramTypes = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[ParamTypes]" +
                "( Id,Type)  SELECT   param_ID,param_type   " +
                "FROM [egsa_final].[dbo].[param_TB_type]");
            SqlCommand commandParams = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[CommandParams]" +
                "(Id,CommandId,SubSystemId,ParamTypeId )   SELECT   param_ID ,com_id,sub_Id,param_type   " +
                "FROM [egsa_final].[dbo].[CoM_Param]");
            SqlCommand paramValues = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[ParamValues]" +
                "( CommandID,SubSystemID,CommandParamID,Device,Description)   SELECT   sub_ID,com_id,parm_ID,device,description   " +
                "FROM [egsa_final].[dbo].[Param_Value]");
            SqlCommand plan = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[Plans]" +
                "( Id,SequenceNumber,Delay,AcknowledgeId,Repeat,EX_Time,AcknowledgeId,SubSystemId,commandID )  " +
                " SELECT   plan_ID ,squn_command,delay,ack_id,repeat,EX_time,sub_ID,com_ID)   " +
                "FROM [egsa_final].[dbo].[plan_]");
            SqlCommand plan_result = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[PlanResults]" +
                "( Id,PlanSequenceNumber,PlanId,Time,Result)   SELECT  plan_result_id ,sequance_id,plan_id,time_value,value_result  " +
                "FROM [egsa_final].[dbo].[plan_result]");
            SqlCommand ack = new SqlCommand(@"INSERT INTO [FlightControlCenter1].[dbo].[Acknowledges]" +
                "( Id,AckNum,AckDescription)   SELECT  ack_id,ack_num,ack_dec  " +
                "FROM [egsa_final].[dbo].[ACK]");

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=FlightControlCenter1;Integrated Security=True");

            SqlCommand station = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[Stations] ", con);
            SqlCommand satellite = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[Satellites] ", con);
            SqlCommand SatelliteStation = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[SatelliteStation] ", con);
            SqlCommand Subsystems = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[Subsystems] ", con);
            SqlCommand Commands = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[Commands] ", con);
            SqlCommand ParamTypes = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[ParamTypes] ", con);
            SqlCommand CommandParams = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[CommandParams] ", con  );
            SqlCommand ParamValues = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[ParamValues] ", con );
            SqlCommand Plans = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[Plans] ", con);
            SqlCommand PlanResults = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[PlanResults] ", con);
            SqlCommand Acknowledges = new SqlCommand(@"DELETE From [FlightControlCenter1].[dbo].[Acknowledges] ",con);

        }
    }
}

