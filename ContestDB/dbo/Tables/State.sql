CREATE TABLE [dbo].[State] (
    [StateID]     INT          IDENTITY (1, 1) NOT NULL,
    [StateName]   VARCHAR (50) NULL,
    [Description] NCHAR (10)   NULL,
    [Flag]        INT          CONSTRAINT [DF_State_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'State', @level2type = N'COLUMN', @level2name = N'StateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'State', @level2type = N'COLUMN', @level2name = N'StateName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'State', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'State', @level2type = N'COLUMN', @level2name = N'Flag';

