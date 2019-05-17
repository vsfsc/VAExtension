CREATE TABLE [dbo].[ExpertGroupType] (
    [ExpertGroupTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]              VARCHAR (100) NULL,
    [Flag]              INT           CONSTRAINT [DF_ExpertType_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ExpertType] PRIMARY KEY CLUSTERED ([ExpertGroupTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '评分分组类别ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertGroupType', @level2type = N'COLUMN', @level2name = N'ExpertGroupTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '分组类别名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertGroupType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertGroupType', @level2type = N'COLUMN', @level2name = N'Flag';

