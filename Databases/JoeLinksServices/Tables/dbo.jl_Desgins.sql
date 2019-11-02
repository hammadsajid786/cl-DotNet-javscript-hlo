CREATE TABLE [dbo].[jl_Desgins]
(
[DesignId] [int] NOT NULL IDENTITY(1, 1),
[DesignName] [varchar] (350) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DesignDescription] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DesignThumbnail] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DateCreated] [datetime] NOT NULL,
[Status] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Desgins] ADD CONSTRAINT [PK_jl_Desgins] PRIMARY KEY CLUSTERED  ([DesignId]) ON [PRIMARY]
GO
