CREATE TABLE [dbo].[Company]
(
[CompanyId] [int] NOT NULL,
[EntityName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TradingName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ABN] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_RegisteredAddressUnit] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_RegisteredAddressNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_RegisteredAddressAddress1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_RegisteredAddressAddress2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_RegisteredAddressPostCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_RegisteredAddressState] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_PostalAddressUnit] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_PostalAddressNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_PostalAddressAddress1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_PostalAddressAddress2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_PostalAddressPostCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_PostalAddressState] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_DateCreated] [datetime] NULL,
[Co_CreateMethod] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Co_CreatedBy] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsRegisteredAddressDepot] [bit] NULL,
[IsSoleTrader] [bit] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

    create trigger [dbo].[MSmerge_disabledml_82C2182DB84446E5AE71B73B99934167] on [dbo].[Company] for update, insert, delete
        not for replication
    as
        set nocount on

        if @@trancount > 0 rollback tran
        raiserror (20092, 16, -1, '[dbo].[Company]')

        return
GO
ALTER TABLE [dbo].[Company] ADD CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED  ([CompanyId]) ON [PRIMARY]
GO
