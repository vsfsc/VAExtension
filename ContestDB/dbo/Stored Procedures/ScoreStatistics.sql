-- =============================================
-- Author:		mys
-- Create date: 2015-02-01
-- Description:	<成绩统计>
-- =============================================
CREATE PROCEDURE  [dbo].[ScoreStatistics]
	-- Add the parameters for the stored procedure here
	@periodID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   Course.CourseName AS 课程, Periods.PeriodTitle AS 期次, [User].Major AS 专业, [User].Class AS 班级, 
                [User].Name AS 姓名, [User].Account AS 账号, Works.WorksName AS 作品名称, Works.Score AS 成绩
FROM      Works INNER JOIN
                Periods ON Works.PeriodID = Periods.PeriodID INNER JOIN
                Course ON Periods.CourseID = Course.CourseID LEFT OUTER JOIN
                [User] ON Works.CreatedBy = [User].UserID
WHERE   ([User].Major = '机械类') AND (Works.Flag = 1) AND (Periods.PeriodID =@periodID)
ORDER BY Score
END
