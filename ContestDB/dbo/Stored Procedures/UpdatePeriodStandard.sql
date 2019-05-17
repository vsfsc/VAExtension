-- =============================================
-- Author:		xqx
-- Create date: <Create Date,,>
-- Description:	更新指标
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePeriodStandard] 
	-- Add the parameters for the stored procedure here
	@WorkTypeID int,
	@PeriodID  bigint,
	@StandardID bigint,
	@StandardDescription varchar(1000),
	@Score int,
    @Modified  datetime, 
    @ModifiedBy bigint, 
	@Flag int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update [dbo].[PeriodStandard]
          set [StandardDescription]=@StandardDescription
           ,[Score]=@Score,Modified=@Modified,ModifiedBy=@ModifiedBy,Flag=@Flag
  		   where WorkTypeID=@WorkTypeID and PeriodID=@PeriodID and StandardID=@StandardID
  
END
