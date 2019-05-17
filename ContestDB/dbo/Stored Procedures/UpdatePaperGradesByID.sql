CREATE PROCEDURE [dbo].[UpdatePaperGradesByID]
	@GradesID bigint,@EssayID bigint,
	@SpecialtyID int ,
	@Grade varchar(50),
	@Class varchar(50),
	@StudentID varchar(20),
	@CreatedBy varchar(20),
	@TotalScores real,
	@WritingScores real,
	@ScoreLevel varchar(4),@EssayTitle varchar(100),
	@EssayText	varchar(3000),
	@Flag int
AS
	UPDATE [dbo].[PaperGrades]
   SET [EssayID] = @EssayID
		,[SpecialtyID] = @SpecialtyID
      ,[Grade] = @Grade
      ,[Class] = @Class
      ,[StudentID] = @StudentID
      ,[CreatedBy] = @CreatedBy
      ,[TotalScores] =@TotalScores
      ,[WritingScores] = @WritingScores
      ,[ScoreLevel] = @ScoreLevel,[EssayTitle] = @EssayTitle
      ,[EssayText] = @EssayText
      ,[Flag] = @Flag
 WHERE GradesID=@GradesID
RETURN 0
