
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertPeriods]
@PeriodID bigint output,
@PeriodTitle varchar(100), 
@CourseID bigint,
@Require varchar(Max),  
@WorksTypeID bigint,
@Number int, 
@StartSubmit datetime, 
@EndSubmit datetime, 
@StartScore datetime, 
@EndScore datetime, 
@StartPublic datetime, 
@EndPublic datetime, 
@CreatedBy bigint,
@Created datetime,
@Flag int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Periods
    (PeriodTitle, 
     CourseID,
     Require,  
     WorksTypeID, 
	 Number, 
     StartSubmit, 
     EndSubmit, 
     StartScore, 
     EndScore, 
     StartPublic, 
     EndPublic, 
	 CreatedBy,
	 Created,
     Flag)
VALUES   (@PeriodTitle, 
     @CourseID,
     @Require,  
     @WorksTypeID,
	 @Number,  
     @StartSubmit, 
     @EndSubmit, 
     @StartScore, 
     @EndScore, 
     @StartPublic,
	 @EndPublic,
	 @CreatedBy,
	 @Created,
     @Flag)
		  SELECT @PeriodID=@@IDENTITY
END

