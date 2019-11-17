CREATE TABLE [dbo].[lrmis_web_fee_schedule]
(
[fee_schedule_id] [int] NOT NULL IDENTITY(1, 1),
[location_id] [int] NOT NULL,
[attachment_name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[attachment_path] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[fee_type_id] [int] NOT NULL,
[original_fileName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[mimetype] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[filesize] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[created_date] [datetime] NOT NULL,
[created_by] [int] NOT NULL,
[modified_date] [datetime] NULL,
[modified_by] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_fee_schedule] ADD CONSTRAINT [PK_lrmis_web_fee_schedule] PRIMARY KEY CLUSTERED  ([fee_schedule_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_fee_schedule] ADD CONSTRAINT [FK_lrmis_web_fee_schedule_lrmis_web_fee_schedule] FOREIGN KEY ([location_id]) REFERENCES [dbo].[lrmis_web_location] ([location_id])
GO
ALTER TABLE [dbo].[lrmis_web_fee_schedule] ADD CONSTRAINT [FK_lrmis_web_fee_schedule_lrmis_web_feetype] FOREIGN KEY ([fee_type_id]) REFERENCES [dbo].[lrmis_web_feetype] ([fee_type_id])
GO
ALTER TABLE [dbo].[lrmis_web_fee_schedule] ADD CONSTRAINT [FK_lrmis_web_fee_schedule_lrmis_web_user] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_fee_schedule] ADD CONSTRAINT [FK_lrmis_web_fee_schedule_lrmis_web_user1] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
