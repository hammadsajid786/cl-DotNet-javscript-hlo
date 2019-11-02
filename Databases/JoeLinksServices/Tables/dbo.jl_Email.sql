CREATE TABLE [dbo].[jl_Email]
(
[EmailId] [int] NOT NULL IDENTITY(1, 1),
[To] [nvarchar] (75) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Product] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ZipCode1] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address1] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address2] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ZipCode2] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Telephone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fax] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Website] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Brochure] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vcard] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Linkedin] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Youtube] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JoinUs] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BootNo] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Date] [datetime] NULL,
[Address] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Message] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FontSize] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BackgroundColor] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LogobackgroundColor] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LogoText] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Id] [bigint] NOT NULL,
[Subject] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CompanyName] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Pobox] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Mobile] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Twitter] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Facebook] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignImage] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailLogo] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailAlphabet] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailAlphaColor] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Email] ADD CONSTRAINT [PK_jl_Email] PRIMARY KEY CLUSTERED  ([EmailId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_Email_jl_Users] ON [dbo].[jl_Email] ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Email] ADD CONSTRAINT [FK_jl_Email_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
