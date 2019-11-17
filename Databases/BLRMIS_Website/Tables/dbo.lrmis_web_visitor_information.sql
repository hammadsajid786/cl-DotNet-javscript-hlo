CREATE TABLE [dbo].[lrmis_web_visitor_information]
(
[visitor_id] [int] NOT NULL IDENTITY(1, 1),
[machine_name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ip_address] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[user_agent] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[created_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_visitor_information] ADD CONSTRAINT [PK_lrmis_web_visitor_information] PRIMARY KEY CLUSTERED  ([visitor_id]) ON [PRIMARY]
GO
