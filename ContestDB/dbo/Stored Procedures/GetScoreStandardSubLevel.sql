

-- =============================================
-- 获取评分标准的其他级别 2012-4-16 xqx
-- =============================================

CREATE PROCEDURE [dbo].[GetScoreStandardSubLevel]
AS
	SELECT StandardID, StandardName,Description,Flag
	FROM         dbo.ScoreStandard
	WHERE     (Flag = 1)
	
RETURN 0;
