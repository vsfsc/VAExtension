-- =============================================
-- Author:		mys
-- Create date: 2014-11-08
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksIDByPeriodIDAndUserID]
	-- Add the parameters for the stored procedure here
	@userID bigint,
	@PeriodID BIGINT,
	@allotTimes BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  Works.CreatedBy AS UserID ,
        Works.WorksID
FROM    Works
WHERE   PeriodID = @PeriodID
        AND IsSample = 0
        AND CreatedBy <> @userID
        AND Flag > 0
        AND AllotTimes < @AllotTimes
		AND Flag>0
ORDER BY AllotTimes

END
