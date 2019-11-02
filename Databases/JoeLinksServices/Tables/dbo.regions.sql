CREATE TABLE [dbo].[regions]
(
[region_id] [int] NOT NULL IDENTITY(1, 1),
[region_name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[country_id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[regions] ADD CONSTRAINT [PK_regions] PRIMARY KEY CLUSTERED  ([region_id]) ON [PRIMARY]
GO
