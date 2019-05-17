CREATE TABLE [dbo].[RatingDetails] (
    [RatingDetailsID] BIGINT IDENTITY (1, 1) NOT NULL,
    [WorksExpertID]   BIGINT NULL,
    [StandardID]      INT    NULL,
    [Score]           REAL   NULL,
    [Flag]            BIGINT CONSTRAINT [DF_RatingDetails_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_RatingDetails] PRIMARY KEY CLUSTERED ([RatingDetailsID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评分明细', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RatingDetails', @level2type = N'COLUMN', @level2name = N'RatingDetailsID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品评分ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RatingDetails', @level2type = N'COLUMN', @level2name = N'WorksExpertID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '指标ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RatingDetails', @level2type = N'COLUMN', @level2name = N'StandardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应指标评分', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RatingDetails', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RatingDetails', @level2type = N'COLUMN', @level2name = N'Flag';

