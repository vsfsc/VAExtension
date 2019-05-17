CREATE TABLE [dbo].[LearningObject] (
    [LearningObjectID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Title]            VARCHAR (50)  NULL,
    [Description]      VARCHAR (200) NOT NULL,
    [CatalogID]        BIGINT        NULL,
    [LOTypeID]         INT           NULL,
    [LearningContent]  VARCHAR (MAX) NULL,
    [CreatedBy]        BIGINT        NULL,
    [CreatedDate]      DATETIME      NULL,
    CONSTRAINT [PK_LearningObject] PRIMARY KEY CLUSTERED ([LearningObjectID] ASC),
    CONSTRAINT [FK_LearningObject_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LearningObject', @level2type = N'COLUMN', @level2name = N'LearningContent';

