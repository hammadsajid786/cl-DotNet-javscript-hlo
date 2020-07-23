CREATE TABLE [dbo].[Co_Cust_Contact_History]
(
[id] [int] NULL,
[Contact_date] [datetime] NULL,
[Contact_with] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Contact_method] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Contact_direction] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Contact_outcome] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
