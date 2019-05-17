CREATE TABLE [dbo].[School] (
    [SchoolID]    INT            NOT NULL,
    [SchoolName]  VARCHAR (100)  NULL,
    [SchoolCode]  VARCHAR (50)   NULL,
    [Description] VARCHAR (1000) NULL,
    [AreaID]      INT            NULL,
    [CreatedBy]   BIGINT         NULL,
    [Created]     DATETIME       NULL,
    [ModifiedBy]  BIGINT         NULL,
    [Modified]    DATETIME       NULL,
    [Flag]        INT            NULL
);

