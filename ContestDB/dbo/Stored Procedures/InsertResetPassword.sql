-- =============================================
-- Author:		<Author,,Name>
-- Create date: 2015-12-28
-- Description:	忘记密码操作
-- =============================================
CREATE PROCEDURE [dbo].[InsertResetPassword]
	-- Add the parameters for the stored procedure here
	@OperateID  bigint output
           ,@Account  varchar(50)
           ,@Email  varchar(100)
           ,@CheckCode  varchar(4) 
           ,@StartTime  datetime 
           ,@EndTime  datetime  ,@IsFinished bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[ResetPassword]
           ([Account]
           ,[Email]
           ,[CheckCode]
           ,[StartTime]
           ,[EndTime],IsFinished)
     VALUES
           (@Account
           ,@Email
           ,@CheckCode
           ,@StartTime
           ,@EndTime  ,@IsFinished)
	SELECT @OperateID=@@IDENTITY   
END

