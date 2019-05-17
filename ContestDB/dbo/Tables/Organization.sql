CREATE TABLE [dbo].[Organization] (
    [OrganizationID]   INT          NOT NULL,
    [OrganizationCode] VARCHAR (20) NULL,
    [OrganizationName] VARCHAR (50) NULL,
    CONSTRAINT [PK_Academy] PRIMARY KEY CLUSTERED ([OrganizationID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组织单位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Organization', @level2type = N'COLUMN', @level2name = N'OrganizationID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组织单位编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Organization', @level2type = N'COLUMN', @level2name = N'OrganizationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组织单位名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Organization', @level2type = N'COLUMN', @level2name = N'OrganizationName';

