CREATE TABLE [dbo].[Module_Access_Audit]
(
[id] [int] NULL,
[userid] [int] NULL,
[RequestingUser] [int] NULL,
[Affected_User] [int] NULL,
[Change_Permission_From] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_Permission_To] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[datetime] [datetime] NULL,
[Module_affected] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
