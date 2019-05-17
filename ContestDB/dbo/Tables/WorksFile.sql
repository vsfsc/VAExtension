CREATE TABLE [dbo].[WorksFile] (
    [WorksFileID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [WorksID]     BIGINT         NOT NULL,
    [Type]        INT            NULL,
    [FileName]    VARCHAR (200)  NULL,
    [FilePath]    VARCHAR (1000) NULL,
    [FileSize]    INT            NULL,
    [CreatedBy]   BIGINT         NULL,
    [Created]     DATETIME       NULL,
    [ModifiedBy]  BIGINT         NULL,
    [Modified]    DATETIME       NULL,
    [Flag]        BIGINT         NULL,
    CONSTRAINT [PK_WorksImagesID] PRIMARY KEY CLUSTERED ([WorksFileID] ASC),
    CONSTRAINT [FK_WorksFile_Works] FOREIGN KEY ([WorksID]) REFERENCES [dbo].[Works] ([WorksID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品文件ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'WorksFileID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '作品ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'WorksID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1压缩文件2图片文件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'FileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件内容大小', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'FileSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上传人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上传时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WorksFile', @level2type = N'COLUMN', @level2name = N'Flag';

