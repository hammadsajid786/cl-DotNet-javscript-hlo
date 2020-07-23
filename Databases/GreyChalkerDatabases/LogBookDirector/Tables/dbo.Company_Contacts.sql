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
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_79355D61C7D74F02AAA7771B94D661D6] on [dbo].[Company_Contacts] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[Company_Contacts]')

        return
GO
