

CREATE PROCEDURE [dbo].[UpdateWorksImages]
@WorksImagesID BIGINT,
@ModifiedBy BIGINT, 
@Modified DATETIME
AS
	UPDATE    WorksImages
    SET ModifiedBy =@ModifiedBy, 
        Modified =@Modified, 
        Flag =0
    WHERE WorksImagesID=@WorksImagesID
