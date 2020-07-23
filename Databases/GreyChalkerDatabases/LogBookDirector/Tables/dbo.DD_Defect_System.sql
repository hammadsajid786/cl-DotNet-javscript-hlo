CREATE TABLE [dbo].[DD_Defect_System]
(
[id] [int] NULL,
[System] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Short_Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_00A653529E62473C81FA64C80983D563] on [dbo].[DD_Defect_System] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_Defect_System]')

        return
GO
