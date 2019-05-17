-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertWorksTypeScoreStandard
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

   INSERT INTO [dbo].[WorksTypeScoreStandard]
           ([WorkTypeID]
           ,[StandardID],[Score]
           ,[StandardDescription]
           ,[Flag])
     VALUES
           (@WorkTypeID
           ,@StandardID,@Score
           ,@StandardDescription
           ,@Flag)
END
