-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByWorksID]
	@WorksID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 SELECT   UserWorks.UserID,UserWorks.WorksID, UserWorks.Relationship,[User].Account, [User].Name

 FROM      UserWorks 
 INNER JOIN [User] 
 ON UserWorks.UserID = [User].UserID
 Where UserWorks.Flag=1 and UserWorks.WorksID=@WorksID
END
