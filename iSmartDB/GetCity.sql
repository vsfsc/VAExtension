IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCity]
GO
CREATE PROCEDURE [dbo].[GetCity]
	@ParentID int
AS
	SET NOCOUNT ON;
    SELECT AreaID, AreaName, ParentID, AreaLevel, Flag
    FROM  Area
    where Flag=1 and ParentID=@ParentID
RETURN 0
