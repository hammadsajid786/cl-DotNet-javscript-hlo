CREATE TABLE [dbo].[lrmis_web_function_role_mapping]
(
[mapping_id] [int] NOT NULL IDENTITY(1, 1),
[function_id] [int] NOT NULL,
[role_id] [int] NOT NULL,
[include] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_function_role_mapping] ADD CONSTRAINT [PK_lrmis_web_function_role_mapping] PRIMARY KEY CLUSTERED  ([mapping_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_function_role_mapping] ADD CONSTRAINT [FK_lrmis_web_function_role_mapping_lrmis_web_functions] FOREIGN KEY ([function_id]) REFERENCES [dbo].[lrmis_web_functions] ([function_id])
GO
ALTER TABLE [dbo].[lrmis_web_function_role_mapping] ADD CONSTRAINT [FK_lrmis_web_function_role_mapping_lrmis_web_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[lrmis_web_role] ([role_id])
GO
