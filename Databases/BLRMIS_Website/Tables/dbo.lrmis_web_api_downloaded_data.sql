CREATE TABLE [dbo].[lrmis_web_api_downloaded_data]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TotalVisited] [int] NULL,
[TotalLandTransfered] [int] NULL,
[TotalRegistries] [int] NULL,
[TotalLandTransferAmount] [decimal] (18, 2) NULL,
[TotalAmount] [decimal] (18, 2) NULL,
[IssuanceCount] [decimal] (18, 2) NULL,
[ReportedGrievance] [int] NULL,
[ResolvedGrievance] [int] NULL,
[Downloaded_Date] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lrmis_web_api_downloaded_data] ADD CONSTRAINT [PK_lrmis-web_ApiDownloadedData] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
