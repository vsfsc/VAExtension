

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetWorksForCheck]
@WorksName VARCHAR(100),
@AreaID INT,
@SchoolID INT,
@StateID INT
AS
BEGIN    

SET NOCOUNT ON;
DECLARE @strSQL NVARCHAR(4000)
SET @strSQL='SELECT  Works.WorksID,Works.WorksName,Works.TeamName,Works.WorksTypeID, 
Works.WorksCode,Works.WorksGuid,Works.SchoolID,Works.StateID,School.SchoolName,School.AreaID,Area.AreaName
FROM    Works 
INNER JOIN  School  
ON Works.SchoolID = School.SchoolID 
INNER JOIN Area 
ON School.AreaID = Area.AreaID
where Works.Flag>0 and School.Flag=1 and Area.Flag=1 and Works.StateID=@StateID'
IF @WorksName!=''
BEGIN
SET @strSQL = @strSQL +' and Works.WorksName like  @WorksName '
END
IF @AreaID!=0 AND @SchoolID=0
BEGIN
SET @strSQL = @strSQL+' and School.AreaID=@AreaID'
END
IF @SchoolID!=0
BEGIN
SET @strSQL = @strSQL+' and School.SchoolID=@SchoolID'
END
EXECUTE sp_executesql @strSQL,

N' @WorksName varchar(100),
@AreaID int,
@SchoolID int,
@StateID int
',
@WorksName,
@AreaID,
@SchoolID,
@StateID



END
