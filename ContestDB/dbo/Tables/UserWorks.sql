CREATE TABLE [dbo].[UserWorks] (
    [UserWorksID]  BIGINT IDENTITY (1, 1) NOT NULL,
    [UserID]       BIGINT CONSTRAINT [DF_UserWorks_UserID] DEFAULT ((1)) NULL,
    [WorksID]      BIGINT NULL,
    [Relationship] INT    NULL,
    [Flag]         INT    NULL,
    CONSTRAINT [PK_UserWorks_1] PRIMARY KEY CLUSTERED ([UserWorksID] ASC),
    CONSTRAINT [FK_UserWorks_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]),
    CONSTRAINT [FK_UserWorks_Works] FOREIGN KEY ([WorksID]) REFERENCES [dbo].[Works] ([WorksID])
);


GO
ALTER TABLE [dbo].[UserWorks] NOCHECK CONSTRAINT [FK_UserWorks_User];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户作品ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserWorks', @level2type = N'COLUMN', @level2name = N'UserWorksID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserWorks', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserWorks', @level2type = N'COLUMN', @level2name = N'WorksID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'在作品中参与的形式：0独创作者，1组长，2组员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserWorks', @level2type = N'COLUMN', @level2name = N'Relationship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserWorks', @level2type = N'COLUMN', @level2name = N'Flag';

