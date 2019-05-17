
-- =============================================
--更新作品分组中的分数 xqx 2012-4-19
-- =============================================


CREATE PROCEDURE [dbo].[UpdateWorksExpert]
	@WorksExpertID BIGINT,
	@WorksID BIGINT,
	@ExpertID BIGINT,
	@Score real,
	@Comments varchar(200),
	@Modified datetime,
	@Flag BIGINT
AS
	UPDATE  [WorksExpert]
	SET [WorksID] = @WorksID
      ,[ExpertID] = @ExpertID
      ,[Score] =@Score,[Comments]=@Comments,Modified=@Modified
      ,[Flag] = @Flag
	WHERE WorksExpertID=@WorksExpertID
RETURN 0;
