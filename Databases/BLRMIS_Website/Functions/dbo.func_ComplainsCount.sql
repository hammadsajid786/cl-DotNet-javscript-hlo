SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[func_ComplainsCount] --SELECT * FROM [dbo].[func_ComplainsCount] (4, 1, '2019-06-10', '2019-06-17')
(	
	--@ComplainStatusID INT,
	@UserId INT,
	@LocationId INT
	,@FromDate DateTime,
	@ToDate DateTime
)
RETURNS @temp TABLE(
OpenCount int, PendingCount int, InProgressCount int, ReOpenedCount int,ResolvedCount int,Closed int
) 
AS
	
BEGIN
	DECLARE 
	@OpenCount int=0, @PendingCount int=0, @InProgressCount int=0, @ReOpenedCount int=0, @ResolvedCount int=0, @Closed int=0
	
	SELECT 
	@OpenCount = COUNT(complaint_id) 
	FROM 
	dbo.lrmis_web_complaint AS complains 
	WHERE 
	complains.complaint_status_id=1 
	AND (complains.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	AND  (complains.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	AND CAST(complains.created_date AS DATE) >= @FromDate AND CAST(complains.created_date AS DATE) <= @ToDate
	 group by CAST(created_date as Date)


	SELECT 
	@ReOpenedCount = COUNT(1) FROM dbo.lrmis_web_complaint AS complains WHERE complains.complaint_status_id=2 
	
	 AND (complains.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	 AND  (complains.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	  AND CAST(complains.created_date AS DATE) >= @FromDate AND CAST(complains.created_date AS DATE) <= @ToDate
	   group by CAST(created_date as Date)

	
SELECT @Closed = COUNT(1) FROM dbo.lrmis_web_complaint AS complains WHERE complains.complaint_status_id=3 

	 AND (complains.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	 AND  (complains.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	 AND CAST(complains.created_date AS DATE) >= @FromDate AND CAST(complains.created_date AS DATE) <= @ToDate
	 group by CAST(created_date as Date)
SELECT 
@ResolvedCount = COUNT(1) FROM dbo.lrmis_web_complaint AS complains WHERE complains.complaint_status_id=4
	
	  AND (complains.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	 AND  (complains.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	 AND CAST(complains.created_date AS DATE) >= @FromDate AND CAST(complains.created_date AS DATE) <= @ToDate
	  group by CAST(created_date as Date)

SELECT 
     @PendingCount = COUNT(1) FROM dbo.lrmis_web_complaint AS complains WHERE complains.complaint_status_id=5 
	 
	  AND (complains.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	  AND  (complains.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	  AND CAST(complains.created_date AS DATE)>= @FromDate AND CAST(complains.created_date AS DATE) <= @ToDate
	   group by CAST(created_date as Date)

	SELECT 
	@InProgressCount = COUNT(1) FROM dbo.lrmis_web_complaint AS complains WHERE complains.complaint_status_id=6 
	 --AND (complains.complaint_status_id =@ComplainStatusID OR COALESCE(@ComplainStatusID, 0) = 0)
	  AND (complains.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	  AND  (complains.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	  AND CAST(complains.created_date AS DATE) >= @FromDate AND CAST(complains.created_date AS DATE) <= @ToDate
	   group by CAST(created_date as Date)

	insert INTO @temp
	SELECT 
	@OpenCount, @PendingCount, @InProgressCount, @ReOpenedCount,@ResolvedCount,@Closed

	RETURN

END


--SELECT * FROM [func_ComplainsCount](0,1)
GO
