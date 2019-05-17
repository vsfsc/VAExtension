CREATE TABLE [dbo].[FileType] (
    [FileTypeID] BIGINT       IDENTITY (1, 1) NOT NULL,
    [TypeName]   VARCHAR (10) NULL,
    [ParentID]   BIGINT       NULL,
    CONSTRAINT [PK_FileType] PRIMARY KEY CLUSTERED ([FileTypeID] ASC)
);

