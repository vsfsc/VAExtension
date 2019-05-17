CREATE PROCEDURE [dbo].[GetSpecialtyInfo]
	@SpecialtyID int
AS
	SELECT   dbo.School.SchoolID, dbo.School.SchoolName, dbo.College.CollegeID, dbo.College.CollegeName, 
                dbo.Specialty.SpecialtyID, dbo.Specialty.SpecialtyName
FROM      dbo.School INNER JOIN
                dbo.College ON dbo.School.SchoolID = dbo.College.SchoolID INNER JOIN
                dbo.Specialty ON dbo.College.CollegeID = dbo.Specialty.CollegeID
				where  Specialty.SpecialtyID=@SpecialtyID
RETURN 0
