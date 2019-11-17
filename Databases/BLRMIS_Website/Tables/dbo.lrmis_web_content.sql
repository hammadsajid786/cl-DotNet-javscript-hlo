CREATE TABLE [dbo].[lrmis_web_content]
(
[content_id] [int] NOT NULL IDENTITY(1, 1),
[content_page_id] [int] NOT NULL,
[content_description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_content_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_content] ADD CONSTRAINT [PK_lrmis_web_content] PRIMARY KEY CLUSTERED  ([content_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_content] ADD CONSTRAINT [FK_lrmis_web_content_lrmis_web_page] FOREIGN KEY ([content_page_id]) REFERENCES [dbo].[lrmis_web_page] ([page_id])
GO
ALTER TABLE [dbo].[lrmis_web_content] ADD CONSTRAINT [FK_lrmis_web_content_lrmis_web_user] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_content] ADD CONSTRAINT [FK_lrmis_web_content_lrmis_web_user1] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
