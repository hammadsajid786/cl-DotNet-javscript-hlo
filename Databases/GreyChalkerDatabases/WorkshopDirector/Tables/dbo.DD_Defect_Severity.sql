CREATE TABLE [dbo].[DD_Defect_Severity]
(
[id] [int] NULL,
[Severity] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Short_Description] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Enabled] [bit] NULL
) ON [PRIMARY]
GO
