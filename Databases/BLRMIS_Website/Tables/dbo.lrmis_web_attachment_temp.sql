CREATE TABLE [dbo].[lrmis_web_attachment_temp]
(
[attachment_id] [int] NOT NULL IDENTITY(1, 1),
[attachment_path] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[attachment_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[source_type] [int] NULL,
[source_id] [int] NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_attachment_temp_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NULL,
[original_fileName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[mimetype] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[filesize] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_attachment_temp] ADD CONSTRAINT [PK_lrmis_web_attachment_temp] PRIMARY KEY CLUSTERED  ([attachment_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_attachment_temp] ADD CONSTRAINT [FK_lrmis_web_attachment_temp_lrmis_web_user] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_attachment_temp] ADD CONSTRAINT [FK_lrmis_web_attachment_temp_lrmis_web_user1] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
