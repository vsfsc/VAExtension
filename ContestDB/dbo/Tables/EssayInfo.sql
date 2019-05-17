CREATE TABLE [dbo].[EssayInfo] (
    [EssayID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [ExaminationDates]   DATETIME       NULL,
    [Teacher]            VARCHAR (20)   NULL,
    [EssayTitleType]     INT            NULL,
    [EssayTitle]         VARCHAR (100)  NULL,
    [TextDescription]    VARCHAR (1000) NULL,
    [PictureDescription] IMAGE          NULL,
    [Flag]               INT            CONSTRAINT [DF_EssayInfo_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_EssayInfo] PRIMARY KEY CLUSTERED ([EssayID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'EssayID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '考试日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'ExaminationDates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出题教师', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'Teacher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文题目类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'EssayTitleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文题目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'EssayTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作文要求', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'TextDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '看图作文图片说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'PictureDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EssayInfo', @level2type = N'COLUMN', @level2name = N'Flag';

