CREATE TABLE [dbo].[UserRole] (
    [UserRoleID] BIGINT   IDENTITY (1, 1) NOT NULL,
    [UserID]     BIGINT   NOT NULL,
    [RoleID]     INT      NOT NULL,
    [CreatedBy]  BIGINT   NULL,
    [Created]    DATETIME NULL,
    [StateID]    INT      NULL,
    [ApprovedBy] BIGINT   NULL,
    [Approved]   DATETIME NULL,
    [ContestID]  BIGINT   NULL,
    [Flag]       BIGINT   NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserRoleID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'UserRoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'StateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '审批人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'ApprovedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '审批时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'Approved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '竞赛ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'ContestID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserRole', @level2type = N'COLUMN', @level2name = N'Flag';

