

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[InsertWorksImages]
@WorksFileID BIGINT OUTPUT,    
@WorksID BIGINT,
@Type INT,
@FileName VARCHAR(200),
@FilePath VARCHAR(1000), 
@FileSize INT,
@CreatedBy BIGINT, 
@Created DATETIME, 
@Flag BIGINT
AS
	INSERT INTO WorksFile
    (    
     WorksID,
     Type,
     FileName,
     FilePath, 
     FileSize,
     CreatedBy, 
     Created, 
     Flag
     )
   VALUES 
   (     
     @WorksID,
     @Type,
     @FileName,
     @FilePath, 
     @FileSize,
     @CreatedBy, 
     @Created, 
     @Flag
     )
   SELECT @WorksFileID=@@IDENTITY     
     
