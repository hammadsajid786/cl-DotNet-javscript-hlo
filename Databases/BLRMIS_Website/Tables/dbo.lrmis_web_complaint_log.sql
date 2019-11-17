CREATE TABLE [dbo].[lrmis_web_complaint_log]
(
[complaint_comment_id] [int] NOT NULL IDENTITY(1, 1),
[complaint_id] [int] NOT NULL,
[complaint_status_id] [int] NOT NULL,
[complaint_assign_to] [int] NULL,
[complaint_assign_by] [int] NULL,
[complaint_comments] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[user_opinion] [int] NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_complaint_log_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [PK_lrmis_web_complaint_log] PRIMARY KEY CLUSTERED  ([complaint_comment_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [FK_lrmis_web_complaint_log_lrmis_web_complaint] FOREIGN KEY ([complaint_id]) REFERENCES [dbo].[lrmis_web_complaint] ([complaint_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [FK_lrmis_web_complaint_log_lrmis_web_complaint_status] FOREIGN KEY ([complaint_status_id]) REFERENCES [dbo].[lrmis_web_complaint_status] ([complaint_status_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [FK_lrmis_web_complaint_log_lrmis_web_user] FOREIGN KEY ([complaint_assign_to]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [FK_lrmis_web_complaint_log_lrmis_web_user1] FOREIGN KEY ([complaint_assign_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [FK_lrmis_web_complaint_log_lrmis_web_user2] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint_log] ADD CONSTRAINT [FK_lrmis_web_complaint_log_lrmis_web_user3] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
