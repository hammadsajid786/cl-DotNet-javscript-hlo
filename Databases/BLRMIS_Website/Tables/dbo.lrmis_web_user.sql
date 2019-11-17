CREATE TABLE [dbo].[lrmis_web_user]
(
[user_id] [int] NOT NULL IDENTITY(1, 1),
[first_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[last_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[father_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[cnic] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[email_address] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[phone_number] [nvarchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[user_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[password] [nvarchar] (40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[location_id] [int] NOT NULL,
[role_id] [int] NOT NULL,
[active] [bit] NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_user_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL,
[department_id] [int] NOT NULL,
[designation_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [PK_lrmis_web_user] PRIMARY KEY CLUSTERED  ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [IX_lrmis_web_user] UNIQUE NONCLUSTERED  ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [FK_lrmis_web_user_lrmis_web_department] FOREIGN KEY ([department_id]) REFERENCES [dbo].[lrmis_web_department] ([department_id])
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [FK_lrmis_web_user_lrmis_web_designation] FOREIGN KEY ([designation_id]) REFERENCES [dbo].[lrmis_web_designation] ([designation_id])
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [FK_lrmis_web_user_lrmis_web_location] FOREIGN KEY ([location_id]) REFERENCES [dbo].[lrmis_web_location] ([location_id])
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [FK_lrmis_web_user_lrmis_web_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[lrmis_web_role] ([role_id])
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [FK_lrmis_web_user_lrmis_web_user] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_user] ADD CONSTRAINT [FK_lrmis_web_user_lrmis_web_user1] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
