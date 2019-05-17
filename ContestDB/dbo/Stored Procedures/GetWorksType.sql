
--获取作品类别 xqx
CREATE PROCEDURE [dbo].[GetWorksType]
AS
	SELECT [WorksTypeID]
      ,[WorksTypeName],[Description]
      ,[ParentID]
      ,[LevelID]
      ,[CreatedBy]
      ,[Created]
      ,[ModifiedBy]
      ,[Modified]
      ,[Flag]
  FROM [WorksType]
  where Flag=1 
  
  RETURN 0;