-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertUserWorks]
@UserWorksID BIGINT OUTPUT,
@UserID bigint, 
@WorksID bigint, 
@Relationship int, 
@Flag int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

  INSERT INTO UserWorks
              (UserID, 
			  WorksID, 
			  Relationship, 
			  Flag)
VALUES       (@UserID, 
			  @WorksID, 
			  @Relationship, 
			  @Flag)

SELECT @UserWorksID=@@IDENTITY   
END
