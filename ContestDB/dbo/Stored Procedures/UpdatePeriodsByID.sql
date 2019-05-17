-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePeriodsByID]
	(
	@PeriodID bigint,
@PeriodTitle varchar(100), 
@Require varchar(max),
@Number int,
@WorksTypeID bigint,  
@StartSubmit datetime, 
@EndSubmit datetime, 
@StartScore datetime, 
@EndScore datetime, 
@StartPublic datetime, 
@EndPublic datetime, 
@CreatedBy bigint,
@Created datetime,
@Flag int
)
AS
BEGIN
SET NOCOUNT ON;
	 UPDATE  Periods
             SET PeriodTitle =@PeriodTitle,			    
				 Require=@Require, 				
				 Number=@Number, 
				 WorksTypeID=@WorksTypeID,
				 StartSubmit=@StartSubmit, 
				 EndSubmit=@EndSubmit, 
				 StartScore=@StartScore, 
				 EndScore=@EndScore, 
                 StartPublic=@StartPublic, 
				 EndPublic=@EndPublic,
				 ModifiedBy=@CreatedBy,
                Modified=@Created,
				 Flag =@Flag
				 where PeriodID=@PeriodID
	 
	
	
END

