-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksNumByPeriodID] 
 
@PeriodID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT   COUNT(WorksID) AS Num
   FROM     Works
   WHERE    Flag > 0
			AND IsSample=0
            AND PeriodID = @PeriodID
END
