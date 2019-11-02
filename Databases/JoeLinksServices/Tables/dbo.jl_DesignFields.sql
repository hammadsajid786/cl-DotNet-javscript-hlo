CREATE TABLE [dbo].[jl_DesignFields]
(
[DesignFieldsId] [int] NOT NULL IDENTITY(1, 1),
[FieldKey] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FieldValue] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [bit] NULL,
[DesignId] [int] NOT NULL,
[FieldOrder] [int] NULL,
[FieldPosition] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FieldCSSClass] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FieldLayout] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_DesignFields] ADD CONSTRAINT [PK_jl_DesignFields] PRIMARY KEY CLUSTERED  ([DesignFieldsId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_DesignFields_jl_Desgins] ON [dbo].[jl_DesignFields] ([DesignId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_DesignFields] ADD CONSTRAINT [FK_jl_DesignFields_jl_Desgins] FOREIGN KEY ([DesignId]) REFERENCES [dbo].[jl_Desgins] ([DesignId])
GO
