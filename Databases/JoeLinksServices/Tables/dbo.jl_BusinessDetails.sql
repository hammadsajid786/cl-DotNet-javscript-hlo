CREATE TABLE [dbo].[jl_BusinessDetails]
(
[BusinessDetailId] [int] NOT NULL IDENTITY(1, 1),
[BusinessName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BusinessWebUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LandLineNumber] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MobileNumber] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fax] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessDescription] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FacebookLink] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[YoutubeLink] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TwitterLink] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GoogleLink] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InstagramLink] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PinterestLink] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserId] [bigint] NOT NULL,
[BusinessLogoPath] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessHoursJson] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PlaceId] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessDetails] ADD CONSTRAINT [PK__jl_Busin__9F72EFE2CAA0779A] PRIMARY KEY CLUSTERED  ([BusinessDetailId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_BusinessDetails] ADD CONSTRAINT [FK_BusinessDetail_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
