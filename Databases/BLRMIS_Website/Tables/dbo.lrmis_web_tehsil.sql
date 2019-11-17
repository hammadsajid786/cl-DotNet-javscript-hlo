CREATE TABLE [dbo].[lrmis_web_tehsil]
(
[tehsil_id] [int] NOT NULL,
[tehsil_name] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[active] [bit] NOT NULL,
[district_id] [int] NOT NULL,
[created_date] [datetime] NOT NULL,
[modified_date] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_tehsil] ADD CONSTRAINT [PK_lrmis_web_tehsil] PRIMARY KEY CLUSTERED  ([tehsil_id]) ON [PRIMARY]
GO
