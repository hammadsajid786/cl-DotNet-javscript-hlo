CREATE TABLE [dbo].[User_Modules]
(
[Id] [int] NOT NULL,
[UserId] [int] NULL,
[ModuleId] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Modules] ADD CONSTRAINT [PK_User_Modules] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
