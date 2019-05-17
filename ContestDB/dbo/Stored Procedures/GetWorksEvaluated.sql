

-- =============================================
-- 专家已经评分的作品 xqx 2012-4-27
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksEvaluated]
	@WorksID BIGINT 
AS
	SELECT  dbo.WorksExpert.WorksExpertID, dbo.WorksExpert.WorksID, dbo.WorksExpert.ExpertID, dbo.WorksExpert.Score, dbo.[User].Name AS ExpertName, 
                      dbo.Works.WorksTypeID, dbo.Works.WorksName
	FROM   dbo.Works INNER JOIN
						  dbo.[User] INNER JOIN
						  dbo.WorksExpert ON dbo.[User].UserID = dbo.WorksExpert.ExpertID ON dbo.Works.WorksID = dbo.WorksExpert.WorksID
	WHERE  dbo.WorksExpert.Flag = 1 AND dbo.WorksExpert.WorksID = @WorksID AND WorksExpert.Score>0
RETURN 0;
