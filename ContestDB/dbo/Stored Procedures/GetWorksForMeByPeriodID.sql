-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	获取每一期次分配给我评分的作品
-- =============================================
CREATE PROCEDURE GetWorksForMeByPeriodID
	-- Add the parameters for the stored procedure here
	@PeriodID BIGINT, --期次ID
	@userID BIGINT   --我的UserID
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  ExpertID ,
        WorksID 
FROM    [dbo].[WorksExpert]
WHERE   WorksID IN ( SELECT WorksID
                     FROM   Works
                     WHERE  PeriodID = @PeriodID )
AND ExpertID=@userID
END
