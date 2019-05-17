CREATE TABLE [dbo].[Email] (
    [EmailID]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Message]   VARCHAR (1000) NULL,
    [EmailType] INT            NULL,
    [Flag]      BIGINT         NULL,
    CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED ([EmailID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮件ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'EmailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮件内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邮件类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'EmailType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Flag';

