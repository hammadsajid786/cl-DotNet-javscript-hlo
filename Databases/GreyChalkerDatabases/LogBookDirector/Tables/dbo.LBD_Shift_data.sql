CREATE TABLE [dbo].[LBD_Shift_data]
(
[id] [int] NOT NULL,
[Time] [time] NULL,
[Location_GPS] [sys].[geography] NULL,
[Location_DD] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_ID] [int] NULL,
[Odometer] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Event] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedOn] [datetime] NULL,
[IsTwoUpDriver] [bit] NULL,
[IsWork] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LBD_Shift_data] ADD CONSTRAINT [PK_LBD_Shift_data] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
