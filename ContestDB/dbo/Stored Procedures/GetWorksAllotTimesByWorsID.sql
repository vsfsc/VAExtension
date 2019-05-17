-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	根据作品ID获取作品分配次数和作品状态
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksAllotTimesByWorsID]
@WorksID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT dbo.Works.WorksID,dbo.Works.AllotTimes,WorksState
	FROM dbo.Works
	WHERE WorksID=@WorksID
END
