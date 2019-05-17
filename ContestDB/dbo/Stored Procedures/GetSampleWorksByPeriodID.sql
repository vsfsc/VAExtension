-- =============================================
-- Author:		xqx
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSampleWorksByPeriodID] 
	-- Add the parameters for the stored procedure here
	@PeriodID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   dbo.Works.WorksID, dbo.Works.PeriodID,Works.WorksTypeID, dbo.Works.WorksName, dbo.Works.TeamName, dbo.Works.WorksCode, 
                dbo.Works.OrgID, dbo.Works.IsSample, dbo.Works.Score, dbo.Works.Flag, dbo.Works.WorksState, 
                dbo.Works.Comment, dbo.Works.Profile, dbo.Works.SubmitProfile, dbo.Works.Suggestion, dbo.Works.InstallationGuide, 
                dbo.Works.DesignIdeas, dbo.Works.KeyPoints, dbo.Works.SelfAssessment, dbo.Works.DemoURL, 
                dbo.Works.BackReason, dbo.WorksType.WorksTypeName
FROM      dbo.Works INNER JOIN
                dbo.WorksType ON dbo.Works.WorksTypeID = dbo.WorksType.WorksTypeID
WHERE   (dbo.Works.IsSample = 1) AND (dbo.Works.PeriodID = @PeriodID)
order by NEWID()
END
