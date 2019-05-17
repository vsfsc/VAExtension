-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksTypeScoreStandardByTypeID]
	-- Add the parameters for the stored procedure here
	@WorksTypeID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   dbo.WorksTypeScoreStandard.WorkTypeID, dbo.WorksTypeScoreStandard.StandardID, 
                dbo.WorksTypeScoreStandard.Score, dbo.WorksTypeScoreStandard.StandardDescription, 
                dbo.WorksTypeScoreStandard.Flag, dbo.ScoreStandard.StandardName, dbo.ScoreStandard.Description
FROM      dbo.WorksTypeScoreStandard INNER JOIN
                dbo.ScoreStandard ON dbo.WorksTypeScoreStandard.StandardID = dbo.ScoreStandard.StandardID where WorkTypeID=@WorksTypeID and WorksTypeScoreStandard.Flag=1
END
