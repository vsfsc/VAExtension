CREATE PROCEDURE [dbo].[GetEssayInfoById]
	@EssayID bigint
AS
	SELECT [EssayID]
      ,[ExaminationDates],Teacher
      ,[EssayTitleType]
      ,[EssayTitle]
      ,[TextDescription]
      ,[PictureDescription]
      ,[Flag]
  FROM [dbo].[EssayInfo]
  where EssayID=@EssayID
RETURN 0
