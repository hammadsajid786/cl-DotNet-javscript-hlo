CREATE TABLE [dbo].[lrmis_web_role]
(
[role_id] [int] NOT NULL IDENTITY(1, 1),
[role_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[role_description] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[active] [bit] NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_role_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_role] ADD CONSTRAINT [PK_lrmis_web_role] PRIMARY KEY CLUSTERED  ([role_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_role] ADD CONSTRAINT [FK_lrmis_web_role_lrmis_web_user] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_role] ADD CONSTRAINT [FK_lrmis_web_role_lrmis_web_user1] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
