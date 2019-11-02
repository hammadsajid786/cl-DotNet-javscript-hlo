CREATE TABLE [dbo].[jl_Order]
(
[OrderId] [int] NOT NULL IDENTITY(1, 1),
[BusinessId] [int] NULL,
[PackagePrice] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PackageId] [int] NULL,
[OrderStatus] [bit] NULL,
[Date] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ExpireDate] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Order] ADD CONSTRAINT [PK_jl_Order] PRIMARY KEY CLUSTERED  ([OrderId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_jl_Order_jl_Pakages] ON [dbo].[jl_Order] ([PackageId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[jl_Order] ADD CONSTRAINT [FK_jl_Order_jl_Pakages] FOREIGN KEY ([PackageId]) REFERENCES [dbo].[jl_Pakages] ([PkgId])
GO
