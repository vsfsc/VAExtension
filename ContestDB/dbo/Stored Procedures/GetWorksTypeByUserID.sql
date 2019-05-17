
-- =============================================
--ExpertWorksType填加新数据 xqx 2012-3-9

CREATE PROCEDURE [dbo].[GetWorksTypeByUserID]
	@UserID BIGINT 
AS
	SELECT [ExpertWorksTypeID]
      ,[UserID]
      ,[WorksTypeID]
      ,[Flag]
  FROM [ExpertWorksType]
  WHERE UserID=@UserID AND Flag=1
RETURN 0;
