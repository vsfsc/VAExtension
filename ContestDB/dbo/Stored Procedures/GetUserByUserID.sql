-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetUserByUserID 
@UserID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT   UserID, Account, Name, IDCard, Sex, Telephone, Email, Duty, ProfessionalTitle, Department, Major, Class, Grade, 
                RoleID, SchoolID, CreatedBy, Created, ModifiedBy, Modified, StateID, ApprovedBy, Approved, Flag
FROM      [User] 
where UserID=@UserID
END
