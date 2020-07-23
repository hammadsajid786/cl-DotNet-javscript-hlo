CREATE TABLE [dbo].[Co_Customer_Audit_Log]
(
[id] [int] NULL,
[Field_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_from] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_to] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_id] [int] NULL,
[Table_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Timestamp] [datetime] NULL
) ON [PRIMARY]
GO
