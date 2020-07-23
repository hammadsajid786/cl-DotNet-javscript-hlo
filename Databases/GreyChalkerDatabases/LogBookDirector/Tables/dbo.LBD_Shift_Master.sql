CREATE TABLE [dbo].[LBD_Shift_Master]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[vehicleId] [int] NULL,
[User_Credential_Id] [int] NULL,
[WorkDiaryNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[First_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Middle_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Last_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Licence_Number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_NumberPlate] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Date] [date] NULL,
[Day_of_Week] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DailyCheckedInTime] [time] NULL,
[Fatigue_Mode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Timezone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UDI] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Imported] [bit] NULL,
[Data_Provider] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Date_Imported] [datetime] NULL,
[Is_Backdated] [bit] NULL,
[Date_Backdate_Created] [datetime] NULL,
[Is_Completed] [bit] NULL,
[IsAFM] [bit] NULL,
[IsBFM] [bit] NULL,
[IsStandard] [bit] NULL,
[IsStandardBus] [bit] NULL,
[IsExemptionHours] [bit] NULL,
[Is_Two_Up] [bit] NULL,
[Is_Two_Up_LDB] [bit] NULL,
[TWOUP_Driver_ID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWOUP_First_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWOUP_Middle_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWOUP_Last_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWOUP_Fatigue_Mode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWOUP_Record_Number] [int] NULL,
[TWOUP_Timezone] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWOUP_Licence_Number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TWO_UP_IsAFM] [bit] NULL,
[TWO_UP_IsStandard] [bit] NULL,
[TWO_UP_IsBFM] [bit] NULL,
[TWO_UP_IsExemptionHours] [bit] NULL,
[TWO_UP_WorkDairyNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedOn] [datetime] NULL,
[LastUpdateOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LBD_Shift_Master] ADD CONSTRAINT [PK_LBD_Shift_Master] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LBD_Shift_Master] ADD CONSTRAINT [FK_LBD_Shift_Master_User_Credential] FOREIGN KEY ([User_Credential_Id]) REFERENCES [dbo].[User_Credential] ([Id])
GO
ALTER TABLE [dbo].[LBD_Shift_Master] ADD CONSTRAINT [FK_LBD_Shift_Master_Vehicle_Directory_Master] FOREIGN KEY ([vehicleId]) REFERENCES [dbo].[Vehicle_Directory_Master] ([id])
GO
