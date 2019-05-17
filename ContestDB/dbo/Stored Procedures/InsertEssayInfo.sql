CREATE PROCEDURE [dbo].[InsertEssayInfo]
	@EssayID bigint OUTPUT,
	@ExaminationDates datetime,
	@Teacher varchar(20),
	@EssayTitleType int ,
	@EssayTitle varchar(100),
	@TextDescription varchar(1000),
	@PictureDescription image,
	@Flag int
AS
	INSERT INTO [dbo].[EssayInfo]
           ([ExaminationDates],Teacher
		   ,[EssayTitleType]
           ,[EssayTitle]
           ,[TextDescription]
           ,[PictureDescription]
           ,[Flag])
     VALUES
           (@ExaminationDates,@Teacher,@EssayTitleType
           ,@EssayTitle
           ,@TextDescription
           ,@PictureDescription
           ,@Flag)
	SELECT @EssayID=@@IDENTITY
RETURN 0
