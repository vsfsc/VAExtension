CREATE TABLE [dbo].[Works] (
    [WorksID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [PeriodID]          BIGINT         NULL,
    [WorksName]         VARCHAR (100)  NULL,
    [TeamName]          VARCHAR (100)  NULL,
    [WorksTypeID]       INT            NULL,
    [WorksCode]         VARCHAR (50)   NULL,
    [OrgID]             INT            NULL,
    [IsSample]          INT            CONSTRAINT [DF_Works_IsSample] DEFAULT ((0)) NULL,
    [Comment]           VARCHAR (2000) NULL,
    [Profile]           VARCHAR (8000) NULL,
    [SubmitProfile]     VARCHAR (8000) NULL,
    [Suggestion]        VARCHAR (2000) NULL,
    [InstallationGuide] VARCHAR (2000) NULL,
    [DesignIdeas]       VARCHAR (MAX)  NULL,
    [KeyPoints]         VARCHAR (2500) NULL,
    [SelfAssessment]    VARCHAR (2000) NULL,
    [DemoURL]           VARCHAR (2000) NULL,
    [BackReason]        VARCHAR (2000) NULL,
    [Score]             REAL           CONSTRAINT [DF_Works_Score] DEFAULT ((0)) NULL,
    [CreatedBy]         BIGINT         NULL,
    [Created]           DATETIME       NULL,
    [ModifiedBy]        BIGINT         NULL,
    [Modified]          DATETIME       NULL,
    [AllotTimes]        INT            CONSTRAINT [DF_Works_AllotTimes] DEFAULT ((0)) NULL,
    [WorksState]        INT            NULL,
    [Flag]              BIGINT         CONSTRAINT [DF_Works_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Works] PRIMARY KEY CLUSTERED ([WorksID] ASC),
    CONSTRAINT [FK_Works_Periods] FOREIGN KEY ([PeriodID]) REFERENCES [dbo].[Periods] ([PeriodID]),
    CONSTRAINT [FK_Works_State] FOREIGN KEY ([WorksState]) REFERENCES [dbo].[State] ([StateID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品期次', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'CONSTRAINT', @level2name = N'FK_Works_Periods';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'WorksID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期次ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'PeriodID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'WorksName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '团队名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'TeamName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品类型ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'WorksTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'WorksCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'机构ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'OrgID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否样例作品', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'IsSample';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'其他说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Profile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品简介', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'SubmitProfile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Suggestion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品安装效果说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'InstallationGuide';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设计思路', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'DesignIdeas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创意特色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'KeyPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'指导老师自评', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'SelfAssessment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '演示地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'DemoURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品得分', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品评分分配次数（默认0次，最大9次）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'AllotTimes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'作品状态（0暂时保存可以修改；1已提交不可修改；2作品已分组，等待评分；3作品评分结束；4作品公示）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'WorksState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Works', @level2type = N'COLUMN', @level2name = N'Flag';

