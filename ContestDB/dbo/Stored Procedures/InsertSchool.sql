

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertSchool]
(
	@SchoolID INT OUTPUT,
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
    
    INSERT INTO School
	  (	  
	   SchoolName, 
	   SchoolCode, 
	   AreaID, 
	   CreatedBy, 
	   Created, 
	   Flag
	   )
    VALUES 
	 (    
		@SchoolName,
		@SchoolCode,
		@AreaID,
		@CreatedBy,
		@Created,
		@Flag
	) 
	SELECT @SchoolID=@@IDENTITY
END
