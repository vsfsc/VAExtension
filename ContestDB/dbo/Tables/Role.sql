CREATE TABLE [dbo].[Role] (
    [RoleID]   INT          IDENTITY (1, 1) NOT NULL,
    [RoleName] VARCHAR (50) NULL,
    [Flag]     INT          CONSTRAINT [DF_Role_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Role', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Role', @level2type = N'COLUMN', @level2name = N'RoleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Role', @level2type = N'COLUMN', @level2name = N'Flag';

