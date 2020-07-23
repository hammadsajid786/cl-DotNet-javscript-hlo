CREATE TABLE [dbo].[Module_Master]
(
[Id] [int] NOT NULL,
[ModuleName] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Module_Master] ADD CONSTRAINT [PK_Module_Master] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
