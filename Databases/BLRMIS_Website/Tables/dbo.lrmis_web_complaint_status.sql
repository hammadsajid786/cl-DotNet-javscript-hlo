CREATE TABLE [dbo].[lrmis_web_complaint_status]
(
[complaint_status_id] [int] NOT NULL,
[complaint_status] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[complaint_status_code] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_complaint_status] ADD CONSTRAINT [PK_lrmis_web_complaint_status] PRIMARY KEY CLUSTERED  ([complaint_status_id]) ON [PRIMARY]
GO
