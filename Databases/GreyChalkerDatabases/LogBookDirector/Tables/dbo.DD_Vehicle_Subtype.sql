CREATE TABLE [dbo].[DD_Vehicle_Subtype]
(
[VehicleSubType] [int] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_95D61E5D326F4DE6BD948D1A7E6B4E93] on [dbo].[DD_Vehicle_Subtype] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Vehicle_Subtype]')

        return
GO
