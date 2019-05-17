-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [la_GetLongestActionsByUserID]
	-- Add the parameters for the stored procedure here
	@uid bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   TOP (100) PERCENT dbo.ActivityType.Action, SUM(dbo.LearningActivity.During) AS 总时长
FROM      dbo.LearningActivity INNER JOIN
                dbo.ActivityType ON dbo.LearningActivity.ActivityTypeID = dbo.ActivityType.ActivityTypeID
				WHERE   (dbo.LearningActivity.UserID = @uid)
GROUP BY dbo.ActivityType.Action
ORDER BY 总时长 DESC
END
