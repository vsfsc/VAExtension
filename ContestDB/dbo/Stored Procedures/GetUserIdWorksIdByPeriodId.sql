-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetUserIdWorksIdByPeriodId
	-- Add the parameters for the stored procedure here
@PeriodID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   UserWorks.UserID, UserWorks.WorksID
FROM      UserWorks INNER JOIN
                Works ON UserWorks.WorksID = Works.WorksID
WHERE   (Works.PeriodID = @PeriodID) AND (Works.IsSample = 0) AND (UserWorks.Relationship = 0 OR
                UserWorks.Relationship = 1) AND UserWorks.Flag=1 AND Works.Flag=1
END
