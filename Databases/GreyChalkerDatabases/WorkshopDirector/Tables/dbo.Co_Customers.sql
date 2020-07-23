CREATE TABLE [dbo].[Co_Customers]
(
[id] [int] NULL,
[User_Link] [int] NULL,
[Company_Link] [int] NULL,
[Cust_Company_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[First_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Middle_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Last_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_Unit] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_Number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_Address1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_Address2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_PostCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_State] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Address_Suburb] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Date_Created] [datetime] NULL,
[Cust_Created_by] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Phone_Main] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cust_Phone_Mobile] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Sole_Trader] [bit] NULL,
[Is_TP] [bit] NULL,
[Is_Transport_Company] [bit] NULL,
[Is_Workshop] [bit] NULL,
[Is_Agency] [bit] NULL,
[Is_Invalid] [bit] NULL,
[Is_Created_by_ADP] [bit] NULL
) ON [PRIMARY]
GO
