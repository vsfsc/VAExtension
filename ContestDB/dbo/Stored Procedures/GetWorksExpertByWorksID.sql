-- =============================================
-- Author:		xqx
-- Create date: <Create Date,,>
-- Description:	获取每个作品的评分
-- =============================================
CREATE PROCEDURE GetWorksExpertByWorksID
	-- Add the parameters for the stored procedure here
	@WorksID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   WorksID, ExpertID, Score, Comments, Flag
FROM      dbo.WorksExpert where WorksID=@WorksID and Flag=1
order by Score
END
