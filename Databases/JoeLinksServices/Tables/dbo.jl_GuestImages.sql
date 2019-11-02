CREATE TABLE [dbo].[jl_GuestImages]
(
[GuestId] [uniqueidentifier] NOT NULL,
[GuestImagePath] [nvarchar] (550) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GuestImageId] [int] NOT NULL IDENTITY(1, 1)
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_GuestImages] ADD CONSTRAINT [PK_jl_GuestImages] PRIMARY KEY CLUSTERED  ([GuestImageId]) ON [PRIMARY]
GO
