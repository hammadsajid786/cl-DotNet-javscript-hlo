CREATE TABLE [dbo].[jl_Pakages]
(
[PkgId] [int] NOT NULL IDENTITY(1, 1),
[FreeBusinessCard] [int] NULL,
[FreeBusinessLetter] [int] NULL,
[FreeResume] [int] NULL,
[FreeBusinessEmail] [int] NULL,
[FreeBusinessFax] [int] NULL,
[FreeBusinessServices] [int] NULL,
[SocialMediaLinks] [bit] NULL,
[BusinessLogo] [bit] NULL,
[BusinessBanner] [bit] NULL,
[BusinessWorkingHours] [bit] NULL,
[ImageGallery] [bit] NULL,
[PackageName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Pakages] ADD CONSTRAINT [PK_jl_Pakages] PRIMARY KEY CLUSTERED  ([PkgId]) ON [PRIMARY]
GO
