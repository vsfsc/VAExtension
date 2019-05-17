
-- =============================================
--ExpertWorksType填加新数据 xqx 2012-3-9
CREATE PROCEDURE [dbo].[InsertExpertWorksType]
	@ExpertWorksTypeID BIGINT OUTPUT,
	@UserID BIGINT, 
	@WorksTypeID BIGINT,
	@Flag BIGINT=1
AS
	INSERT INTO  [ExpertWorksType]
           ([UserID]
           ,[WorksTypeID]
           ,[Flag])
     VALUES
           (@UserID
           ,@WorksTypeID
           ,@Flag)
	 SELECT @ExpertWorksTypeID=@@IDENTITY   

