-- =============================================
-- Author:		xqx
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCourseByName] 
	-- Add the parameters for the stored procedure here
	 @Name varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [CourseID]
      ,[CourseName]
      
      ,[Url]
      ,[Flag]
  FROM [dbo].[Course] Where CourseName=@Name and Flag=1
END
