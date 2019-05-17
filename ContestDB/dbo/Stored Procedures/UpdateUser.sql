
-- =============================================
--用户表编辑数据 xqx 2012-3-8


CREATE PROCEDURE [dbo].[UpdateUser]
	@UserID BIGINT,
			@Account  VARCHAR(50),
           @Name VARCHAR(50),
           @IDCard VARCHAR(18),
           @Sex BIT,
           @Telephone VARCHAR(50),
           @Email VARCHAR(100),
           @Duty VARCHAR(100),
           @ProfessionalTitle VARCHAR(100),
           @Department VARCHAR(100),
           @Major VARCHAR(100),
           @Class VARCHAR(50),
           @Grade VARCHAR(50),
           @RoleID INT,
           @SchoolID INT,
           @CreatedBy BIGINT,
           @Created DATETIME,
           @ModifiedBy BIGINT,
           @Modified DATETIME,
           @StateID INT,
           @ApprovedBy BIGINT,
           @Approved DATETIME,
           @Flag BIGINT
AS
	UPDATE  [dbo].[User]
   SET [Account]=@Account
      ,[Name]=@Name
      ,[IDCard]=@IDCard
      ,[Sex]=@Sex
      ,[Telephone]=@Telephone
      ,[Email]=@Email
      ,[Duty]=@Duty
      ,[ProfessionalTitle]=@ProfessionalTitle
      ,[Department]=@Department
      ,[Major]=@Major
      ,[Class]=@Class
      ,[Grade]=@Grade
      ,[RoleID]=@RoleID
      ,[SchoolID]=@SchoolID
      ,[CreatedBy]=@CreatedBy
      ,[Created]=@Created
      ,[ModifiedBy]=@ModifiedBy
      ,[Modified]=@Modified
      ,[StateID]=@StateID
      ,[ApprovedBy]=@ApprovedBy
      ,[Approved]=@Approved
      ,[Flag]=@Flag
 WHERE UserID=@UserID
RETURN 0;
