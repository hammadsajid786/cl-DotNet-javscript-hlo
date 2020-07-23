CREATE TABLE [dbo].[Vehicle_Profile_Master]
(
[ID] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Name] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Created_By] [int] NULL,
[Vehicle_Profile_Created_date] [datetime] NULL,
[IsPending] [bit] NULL,
[IsActive] [bit] NULL,
[Vehicle_Profile_Make] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Model] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Year] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Type] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_SubType] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Body] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsFindersFeePayable] [bit] NULL,
[IsFindersFeePaid] [bit] NULL,
[Vehicle_Profile_Picture] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Release_Date] [date] NULL,
[Vehicle_Profile_Fuel_Consumption_Combined] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Fuel_Consumption_Highway] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Fuel_Consumption_City] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
