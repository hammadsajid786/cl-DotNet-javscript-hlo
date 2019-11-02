CREATE TABLE [dbo].[jl_Fax]
(
[FaxId] [int] NOT NULL IDENTITY(1, 1),
[CompanyName] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MailingAddress] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ToName] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ToFaxNumber] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ToDate] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FromName] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FromFaxNumber] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FromDate] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HeadingFontSize] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalFontSize] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FaxbackgroundColor] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FaxDesign] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MessageBoxText] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignImagePath] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StreetAddress] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateName] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ZipCode] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Telephone] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WebAddress] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DateDesign2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ToRe] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ToCc] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FaxName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Id] [bigint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Fax] ADD CONSTRAINT [PK_jl_Fax] PRIMARY KEY CLUSTERED  ([FaxId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_Fax_jl_Users] ON [dbo].[jl_Fax] ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Fax] ADD CONSTRAINT [FK_jl_Fax_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
