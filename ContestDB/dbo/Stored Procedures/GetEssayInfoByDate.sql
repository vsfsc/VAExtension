CREATE PROCEDURE [dbo].[GetEssayInfoByDate]
	@ExaminationDates datetime,
	@Teacher varchar(20)
AS
	SELECT [EssayID]
      ,[ExaminationDates],Teacher
      ,[EssayTitleType]
      ,[EssayTitle]
      ,[TextDescription]
      ,[PictureDescription]
      ,[Flag]
  FROM [dbo].[EssayInfo]
  where ExaminationDates=@ExaminationDates and Teacher=@Teacher
RETURN 0
