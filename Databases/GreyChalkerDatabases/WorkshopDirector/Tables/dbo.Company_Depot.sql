CREATE TABLE [dbo].[Company_Depot]
(
[CompanyID] [int] NULL,
[Depot_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_Unit] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_Number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_Address1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_Address2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_Suburb] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_PostCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Address_State] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Gatehouse_Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Security_Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Main_Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Despatch_Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Leading_Hand_Phone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_CB_Channel] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Gate_Code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Max_Vehicle_Access] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_HeavyVehicle_Enter_From] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
