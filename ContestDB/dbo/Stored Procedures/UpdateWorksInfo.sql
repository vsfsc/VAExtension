-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateWorksInfo]

@WorksID bigint, 
@WorksName varchar(100),
@WorksTypeID int,
@WorksCode varchar(50),
@Members varchar(100),
@PeriodID bigint,
@SubmitProfile varchar(8000), 
@DesignIdeas varchar(max),
@KeyPoints varchar(2500),
@DemoURL  varchar(2000),
@Flag bigint,
@WorksState int,
@Score real,
@CreatedBy bigint,
@Created datetime
AS
 
  UPDATE    Works
   SET 
      WorksName=@WorksName,
      WorksTypeID=@WorksTypeID,
      WorksCode=@WorksCode,	       
	  PeriodID=@PeriodID,
      SubmitProfile=@SubmitProfile, 
      DesignIdeas= @DesignIdeas,
      KeyPoints=@KeyPoints,
      DemoURL=@DemoURL,
	  Flag=@Flag,
	  WorksState=@WorksState,
	  Score=@Score,
CreatedBy=@CreatedBy,
Created=@Created
   where WorksID=@WorksID
	
RETURN 0;
