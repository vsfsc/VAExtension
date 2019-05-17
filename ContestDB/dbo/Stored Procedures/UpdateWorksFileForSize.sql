-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateWorksFileForSize 
@WorksFileID BIGINT,
@Type int,
@FileName varchar(100),
@ModifiedBy BIGINT, 
@Modified DATETIME,
@FilePath varchar(1000),
@FileSize int,
@Flag bigint

AS
	UPDATE  WorksFile
    SET         FileSize =@FileSize, 
				ModifiedBy =@ModifiedBy, 
				Modified =@Modified 
				
    WHERE WorksFileID=@WorksFileID

