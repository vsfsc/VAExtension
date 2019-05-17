CREATE TABLE [dbo].[ResetPassword] (
    [OperateID]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [Account]    VARCHAR (50)  NULL,
    [Email]      VARCHAR (100) NULL,
    [CheckCode]  VARCHAR (4)   NULL,
    [StartTime]  DATETIME      NULL,
    [EndTime]    DATETIME      NULL,
    [IsFinished] BIT           NULL,
    CONSTRAINT [PK_ResetPassword] PRIMARY KEY CLUSTERED ([OperateID] ASC)
);

