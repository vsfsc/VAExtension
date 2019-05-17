-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksByWorksID]
	-- Add the parameters for the stored procedure here
	@WorksID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT   dbo.Works.WorksID, dbo.Works.PeriodID, dbo.Works.WorksName, dbo.Works.TeamName, dbo.Works.WorksTypeID, 
                dbo.Works.WorksCode, dbo.Works.OrgID, dbo.Works.IsSample, dbo.Works.Comment, dbo.Works.Profile, 
                dbo.Works.Flag, dbo.Works.WorksState, dbo.Works.Modified, dbo.Works.ModifiedBy, dbo.Works.Created, 
                dbo.Works.CreatedBy, dbo.Works.Score, dbo.Works.BackReason, dbo.Works.DemoURL, dbo.Works.SelfAssessment, 
                dbo.Works.KeyPoints, dbo.Works.SubmitProfile, dbo.Works.Suggestion, dbo.Works.InstallationGuide, 
                dbo.Works.DesignIdeas, dbo.WorksType.WorksTypeName, dbo.WorksType.WorksTypeID AS Expr1,dbo.WorksType.LevelID,dbo.WorksType.ParentID
FROM      dbo.Works LEFT OUTER JOIN
                dbo.WorksType ON dbo.Works.WorksTypeID = dbo.WorksType.WorksTypeID
	where WorksID=@WorksID
END
