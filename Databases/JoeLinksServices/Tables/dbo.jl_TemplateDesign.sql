CREATE TABLE [dbo].[jl_TemplateDesign]
(
[TemplateId] [bigint] NOT NULL IDENTITY(1, 1),
[Template] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TemplateType] [int] NOT NULL,
[Icon] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_TemplateDesign] ADD CONSTRAINT [PK_jl_TemplateDesign] PRIMARY KEY CLUSTERED  ([TemplateId]) ON [PRIMARY]
GO
