
-- =============================================
--专家需要评审的作品 xqx 2012-4-16

CREATE PROCEDURE [dbo].[InsertWorksExpert]
	@WorksExpertID BIGINT OUTPUT, 
	@WorksID BIGINT,
	@ExpertID BIGINT,
	@Score real,
	@Comments varchar(200),
	@Created datetime,
	@ScoreState int,
	@Flag BIGINT=1
AS
	INSERT INTO  [WorksExpert]
           ([WorksID]
           ,[ExpertID]
           ,[Score],Comments,Created,ScoreState
           ,[Flag])
     VALUES
           (@WorksID,@ExpertID,@Score,@Comments,@Created,@ScoreState,@Flag)
           
	 SELECT @WorksExpertID=@@IDENTITY   

RETURN 0;
