



CREATE PROCEDURE [dbo].[UpdateWorksFile]
@WorksFileID BIGINT,
@Type int,
@FileName VARCHAR(200),
@ModifiedBy BIGINT, 
@Modified DATETIME,
@FilePath varchar(1000),
@FileSize int,
@Flag bigint

AS
	UPDATE  WorksFile
    SET         FileSize =@FileSize, 
				ModifiedBy =@ModifiedBy, 
				Modified =@Modified, 
				Flag =@Flag
    WHERE WorksFileID=@WorksFileID


