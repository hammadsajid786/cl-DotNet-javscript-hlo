CREATE TABLE [dbo].[DD_States]
(
[state] [int] NOT NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_4A726608C1E447A78DD2F4361B61E8A4] on [dbo].[DD_States] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[DD_States]')

        return
GO
ALTER TABLE [dbo].[DD_States] ADD CONSTRAINT [PK_DD_States] PRIMARY KEY CLUSTERED  ([state]) ON [PRIMARY]
GO
