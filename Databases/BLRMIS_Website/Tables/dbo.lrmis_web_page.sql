CREATE TABLE [dbo].[lrmis_web_page]
(
[page_id] [int] NOT NULL,
[page_name] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_page] ADD CONSTRAINT [PK_lrmis_web_page] PRIMARY KEY CLUSTERED  ([page_id]) ON [PRIMARY]
GO
