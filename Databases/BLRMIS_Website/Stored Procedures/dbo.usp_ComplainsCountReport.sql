SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


-- =============================================
-- Author:		 
-- Create date: 01-03-2019
-- Description:	used by 'ComplainCount Report'
-- =============================================
CREATE PROCEDURE [dbo].[usp_ComplainsCountReport]--'2019-01-10' , '2019-10-17',4,1
	@FromDate Date,
	@ToDate Date,
	@UserId INT ,
	@LocationId INT

AS
BEGIN
SELECT users.user_name,
    loc.location_name,
	CAST(complaint.created_date AS DATE),
	users.user_id,
	loc.location_id,
	ComplaintCount.OpenCount,
	ComplaintCount.Closed

	--(SELECT OpenCount FROM [dbo].[func_ComplainsCount](users.user_id, loc.location_id,complaint.created_date, complaint.created_date)) OpenCount

FROM dbo.lrmis_web_complaint complaint
	INNER JOIN dbo.lrmis_web_complaint_status AS complaintStatus WITH (NOLOCK ) ON complaint.complaint_status_id = complaintStatus.complaint_status_id
	INNER JOIN dbo.lrmis_web_location AS loc WITH (NOLOCK ) ON loc.location_id = complaint.location_id
	INNER JOIN dbo.lrmis_web_user AS users WITH (NOLOCK) ON complaint.complaint_assign_to = users.user_id
	CROSS APPLY [dbo].[func_ComplainsCount] (users.user_id, loc.location_id,@FromDate, @ToDate) ComplaintCount


--WHERE complaint_assign_to = 4
--AND complaint.created_date BETWEEN '2019-01-10' AND '2019-10-17'
----AND complaint.complaint_status_id = 3
    WHERE (complaint.complaint_assign_to =@UserId OR COALESCE(@UserId, 0) = 0)
	 AND  (complaint.location_id=@LocationId OR COALESCE(@LocationId, 0) = 0)
     AND CAST(complaint.created_date AS DATE) >= @FromDate AND CAST(complaint.created_date AS DATE) <= @ToDate
GROUP BY CAST(complaint.created_date AS DATE), loc.location_name, users.user_name
,	users.user_id,
	loc.location_id,ComplaintCount.OpenCount
	,ComplaintCount.Closed
	END


--SELECT * FROM [dbo].[func_ComplainsCount] (4, 1, '2019-06-10', '2019-06-17')
GO
