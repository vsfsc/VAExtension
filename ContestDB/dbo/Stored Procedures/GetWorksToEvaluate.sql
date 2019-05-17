-- =============================================
--专家待审批的作品 xqx 2012-4-16

CREATE PROCEDURE [dbo].[GetWorksToEvaluate]
@ExpertID bigint,@PeriodID bigint,@IsSample int=0
AS
  if (@IsSample=0)
   begin
		SELECT     dbo.WorksExpert.WorksExpertID,WorksExpert.Comments,Works.Flag ,dbo.WorksExpert.WorksID, WorksExpert.ScoreState,WorksExpert.ExpertID, dbo.Works.WorksCode, dbo.Works.WorksName,IsSample,dbo.WorksExpert.Score, dbo.Works.WorksTypeID, dbo.WorksType.WorksTypeName
		FROM  Works INNER JOIN
			  dbo.WorksExpert ON dbo.Works.WorksID = dbo.WorksExpert.WorksID INNER JOIN
		  dbo.WorksType ON dbo.WorksType.WorksTypeID = dbo.Works.WorksTypeID
		WHERE     (dbo.WorksExpert.ExpertID = @ExpertID) AND (dbo.WorksExpert.Flag = 1) AND Works.PeriodID=@PeriodID
   end
   else
   begin
   SELECT     dbo.WorksExpert.WorksExpertID,WorksExpert.Comments, dbo.WorksExpert.WorksID, WorksExpert.ScoreState,WorksExpert.ExpertID,WorksExpert.Flag, dbo.Works.WorksCode, dbo.Works.WorksName,IsSample,dbo.WorksExpert.Score, dbo.Works.WorksTypeID, dbo.WorksType.WorksTypeName
		FROM  Works INNER JOIN
			  dbo.WorksExpert ON dbo.Works.WorksID = dbo.WorksExpert.WorksID INNER JOIN
		  dbo.WorksType ON dbo.WorksType.WorksTypeID = dbo.Works.WorksTypeID
		WHERE   (dbo.WorksExpert.ExpertID = @ExpertID) AND (dbo.WorksExpert.Flag >2) AND Works.PeriodID=@PeriodID
   end

	
RETURN 0;