

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================




CREATE PROCEDURE [dbo].[GetWorksSubmitByID]
@WorksID BIGINT
AS
	 SELECT     WorksID, WorksName,WorksCode,Profile,SubmitProfile,
              Suggestion, InstallationGuide, DesignIdeas, KeyPoints,
              SelfAssessment, Comment,  DemoURL,WorksState
FROM         Works
WHERE WorksID=@WorksID AND Flag>0
RETURN 0;
