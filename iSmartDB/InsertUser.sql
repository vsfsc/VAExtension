IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertUser]
GO
-- =============================================
--用户表填加新数据 xqx 2012-3-8

CREATE PROCEDURE [dbo].[InsertUser]
			@UserID bigint output,
			@Account  varchar(50),
           @Name varchar(50),
           @IDCard varchar(18),
           @Sex bit,
           @Telephone varchar(50),
           @Email varchar(100),
           @RoleID int,
           @SchoolID int,
           @CreatedBy bigint,
           @Created datetime,
           @ModifiedBy bigint,
           @Modified datetime,
           @StateID int,
           @Flag bigint=1,
		   @IP varchar(15)
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
           ,[RoleID]
           ,[SchoolID]
           ,[CreatedBy]
           ,[Created]
           ,[ModifiedBy]
           ,[Modified]
           ,[StateID]
           ,[Flag]
		   ,[IP])
     VALUES
           (@Account,
           @Name,
           @IDCard, 
           @Sex,
           @Telephone,
           @Email,
           @RoleID,
           @SchoolID,
           @CreatedBy,
           @Created,
           @ModifiedBy,
           @Modified,
           @StateID,
           @Flag,
		   @IP)
	 SELECT @UserID=@@IDENTITY   
END