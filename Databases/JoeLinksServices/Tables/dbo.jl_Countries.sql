CREATE TABLE [dbo].[jl_Countries]
(
[CountryID] [int] NOT NULL IDENTITY(1, 1),
[CountryName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Countries] ADD CONSTRAINT [PK_jl_Countries] PRIMARY KEY CLUSTERED  ([CountryID]) ON [PRIMARY]
GO
