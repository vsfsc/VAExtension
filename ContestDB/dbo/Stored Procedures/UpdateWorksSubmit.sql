-- =============================================
--更新作品分数 xqx 2012-06-28
-- =============================================

CREATE PROCEDURE [dbo].[UpdateWorksSubmit]
	@WorksID BIGINT,
	@Score REAL,
	@WorksState INT
AS
    UPDATE  [Works]
    SET     [Score] = @Score,
            WorksState = @WorksState
    WHERE   [WorksID] = @WorksID
RETURN 0;
