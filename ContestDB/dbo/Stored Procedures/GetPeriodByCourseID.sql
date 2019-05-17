-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPeriodByCourseID]
	-- Add the parameters for the stored procedure here
	@CourseID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT  Periods.[PeriodID] ,
            Periods.[PeriodTitle] ,
            Periods.Require ,
            Periods.[CourseID] ,
            Periods.WorksTypeID ,
            Periods.Number ,
            Periods.[StartSubmit] ,
            Periods.[EndSubmit] ,
            Periods.[StartScore] ,
            Periods.[EndScore] ,
            Periods.[StartPublic] ,
            Periods.[EndPublic] ,
            Periods.[CreatedBy] ,
            Periods.[Created] ,
            Periods.[ModifiedBy] ,
            Periods.[Modified] ,
            Periods.[Flag] ,
            WorksType.LevelID ,
            WorksType.ParentID
    FROM    [dbo].[Periods]
            LEFT JOIN WorksType ON Periods.WorksTypeID = WorksType.WorksTypeID
    WHERE   CourseID = @CourseID
            AND Periods.Flag = 1
    ORDER BY [StartSubmit] DESC
END
