-- =============================================
-- Author:		mys
-- Create date: 2014-11-07
-- Description:	根据用户ID和期次名获取该用户本期次的作品ID
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksIDByUserAccountAndPeriodTitle] 
	-- Add the parameters for the stored procedure here
	@Account BIGINT,
	@PeriodTitle VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   Works.WorksID
FROM      [User] RIGHT OUTER JOIN
                UserWorks ON [User].UserID = UserWorks.UserID LEFT OUTER JOIN
                Works LEFT OUTER JOIN
                Periods ON Works.PeriodID = Periods.PeriodID ON UserWorks.WorksID = Works.WorksID
WHERE dbo.[User].Account=@Account AND dbo.Periods.PeriodTitle=@PeriodTitle
END
