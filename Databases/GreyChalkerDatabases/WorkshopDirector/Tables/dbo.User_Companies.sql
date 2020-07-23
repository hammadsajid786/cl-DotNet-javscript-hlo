CREATE TABLE [dbo].[User_Companies]
(
[Id] [int] NOT NULL,
[UserId] [int] NOT NULL,
[CompanyId] [int] NOT NULL,
[PositionTitle] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Company_Access_Rights] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Companies] ADD CONSTRAINT [PK_User_Companies] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Defines associations between USERS and COMPANIES', 'SCHEMA', N'dbo', 'TABLE', N'User_Companies', NULL, NULL
GO
