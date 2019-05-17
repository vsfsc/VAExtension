-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPeriodsByID] 
@PeriodID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT  PeriodID ,
            PeriodTitle ,
            CourseID ,
            Require ,
            Periods.WorksTypeID ,
            Number ,
            StartSubmit ,
            EndSubmit ,
            StartScore ,
            EndScore ,
            StartPublic ,
            EndPublic ,
            Periods.CreatedBy ,
            Periods.Created ,
            Periods.ModifiedBy ,
            Periods.Modified ,
            Periods.Flag ,
            WorksType.ParentID ,
            WorksType.LevelID
    FROM    Periods
            LEFT JOIN WorksType ON Periods.WorksTypeID = WorksType.WorksTypeID
    WHERE   Periods.Flag = 1
            AND Periods.PeriodID = @PeriodID
    
END
