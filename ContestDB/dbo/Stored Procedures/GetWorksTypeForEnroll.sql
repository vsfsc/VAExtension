

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[GetWorksTypeForEnroll]

AS
SELECT     WorksTypeID, WorksTypeName, ParentID, LevelID
FROM         WorksType
WHERE Flag=1 AND LevelID=0
RETURN 0;
