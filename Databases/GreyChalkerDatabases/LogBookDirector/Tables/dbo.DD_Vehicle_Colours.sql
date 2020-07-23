CREATE TABLE [dbo].[DD_Vehicle_Colours]
(
[ID] [nchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vehicle_Colour_Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_A720C6C4026E4E3B98DD0858A8598493] on [dbo].[DD_Vehicle_Colours] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Vehicle_Colours]')

        return
GO
