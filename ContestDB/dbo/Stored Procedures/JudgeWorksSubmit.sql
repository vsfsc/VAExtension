

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[JudgeWorksSubmit]
@WorksSubmitID BIGINT,
@StateID INT
AS
	 SELECT     WorksSubmitID, StateID
  FROM       WorksSubmit
  WHERE WorksSubmitID=@WorksSubmitID AND StateID=@StateID
RETURN 0;
