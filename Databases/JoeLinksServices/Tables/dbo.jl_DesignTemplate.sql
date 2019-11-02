CREATE TABLE [dbo].[jl_DesignTemplate]
(
[DesignTemplateId] [int] NOT NULL IDENTITY(1, 1),
[Template] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TemplateType] [int] NOT NULL,
[Icon] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DivId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_DesignTemplate] ADD CONSTRAINT [PK_jl_DesignTemplate] PRIMARY KEY CLUSTERED  ([DesignTemplateId]) ON [PRIMARY]
GO
