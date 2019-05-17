CREATE PROCEDURE [dbo].[InsertPaperGrades]
	@GradesID bigint OUTPUT,@EssayID bigint,
	@SpecialtyID int ,
	@Grade varchar(50),
	@Class varchar(50),
	@StudentID varchar(20),
	@CreatedBy varchar(20),
	@TotalScores real,
	@WritingScores real,
	@ScoreLevel varchar(4),
	@EssayTitle varchar(100),
	@EssayText	varchar(3000),
	@Flag int
AS
	INSERT INTO [dbo].[PaperGrades]
           (EssayID,[SpecialtyID]
           ,[Grade]
           ,[Class]
           ,[StudentID]
           ,[CreatedBy]
           ,[TotalScores]
           ,[WritingScores]
           ,[ScoreLevel],[EssayTitle]
           ,[EssayText]
           ,[Flag])
     VALUES
           (@EssayID,@SpecialtyID
           ,@Grade
           ,@Class
           ,@StudentID
           ,@CreatedBy
		   ,@TotalScores
           ,@WritingScores
           ,@ScoreLevel ,@EssayTitle
           ,@EssayText
           ,@Flag)
		SELECT @GradesID=@@IDENTITY
RETURN 0
