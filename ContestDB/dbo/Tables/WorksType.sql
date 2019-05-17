CREATE TABLE [dbo].[WorksType] (
    [WorksTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [WorksTypeName] VARCHAR (100) NULL,
    [Description]   VARCHAR (500) NULL,
    [ParentID]      INT           NULL,
    [LevelID]       INT           NULL,
    [CreatedBy]     BIGINT        NULL,
    [Created]       DATETIME      NULL,
    [ModifiedBy]    BIGINT        NULL,
    [Modified]      DATETIME      NULL,
    [Flag]          INT           CONSTRAINT [DF_WorksType_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_WorksType] PRIMARY KEY CLUSTERED ([WorksTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'WorksTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品类型名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'WorksTypeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品类别说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品父级类别ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'ParentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品类别等级ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'LevelID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksType', @level2type = N'COLUMN', @level2name = N'Flag';

