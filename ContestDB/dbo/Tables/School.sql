CREATE TABLE [dbo].[School] (
    [SchoolID]    INT            IDENTITY (1, 1) NOT NULL,
    [SchoolName]  VARCHAR (50)   NULL,
    [SchoolCode]  VARCHAR (10)   NULL,
    [Description] VARCHAR (1000) NULL,
    [AreaID]      INT            NULL,
    [CreatedBy]   VARCHAR (50)   NULL,
    [Created]     DATETIME       NULL,
    [ModifiedBy]  VARCHAR (50)   NULL,
    [Modified]    DATETIME       NULL,
    [Flag]        INT            NULL,
    CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED ([SchoolID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '学校ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'SchoolID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '学校名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'SchoolName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '学校编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'SchoolCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '学校描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'School', @level2type = N'COLUMN', @level2name = N'Flag';

