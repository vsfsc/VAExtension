CREATE PROCEDURE [dbo].[GetCity]
	@ParentID int
AS
	SET NOCOUNT ON;
    SELECT AreaID, AreaName, ParentID, AreaLevel, Flag
    FROM  Area
    where Flag=1 and ParentID=@ParentID
RETURN 0

