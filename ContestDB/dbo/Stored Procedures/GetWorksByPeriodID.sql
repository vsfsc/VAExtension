-- =============================================
-- Author:		xqx
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksByPeriodID]
	-- Add the parameters for the stored procedure here
	@PeriodID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT    WorksID, PeriodID, WorksName, TeamName, Works.WorksTypeID, WorksCode, OrgID, IsSample, Comment, Profile, 
                SubmitProfile, Suggestion, InstallationGuide, DesignIdeas, KeyPoints, DemoURL, SelfAssessment, BackReason, Score, 
                Works.CreatedBy, Works.Created, Works.ModifiedBy, Works.Modified, AllotTimes, WorksState,Works. Flag
,dbo.WorksType.WorksTypeName
FROM      dbo.Works INNER JOIN
                dbo.WorksType ON dbo.Works.WorksTypeID = dbo.WorksType.WorksTypeID
WHERE   (PeriodID = @PeriodID) AND (IsSample = 0) AND (Works.Flag = 1)
END
