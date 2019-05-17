CREATE TABLE [dbo].[PeriodStandard] (
    [WorkTypeID]          INT            NOT NULL,
    [PeriodID]            BIGINT         NOT NULL,
    [StandardID]          INT            NOT NULL,
    [StandardDescription] VARCHAR (1000) NULL,
    [Score]               INT            NULL,
    [Created]             DATETIME       NULL,
    [CreatedBy]           BIGINT         NULL,
    [Modified]            DATETIME       NULL,
    [ModifiedBy]          BIGINT         NULL,
    [Flag]                INT            CONSTRAINT [DF_PeriodStandard_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_PeriodStandard] PRIMARY KEY CLUSTERED ([WorkTypeID] ASC, [PeriodID] ASC, [StandardID] ASC),
    CONSTRAINT [FK_PeriodStandard_Periods] FOREIGN KEY ([PeriodID]) REFERENCES [dbo].[Periods] ([PeriodID]),
    CONSTRAINT [FK_PeriodStandard_ScoreStandard] FOREIGN KEY ([StandardID]) REFERENCES [dbo].[ScoreStandard] ([StandardID]),
    CONSTRAINT [FK_PeriodStandard_WorksType] FOREIGN KEY ([WorkTypeID]) REFERENCES [dbo].[WorksType] ([WorksTypeID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品类别ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'WorkTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期次ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'PeriodID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '指标ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'StandardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'指标描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'StandardDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'该指标分值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PeriodStandard', @level2type = N'COLUMN', @level2name = N'Flag';

