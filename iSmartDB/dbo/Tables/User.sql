CREATE TABLE [dbo].[User] (
    [UserID]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [Account]    VARCHAR (50)  NULL,
    [Name]       VARCHAR (50)  NULL,
    [IDCard]     VARCHAR (18)  NULL,
    [Sex]        BIT           NULL,
    [Telephone]  VARCHAR (50)  NULL,
    [Email]      VARCHAR (100) NULL,
    [RoleID]     INT           NULL,
    [SchoolID]   INT           NULL,
    [CreatedBy]  BIGINT        NULL,
    [Created]    DATETIME      NULL,
    [ModifiedBy] BIGINT        NULL,
    [Modified]   DATETIME      NULL,
    [StateID]    INT           NULL,
    [Flag]       BIGINT        CONSTRAINT [DF_User_Flag] DEFAULT ((1)) NULL,
    [IP] VARCHAR(15) NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

