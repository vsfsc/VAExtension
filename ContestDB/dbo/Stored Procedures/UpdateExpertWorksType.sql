
-- =============================================
--专家类型编辑数据 xqx 2012-3-27

CREATE PROCEDURE [dbo].[UpdateExpertWorksType]
	@UserID BIGINT, 
	@WorksTypeID BIGINT,
	@Flag BIGINT
AS
   UPDATE [ExpertWorksType]
   SET [WorksTypeID] =@WorksTypeID,[Flag] = @Flag
   WHERE UserID=@UserID
RETURN 0;
