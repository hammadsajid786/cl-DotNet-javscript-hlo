CREATE TABLE [dbo].[DD_Licence_Class]
(
[LicenceClass] [int] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_620AB71799C345628F677F047D1A5A88] on [dbo].[DD_Licence_Class] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Licence_Class]')

        return
GO
