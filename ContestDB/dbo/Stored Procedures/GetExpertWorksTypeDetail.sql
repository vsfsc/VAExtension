
-- =============================================
--获取专家类别，在分组时显示 xqx 2012-4-17
-- =============================================

CREATE PROCEDURE [dbo].[GetExpertWorksTypeDetail]
	
AS
	SELECT     dbo.ExpertWorksType.UserID, dbo.WorksType.WorksTypeName
	FROM         dbo.ExpertWorksType INNER JOIN
						  dbo.WorksType ON dbo.ExpertWorksType.WorksTypeID = dbo.WorksType.WorksTypeID
	WHERE     (dbo.WorksType.Flag = 1) AND (dbo.ExpertWorksType.Flag = 1)
RETURN 0;
