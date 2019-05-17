CREATE PROCEDURE [dbo].[UpdateEssayInfo]
	@EssayID bigint,
	@ExaminationDates datetime,
	@Teacher varchar(20),
	@EssayTitleType int ,
	@EssayTitle varchar(100),
	@TextDescription varchar(1000),
	@PictureDescription image,
	@Flag int
AS
	UPDATE [dbo].[EssayInfo]
   SET [ExaminationDates] = @ExaminationDates
      ,[Teacher] =@Teacher
      ,[EssayTitleType] = @EssayTitleType
      ,[EssayTitle] =@EssayTitle
      ,[TextDescription] = @TextDescription
      ,[PictureDescription] = @PictureDescription
      ,[Flag] = @Flag
 WHERE EssayID=@EssayID
RETURN 0
