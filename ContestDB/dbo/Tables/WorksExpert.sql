CREATE TABLE [dbo].[WorksExpert] (
    [WorksExpertID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [WorksID]       BIGINT        NULL,
    [ExpertID]      BIGINT        NULL,
    [Score]         REAL          CONSTRAINT [DF_WorksExpert_Score] DEFAULT ((0)) NULL,
    [Comments]      VARCHAR (200) NULL,
    [Created]       DATETIME      NULL,
    [Modified]      DATETIME      NULL,
    [ScoreState]    INT           NULL,
    [Flag]          BIGINT        CONSTRAINT [DF_WorksGroup_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_WorksExpert] PRIMARY KEY CLUSTERED ([WorksExpertID] ASC),
    CONSTRAINT [FK_WorksExpert_User] FOREIGN KEY ([ExpertID]) REFERENCES [dbo].[User] ([UserID]),
    CONSTRAINT [FK_WorksExpert_Works] FOREIGN KEY ([WorksID]) REFERENCES [dbo].[Works] ([WorksID])
);


GO
ALTER TABLE [dbo].[WorksExpert] NOCHECK CONSTRAINT [FK_WorksExpert_User];


GO
ALTER TABLE [dbo].[WorksExpert] NOCHECK CONSTRAINT [FK_WorksExpert_Works];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品评分ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'WorksExpertID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'WorksID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评分人ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'ExpertID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '评分人对该作品所做评分', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '评分人对该作品所做评语', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建评分时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改评分时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0、保存1、提交', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'ScoreState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0不显示，1正常评分，2作品点评，3评价训练不通过，4评价训练通过', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksExpert', @level2type = N'COLUMN', @level2name = N'Flag';

