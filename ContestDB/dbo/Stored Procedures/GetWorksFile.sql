

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================



CREATE PROCEDURE [dbo].[GetWorksFile]
@WorksID BIGINT,
@Type INT
AS
 if( @Type=4)
 begin
	SELECT     WorksFileID, WorksID, Type, FileName, FilePath,FileSize, CreatedBy,Created,Flag
    FROM         WorksFile
    WHERE WorksID=@WorksID AND  Type=@Type AND Flag=1
	end
else 
begin
SELECT     WorksFileID, WorksID, Type, FileName, FilePath,FileSize, CreatedBy,Created,Flag
    FROM         WorksFile
    WHERE WorksID=@WorksID AND  Type<>4 AND Flag=1
	end
RETURN 0;
