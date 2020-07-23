CREATE TABLE [dbo].[Company_Preference_Audit]
(
[id] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[userID] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Setting] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_From] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Change_To] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Date] [datetime] NULL
) ON [PRIMARY]
GO
