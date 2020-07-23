CREATE TABLE [dbo].[DD_Vehicle_Make]
(
[Make] [int] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_379F4082ED764998A605D3EC4C0909C4] on [dbo].[DD_Vehicle_Make] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Vehicle_Make]')

        return
GO
