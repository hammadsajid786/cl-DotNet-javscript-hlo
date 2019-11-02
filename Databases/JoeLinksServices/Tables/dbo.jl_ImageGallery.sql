CREATE TABLE [dbo].[jl_ImageGallery]
(
[ImageId] [int] NOT NULL IDENTITY(1, 1),
[ImagePath] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BusinessInfoId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_ImageGallery] ADD CONSTRAINT [PK_jl_ImageGallery] PRIMARY KEY CLUSTERED  ([ImageId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_ImageGallery_jl_BusinessInfo] ON [dbo].[jl_ImageGallery] ([BusinessInfoId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_ImageGallery] ADD CONSTRAINT [FK_jl_ImageGallery_jl_BusinessInfo] FOREIGN KEY ([BusinessInfoId]) REFERENCES [dbo].[jl_BusinessInfo] ([BusinessInfoId])
GO
