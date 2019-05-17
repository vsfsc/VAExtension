CREATE TABLE [dbo].[College] (
    [CollegeID]   INT          IDENTITY (1, 1) NOT NULL,
    [CollegeName] VARCHAR (50) NULL,
    [SchoolID]    INT          NULL,
    [Flag]        INT          NULL,
    CONSTRAINT [PK_College] PRIMARY KEY CLUSTERED ([CollegeID] ASC),
    CONSTRAINT [FK_College_School] FOREIGN KEY ([SchoolID]) REFERENCES [dbo].[School] ([SchoolID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'学院ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'College', @level2type = N'COLUMN', @level2name = N'CollegeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'学院名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'College', @level2type = N'COLUMN', @level2name = N'CollegeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'学校ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'College', @level2type = N'COLUMN', @level2name = N'SchoolID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'College', @level2type = N'COLUMN', @level2name = N'Flag';

