
-- =============================================
--通过帐号获取用户信息 xqx

CREATE PROCEDURE [dbo].[GetUserByIDCard]
	@IDCard VARCHAR(18)
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
  FROM [dbo].[User] WHERE [IDCard]=@IDCard AND Flag=1
RETURN 0;
