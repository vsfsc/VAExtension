
-- =============================================
--专家评分明细 xqx 2012-4-16

CREATE PROCEDURE [dbo].[InsertRatingDetails]
	@RatingDetailsID BIGINT OUTPUT,
	@WorksExpertID BIGINT,
	@StandardID INT,
	@Score INT,
	@Flag BIGINT=1
AS
	INSERT INTO [RatingDetails]
           ([WorksExpertID]
           ,[StandardID]
           ,[Score]
           ,[Flag])
     VALUES
           (@WorksExpertID,@StandardID,@Score,@Flag)
	 SELECT @RatingDetailsID=@@IDENTITY   
       
RETURN 0;
