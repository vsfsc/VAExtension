CREATE PROCEDURE [dbo].[GetAllSchool]
	
AS
	SELECT [SchoolID]
      ,[SchoolName]
      ,[SchoolCode]
      ,[Description]
      ,[CreatedBy]
      ,[Created]
      ,[ModifiedBy]
      ,[Modified]
      ,[Flag]
  FROM [dbo].[School]
  where Flag=1
RETURN 0
