-- =============================================
-- Author:		mys
-- Create date: 2014-11-08
-- Description:	获取本期次所有已上传了作品的独创作者或组长用户的用户ID
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersIDByPeriodID]
	-- Add the parameters for the stored procedure here
	@PeriodID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT DISTINCT(Works.CreatedBy) as UserID
FROM      Works 
WHERE   dbo.Works.Flag>0 AND Works.PeriodID = @PeriodID AND IsSample=0
END
