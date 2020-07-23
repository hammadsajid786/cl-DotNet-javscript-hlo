CREATE TABLE [dbo].[Module_Permission]
(
[Id] [int] NOT NULL,
[ModuleId] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PermissionName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Module_Permission] ADD CONSTRAINT [PK_Module_Permission] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
