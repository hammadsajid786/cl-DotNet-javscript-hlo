CREATE TABLE [dbo].[User_Credential]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Password] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_Account_Locked_Retries] [bit] NULL,
[User_Account_Locked_Admin] [bit] NULL,
[LoginAttempts] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Captcha_Triggered] [bit] NULL,
[User_Account_Create_Date] [datetime] NULL,
[MobilePhone] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Credential] ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Credential] ADD CONSTRAINT [FK_SMS_History_User_Credential] FOREIGN KEY ([Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
ALTER TABLE [dbo].[User_Credential] ADD CONSTRAINT [FK_User_Credential_AuthToken_User_Credential] FOREIGN KEY ([Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
ALTER TABLE [dbo].[User_Credential] ADD CONSTRAINT [FK_User_Data_User_Credential] FOREIGN KEY ([Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
