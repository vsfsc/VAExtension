
-- =============================================
-- 作品类别  hx 2012-3-14
CREATE PROCEDURE [dbo].[InsertWorksType]
	(
	@WorksTypeID INT OUTPUT,
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
IF(@F=0)
BEGIN
SET @ParentID=0;
SET @LevelID=0;
END
ELSE
BEGIN
SELECT @ParentID=WorksTypeID,@LevelID=LevelID+1 FROM WorksType 
WHERE WorksTypeID=@F
END

INSERT INTO WorksType
(
WorksTypeName, 
ParentID, 
LevelID, 
CreatedBy, 
Created, 
Flag)
VALUES     
(
@WorksTypeName,
@ParentID,
@LevelID,
@CreatedBy,
@Created,
@Flag
)
 SELECT @WorksTypeID=@@IDENTITY   
 END
