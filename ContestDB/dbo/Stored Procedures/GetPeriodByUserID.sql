-- =============================================
-- Author:		mys
-- Create date: 2014-11-14
-- Description:	获取该用户ID创建的所有期次,但只展示在本课程下的.
-- =============================================
CREATE PROCEDURE [dbo].[GetPeriodByUserID]
	-- Add the parameters for the stored procedure here
@UserID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  Periods.PeriodID ,
        Periods.PeriodTitle ,
        sTable.WorksCount ,
        Course.CourseName ,
        Periods.StartSubmit ,
        Periods.EndSubmit ,
        Periods.StartScore ,
        Periods.EndScore ,
        Periods.StartPublic ,
        Periods.EndPublic,
		Periods.CourseID
FROM    Periods
        LEFT OUTER JOIN Course ON Periods.CourseID = Course.CourseID
        LEFT OUTER JOIN ( SELECT DISTINCT
                                    MainTable.PeriodID ,
                                    ISNULL(SubTable.SubNum, 0) AS WorksCount
                          FROM      Periods AS MainTable
                                    LEFT OUTER JOIN ( SELECT  PeriodID ,
                                                              COUNT(WorksID) AS SubNum
                                                      FROM    Works
                                                      WHERE   ( IsSample = 0 )
                                                              AND ( Flag > 0 )
                                                      GROUP BY PeriodID
                                                    ) AS SubTable ON MainTable.PeriodID = SubTable.PeriodID
                        ) AS sTable ON Periods.PeriodID = sTable.PeriodID
WHERE   ( Periods.CreatedBy =  @UserID)

END
