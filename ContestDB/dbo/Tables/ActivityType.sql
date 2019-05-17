CREATE TABLE [dbo].[ActivityType] (
    [ActivityTypeID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Action]         NCHAR (10)    NOT NULL,
    [Description]    VARCHAR (200) NULL,
    [ParentID]       BIGINT        NULL,
    CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED ([ActivityTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ActivityType', @level2type = N'COLUMN', @level2name = N'Action';

