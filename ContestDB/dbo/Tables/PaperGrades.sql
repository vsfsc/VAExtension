CREATE TABLE [dbo].[PaperGrades] (
    [GradesID]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [EssayID]       BIGINT         NULL,
    [SpecialtyID]   INT            NULL,
    [Grade]         VARCHAR (50)   NULL,
    [Class]         VARCHAR (50)   NULL,
    [StudentID]     VARCHAR (20)   NULL,
    [CreatedBy]     VARCHAR (20)   NULL,
    [TotalScores]   REAL           NULL,
    [WritingScores] REAL           NULL,
    [ScoreLevel]    VARCHAR (4)    NULL,
    [EssayTitle]    VARCHAR (100)  NULL,
    [EssayText]     VARCHAR (3000) NULL,
    [Flag]          INT            CONSTRAINT [DF_PaperGrades_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_PaperGrades] PRIMARY KEY CLUSTERED ([GradesID] ASC),
    CONSTRAINT [FK_PaperGrades_EssayInfo] FOREIGN KEY ([EssayID]) REFERENCES [dbo].[EssayInfo] ([EssayID]),
    CONSTRAINT [FK_PaperGrades_Specialty] FOREIGN KEY ([SpecialtyID]) REFERENCES [dbo].[Specialty] ([SpecialtyID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文等级ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'GradesID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'EssayID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '专业ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'SpecialtyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '年级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'Grade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '班级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '学生ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'StudentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总成绩', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'TotalScores';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '写作成绩', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'WritingScores';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '成绩水平', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'ScoreLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文题目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'EssayTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文正文', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'EssayText';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PaperGrades', @level2type = N'COLUMN', @level2name = N'Flag';

