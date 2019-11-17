CREATE TABLE [dbo].[lrmis_web_sourcetype]
(
[source_id] [int] NOT NULL,
[source_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_sourcetype] ADD CONSTRAINT [PK_lrmis_web_sourcetype] PRIMARY KEY CLUSTERED  ([source_id]) ON [PRIMARY]
GO
