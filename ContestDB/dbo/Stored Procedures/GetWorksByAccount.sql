-- =============================================
-- Author:		xqx
-- Create date: <Create Date,,>
-- Description:	获取当前登录帐号给定批期的作品ID
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksByAccount]
	-- Add the parameters for the stored procedure here
	@PeriodID bigint,
	@Account varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   dbo.UserWorks.UserID, dbo.UserWorks.WorksID, dbo.Works.PeriodID, dbo.[User].Account
FROM      dbo.Works INNER JOIN
                dbo.UserWorks ON dbo.Works.WorksID = dbo.UserWorks.WorksID INNER JOIN
                dbo.[User] ON dbo.UserWorks.UserID = dbo.[User].UserID
				WHERE   (dbo.UserWorks.Flag = 1 and [User].Account=@Account and PeriodID=@PeriodID and dbo.Works.Flag>0)
END
