CREATE TABLE [dbo].[LearningActivity] (
    [LearningActivityID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserID]             BIGINT        NOT NULL,
    [ActivityTypeID]     BIGINT        NOT NULL,
    [LearningObjectID]   BIGINT        NOT NULL,
    [Start]              DATETIME      NULL,
    [During]             INT           NULL,
    [LocationID]         BIGINT        NOT NULL,
    [WorksUrl]           VARCHAR (MAX) NULL,
    [Others]             XML           NULL,
    [Flag]               INT           NULL,
    CONSTRAINT [PK_LearningActivity] PRIMARY KEY CLUSTERED ([LearningActivityID] ASC),
    CONSTRAINT [FK_LearningActivity_ActivityType] FOREIGN KEY ([ActivityTypeID]) REFERENCES [dbo].[ActivityType] ([ActivityTypeID]),
    CONSTRAINT [FK_LearningActivity_LearningObject] FOREIGN KEY ([LearningObjectID]) REFERENCES [dbo].[LearningObject] ([LearningObjectID]),
    CONSTRAINT [FK_LearningActivity_Location] FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Location] ([LocationID]),
    CONSTRAINT [FK_LearningActivity_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位为分钟', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LearningActivity', @level2type = N'COLUMN', @level2name = N'During';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'逻辑删除标记，0隐藏，1显示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LearningActivity', @level2type = N'COLUMN', @level2name = N'Flag';

