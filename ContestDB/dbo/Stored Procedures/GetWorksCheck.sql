

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWorksCheck]
@WorksName VARCHAR(100),
@AreaID INT,
@SchoolID INT,
@StateID INT,
@OneWorksTypeID INT,
@TwoWorksTypeID INT,
@WorksCode VARCHAR(50)
AS
	DECLARE @strSQL NVARCHAR(4000)
SET @strSQL='select w.WorksID, w.WorksName, w.TeamName, w.WorksTypeID, ws.WorksCode,w.SchoolID,
ws.WorksSubmitID,ws.StateID,wt.WorksTypeName,s.SchoolName, s.SchoolCode,s.AreaID,a.AreaName,st.StateName from
(SELECT     WorksID, WorksName, TeamName, WorksTypeID, WorksCode,SchoolID,Flag
FROM         Works
where Flag=1 and 
WorksID in (
SELECT     WorksID
FROM         WorksSubmit
where Flag=1
)) w
left join 
(
SELECT     WorksSubmitID, WorksID, Flag, StateID,WorksCode
FROM         WorksSubmit
where Flag=1
) ws
on w.WorksID=ws.WorksID
left join 
(SELECT     WorksTypeID, WorksTypeName
FROM         WorksType) wt
on w.WorksTypeID=wt.WorksTypeID
left join 
(
SELECT     SchoolID, SchoolName, SchoolCode,AreaID,Flag
FROM         School
) s
on s.SchoolID=w.SchoolID 
INNER JOIN Area  a
ON s.AreaID = a.AreaID
left join State st
on ws.StateID=st.StateID
where w.Flag=1 and s.Flag=1 and a.Flag=1 and ws.StateID=@StateID'
IF @WorksName!=''
BEGIN
SET @strSQL = @strSQL +'  and  w.WorksName like  @WorksName '
END
IF @AreaID!=0 AND @SchoolID=0
BEGIN
SET @strSQL = @strSQL+' and s.AreaID=@AreaID'
END
IF @SchoolID!=0
BEGIN
SET @strSQL = @strSQL+' and s.SchoolID=@SchoolID'
END
IF @OneWorksTypeID!=0 AND @TwoWorksTypeID=0
BEGIN
SET @strSQL = @strSQL+'  and  w.WorksTypeID in 
(
select WorksTypeID from WorksType 
where ParentID in(select WorksTypeID from WorksType
where ParentID=@OneWorksTypeID) or ParentID=@OneWorksTypeID or WorksTypeID=@OneWorksTypeID)'
END
IF @TwoWorksTypeID!=0
BEGIN
SET @strSQL = @strSQL+' and  w.WorksTypeID in 
(select WorksTypeID from WorksType 
where ParentID=@TwoWorksTypeID or w.WorksTypeID=@TwoWorksTypeID)'
END
IF @WorksCode!=''
BEGIN
SET @strSQL = @strSQL+ ' and ws.WorksCode like @WorksCode '
END

EXECUTE sp_executesql @strSQL,

N' @WorksName varchar(100),
@AreaID int,
@SchoolID int,
@StateID int,
@OneWorksTypeID int,
@TwoWorksTypeID int,
@WorksCode varchar(50)
',
@WorksName,
@AreaID,
@SchoolID,
@StateID,
@OneWorksTypeID,
@TwoWorksTypeID,
@WorksCode

RETURN 0;
