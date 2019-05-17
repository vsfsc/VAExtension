

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateWorksTypeForSubmit]
@WorksID BIGINT,
@WorksTypeID INT,
@ModifiedBy BIGINT,
@Modified DATETIME
AS
	 UPDATE Works
    SET WorksTypeID=@WorksTypeID,
        ModifiedBy=@ModifiedBy,
        Modified=@Modified
    WHERE WorksID=@WorksID
RETURN 0;
