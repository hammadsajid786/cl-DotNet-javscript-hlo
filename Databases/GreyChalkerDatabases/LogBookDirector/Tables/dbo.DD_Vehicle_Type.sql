CREATE TABLE [dbo].[DD_Vehicle_Type]
(
[VehicleType] [int] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_868DC3BEDD51456BA9E188595721867E] on [dbo].[DD_Vehicle_Type] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Vehicle_Type]')

        return
GO
