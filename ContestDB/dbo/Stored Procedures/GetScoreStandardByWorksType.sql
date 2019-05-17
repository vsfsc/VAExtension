

-- =============================================
-- 获取作品期次的评分标准 2012-4-18 xqx
-- =============================================

CREATE PROCEDURE [dbo].[GetScoreStandardByWorksType]
	@WorkTypeID int,
	@PeriodID bigint
AS
	SELECT   dbo.PeriodStandard.WorkTypeID, dbo.PeriodStandard.PeriodID, dbo.PeriodStandard.StandardID,PeriodStandard.Created,PeriodStandard.CreatedBy,PeriodStandard.Modified,PeriodStandard.ModifiedBy, 
                dbo.PeriodStandard.StandardDescription,PeriodStandard.Score, dbo.PeriodStandard.Score, dbo.PeriodStandard.Flag, 
                dbo.ScoreStandard.StandardName, dbo.ScoreStandard.Description
FROM      dbo.PeriodStandard INNER JOIN
                dbo.ScoreStandard ON dbo.PeriodStandard.StandardID = dbo.ScoreStandard.StandardID
	WHERE   (PeriodStandard.Flag = 1 and PeriodID=@PeriodID and WorkTypeID=@WorkTypeID)
RETURN 0;
