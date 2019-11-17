CREATE TABLE [dbo].[lrmis_web_mauza]
(
[mauza_id] [int] NOT NULL,
[mauza_name] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[active] [bit] NOT NULL,
[tehsil_id] [int] NOT NULL,
[created_date] [datetime] NOT NULL,
[modified_date] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_mauza] ADD CONSTRAINT [PK_lrmis_web_mauza] PRIMARY KEY CLUSTERED  ([mauza_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_mauza] ADD CONSTRAINT [FK_lrmis_web_mauza_lrmis_web_tehsil] FOREIGN KEY ([tehsil_id]) REFERENCES [dbo].[lrmis_web_tehsil] ([tehsil_id])
GO
