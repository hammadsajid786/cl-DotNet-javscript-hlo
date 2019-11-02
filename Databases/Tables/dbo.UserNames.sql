CREATE TABLE [dbo].[UserNames]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserNames] ADD CONSTRAINT [PK_UserNames] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
