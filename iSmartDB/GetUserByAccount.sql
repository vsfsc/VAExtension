IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserByAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserByAccount]
GO
-- =============================================
--通过帐号获取用户信息 xqx

CREATE PROCEDURE [dbo].[GetUserByAccount]
	@Account varchar(50)
AS
	SELECT [UserID]
      ,[Account]
      ,[Name]
      ,[IDCard]
      ,[Sex]
      ,[Telephone]
      ,[Email]
      ,[RoleID]
      ,[SchoolID]
      ,[CreatedBy]
      ,[Created]
      ,[ModifiedBy]
      ,[Modified]
      ,[StateID]
      ,[Flag]
	  ,[IP]
  FROM [dbo].[User] where [Account]=@Account and Flag=1
RETURN 0;