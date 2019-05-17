-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksForMeToScore]
	-- Add the parameters for the stored procedure here
	@userAccount VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT dbo.WorksExpert.WorksID,dbo.Periods.PeriodTitle,dbo.Works.WorksCode,dbo.State.StateName
	FROM  dbo.WorksExpert,dbo.[User],dbo.Works,dbo.Periods,dbo.State
	WHERE
    dbo.[User].UserID=dbo.WorksExpert.ExpertID 
	AND dbo.WorksExpert.Flag=1
	AND dbo.[User].Account=@userAccount
	AND State.StateID=dbo.Works.WorksState

END
