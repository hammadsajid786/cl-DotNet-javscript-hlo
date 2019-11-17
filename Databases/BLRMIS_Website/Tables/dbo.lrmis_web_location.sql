CREATE TABLE [dbo].[lrmis_web_location]
(
[location_id] [int] NOT NULL,
[location_name] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[digitization_progress_percentage] [int] NOT NULL,
[active] [bit] NOT NULL,
[created_date] [datetime] NOT NULL,
[modified_date] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_location] ADD CONSTRAINT [PK_LRMIS_Web_Location] PRIMARY KEY CLUSTERED  ([location_id]) ON [PRIMARY]
GO
