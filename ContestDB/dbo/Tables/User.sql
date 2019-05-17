CREATE TABLE [dbo].[User] (
    [UserID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [Account]           VARCHAR (50)  NULL,
    [Name]              VARCHAR (50)  NULL,
    [IDCard]            VARCHAR (18)  NULL,
    [Sex]               BIT           NULL,
    [Telephone]         VARCHAR (50)  NULL,
    [Email]             VARCHAR (100) NULL,
    [Duty]              VARCHAR (100) NULL,
    [ProfessionalTitle] VARCHAR (100) NULL,
    [Department]        VARCHAR (100) NULL,
    [Major]             VARCHAR (100) NULL,
    [Class]             VARCHAR (50)  NULL,
    [Grade]             VARCHAR (50)  NULL,
    [RoleID]            INT           NULL,
    [SchoolID]          INT           NULL,
    [CreatedBy]         BIGINT        NULL,
    [Created]           DATETIME      NULL,
    [ModifiedBy]        BIGINT        NULL,
    [Modified]          DATETIME      NULL,
    [StateID]           INT           NULL,
    [ApprovedBy]        BIGINT        NULL,
    [Approved]          DATETIME      NULL,
    [Flag]              BIGINT        CONSTRAINT [DF_User_Flag] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'账号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Account';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IDCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'性别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Telephone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮箱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'职务', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Duty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'职称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ProfessionalTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'院系', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Department';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'专业', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Major';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'班级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'年级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Grade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'学校ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'SchoolID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Created';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Modified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户状态，0用户注册，1审批通过，2审批失败', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'StateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审批人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ApprovedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审批时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Approved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标记', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Flag';

