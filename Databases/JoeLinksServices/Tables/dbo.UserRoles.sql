CREATE TABLE [dbo].[UserRoles]
(
[RoleId] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRoles] ADD CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED  ([RoleId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRoles] ADD CONSTRAINT [FK_UserRoles_UserRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([RoleId])
GO
