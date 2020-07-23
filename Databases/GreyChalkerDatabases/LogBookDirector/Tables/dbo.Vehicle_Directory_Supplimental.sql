CREATE TABLE [dbo].[Vehicle_Directory_Supplimental]
(
[ID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fuel_tank_capacity] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Spare_Key_Location] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Battery_Isolator_Location] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Distance_per_tank] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IS_Retarder_fitted] [bit] NULL,
[Is_Retartder_Driver_controlled] [bit] NULL,
[Alert_Message_New_Driver] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_fuel_card] [bit] NULL,
[Fuel_card_pin] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_fuel_card_mileage] [bit] NULL
) ON [PRIMARY]
GO
