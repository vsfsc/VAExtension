CREATE TABLE [dbo].[ScoreStandard] (
    [StandardID]   INT           IDENTITY (1, 1) NOT NULL,
    [StandardName] VARCHAR (200) NULL,
    [Description]  VARCHAR (500) NULL,
    [Flag]         BIGINT        CONSTRAINT [DF_ScoreStandard_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ScoreStandard] PRIMARY KEY CLUSTERED ([StandardID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '指标ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ScoreStandard', @level2type = N'COLUMN', @level2name = N'StandardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'指标名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ScoreStandard', @level2type = N'COLUMN', @level2name = N'StandardName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'指标说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ScoreStandard', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ScoreStandard', @level2type = N'COLUMN', @level2name = N'Flag';

