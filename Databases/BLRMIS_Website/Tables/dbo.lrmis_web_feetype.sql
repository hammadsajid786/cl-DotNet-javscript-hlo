CREATE TABLE [dbo].[lrmis_web_feetype]
(
[fee_type_id] [int] NOT NULL,
[type_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_feetype] ADD CONSTRAINT [PK_lrmis_web_feetype] PRIMARY KEY CLUSTERED  ([fee_type_id]) ON [PRIMARY]
GO
