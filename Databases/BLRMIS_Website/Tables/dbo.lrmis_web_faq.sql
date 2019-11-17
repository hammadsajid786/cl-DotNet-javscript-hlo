CREATE TABLE [dbo].[lrmis_web_faq]
(
[faq_id] [int] NOT NULL IDENTITY(1, 1),
[faq_description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_faq_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL,
[faq_title] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_faq] ADD CONSTRAINT [PK_lrmis_web_faq] PRIMARY KEY CLUSTERED  ([faq_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_faq] ADD CONSTRAINT [FK_lrmis_web_faq_lrmis_web_user] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_faq] ADD CONSTRAINT [FK_lrmis_web_faq_lrmis_web_user1] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
