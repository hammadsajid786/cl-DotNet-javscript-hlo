SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE PROCEDURE [dbo].[usp_insertGrievanceData] --'2019-01-11','2019-07-12' 



AS
BEGIN

    DECLARE @ReportedGrievance INT;
    DECLARE @ResolvedGrievance INT;

    SELECT @ReportedGrievance = COUNT(complaint_id)
    FROM dbo.lrmis_web_complaint
    WHERE CAST(created_date AS DATE)
    BETWEEN CAST(GETDATE() - 1 AS DATE) AND CAST(GETDATE() AS DATE);
	--BETWEEN CAST('2019-06-09' AS DATE) AND CAST('2019-06-10' AS DATE);


    SELECT @ResolvedGrievance = COUNT(complaint_id)
    FROM dbo.lrmis_web_complaint
    WHERE complaint_status_id = 4
          AND CAST(modified_date AS DATE)
          BETWEEN CAST(GETDATE() - 1 AS DATE) AND CAST(GETDATE() AS DATE);
		  --BETWEEN CAST('2019-06-09' AS DATE) AND CAST('2019-06-15' AS DATE);

    IF (@ReportedGrievance > 0)
    BEGIN
        INSERT INTO [dbo].[lrmis_web_api_downloaded_data]
        (
            [TotalVisited],
            [TotalLandTransfered],
            [TotalRegistries],
            [TotalAmount],
            [IssuanceCount],
            [ReportedGrievance],
            [ResolvedGrievance],
            [Downloaded_Date],
			[TotalLandTransferAmount]
        )
        VALUES
        (0, 0, 0, 0, 0, @ReportedGrievance, 0, GETDATE(),0);
    END;

    IF (@ResolvedGrievance > 0)
    BEGIN
        INSERT INTO [dbo].[lrmis_web_api_downloaded_data]
        (
            [TotalVisited],
            [TotalLandTransfered],
            [TotalRegistries],
            [TotalAmount],
            [IssuanceCount],
            [ReportedGrievance],
            [ResolvedGrievance],
            [Downloaded_Date],
		    [TotalLandTransferAmount]
        )
        VALUES
        (0, 0, 0, 0, 0, 0, @ResolvedGrievance, GETDATE(),0);
    END;

END;
GO
