-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [la_GetMostLocationsByUserId] 
	-- Add the parameters for the stored procedure here
	@UserId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   TOP (5) dbo.Location.Address, COUNT(dbo.LearningActivity.During) AS 总次数
FROM      dbo.LearningActivity INNER JOIN
                dbo.Location ON dbo.LearningActivity.ActivityTypeID = dbo.Location.LocationID
WHERE   (dbo.LearningActivity.UserID = @UserId)
GROUP BY dbo.Location.Address
ORDER BY 总次数 DESC
END
