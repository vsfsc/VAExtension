
CREATE PROCEDURE [dbo].[GetUserRoleByUserID]
	@UserID BIGINT
AS
	SELECT [UserRoleID]
      ,[UserID]
      ,[RoleID]
      ,[CreatedBy]
      ,[Created]
      ,[StateID]
      ,[ApprovedBy]
      ,[Approved]
      ,[ContestID]
      ,[Flag]
  FROM [UserRole]
  WHERE UserID=@UserID AND Flag=1
RETURN 0;
