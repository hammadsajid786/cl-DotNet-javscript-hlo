CREATE TABLE [dbo].[Co_Cust_Billing]
(
[ID] [int] NULL,
[Account_Status] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Terms] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Accounts_Email] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Accounts_Name] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Accounts_userID] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Accounts_phone] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Invoice_Limit] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Account_Limit] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Send_Monthly_statement_auto] [bit] NULL,
[Is_Send_Acc_Overdue_Email_auto] [bit] NULL,
[Discount_Labour] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Never_Suspend] [bit] NULL
) ON [PRIMARY]
GO
