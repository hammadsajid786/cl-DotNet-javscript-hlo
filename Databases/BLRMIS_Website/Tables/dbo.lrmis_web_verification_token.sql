CREATE TABLE [dbo].[lrmis_web_verification_token]
(
[verification_id] [int] NOT NULL IDENTITY(1, 1),
[verification_code] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[email_address] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[created_date] [datetime] NULL CONSTRAINT [DF_lrmis_web_verification_token_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NULL,
[Expired] [bit] NULL CONSTRAINT [DF__lrmis_web__Expir__778AC167] DEFAULT ((0)),
[phone_number] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Consumed] [bit] NULL CONSTRAINT [DF__lrmis_web__Consu__787EE5A0] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_verification_token] ADD CONSTRAINT [PK_lrmis_web_verification_token] PRIMARY KEY CLUSTERED  ([verification_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_verification_token] ADD CONSTRAINT [FK_lrmis_web_verification_token_lrmis_web_user] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_verification_token] ADD CONSTRAINT [FK_lrmis_web_verification_token_lrmis_web_user1] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
