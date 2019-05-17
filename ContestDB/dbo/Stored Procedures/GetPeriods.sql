-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:获取所有期次
-- =============================================
CREATE PROCEDURE [dbo].[GetPeriods]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT  *
    FROM    dbo.Periods
    ORDER BY StartSubmit DESC
END
