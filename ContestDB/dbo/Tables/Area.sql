CREATE TABLE [dbo].[Area] (
    [AreaID]    INT          IDENTITY (1, 1) NOT NULL,
    [AreaName]  VARCHAR (50) NULL,
    [ParentID]  INT          NULL,
    [AreaLevel] INT          NULL,
    [Flag]      INT          CONSTRAINT [DF_Area_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([AreaID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地区ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'AreaID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地区名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'AreaName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'父级地区ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'ParentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地区等级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'AreaLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Area', @level2type = N'COLUMN', @level2name = N'Flag';

