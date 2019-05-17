CREATE TABLE [dbo].[WorksComments] (
    [CommentsID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserID]     BIGINT         NULL,
    [WorksID]    BIGINT         NULL,
    [Comments]   VARCHAR (1000) NULL,
    [Score]      REAL           CONSTRAINT [DF_WorksComments_Score] DEFAULT ((0)) NULL,
    [Created]    DATETIME       NULL,
    [Flag]       BIGINT         CONSTRAINT [DF_WorksComments_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_WorksComments] PRIMARY KEY CLUSTERED ([CommentsID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点评与训练', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'CommentsID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'WorksID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打分', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评分时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0不显示，1公式作品点评，2评价训练标记：不通过测试，3评价训练标记：通过测试', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksComments', @level2type = N'COLUMN', @level2name = N'Flag';

