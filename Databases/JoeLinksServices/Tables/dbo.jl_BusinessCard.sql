CREATE TABLE [dbo].[jl_BusinessCard]
(
[UserDesignId] [int] NOT NULL IDENTITY(1, 1),
[Logo] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BackgroundColor] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BackgroundImage] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignType] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CompanyName] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CompanyMessage] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Web] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FullName] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Job] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address1] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address2] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[email] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignTemplateId] [int] NULL,
[DynamicFieldA] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DynamicFieldB] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DynamicFieldC] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fax] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Id] [bigint] NULL,
[CardPdfImage] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CardName] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessCard] ADD CONSTRAINT [PK_jl_BusinessCard] PRIMARY KEY CLUSTERED  ([UserDesignId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_BusinessCard_jl_Users] ON [dbo].[jl_BusinessCard] ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessCard] ADD CONSTRAINT [FK_jl_BusinessCard_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
