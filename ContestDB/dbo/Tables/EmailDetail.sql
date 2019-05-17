CREATE TABLE [dbo].[EmailDetail] (
    [EmailDetailID] BIGINT     IDENTITY (1, 1) NOT NULL,
    [FromUserID]    BIGINT     NULL,
    [ToUserID]      BIGINT     NULL,
    [StateID]       NCHAR (10) NULL,
    [CreatedBy]     BIGINT     NULL,
    [Created]       DATETIME   NULL,
    [Flag]          BIGINT     NULL,
    CONSTRAINT [PK_EmailDetail] PRIMARY KEY CLUSTERED ([EmailDetailID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮件详细ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'EmailDetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发件人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'FromUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收件人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'ToUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'StateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EmailDetail', @level2type = N'COLUMN', @level2name = N'Flag';

