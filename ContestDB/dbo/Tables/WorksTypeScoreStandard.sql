CREATE TABLE [dbo].[WorksTypeScoreStandard] (
    [WorkTypeID]          INT           NOT NULL,
    [StandardID]          INT           NOT NULL,
    [Score]               INT           NULL,
    [StandardDescription] VARCHAR (200) NULL,
    [Flag]                INT           CONSTRAINT [DF_WorksTypeScoreStandard_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_WorksTypeScoreStandard_1] PRIMARY KEY CLUSTERED ([WorkTypeID] ASC, [StandardID] ASC),
    CONSTRAINT [FK_WorksTypeScoreStandard_ScoreStandard] FOREIGN KEY ([StandardID]) REFERENCES [dbo].[ScoreStandard] ([StandardID]),
    CONSTRAINT [FK_WorksTypeScoreStandard_WorksType] FOREIGN KEY ([WorkTypeID]) REFERENCES [dbo].[WorksType] ([WorksTypeID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品类别ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksTypeScoreStandard', @level2type = N'COLUMN', @level2name = N'WorkTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'指标ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksTypeScoreStandard', @level2type = N'COLUMN', @level2name = N'StandardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '指标分值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksTypeScoreStandard', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'指标描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksTypeScoreStandard', @level2type = N'COLUMN', @level2name = N'StandardDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksTypeScoreStandard', @level2type = N'COLUMN', @level2name = N'Flag';

