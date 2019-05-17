-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [la_GetMostTypesByUserId]
	-- Add the parameters for the stored procedure here
	@UserId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   TOP (5) dbo.ActivityType.Action, COUNT(dbo.LearningActivity.During) AS 总次数
FROM      dbo.LearningActivity INNER JOIN
                dbo.ActivityType ON dbo.LearningActivity.ActivityTypeID = dbo.ActivityType.ActivityTypeID
WHERE   (dbo.LearningActivity.UserID = @UserId)
GROUP BY dbo.ActivityType.Action
ORDER BY 总次数 DESC
END
