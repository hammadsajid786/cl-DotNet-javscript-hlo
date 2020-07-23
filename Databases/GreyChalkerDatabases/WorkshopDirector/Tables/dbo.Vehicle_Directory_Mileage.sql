CREATE TABLE [dbo].[Vehicle_Directory_Mileage]
(
[id] [int] NULL,
[Vehicle_ID] [int] NULL,
[userid] [int] NULL,
[Mileage] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Mileeage_source] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MIleage_datetime] [datetime] NULL,
[Is_invalid] [bit] NULL
) ON [PRIMARY]
GO
