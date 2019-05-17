

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================



CREATE PROCEDURE [dbo].[GetWorksForSummary]
@SchoolID INT,
@OneWorksTypeID INT,
@TwoWorksTypeID INT,
@IsSubmit INT
AS
DECLARE @strSQL NVARCHAR(4000)
SET @strSQL=' SELECT WorksType.WorksTypeName, Works.WorksTypeID,
Works.WorksID, Works.WorksName, Works.SchoolID, Works.StateID,
s.SchoolCode,ws.WorksCode, ws.StateID as TStateID  FROM   Works 
INNER JOIN   WorksType ON Works.WorksTypeID = WorksType.WorksTypeID
inner join School s
on Works.SchoolID=s.SchoolID '
IF @IsSubmit=1
BEGIN
SET @strSQL = @strSQL+' left join WorksSubmit ws
on ws.WorksID=Works.WorksID'
END
ELSE
BEGIN
SET @strSQL = @strSQL+' Inner join WorksSubmit ws
on ws.WorksID=Works.WorksID'
END
SET @strSQL = @strSQL+' where Works.Flag>0'
IF @SchoolID !=0
BEGIN
SET @strSQL = @strSQL+' and Works.SchoolID=@SchoolID'
END
IF @OneWorksTypeID!=0 AND @TwoWorksTypeID=0
BEGIN
SET @strSQL = @strSQL+'  and  Works.WorksTypeID in 
(
select WorksTypeID from WorksType 
where ParentID in (select WorksTypeID from WorksType
where ParentID=@OneWorksTypeID) or ParentID=@OneWorksTypeID or WorksTypeID=@OneWorksTypeID)'
END
IF @TwoWorksTypeID!=0
BEGIN
SET @strSQL = @strSQL+' and  Works.WorksTypeID in 
(select WorksTypeID from WorksType 
where ParentID=@TwoWorksTypeID or WorksTypeID=@TwoWorksTypeID)'
END

SET @strSQL = @strSQL+' order By Works.WorksTypeID'

EXECUTE sp_executesql @strSQL,

N' @SchoolID int,
@OneWorksTypeID int,
@TwoWorksTypeID int,
@IsSubmit int
',
@SchoolID,
@OneWorksTypeID,
@TwoWorksTypeID,
@IsSubmit 
RETURN 0;
