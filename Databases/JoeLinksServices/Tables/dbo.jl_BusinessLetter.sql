CREATE TABLE [dbo].[jl_BusinessLetter]
(
[LetterPadeDesignId] [int] NOT NULL IDENTITY(1, 1),
[Logo] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BackgroundColor] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
[Paragraph1] [varchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Paragraph2] [varchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Paragraph3] [varchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FontSize] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address3] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fax] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignImage] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HeadingBoxBgColor] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Heading1FontSize] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Heading2FontSize] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Id] [bigint] NULL,
[ContactNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Designation] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LetterName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Salutation] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessLetter] ADD CONSTRAINT [PK_jl_BusinessLetter] PRIMARY KEY CLUSTERED  ([LetterPadeDesignId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_BusinessLetter_jl_Users] ON [dbo].[jl_BusinessLetter] ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessLetter] ADD CONSTRAINT [FK_jl_BusinessLetter_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
