CREATE TABLE [dbo].[Vehicle_Directory_Master]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[owner] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Rego] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fleet_Number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WSD_Fleet_Number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Type] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Sub_Type] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Make] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Model] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Series] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Year] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[VIN] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Colour] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Depot_Stored] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vehicle_Directory_Master] ADD CONSTRAINT [PK_Vehicle_Directory_Master] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
