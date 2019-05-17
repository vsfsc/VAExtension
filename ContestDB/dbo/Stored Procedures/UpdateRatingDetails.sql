-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateRatingDetails
	-- Add the parameters for the stored procedure here
	@WorksExpertID bigint,
	@StandardID int,
	@Score real,
	@Flag bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[RatingDetails]
   SET [Score] = @Score
      ,[Flag] = @Flag
	WHERE WorksExpertID=@WorksExpertID and StandardID=@StandardID
END
