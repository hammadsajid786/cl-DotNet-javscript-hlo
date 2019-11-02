CREATE TABLE [dbo].[jl_UserImages]
(
[UserImages] [int] NOT NULL IDENTITY(1, 1),
[ImagePath] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Id] [bigint] NOT NULL,
[ImageDescription] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [bit] NOT NULL,
[DateCreated] [datetime] NOT NULL,
[IsBackGround] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_UserImages] ADD CONSTRAINT [PK_jl_UserImages] PRIMARY KEY CLUSTERED  ([UserImages]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_UserImages_jl_Users] ON [dbo].[jl_UserImages] ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_UserImages] ADD CONSTRAINT [FK_jl_UserImages_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
