CREATE TABLE [dbo].[User_Authenication_Audit]
(
[ID] [int] NULL,
[UserID] [int] NULL,
[Authentication_Method] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Authentication_Outcome] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[From_IP_Address] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
