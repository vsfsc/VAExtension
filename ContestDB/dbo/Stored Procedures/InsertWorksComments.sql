
-- =============================================
--插入新数据 师生对作品的评审 xqx 2012-4-25
-- =============================================

CREATE PROCEDURE [dbo].[InsertWorksComments]
	@CommentsID BIGINT OUTPUT, 
	@WorksID BIGINT,
	@UserID BIGINT,
	--@Comments VARCHAR(1000),
	--@Score REAL,
	--@Created DATETIME,
	@Flag BIGINT=1
AS
	INSERT INTO [dbo].[WorksExpert]
	        (
			[WorksID] ,
	          [ExpertID] ,
	          [Flag]
	        )
     VALUES
           (@WorksID
           ,@UserID
           --,@Comments
           --,@Score
           --,@Created
           ,@Flag)
     	 SELECT @CommentsID=@@IDENTITY   
      
RETURN 0;
