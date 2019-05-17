-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateResetPassword]
	-- Add the parameters for the stored procedure here
	@OperateID  bigint
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
	UPDATE [dbo].[ResetPassword]
   SET Account=@Account,Email=@Email,CheckCode=@CheckCode,StartTime=@StartTime,[EndTime] = @EndTime,IsFinished=@IsFinished
	WHERE OperateID=@OperateID
END

