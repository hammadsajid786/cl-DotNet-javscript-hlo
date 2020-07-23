CREATE TABLE [dbo].[Vehicle_Profile_Engine]
(
[ID] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Profile_Engine_Available] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsTurbo] [bit] NULL,
[IsElectric] [bit] NULL,
[IsHybrid] [bit] NULL,
[IsDiesel] [bit] NULL,
[HP] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GCM] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GVM] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Transmission_Mode] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Transmission_Gears] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AdBlue] [bit] NULL,
[Euro] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
