-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateWorksTypeScoreStandard
	-- Add the parameters for the stored procedure here
	@WorkTypeID int,
	@StandardID int,
	@Score int,
	@StandardDescription varchar(200),
	@Flag int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[WorksTypeScoreStandard]
   SET  Score=@Score,StandardDescription=@StandardDescription,[Flag] = @Flag
   WHERE [WorkTypeID] = @WorkTypeID and [StandardID] = @StandardID
END
