CREATE TABLE [dbo].[Vehicle_Profile_InstumentControl]
(
[ID] [int] NULL,
[Is_Adjustable_Speed_limit] [bit] NULL,
[Is_Trip_Computer] [bit] NULL,
[Is_Analog_Clock] [bit] NULL,
[Is_Digital_Clock] [bit] NULL,
[Is_Gauge_Tacho] [bit] NULL,
[Is_Gauge_Type_Pressure] [bit] NULL,
[Is_Gauge_Engine_Temp] [bit] NULL,
[Is_Gauge_Transmission_Temp] [bit] NULL,
[Is_Gauge_Fuel_Analog] [bit] NULL,
[Is_Gauge_Fuel_Digital] [bit] NULL,
[Is_Gauge_Air_Pressure_Analog] [bit] NULL,
[Is_Gauge_Air_Pressure_Digital] [bit] NULL,
[Is_Gauge_AdBlue_Analog] [bit] NULL,
[Is_Gauge_AdBlue_Digital] [bit] NULL
) ON [PRIMARY]
GO
