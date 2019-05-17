

-- =============================================
-- 作品公示
-- =============================================

CREATE PROCEDURE [dbo].[GetWorksPublic]
AS
		SELECT    WorksSubmit.WorksID,Works.WorksName,WorksSubmit.WorksCode,Works.WorksTypeID,ISNULL(ExpertScores.ExpertScore,0) AS ExpertScore,ISNULL(TSComments.TSScore,0) AS TSScore,WorksType.WorksTypeName
		FROM   Works INNER JOIN
		 WorksSubmit ON Works.WorksID =WorksSubmit.WorksID LEFT JOIN
			  (SELECT     WorksID, AVG(Score) AS TSScore
				FROM         WorksComments
				WHERE      (Flag = 1)
				GROUP BY WorksID) AS TSComments ON WorksSubmit.WorksID = TSComments.WorksID INNER JOIN
                      WorksType ON Works.WorksTypeID = WorksType.WorksTypeID LEFT JOIN (SELECT  WorksID, AVG(Score) AS ExpertScore
FROM         dbo.WorksExpert
WHERE     (Flag = 1) AND (Score > 0)
GROUP BY WorksID) AS ExpertScores ON WorksSubmit.WorksID = ExpertScores.WorksID
		WHERE  ( WorksSubmit.Flag = 1 AND WorksSubmit.StateID = 2 AND WorksSubmit.Score=1) ORDER BY ExpertScore DESC, TSScore DESC
		
RETURN 0;
