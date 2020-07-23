CREATE TABLE [dbo].[Company_Contacts]
(
[CompanyID] [int] NULL,
[OperationsControllerName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OperationsControllerNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsOperationsControllerViewableAll] [bit] NULL,
[LBD_Reports_To_Email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OperationsControllerUserLink] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
