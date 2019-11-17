CREATE TABLE [dbo].[lrmis_web_complaint]
(
[complaint_id] [int] NOT NULL IDENTITY(1, 1),
[complaint_title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[complaint_description] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[citizen_name] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[citizen_phone_number] [nvarchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[citizen_email_address] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[citizen_cnic] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[location_id] [int] NOT NULL,
[complaint_category_id] [int] NOT NULL,
[complaint_status_id] [int] NOT NULL,
[created_date] [datetime] NOT NULL CONSTRAINT [DF_lrmis_web_complaint_created_date] DEFAULT (getdate()),
[modified_date] [datetime] NULL,
[modified_by] [int] NULL,
[created_by] [int] NOT NULL,
[is_locked] [bit] NULL,
[locked_by] [int] NULL,
[complaint_code] [nvarchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[complaint_access_token] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__lrmis_web__compl__6D0D32F4] DEFAULT (replace(newid(),'-','')),
[function_id] [int] NULL,
[complaint_assign_to] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_complaint] ADD CONSTRAINT [PK_lrmis_web_complaint] PRIMARY KEY CLUSTERED  ([complaint_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_complaint] ADD CONSTRAINT [FK_lrmis_web_complaint_lrmis_web_category] FOREIGN KEY ([complaint_category_id]) REFERENCES [dbo].[lrmis_web_category] ([category_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint] ADD CONSTRAINT [FK_lrmis_web_complaint_lrmis_web_complaint_status] FOREIGN KEY ([complaint_status_id]) REFERENCES [dbo].[lrmis_web_complaint_status] ([complaint_status_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint] WITH NOCHECK ADD CONSTRAINT [FK_lrmis_web_complaint_lrmis_web_functions] FOREIGN KEY ([function_id]) REFERENCES [dbo].[lrmis_web_functions] ([function_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint] ADD CONSTRAINT [FK_lrmis_web_complaint_lrmis_web_location] FOREIGN KEY ([location_id]) REFERENCES [dbo].[lrmis_web_location] ([location_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint] ADD CONSTRAINT [FK_lrmis_web_complaint_lrmis_web_user] FOREIGN KEY ([locked_by]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
ALTER TABLE [dbo].[lrmis_web_complaint] ADD CONSTRAINT [FK_lrmis_web_complaint_lrmis_web_user1] FOREIGN KEY ([complaint_assign_to]) REFERENCES [dbo].[lrmis_web_user] ([user_id])
GO
