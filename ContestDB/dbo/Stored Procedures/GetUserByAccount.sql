
-- =============================================
--通过帐号获取用户信息 xqx

CREATE PROCEDURE [dbo].[GetUserByAccount]
	@Account VARCHAR(50)
AS
	SELECT [UserID]
      ,[Account]
      ,[Name]
      ,[IDCard]
      ,[Sex]
      ,[Telephone]
      ,[Email]
      ,[Duty]
      ,[ProfessionalTitle],Department
      ,[Major]
      ,[Class]
      ,[Grade]
      ,[RoleID]
      ,[SchoolID]
      ,[CreatedBy]
      ,[Created]
      ,[ModifiedBy]
      ,[Modified]
      ,[StateID]
      ,[ApprovedBy]
      ,[Approved]
      ,[Flag]
  FROM [dbo].[User] WHERE [Account]=@Account AND Flag=1
RETURN 0;
