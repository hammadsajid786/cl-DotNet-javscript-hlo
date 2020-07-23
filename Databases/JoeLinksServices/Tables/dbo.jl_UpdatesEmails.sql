CREATE TABLE [dbo].[jl_UpdatesEmails]
(
[UpdatesEmailId] [int] NOT NULL IDENTITY(1, 1),
[UpdatesEmail] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedDateTime] [datetime] NOT NULL,
[Active] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_UpdatesEmails] ADD CONSTRAINT [PK_jl_UpdateEmails] PRIMARY KEY CLUSTERED  ([UpdatesEmailId]) ON [PRIMARY]
GO
