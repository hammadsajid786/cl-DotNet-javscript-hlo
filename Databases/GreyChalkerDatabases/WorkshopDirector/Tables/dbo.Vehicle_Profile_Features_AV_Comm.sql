CREATE TABLE [dbo].[Vehicle_Profile_Features_AV_Comm]
(
[ID] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Aux_Input] [bit] NULL,
[Is_Bluetooth] [bit] NULL,
[Is_Android_Auto] [bit] NULL,
[Is_Apple_Carplay] [bit] NULL,
[Is_Wireless_Charging] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_CD_Player] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Speakers] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_SatNav] [bit] NULL,
[Is_Fridge] [bit] NULL
) ON [PRIMARY]
GO
