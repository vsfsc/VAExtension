

-- =============================================
-- 获取作品的评分明细 2012-4-16 xqx
-- =============================================

CREATE PROCEDURE [dbo].[GetRatingDetailsByWorksExpertID]
	@WorksExpertID BIGINT = 0
AS
	SELECT [RatingDetailsID]
      ,[WorksExpertID]
      ,[StandardID]
      ,[Score]
      ,[Flag]
  FROM [RatingDetails] WHERE WorksExpertID=@WorksExpertID AND Flag=1
RETURN 0;
