

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[UpdateSchool]
	(
	@SchoolID INT,
	@SchoolName VARCHAR(100),
	@SchoolCode VARCHAR(50),
	@AreaID INT,
	@CreatedBy BIGINT,
	@Created DATETIME,
	@Flag INT
)
AS
BEGIN
	 SET NOCOUNT ON;
	 IF(@Flag=0)
	 BEGIN
	 UPDATE    School
     SET   
     ModifiedBy =@CreatedBy, 
     Modified =@Created,    
     Flag =0
     WHERE SchoolID=@SchoolID
	 END
	 ELSE
	 BEGIN
	 UPDATE    School
     SET  
     SchoolName =@SchoolName, 
     SchoolCode =@SchoolCode, 
     AreaID =@AreaID, 
     ModifiedBy =@CreatedBy, 
     Modified =@Created, 
     Flag =@Flag
     WHERE SchoolID=@SchoolID
     END
END
