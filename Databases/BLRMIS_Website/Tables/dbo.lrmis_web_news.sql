CREATE TABLE [dbo].[lrmis_web_news]
(
[news_id] [int] NOT NULL IDENTITY(1, 1),
[news_title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[news_date] [datetime] NOT NULL,
[news_description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_news_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_news] ADD CONSTRAINT [PK_lrmis_web_news] PRIMARY KEY CLUSTERED  ([news_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_news] ADD CONSTRAINT [FK_lrmis_web_news_lrmis_web_user] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_news] ADD CONSTRAINT [FK_lrmis_web_news_lrmis_web_user1] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
