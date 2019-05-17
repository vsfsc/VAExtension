

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateWorksType]
	(
	@WorksTypeID INT,
	@WorksTypeName VARCHAR(100),
	@ParentID INT,
	@LevelID INT,
	@CreatedBy BIGINT,
	@Created DATETIME,
	@Flag INT,
	@F INT
	)
AS
BEGIN
SET NOCOUNT ON;
	 
	  IF(@Flag=0)
		 BEGIN
		 UPDATE WorksType
		 SET  Flag =0
		 WHERE WorksTypeID=@WorksTypeID      
	     END
	  ELSE	 
		 BEGIN
		 UPDATE WorksType
		 SET 
		  WorksTypeName =@WorksTypeName,
		  ModifiedBy =@CreatedBy , 
		  Modified =@Created		 
		 WHERE WorksTypeID=@WorksTypeID 	
		 END
	
	
END
