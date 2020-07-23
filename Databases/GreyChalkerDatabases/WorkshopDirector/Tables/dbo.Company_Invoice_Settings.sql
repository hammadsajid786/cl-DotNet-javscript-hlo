CREATE TABLE [dbo].[Company_Invoice_Settings]
(
[ID] [int] NULL,
[company_ID] [int] NULL,
[Invoice_default_Terms] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Invoice_layout] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Invoice_Email_Copy_to] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Inovice_allow_modify_after_save] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Invoice_maximum_value] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Invoice_] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
