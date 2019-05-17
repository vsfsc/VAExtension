CREATE TABLE [dbo].[ExpertWorksType] (
    [ExpertWorksTypeID] BIGINT IDENTITY (1, 1) NOT NULL,
    [UserID]            BIGINT NULL,
    [WorksTypeID]       BIGINT NULL,
    [Flag]              BIGINT NULL,
    CONSTRAINT [PK_ExpertWorksType] PRIMARY KEY CLUSTERED ([ExpertWorksTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '分组作品类别ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertWorksType', @level2type = N'COLUMN', @level2name = N'ExpertWorksTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertWorksType', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品类别ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertWorksType', @level2type = N'COLUMN', @level2name = N'WorksTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExpertWorksType', @level2type = N'COLUMN', @level2name = N'Flag';

