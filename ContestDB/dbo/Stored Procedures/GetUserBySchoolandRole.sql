
-- =============================================
--获取需要审批的用户的信息 xqx 2012-6-20改为多个角色
-- =============================================

	CREATE PROCEDURE [dbo].[GetUserBySchoolandRole]
		@AreaID INT,
		@SchoolID INT, 
		@RoleID INT,
		@StateID INT =0
	AS
		SET NOCOUNT ON;
		DECLARE @strSQL NVARCHAR(1000) 

		SET @strSQL='SELECT  [User].UserID, [User].Account, [User].Name, [User].IDCard, CASE Sex WHEN 1 THEN ''男'' ELSE ''女'' END AS ''Sex'', 
					  [User].Telephone, [User].Duty, [User].ProfessionalTitle, [User].Department, [User].Major, [User].Class, [User].SchoolID, 
					   [User].Grade,  School.SchoolName,School.AreaID ,Role.RoleName, [User].Email, UserRole.UserRoleID, UserRole.RoleID, UserRole.CreatedBy, UserRole.Created, UserRole.StateID, UserRole.ApprovedBy, UserRole.Approved, UserRole.ContestID, UserRole.Flag
FROM         [User] INNER JOIN
                      School ON [User].SchoolID = School.SchoolID INNER JOIN
                      Area ON School.AreaID = Area.AreaID RIGHT OUTER JOIN
                      UserRole ON [User].UserID = UserRole.UserID INNER JOIN
					  Role ON UserRole.RoleID = Role.RoleID 
 where [User].Flag=1 and UserRole.StateID=@StateID and UserRole.Flag=1'
	  IF @AreaID>0
		BEGIN
			SET @strSQL = @strSQL + ' AND [School].AreaID=@AreaID'
		END
	  IF @SchoolID>0
		BEGIN
			SET @strSQL = @strSQL + ' AND [User].SchoolID=@SchoolID'
		END
	  IF @RoleID>0
		BEGIN
			SET @strSQL = @strSQL + ' AND [UserRole].RoleID=@RoleID'
		END
		EXECUTE sp_executesql @strSQL,N'@AreaID int,@SchoolID int,@RoleID int,@StateID int',@AreaID,@SchoolID,@RoleID,@StateID
 
RETURN 0;

