CREATE TABLE [dbo].[Location] (
    [LocationID]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [Address]     VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (200) NULL,
    [ParentID]    BIGINT        NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

