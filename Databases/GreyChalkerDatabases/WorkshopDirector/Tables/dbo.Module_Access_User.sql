CREATE TABLE [dbo].[Module_Access_User]
(
[Id] [int] NOT NULL,
[ModuleId] [int] NULL,
[UserId] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModulePermissionId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Module_Access_User] ADD CONSTRAINT [PK_Module_Access_User] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
