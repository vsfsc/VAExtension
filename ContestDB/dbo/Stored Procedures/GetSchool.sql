
-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSchool]
(
@AreaID int,
@SchoolID int
)
AS
BEGIN
	SET NOCOUNT ON;

if @AreaID=0 and @SchoolID=0
begin
	SELECT     School.SchoolID, School.SchoolName, School.SchoolCode, School.AreaID,Area.AreaName
	FROM         School 
	INNER JOIN Area 
	ON School.AreaID = Area.AreaID
	WHERE   School.Flag = 1
end
else if @AreaID=0 and @SchoolID!=0
begin
SELECT     School.SchoolID, School.SchoolName, School.SchoolCode, School.AreaID,Area.AreaName
		FROM         School 
		INNER JOIN Area 
		ON School.AreaID = Area.AreaID
		WHERE   School.Flag = 1 and  School.SchoolID=@SchoolID
end
else if @AreaID!=0 and @SchoolID=0
begin
	SELECT     School.SchoolID, School.SchoolName, School.SchoolCode, School.AreaID,Area.AreaName
	FROM         School 
	INNER JOIN Area 
	ON School.AreaID = Area.AreaID
	WHERE   School.Flag = 1 and School.AreaID=@AreaID	
end
else if @AreaID!=0 and @SchoolID!=0
begin
SELECT     School.SchoolID, School.SchoolName, School.SchoolCode, School.AreaID,Area.AreaName
		FROM         School 
		INNER JOIN Area 
		ON School.AreaID = Area.AreaID
		WHERE   School.Flag = 1 and  School.SchoolID=@SchoolID
end
END



