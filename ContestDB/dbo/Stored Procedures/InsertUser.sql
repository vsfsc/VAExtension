
-- =============================================
--用户表填加新数据 xqx 2012-3-8

CREATE PROCEDURE [dbo].[InsertUser]
			@UserID BIGINT OUTPUT,
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
           @Flag BIGINT=1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[User]
           ([Account]
           ,[Name]
           ,[IDCard]
           ,[Sex]
           ,[Telephone]
           ,[Email]
           ,[Duty]
           ,[ProfessionalTitle]
           ,[Department]
           ,[Major]
           ,[Class],Grade
           ,[RoleID]
           ,[SchoolID]
           ,[CreatedBy]
           ,[Created]
           ,[ModifiedBy]
           ,[Modified]
           ,[StateID]
           ,[ApprovedBy]
           ,[Approved]
           ,[Flag])
     VALUES
           (@Account,
           @Name,
           @IDCard, 
           @Sex,
           @Telephone,
           @Email,
           @Duty,
           @ProfessionalTitle,@Department,
           @Major,
           @Class,@Grade,
           @RoleID,
           @SchoolID,
           @CreatedBy,
           @Created,
           @ModifiedBy,
           @Modified,
           @StateID,
           @ApprovedBy ,
           @Approved ,
           @Flag)
	 SELECT @UserID=@@IDENTITY   
END



