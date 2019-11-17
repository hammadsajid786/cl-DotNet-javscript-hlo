CREATE TABLE [dbo].[lrmis_web_functions]
(
[function_id] [int] NOT NULL,
[function_name] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[function_description] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[function_code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_functions] ADD CONSTRAINT [PK_lrmis_web_functions] PRIMARY KEY CLUSTERED  ([function_id]) ON [PRIMARY]
GO
