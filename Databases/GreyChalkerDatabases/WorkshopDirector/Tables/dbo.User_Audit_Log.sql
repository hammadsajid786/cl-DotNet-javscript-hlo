CREATE TABLE [dbo].[User_Audit_Log]
(
[ID] [int] NULL,
[Field_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_From] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_To] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_Time] [datetime] NULL,
[Table_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
