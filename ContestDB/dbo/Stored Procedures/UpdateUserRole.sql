
-- =============================================
--更新数据 xqx 2012-6-19
-- =============================================

CREATE PROCEDURE [dbo].[UpdateUserRole]
	@UserRoleID BIGINT,
	@StateID INT,
	@ApprovedBy BIGINT,
	@Approved DATETIME,
	@Flag BIGINT
AS
	UPDATE [UserRole]
	SET [StateID] = @StateID,
      [ApprovedBy] =@ApprovedBy,
      [Approved] = @Approved,
      [Flag] = @Flag
 WHERE UserRoleID=@UserRoleID
RETURN 0;
