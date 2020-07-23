CREATE TABLE [dbo].[Ref_SMS_From_Module]
(
[id] [smallint] NOT NULL,
[ModuleName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_0139376DCC5240D090E806D951E3082B] on [dbo].[Ref_SMS_From_Module] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[Ref_SMS_From_Module]')

        return
GO
ALTER TABLE [dbo].[Ref_SMS_From_Module] ADD CONSTRAINT [PK_Ref_SMS_From_Module] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
