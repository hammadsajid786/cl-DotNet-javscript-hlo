CREATE TABLE [dbo].[LBD_TC_Audit]
(
[id] [int] NULL,
[UDI] [int] NULL,
[Request_Initiated_By] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Request_Time] [datetime] NULL,
[TC_Provider] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TC_Transaction_Time] [time] NULL,
[TC_Request_Outcome] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Date_Range_From] [date] NULL,
[Date_Range_To] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
