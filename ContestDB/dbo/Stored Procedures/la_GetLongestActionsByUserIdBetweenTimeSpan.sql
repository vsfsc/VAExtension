-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[la_GetLongestActionsByUserIdBetweenTimeSpan]
	-- Add the parameters for the stored procedure here
	@UserId bigint,
	@StartDate datetime,
	@EndDate datetime
AS
--if (LEN(@StartDate)>0)
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SUM(dbo.LearningActivity.During) AS 时长, dbo.ActivityType.Action
FROM      dbo.LearningActivity INNER JOIN
                dbo.ActivityType ON dbo.LearningActivity.ActivityTypeID = dbo.ActivityType.ActivityTypeID
WHERE (dbo.LearningActivity.Start >= CONVERT(DATETIME, @StartDate, 102)) AND 
                (dbo.LearningActivity.Start < CONVERT(DATETIME, @EndDate, 102)) AND (dbo.LearningActivity.UserID = 194)
				and UserID=@userId
GROUP BY dbo.ActivityType.Action
ORDER BY 时长 DESC
END
