

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[UpdateWorksCheck]
@WorksSubmitID BIGINT,
@StateID INT,
@ApprovedBy BIGINT,
@Approved DATETIME,
@BackReason VARCHAR(2000)

AS
IF @StateID=2
BEGIN
	UPDATE    WorksSubmit
    SET       StateID =@StateID, 
              ApprovedBy =@ApprovedBy, 
              Approved =@Approved 
    WHERE WorksSubmitID=@WorksSubmitID
    END
ELSE
BEGIN
UPDATE    WorksSubmit
    SET       StateID =@StateID, 
              ApprovedBy =@ApprovedBy, 
              Approved =@Approved,
              BackReason=@BackReason
            
    WHERE WorksSubmitID=@WorksSubmitID
END
RETURN 0;
