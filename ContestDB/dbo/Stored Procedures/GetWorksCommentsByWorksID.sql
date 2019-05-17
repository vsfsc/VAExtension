-- =============================================
--根据ID获取师生对作品的评审 xqx 2012-4-25
-- =============================================

CREATE PROCEDURE [dbo].[GetWorksCommentsByWorksID]
	-- Add the parameters for the stored procedure here
	@WorksID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   dbo.WorksExpert.WorksID, dbo.WorksExpert.ExpertID, dbo.WorksExpert.Score, dbo.WorksExpert.Comments, 
                dbo.WorksExpert.Created, dbo.WorksExpert.ScoreState, dbo.WorksExpert.Flag, dbo.[User].Name
	FROM      dbo.WorksExpert INNER JOIN
                dbo.[User] ON dbo.WorksExpert.ExpertID = dbo.[User].UserID
	WHERE   (WorksExpert.Flag = 2) AND (WorksID = @WorksID)
END
