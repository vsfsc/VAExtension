CREATE TABLE [dbo].[Specialty] (
    [SpecialtyID]   INT          IDENTITY (1, 1) NOT NULL,
    [SpecialtyName] VARCHAR (50) NULL,
    [CollegeID]     INT          NULL,
    [Flag]          INT          NULL,
    CONSTRAINT [PK_Specialty] PRIMARY KEY CLUSTERED ([SpecialtyID] ASC),
    CONSTRAINT [FK_Specialty_College] FOREIGN KEY ([CollegeID]) REFERENCES [dbo].[College] ([CollegeID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '专业ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Specialty', @level2type = N'COLUMN', @level2name = N'SpecialtyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '专业名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Specialty', @level2type = N'COLUMN', @level2name = N'SpecialtyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '学院ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Specialty', @level2type = N'COLUMN', @level2name = N'CollegeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Specialty', @level2type = N'COLUMN', @level2name = N'Flag';

