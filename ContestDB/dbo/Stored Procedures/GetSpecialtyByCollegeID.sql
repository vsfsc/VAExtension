CREATE PROCEDURE [dbo].[GetSpecialtyByCollegeID]
	@CollegeID int
AS
	SELECT [SpecialtyID]
      ,[SpecialtyName]
      ,[CollegeID]
      ,[Flag]
  FROM [dbo].[Specialty]
  where CollegeID=@CollegeID and Flag=1
RETURN 0
