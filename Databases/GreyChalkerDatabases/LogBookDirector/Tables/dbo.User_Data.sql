CREATE TABLE [dbo].[User_Data]
(
[Id] [int] NOT NULL IDENTITY(1, 1) NOT FOR REPLICATION,
[User_Credential_Id] [int] NULL,
[FirstName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MobileNoPersonal] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MobileNoWork] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailPersonal] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailWork] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MiddleName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetUnit] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetAddress1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetAddress2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetSuburb] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetPostCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[User_AddressStreetState] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Email_Personal_Confirmed] [bit] NULL,
[Is_email_Work_Confirmed] [bit] NULL,
[Is_Mechanic] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_Data] ADD CONSTRAINT [PK_User_Data] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
