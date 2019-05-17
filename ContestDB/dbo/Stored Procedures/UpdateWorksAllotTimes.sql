-- =============================================
-- Author:		穆友胜
-- Create date: 2014-11-14
-- Description:	对一个作品每分配评分一次,作品分配次数+1,当最后一次分配时，作品状态变为2（分配完成，评分中）
-- =============================================
CREATE PROCEDURE [dbo].[UpdateWorksAllotTimes]
	-- Add the parameters for the stored procedure here
	@WorksID BIGINT,
	@AllotTimes BIGINT,
	@WorksState INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 UPDATE Works 
	 SET AllotTimes=@AllotTimes,
	 	 WorksState=@WorksState
	 WHERE Works.WorksID=@WorksID
END
