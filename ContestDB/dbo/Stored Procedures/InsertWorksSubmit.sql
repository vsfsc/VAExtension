

-- =============================================
-- Author:		hx
-- Create date: 2012.3.8
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[InsertWorksSubmit]
@WorksSubmitID BIGINT OUTPUT,
@WorksID BIGINT,
@WorksCode VARCHAR(50),
@SubmitProfile VARCHAR(2000), 
@Suggestion  VARCHAR(2000), 
@InstallationGuide  VARCHAR(2000), 
@DesignIdeas VARCHAR(MAX), 
@KeyPoints  VARCHAR(2000), 
@SelfAssessment VARCHAR(2000),
@WorksShow VARCHAR(2000), 
@Comment  VARCHAR(2000), 
@DemoURL VARCHAR(1000),
@StateID INT,
@CreatedBy BIGINT, 
@Created DATETIME, 
@Flag BIGINT
AS
	INSERT INTO WorksSubmit
   (WorksID, 
    WorksCode, 
    SubmitProfile, 
    Suggestion,
    InstallationGuide, 
    DesignIdeas, 
    KeyPoints, 
    SelfAssessment, 
    WorksShow, 
    Comment, 
    DemoURL, 
    StateID, 
    CreatedBy, 
    Created,
    Flag)
VALUES    
(
@WorksID,
@WorksCode,
@SubmitProfile, 
@Suggestion, 
@InstallationGuide, 
@DesignIdeas, 
@KeyPoints, 
@SelfAssessment,
@WorksShow,
@Comment, 
@DemoURL,
@StateID,
@CreatedBy, 
@Created, 
@Flag
)
SELECT @WorksSubmitID=@@IDENTITY
RETURN 0;
