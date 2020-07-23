CREATE TABLE [dbo].[User_Driver]
(
[Id] [int] NOT NULL,
[LicenseNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LicenseState] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LicenseExpiry] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LicenseClass] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsSnowAccredited] [bit] NULL,
[IsDGAccredited] [bit] NULL,
[IsAFMAAccredited] [bit] NULL,
[IsDCAccredited] [bit] NULL,
[IsPPlater] [bit] NULL,
[DCExpiryDate] [date] NULL,
[SnowExpiryDate] [date] NULL,
[PPlatesExpiryDate] [date] NULL,
[UDI] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_Credential_Id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Driver] ADD CONSTRAINT [PK_User_Driver] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
