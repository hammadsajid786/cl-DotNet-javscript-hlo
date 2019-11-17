CREATE TABLE [dbo].[lrmis_web_department]
(
[department_id] [int] NOT NULL IDENTITY(1, 1),
[department_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_department_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL,
[active] [bit] NOT NULL,
[department_description] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_department] ADD CONSTRAINT [PK_lrmis_web_department] PRIMARY KEY CLUSTERED  ([department_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_department] ADD CONSTRAINT [FK_lrmis_web_department_lrmis_web_user] FOREIGN KEY ([created_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_department] ADD CONSTRAINT [FK_lrmis_web_department_lrmis_web_user1] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
