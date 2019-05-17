CREATE PROCEDURE [dbo].[GetCollegeBySchoolID]
	@SchoolID int
AS
	SELECT [CollegeID]
      ,[CollegeName]
      ,[SchoolID]
      ,[Flag]
  FROM [dbo].[College]
  where SchoolID=@SchoolID
RETURN 0
