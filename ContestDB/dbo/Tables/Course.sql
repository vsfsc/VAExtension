CREATE TABLE [dbo].[Course] (
    [CourseID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [CourseName] VARCHAR (100)    NULL,
    [Guid]       UNIQUEIDENTIFIER NULL,
    [Url]        VARCHAR (200)    NULL,
    [Flag]       INT              CONSTRAINT [DF_Course_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED ([CourseID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'课程ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Course', @level2type = N'COLUMN', @level2name = N'CourseID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'课程名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Course', @level2type = N'COLUMN', @level2name = N'CourseName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'课程网站Guid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Course', @level2type = N'COLUMN', @level2name = N'Guid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'课程网站Url', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Course', @level2type = N'COLUMN', @level2name = N'Url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Course', @level2type = N'COLUMN', @level2name = N'Flag';

