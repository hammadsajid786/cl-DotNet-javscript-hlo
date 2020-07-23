CREATE TABLE [dbo].[DD_Defect_Severity]
(
[id] [int] NULL,
[Severity] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Short_Description] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Is_Enabled] [bit] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_BB451201AEB541BC81DA46A472655E82] on [dbo].[DD_Defect_Severity] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Defect_Severity]')

        return
GO
