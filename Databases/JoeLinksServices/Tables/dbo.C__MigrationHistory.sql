CREATE TABLE [dbo].[C__MigrationHistory]
(
[MigrationId] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ContextKey] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Model] [varbinary] (max) NOT NULL,
[ProductVersion] [nvarchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[C__MigrationHistory] ADD CONSTRAINT [PK_C__MigrationHistory] PRIMARY KEY CLUSTERED  ([MigrationId], [ContextKey]) ON [PRIMARY]
GO
