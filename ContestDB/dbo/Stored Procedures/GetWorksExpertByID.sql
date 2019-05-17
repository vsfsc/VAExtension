
-- =============================================
--根据ID获取专家评审的数据 xqx 2012-4-17

CREATE PROCEDURE [dbo].[GetWorksExpertByID]
	@WorksExpertID BIGINT
AS
	SELECT [WorksExpertID]
      ,[WorksID]
      ,[ExpertID]
      ,[Score]
	  ,[Comments],Created,Modified,ScoreState
      ,[Flag]
  FROM  [WorksExpert] WHERE  WorksExpertID=@WorksExpertID
RETURN 0;
