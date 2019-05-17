-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[GetMyWorks]
	-- Add the parameters for the stored procedure here
	@UserID VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  Works.WorksID ,
        Works.WorksName ,
        Course.CourseName ,
        Periods.PeriodTitle ,
        WorksType.WorksTypeName ,
        State.StateName ,
        Works.Score ,
        UserWorks.Relationship ,
        Periods.CourseID ,
        Works.PeriodID ,
        UserWorks.UserID
FROM    Course
        RIGHT OUTER JOIN WorksType
        RIGHT OUTER JOIN UserWorks
        LEFT OUTER JOIN Works ON UserWorks.WorksID = Works.WorksID
        LEFT OUTER JOIN State ON Works.WorksState = State.StateID
        LEFT OUTER JOIN Periods ON Works.PeriodID = Periods.PeriodID ON WorksType.WorksTypeID = Works.WorksTypeID ON Course.CourseID = Periods.CourseID
WHERE   ( Works.Flag > 0 )
        AND ( UserWorks.UserID = @UserID )
END
