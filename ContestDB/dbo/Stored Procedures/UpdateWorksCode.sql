-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE  [dbo].[UpdateWorksCode]
@WorksID bigint,
@SubmitProfile varchar(8000),
@DesignIdeas varchar(MAX), 
@KeyPoints  varchar(2500), 
@DemoURL varchar(2000),
@WorksCode varchar(50)
AS
	update works set WorksCode=@WorksCode
	where WorksID=@WorksID
RETURN 0;
