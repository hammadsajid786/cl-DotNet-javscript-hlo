CREATE TABLE [dbo].[User_Credential_AuthToken]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[User_Credential_Id] [int] NULL,
[AuthToken] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DeviceCode] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Platform] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsActive] [bit] NULL,
[CreatedOn] [datetime] NULL,
[LastUpdateOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Credential_AuthToken] ADD CONSTRAINT [PK_User_Credential_AuthToken] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Credential_AuthToken] ADD CONSTRAINT [FK_User_Credential_AuthToken_User_Credential1] FOREIGN KEY ([User_Credential_Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
