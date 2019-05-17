CREATE TABLE [dbo].[Periods] (
    [PeriodID]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [PeriodTitle] VARCHAR (100) NULL,
    [CourseID]    BIGINT        NULL,
    [Require]     VARCHAR (MAX) NULL,
    [WorksTypeID] INT           NULL,
    [Number]      INT           NULL,
    [StartSubmit] DATETIME      NULL,
    [EndSubmit]   DATETIME      NULL,
    [StartScore]  DATETIME      NULL,
    [EndScore]    DATETIME      NULL,
    [StartPublic] DATETIME      NULL,
    [EndPublic]   DATETIME      NULL,
    [CreatedBy]   BIGINT        NULL,
    [Created]     DATETIME      NULL,
    [ModifiedBy]  BIGINT        NULL,
    [Modified]    DATETIME      NULL,
    [Flag]        INT           CONSTRAINT [DF_Periods_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Periods] PRIMARY KEY CLUSTERED ([PeriodID] ASC),
    CONSTRAINT [FK_Periods_Course] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '期次ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'PeriodID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '期次标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'PeriodTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'课程ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'CourseID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '其次要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'Require';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'本期作品类型ID（其中0表示不指定类型，为开放的）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'WorksTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '本期次作品完成所需最多人数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品提交开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'StartSubmit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品提交截止时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'EndSubmit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品评分开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'StartScore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品评分截止时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'EndScore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品公示开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'StartPublic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品公示截止时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'EndPublic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '编辑人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '编辑时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Periods', @level2type = N'COLUMN', @level2name = N'Flag';

