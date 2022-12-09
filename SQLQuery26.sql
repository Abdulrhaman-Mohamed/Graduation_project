INSERT INTO [FlightControlCenter1].[dbo].[Stations]
( Id,StationName,StationType,Longitude,Latitude ) 
SELECT   Nat_ID ,Station_name,Station_Type,Longitude,Latitude
FROM [egsa_final].[dbo].[Station]

INSERT INTO [FlightControlCenter1].[dbo].[Satellites]
( Id,Mass,Date,SatelliteType,OrbitType,Name )
SELECT   Sat_ID,Mass,Launch_date,Sat_type,Orbit_Type,Sat_name
FROM [egsa_final].[dbo].[Satellite]

INSERT INTO [FlightControlCenter1].[dbo].[SatelliteStation]
( SatellitesId,StationsId ) 
SELECT   Sat_ID ,Station_ID
FROM [egsa_final].[dbo].[Sat_Station]

INSERT INTO [FlightControlCenter1].[dbo].[Subsystems]
( SatelliteId,SubSystemType,SubSystemName,Id )   
SELECT  Sat_ID ,Sub_type,Sub_name,Sub_ID  
FROM [egsa_final].[dbo].[Subsystem]

INSERT INTO [FlightControlCenter1].[dbo].[Commands]
( Id,Description,SubSystemId,SensorName )   
SELECT com_id ,com_description,sub_ID,sensor_name  
FROM [egsa_final].[dbo].[Command]

INSERT INTO [FlightControlCenter1].[dbo].[ParamTypes]
( Id,Type)  
SELECT   param_ID,param_type 
FROM [egsa_final].[dbo].[param_TB_type]

INSERT INTO [FlightControlCenter1].[dbo].[CommandParams]
(Id,CommandId,SubSystemId,ParamTypeId )  
SELECT   param_ID ,com_id,sub_Id,param_type  
FROM [egsa_final].[dbo].[CoM_Param]

INSERT INTO [FlightControlCenter1].[dbo].[ParamValues]
 (CommandID,SubSystemID,CommandParamID,Device,Description)  
SELECT   sub_ID,com_id,parm_ID,device,description 
FROM [egsa_final].[dbo].[Param_Value]

//INSERT INTO [FlightControlCenter1].[dbo].[Plans]
//( SequenceNumber,Delay,Repeat,AcknowledgeId,EX_Time,SubSystemId,commandID )
//SELECT   squn_command,delay,repeat,ack_id,EX_time,sub_ID,com_ID
//FROM [egsa_final].[dbo].[plan_]

//INSERT INTO [FlightControlCenter1].[dbo].[PlanResults]
//( Id,PlanSequenceNumber,PlanId,Time,Result)  
//SELECT  plan_result_id ,sequance_id,plan_id,time_value,value_result 
//FROM [egsa_final].[dbo].[plan_result]

//INSERT INTO [FlightControlCenter1].[dbo].[Acknowledges]
//( Id,AckNum,AckDescription)   
//SELECT  ack_id,ack_num,ack_dec
//FROM [egsa_final].[dbo].[ACK]

SET IDENTITY_INSERT PlanResults ON