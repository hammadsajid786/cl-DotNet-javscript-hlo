CREATE TABLE [dbo].[User_Preferences]
(
[Id] [int] NOT NULL,
[BackgroundImagePath] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Preferences] ADD CONSTRAINT [PK_User_Preferences] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Preferences] ADD CONSTRAINT [FK_User_Preferences_User_Credential] FOREIGN KEY ([Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
