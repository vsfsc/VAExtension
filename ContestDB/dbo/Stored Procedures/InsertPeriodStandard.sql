-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertPeriodStandard] 
	-- Add the parameters for the stored procedure here
	@WorkTypeID int,
	@PeriodID  bigint,
	@StandardID bigint,
	@StandardDescription varchar(1000),
	@Score int,
	@Created  datetime, 
    @CreatedBy  bigint, 
	@Flag int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[PeriodStandard]
           (WorkTypeID,[PeriodID]
           ,[StandardID]
           ,[StandardDescription]
           ,[Score],Created,CreatedBy
           ,[Flag])
     VALUES
           (@WorkTypeID,@PeriodID,@StandardID ,@StandardDescription
           ,@Score,@Created,@CreatedBy
           ,@Flag)
END
