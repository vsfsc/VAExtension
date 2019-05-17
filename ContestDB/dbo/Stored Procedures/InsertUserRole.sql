
-- =============================================
--填加新数据 xqx 2012-6-19
-- =============================================
CREATE PROCEDURE [dbo].[InsertUserRole]
	@UserRoleID BIGINT OUTPUT,
	@UserID BIGINT, 
	@RoleID INT,
	@CreatedBy BIGINT,
	@Created DATETIME,
	@StateID INT,
	@ApprovedBy BIGINT,
	@Approved DATETIME,
	@ContestID BIGINT,
	@Flag BIGINT
AS
	INSERT INTO [UserRole]
           ([UserID]
           ,[RoleID]
           ,[CreatedBy]
           ,[Created]
           ,[StateID]
           ,[ContestID]
           ,[Flag])
     VALUES
           (@UserID,
           @RoleID,
           @CreatedBy,
           @Created,
           @StateID,
           @ContestID,
           @Flag)
   	 SELECT @UserRoleID=@@IDENTITY   
        
RETURN 0;
