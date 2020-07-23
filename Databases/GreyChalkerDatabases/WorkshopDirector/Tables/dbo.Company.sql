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
ALTER TABLE [dbo].[Company] ADD CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED  ([CompanyId]) ON [PRIMARY]
GO
