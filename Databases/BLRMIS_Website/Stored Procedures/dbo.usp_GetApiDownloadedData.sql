SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[usp_GetApiDownloadedData] --'2019-01-11','2019-07-12' 
    @statusId INT
AS
BEGIN
    DECLARE @fromDate DATE;
    DECLARE @toDate DATE;
    SET @fromDate = CAST(GETDATE() AS DATE);
    SET @toDate = CAST(GETDATE() AS DATE);
    IF (@statusId = 0)
    BEGIN
        SET @fromDate = DATEADD(DAY, -1, CAST(GETDATE() AS DATE));
    END;
    IF (@statusId = 1)
    BEGIN
        SET @fromDate = DATEADD(WEEK, -1, CAST(GETDATE() AS DATE));
    END;
    IF (@statusId = 2)
    BEGIN
        SET @fromDate = DATEADD(MONTH, -1, CAST(GETDATE() AS DATE));
    END;
    IF (@statusId = 3)
    BEGIN
        SET @fromDate = DATEADD(YEAR, -1, CAST(GETDATE() AS DATE));
    END;

    SELECT SUM([TotalVisited]) [TotalVisited],
           SUM([TotalLandTransfered]) [TotalLandTransfered],
           SUM([TotalRegistries]) [TotalRegistries],
		   SUM([TotalLandTransferAmount]) [TotalRegistriesFee],
           SUM([TotalAmount]) [TotalAmount],
           SUM([IssuanceCount]) [IssuanceCount],
           SUM([ReportedGrievance]) [ReportedGrievance],
           SUM([ResolvedGrievance]) [ResolvedGrievance],
           CAST(Downloaded_Date AS DATE) Downloaded_Date
    FROM dbo.lrmis_web_api_downloaded_data
    WHERE CAST(Downloaded_Date AS DATE)
    BETWEEN @fromDate AND @toDate
    GROUP BY CAST(Downloaded_Date AS DATE);

--SELECT 
--	  [TotalVisited],
--         [TotalLandTransfered],
--         [TotalRegistries],
--         [TotalAmount],
--         [IssuanceCount],
--         [ReportedGrievance],
--         [ResolvedGrievance],
--         Downloaded_Date
--   FROM dbo.lrmis_web_api_downloaded_data
--   WHERE CAST(Downloaded_Date AS DATE)
--   BETWEEN BETWEEN @fromDate AND @toDate

END;
GO
