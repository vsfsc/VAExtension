-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetResetPasswordByID]
	-- Add the parameters for the stored procedure here
	@OperateID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [OperateID]
      ,[Account]
      ,[Email]
      ,[CheckCode]
      ,[StartTime]
      ,[EndTime]
      ,[IsFinished]
   FROM [dbo].[ResetPassword] where OperateID=@OperateID
END

