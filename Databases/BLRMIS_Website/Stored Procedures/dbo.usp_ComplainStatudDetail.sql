SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


-- =============================================
-- Author:		 
-- Create date: 01-03-2019
-- Description:	used by 'ComplainCount Report'
-- =============================================
CREATE PROCEDURE [dbo].[usp_ComplainStatudDetail]--'2019-06-10','2019-10-04',0,1
	 @FromDate DateTime--='2019-06-10'
	,@ToDate DateTime--='2019-06-20
	,@UserId INT 
	,@LocationId INT--=1 
AS
BEGIN

SELECT 
 
	users.user_name,
	loc.location_name,
	
	ComplaintCount.OpenCount [OpenCount],
	ComplaintCount.Closed [Closed],
	ComplaintCount.ReOpenedCount [ReOpenedCount],
	ComplaintCount.ResolvedCount [ResolvedCount],
	ComplaintCount.PendingCount [PendingCount],
	ComplaintCount.InProgressCount [InProgressCount]
	
	,CAST(complaint.created_date AS DATE)

	
FROM 
dbo.lrmis_web_complaint complaint
    INNER JOIN dbo.lrmis_web_complaint_status AS complaintStatus WITH (NOLOCK ) ON complaint.complaint_status_id = complaintStatus.complaint_status_id
	INNER JOIN dbo.lrmis_web_location AS loc WITH (NOLOCK ) ON loc.location_id = complaint.location_id
	INNER JOIN dbo.lrmis_web_user AS users WITH (NOLOCK) ON complaint.complaint_assign_to = users.user_id
	CROSS APPLY [func_ComplainsCount] (0,@LocationId,complaint.created_date-1,complaint.created_date) ComplaintCount
	WHERE CAST(complaint.created_date AS DATE)>=@FromDate AND CAST(complaint.created_date AS DATE)<=@ToDate
	AND (complaint.location_id =@LocationId OR COALESCE(@LocationId, 0) = 0) 
	--AND  (complaint.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)

GROUP BY 
users.user_name,
CAST(complaint.created_date AS DATE),loc.location_name, ComplaintCount.OpenCount
,ComplaintCount.ReOpenedCount,ComplaintCount.ResolvedCount ,ComplaintCount.PendingCount,ComplaintCount.InProgressCount
,ComplaintCount.Closed
END

-- SELECT * FROM 
--dbo.lrmis_web_complaint complaint where complaint.complaint_status_id=3 and 
--CAST(complaint.created_date AS DATE)='2019-06-12' and complaint.complaint_assign_to=1018
GO
