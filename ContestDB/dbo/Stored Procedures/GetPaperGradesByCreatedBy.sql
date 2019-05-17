CREATE PROCEDURE [dbo].[GetPaperGradesByCreatedBy]
	@CreatedBy varchar(20)
AS
	SELECT [GradesID]
		,[EssayID]
      ,[SpecialtyID]
      ,[Grade]
      ,[Class]
      ,[StudentID]
      ,[CreatedBy]
      ,[TotalScores]
      ,[WritingScores]
      ,[ScoreLevel],EssayTitle,EssayText
      ,[Flag]
  FROM [dbo].[PaperGrades]
  where CreatedBy=@CreatedBy and Flag=1

RETURN 0

