IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRole]
GO
CREATE PROCEDURE [dbo].[GetRole]
	 
AS
	SELECT RoleID,RoleName from [Role]
RETURN 0
