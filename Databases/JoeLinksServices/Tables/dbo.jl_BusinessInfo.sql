CREATE TABLE [dbo].[jl_BusinessInfo]
(
[BusinessInfoId] [int] NOT NULL IDENTITY(1, 1),
[BusinessName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BusinessAddress] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BusinessNumber] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BusinessFax] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BusinessEmail] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BusinessDetails] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Monday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Tuesday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Wednesday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Thursday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Friday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Saturday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Sunday] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessLogo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BannerLogo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Id] [bigint] NOT NULL,
[FbLink] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TwitterLink] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[YoutubeLink] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GoogleLinks] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FindUsLink] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InstagramLink] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PinterestLink] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessID] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_jl_BusinessInfo_BusinessID] DEFAULT ((1)),
[PakageId] [int] NULL,
[RegionId] [int] NULL,
[CountyId] [int] NULL,
[CityId] [int] NULL,
[StoreLogo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[zipCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountryId] [int] NULL,
[IsRegistered] [bit] NOT NULL CONSTRAINT [DF_jl_BusinessInfo_IsRegistered] DEFAULT ((1)),
[BusinessWebUrl] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LandLineNumber] [nvarchar] (400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MobileNumber] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessHoursJson] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IndustryType] [int] NULL,
[SmsEnabled] [bit] NULL,
[IsBusinessVerified] [bit] NULL,
[SmsEnabled2] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessInfo] ADD CONSTRAINT [PK_jl_BusinessInfo] PRIMARY KEY CLUSTERED  ([BusinessInfoId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_BusinessInfo_jl_Countries] ON [dbo].[jl_BusinessInfo] ([CountryId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_BusinessInfo_jl_Users] ON [dbo].[jl_BusinessInfo] ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_BusinessInfo_jl_Pakages] ON [dbo].[jl_BusinessInfo] ([PakageId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessInfo] ADD CONSTRAINT [FK_jl_BusinessInfo_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[jl_BusinessInfo] ADD CONSTRAINT [FK_jl_BusinessInfo_jl_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[jl_Countries] ([CountryID])
GO
ALTER TABLE [dbo].[jl_BusinessInfo] ADD CONSTRAINT [FK_jl_BusinessInfo_jl_Pakages] FOREIGN KEY ([PakageId]) REFERENCES [dbo].[jl_Pakages] ([PkgId])
GO
