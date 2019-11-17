SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE PROCEDURE [dbo].[usp_insertApiDownloadedData] --'2019-01-11','2019-07-12' 

    @TotalVisited INT,
    @TotalLandTransfered INT,
    @TotalRegistries INT,
	@TotalLandTransferAmount DECIMAL(18,2),
    @TotalAmount DECIMAL(18,2),
    @IssuanceCount DECIMAL(18,2)
AS
BEGIN

    INSERT INTO [dbo].[lrmis_web_api_downloaded_data]
    (
        [TotalVisited],
        [TotalLandTransfered],
        [TotalRegistries],
		[TotalLandTransferAmount],
        [TotalAmount],
        [IssuanceCount],
        [ReportedGrievance],
        [ResolvedGrievance],
        [Downloaded_Date]
    )
    VALUES
    (@TotalVisited, @TotalLandTransfered, @TotalRegistries,@TotalLandTransferAmount ,@TotalAmount, @IssuanceCount, 0, 0, GETDATE());

END;
GO
