-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertWorks]
@WorksID bigint output, 
@WorksName varchar(100), 
@WorksTypeID int,
@PeriodID bigint,
@WorksCode varchar(50),
@IsSample int,
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
	INSERT INTO Works(   
    WorksName,     
    WorksTypeID,   
    PeriodID,
	WorksCode,
	IsSample,
	SubmitProfile, 
	DesignIdeas,
	KeyPoints,
	DemoURL,
	Flag,
	WorksState,
	Score,
    CreatedBy,
    Created   
    )
VALUES (
@WorksName,
@WorksTypeID, 
@PeriodID,
@WorksCode,
@IsSample,
@SubmitProfile, 
@DesignIdeas,
@KeyPoints,
@DemoURL,  
@Flag,
@WorksState,
@Score,
@CreatedBy,
@Created  
)
SELECT @WorksID=@@IDENTITY 


