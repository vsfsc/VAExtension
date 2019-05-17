-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateUserWorks
@UserID bigint,
@WorksID bigint,
@Relationship int,
@Flag int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  UPDATE  UserWorks
SET   UserID =@UserID, 
WorksID =@WorksID, 
Relationship =@Relationship, 
Flag =@Flag
where WorksID=@WorksID and UserID=@UserID
END
