CREATE TABLE [dbo].[Vehicle_Profile_LightsWindows]
(
[ID] [int] NULL,
[Is_Automatic_Headlights] [bit] NULL,
[Is_Headlights_SeeMeHome] [bit] NULL,
[Headlight_Type] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Tail_Light_Type] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Tail_Light_tinted] [bit] NULL,
[Is_Daytime_running_lights] [bit] NULL,
[Is_Foglights_front] [bit] NULL,
[Is_Foglights_rear] [bit] NULL,
[Is_PowerWindows_front] [bit] NULL,
[Is_PowerWindows_rear] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Rear_View_Mirror] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Wipers_Auto] [bit] NULL,
[Is_Rear_Wiper] [bit] NULL,
[Is_Rear_Wiper_Auto] [bit] NULL
) ON [PRIMARY]
GO
