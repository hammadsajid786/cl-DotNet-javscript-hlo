CREATE TABLE [dbo].[SMS_History]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[User_Credential_Id] [int] NULL,
[Ref_SMS_From_Module_Id] [smallint] NULL,
[SMS_date] [datetime] NULL,
[Send_or_Receive] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MobileNumber] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Message] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsViewed] [bit] NULL,
[Is_SMS_Billable] [bit] NULL,
[Is_SMS_Billed] [bit] NULL,
[Is_SMS_System_Generated] [bit] NULL,
[CreatedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SMS_History] ADD CONSTRAINT [PK_SMS_History] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SMS_History] ADD CONSTRAINT [FK_SMS_History_Ref_SMS_From_Module] FOREIGN KEY ([Ref_SMS_From_Module_Id]) REFERENCES [dbo].[Ref_SMS_From_Module] ([id])
GO
ALTER TABLE [dbo].[SMS_History] ADD CONSTRAINT [FK_SMS_History_User_Credential1] FOREIGN KEY ([User_Credential_Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
