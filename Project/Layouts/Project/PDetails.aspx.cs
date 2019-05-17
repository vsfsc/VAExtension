using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ProjectDll;

namespace Project.Layouts.Project
{
    /// <summary>
    /// Class PDetails.
    /// </summary>
    /// <seealso cref="Microsoft.SharePoint.WebControls.LayoutsPageBase" />
    public partial class PDetails : LayoutsPageBase
    {
        #region 属性
        private List<proj_ProjectFile> projectDoc;
        /// <summary>
        /// get doc
        /// </summary>
        private List<proj_ProjectFile> GetProjectDocFiles
        {
            get
            {
                if (projectDoc == null)
                {
                    projectDoc = ProjectDll.BLL.ProjectFileBll.GetFileByProjIdandPurpose(ProjectId, 1);
                }
                return projectDoc;
            }
        }
        private long ProjectId
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ProjectID"]))
                {
                    return long.Parse(Request.QueryString["ProjectID"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 页面显示类型初始化(0 只显示内容,1 显示审批,2 显示评分;3 显示点评;4 显示对接)
        /// </summary>
        private int PageTypeId
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["PageTypeId"]))
                {
                    return int.Parse(Request.QueryString["PageTypeId"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region 页面加载与显示事件
        protected void Page_Load(object sender, EventArgs e)
        {
            btnBack.Click += btnBack_Click;
            btnPass.Click += btnPass_Click;
            long pid = ProjectId;//项目ID初始化
            if (pid>0)
            {
                FillProjectInfo(pid);
                FillDocView(divReport0);
                BindGvComments();
            }
            int pageTypeId = PageTypeId;
            SetDisplay(pageTypeId);
        }
        protected void SetDisplay(int pageTypeId)
        {
            long pid = ProjectId;
            Proj_ProjectState proj = ProjectDll.BLL.ProjectBll.GetProjectStateByProjectId(pid);
            string pname = proj.PName;
            switch (pageTypeId)
            {
                case 1://审批项目
                    lbPNameforPageTitle.Text = "项目审批";
                    divPApprove.Visible = true;//项目审批功能区 pageTypeId ==1才显示
                    divWorksScore.Visible = false;//项目评分功能区 pageTypeId ==2才显示
                    divPublicComments.Visible = false;//项目点评功能区 pageTypeId ==3才显示
                    divMatchSender.Visible = false;//项目对接功能区 pageTypeId ==4才显示
                    btnJoinIn.Visible = false;
                    docJieti.Visible = false;
                    docLixiang.Visible = true;
                    docZhongqi.Visible = false;

                    break;
                case 2://评分项目
                    lbPNameforPageTitle.Text = "项目评分";
                    divPApprove.Visible = false;//项目审批功能区 pageTypeId ==1才显示
                    divWorksScore.Visible = true;//项目评分功能区 pageTypeId ==2才显示
                    divPublicComments.Visible = false;//项目点评功能区 pageTypeId ==3才显示
                    divMatchSender.Visible = false;//项目对接功能区 pageTypeId ==4才显示
                    btnJoinIn.Visible = false;
                    docJieti.Visible = true;
                    docLixiang.Visible = true;
                    docZhongqi.Visible = true;
                    break;
                case 3://点评项目
                    lbPNameforPageTitle.Text = "项目点评";
                    divPApprove.Visible = false;//项目审批功能区 pageTypeId ==1才显示
                    divWorksScore.Visible = false;//项目评分功能区 pageTypeId ==2才显示
                    divPublicComments.Visible = true;//项目点评功能区 pageTypeId ==3才显示
                    divMatchSender.Visible = false;//项目对接功能区 pageTypeId ==4才显示
                    btnJoinIn.Visible = false;
                    docJieti.Visible = true;
                    docLixiang.Visible = true;
                    docZhongqi.Visible = true;
                    break;
                case 4://对接项目
                    lbPNameforPageTitle.Text = "项目对接";
                    divPApprove.Visible = false;//项目审批功能区 pageTypeId ==1才显示
                    divWorksScore.Visible = false;//项目评分功能区 pageTypeId ==2才显示
                    divPublicComments.Visible = false;//项目点评功能区 pageTypeId ==3才显示
                    divMatchSender.Visible = true;//项目对接功能区 pageTypeId ==4才显示
                    btnJoinIn.Visible = false;
                    docJieti.Visible = true;
                    docLixiang.Visible = true;
                    docZhongqi.Visible = true;
                    break;
                case 5://加入项目
                    lbPNameforPageTitle.Text = "申请加入";
                    divPApprove.Visible = false;//项目审批功能区 pageTypeId ==1才显示
                    divWorksScore.Visible = false;//项目评分功能区 pageTypeId ==2才显示
                    divPublicComments.Visible = false;//项目点评功能区 pageTypeId ==3才显示
                    divMatchSender.Visible = false;//项目对接功能区 pageTypeId ==4才显示
                    btnJoinIn.Visible = true;
                    docJieti.Visible = false;
                    docLixiang.Visible = true;
                    docZhongqi.Visible = true;
                    break;
                default://0, 查看项目
                    lbPNameforPageTitle.Text = pname;
                    divPApprove.Visible = false;//项目审批功能区 pageTypeId ==1才显示
                    divWorksScore.Visible = false;//项目评分功能区 pageTypeId ==2才显示
                    divPublicComments.Visible = false;//项目点评功能区 pageTypeId ==3才显示
                    divMatchSender.Visible = false;//项目对接功能区 pageTypeId ==4才显示
                    btnJoinIn.Visible = false;
                    docJieti.Visible = true;
                    docLixiang.Visible = true;
                    docZhongqi.Visible = true;
                    break;
            }
        }
        private void FillProjectInfo(long projectId)
        {
            if (projectId==0)
            {
                Response.Write("<script>alert('你要查看的项目不存在！');window.open('','_self');window.close();'</script>");
                return;
            }
            Proj_ProjectState proj = ProjectDll.BLL.ProjectBll.GetProjectStateByProjectId(projectId);
            
            lbProjectName.Text = proj.PName;
            lbSubject.Text = proj.SubjectName;
            //lbPCode.Text = proj.PCode;
            
            divIntroduce.InnerHtml = proj.Introduce;
            //divBackground.InnerHtml = proj.Background;
            //divImplementation.InnerHtml = proj.Implementation;
            //divExpectedGoal.InnerHtml = proj.ExpectedGoal;

        }

        private void FillProjectFile(long projectId)
        {

        }
         #endregion
        
        #region 项目审批        
        /// <summary>
        /// 给出审批结果.
        /// </summary>
        /// <param name="pStateId">The 项目审批状态ID.</param>
        /// <param name="result">审批结果:0拒绝,1通过.</param>
        private void ApproveResult(int pStateId, int result)
        {
            var pAppro = new Proj_ProjectApprove
            {
                ProjectID = ProjectId,
                Comments = txtApproveComments.Value,//审批意见
                Flag = 1,
                ApprovedTime = DateTime.Now,//审批时间
                ApprovedBy = ProjectDll.DAL.Common.LoginID,//审批人
                Result = result//结果ID
            };
        }
        void btnPass_Click(object sender, EventArgs e)
        {
            ApproveResult(3, 1);
            CreateSiteBySiteTemplate("", "", "", "");//创建项目子网站
        }

        void btnBack_Click(object sender, EventArgs e)
        {
            ApproveResult(2, 0);
        }
        /// <summary>
        /// 根据指定SharePoint网站模板创建子网站
        /// </summary>
        /// <param name="url">The URL.(子网站地址:默认为项目编号,不可为空)</param>
        /// <param name="webName">Name of the web.(子网站名称:缺省为项目名称,不可为空)</param>
        /// <param name="webDescription">The web description.(网站描述:缺省可为空)</param>
        /// <param name="templateName">Name of the template.(网站模板:SharePoint网站模板名称或自定义模板名称,不可为空)</param>
        private void CreateSiteBySiteTemplate(string url, string webName, string webDescription, string templateName)
        {
            string loginName = GetLoginAccountWithDomain;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPSite site = new SPSite(SPContext.Current.Site.Url);
                SPWeb web = site.OpenWeb();
                web.AllowUnsafeUpdates = true;
                //= "subsite1";//创建后的站点是:http://moss:5001/subsite1
                if (!web.Webs[webName].Exists)
                {
                    //1.根据网站模版，创建网站
                    //= "SubSiteTemplate";//网站模版定义
                    SPWebTemplate CustomTemplate = null;
                    SPWebTemplateCollection subWebTemplate = web.GetAvailableWebTemplates((uint)web.Locale.LCID);
                    foreach (SPWebTemplate template in subWebTemplate)
                    {
                        if (template.Title == templateName)
                        {
                            CustomTemplate = template;
                            break;
                        }
                    }
                    SPWeb newWeb = web.Webs.Add(url, webName, webDescription, (uint)2052, CustomTemplate, true, false);
                    newWeb.AllowUnsafeUpdates = true;
                    //2.给创建好的网站分配权限,
                    //2.1断开网站权限
                    newWeb.BreakRoleInheritance(false);
                    //2.2添加网站的"完全控制"权限.即网站管理员
                    SPUser user = web.EnsureUser(loginName);//当前用户
                    SPRoleAssignment myRoleAssignment = new SPRoleAssignment(user);
                    myRoleAssignment.RoleDefinitionBindings.Add(web.RoleDefinitions.GetByType(SPRoleType.Administrator));
                    newWeb.RoleAssignments.Add(myRoleAssignment);
                    newWeb.Update();
                }
                web.Update();
                web.Dispose();

            });
        }
        #endregion

        #region 用户加入项目
        /// <summary>
        /// 申请加入项目数据库添加记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnJoinIn_Click(object sender, EventArgs e)
        {
            string projName = this.lbProjectName.Text;
            SPWeb projWeb = SPContext.Current.Web.Webs[projName];
            
            long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
            var dr = new Proj_Member
            {
                UserID = currentUserId,
                ProjectID = ProjectId,
                ApplyInTime = DateTime.Now,
                Score = 0,
                Flag = 1
            };
            ProjectDll.DAL.ProjectDal.NewProjectMember(dr);
            Page.RegisterStartupScript("温馨提示", String.Format("<script language=\"javascript\">alert(\"{0}\");window.location.replace(\"{1}\")</script>", "欢迎加入: “" + projName + "”", "MyProjects.aspx"));
        }
        /// <summary>
        /// 创建子项目按钮事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCreateSite_Click(object sender, EventArgs e)
        {
            try
            {
                SPSite site = SPContext.Current.Web.Site;
                //string spwt = SPWebTemplate.WebTemplateSTS;//默认选择工作组网站模板 

                SPWeb web = site.OpenWeb("/");
                web.AllowUnsafeUpdates = true;
                SPMember memUser = web.Users[0];
                SPUser suser = web.Users[0];


                //新建组
                //web.SiteGroups.Add("skyteam3", memUser, suser, "new skyteam");
                //web.SiteGroups["skyteam3"].AddUser("adr\\administrator", "pccai@adr.svic1", "蔡", "系统维修人员");
                //web.Groups["skyteam2"].AddUser("adr\\pccai", "pccai@adr.svic1", "蔡", "系统维修人员");


                //新建子站点
                string currentTemplate = web.WebTemplate;
                //web.Webs.Add("Test2", "站点名称2", "站点描述2", 2052, "_GLOBAL_#0", true, false);


                //打开子站点
                SPWeb web2 = site.OpenWeb("Projects/Test2");
                web2.AllowUnsafeUpdates = true;
                // web2.SiteGroups.Add("skyteam6", memUser, suser, "new skyteam");//新建组
                // web2.SiteGroups["skyteam6"].AddUser("adr\\administrator", "administrator@adr.svic1", "边", "系统维修人员");


                //改变站点继承权
                if (!web2.HasUniqueRoleDefinitions)
                {
                    web2.RoleDefinitions.BreakInheritance(true, true);
                }

                //站点继承权改变后重新设置状态
                web2.AllowUnsafeUpdates = true;



                //添加权限级别 (Role) 
                //SPRoleDefinition roleDefinition = new SPRoleDefinition();
                //roleDefinition.Name = "项目角色";
                //roleDefinition.Description = "项目角色可以批准所有项目情况.";
                //roleDefinition.BasePermissions = SPBasePermissions.FullMask ^ SPBasePermissions.ManagePermissions;
                //web2.RoleDefinitions.Add(roleDefinition);


                //更改权限级别 (Permissions) 
                SPRoleDefinitionCollection roles = web2.RoleDefinitions;
                SPRoleDefinition roleDefinition1 = roles["读取"];
                roleDefinition1.BasePermissions = SPBasePermissions.AddListItems |
                    SPBasePermissions.BrowseDirectories |
                    SPBasePermissions.EditListItems |
                    SPBasePermissions.DeleteListItems |
                    SPBasePermissions.AddDelPrivateWebParts;
                roleDefinition1.Update();


                //用户权限分配与定义(New)
                SPRoleDefinitionCollection roleDefinitions = web2.RoleDefinitions;
                SPRoleAssignmentCollection roleAssignments = web2.RoleAssignments;
                SPRoleAssignment roleAssignment = new SPRoleAssignment("adr\\administrator", "administrator@Somewhere.com", "Display_Name", "Notes");
                SPRoleDefinitionBindingCollection roleDefBindings = roleAssignment.RoleDefinitionBindings;
                roleDefBindings.Add(roleDefinitions["项目角色"]);
                roleAssignments.Add(roleAssignment);


                //权限定义(Old)
                //SPRoleCollection siteGroups = web2.Roles;
                //siteGroups.Add("skyteam6", "Description", SPRights.ManageWeb | SPRights.ManageSubwebs);


                //获得权限定义
                SPRoleDefinition sprole = roleDefinitions.GetByType(SPRoleType.Reader);
                string spname = sprole.Name;


                //组权限分配与定义(New)
                SPRoleDefinitionCollection roleDefinitions1 = web2.RoleDefinitions;
                SPRoleAssignmentCollection roleAssignments1 = web2.RoleAssignments;
                SPMember memCrossSiteGroup = web2.SiteGroups["skyteam6"];
                SPPrincipal myssp = (SPPrincipal)memCrossSiteGroup;
                SPRoleAssignment myroles = new SPRoleAssignment(myssp);
                SPRoleDefinitionBindingCollection roleDefBindings1 = myroles.RoleDefinitionBindings;
                roleDefBindings1.Add(roleDefinitions1["设计"]);
                roleDefBindings1.Add(roleDefinitions1["读取"]);
                roleAssignments1.Add(myroles);


                //组权限分配与定义(Old)
                //SPMember member = web2.Roles["skyteam"];
                //web2.Permissions[member].PermissionMask =
                //    SPRights.ManageLists | SPRights.ManageListPermissions;



                //更改列表权限(Old)
                //SPList list = site.Lists["通知"];
                //SPPermissionCollection perms = list.Permissions;
                //SPUserCollection users = site.Users;
                //SPMember member = users["ADR\\pccai"];
                //list.Permissions[member].PermissionMask = SPRights.AddListItems | SPRights.EditListItems;



                //  PermissionCollection perc = web.Permissions;
                //perc.AddUser("adr\\administrator", "administrator@adr.srvc1", "title", "Notes", PortalRight.AllSiteRights);
                // SecurityManager.AddRole(context, "title", "descriptions", PortalRight.ManageSite);



            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region 项目点评事件

        private void GetSatrs()
        {
            int stars = 0;
            if (hdStars.Value!="0")
            {
                stars = int.Parse(hdStars.Value);

            }
            else
            {
                ProjectDll.DAL.Common.Alert("请先给出星级!");
                return;
            }

        }

        void BindGvComments()
        {
            List<Proj_Comments> pc = ProjectDll.BLL.ScoreBll.GetCommentsByItemIdandType(ProjectId, 1);
            gvComments.DataSource = pc;
            gvComments.DataBind();
        }
        #endregion

        #region 项目评分事件

        #endregion

        #region 项目对接事件
        protected void enterprise_CheckedChanged(object sender, EventArgs e)
        {
            if (enterprise.Checked == true)
            {
                divMatchEnterP.Visible = true;
            }
        }

        protected void btnMatchSubmit_Click(object sender, EventArgs e)
        {
            string projName = this.lbProjectName.Text;
            SPWeb projWeb = SPContext.Current.Web.Webs[projName];

            long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
            Proj_Match dr = new Proj_Match();
            dr.ProjectID = ProjectId;
            dr.MatchSender = currentUserId;
            if (enterprise.Checked == true)
                dr.EnterpriseName = txtEnterName.Value;
            else
                dr.EnterpriseName = "非企业";
            dr.Name = txtName.Value;
            dr.Email = txtEmail.Value;
            dr.Telephone = txtPhone.Value;
            dr.SendTime = DateTime.Now;
            dr.IsAccepted = 0;//0 申请中；1 同意 ； -1 拒绝
            dr.Description = txtMatchIdeas.Value;
            dr.Flag = 1;
            ProjectDll.DAL.ProjectDal.NewProjectMatch(dr);
            Page.RegisterStartupScript("温馨提示", String.Format("<script language=\"javascript\">alert(\"{0}\");window.location.replace(\"{1}\")</script>", "欢迎申请对接: “" + projName + ", 您的申请信息已经发送到对方，请耐心等待！", "ProAllowMatch.aspx"));
        }

        protected void matchPerson_CheckedChanged(object sender, EventArgs e)
        {
            if (matchPerson.Checked == true)
                divMatchEnterP.Visible = false;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取带有域名的登录名
        /// </summary>
        public string GetLoginAccountWithDomain
        {
            get
            {
                SPUser us = SPContext.Current.Web.CurrentUser;
                string accountName = "";
                if (us != null)
                {
                    accountName = SPContext.Current.Web.CurrentUser.LoginName;
                    int s = accountName.LastIndexOf("|");
                    if (s > 0)
                        accountName = accountName.Substring(s + 1);
                }
                return accountName;
            }
        }

        
        void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string fileName = btn.CommandArgument;
            string docName = fileName.Substring(fileName.LastIndexOf("/") + 1);
            string saveFileName = DealfileName(docName);
            DownloadFile(fileName, saveFileName);
        }
        public void DownloadFile(string url, string fileName)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(ProjectDll.DAL.Common.SPWeb.ID))
                        {
                            SPFile file = web.GetFile(url);
                            ProjectDll.DAL.Common.DownLoadFileByStream(file, Response, fileName);

                        }
                    }
                });
            }
            catch (Exception)
            { }
        }
        private string DealfileName(string fileName)
        {
            string txtFile = fileName.Substring(0, fileName.IndexOf("-")) + fileName.Substring(fileName.LastIndexOf("-") + 1);
            return txtFile;
        }
        private void FillDocView(HtmlControl divControl)
        {
            string txtDocView = "<iframe src='" + ProjectDll.DAL.Common.SPWeb.Url + "/_layouts/15/WopiFrame.aspx?sourcedoc=thisdocurl&action=embedview'  width='600px' height='470px'></iframe>";

            divControl.Controls.Clear();
            List<proj_ProjectFile> ds =GetProjectDocFiles;
            string txtUrl;
            string subWebUrl = SPContext.Current.Web.Url;
            int urlIndex = subWebUrl.IndexOf("/", 8, StringComparison.Ordinal);
            if (urlIndex > 0)
            {
                subWebUrl = subWebUrl.Substring(urlIndex);
            }
            else
            {
                subWebUrl = "/";
            }
            LinkButton btn = new LinkButton();
            foreach (proj_ProjectFile dr in ds)
            {
                txtUrl = dr.FilePath;
                string docUrl = subWebUrl + txtUrl;
                txtDocView = txtDocView.Replace("thisdocurl", docUrl);//加上子网站的链接
                divControl.Controls.Add(new LiteralControl(txtDocView));
                btn = new LinkButton();
                btn.ID = "btn" + dr.FileID;
                btn.CommandArgument = docUrl;
                btn.Click += btn_Click;
                btn.Text = "下载到本地";
                divControl.Controls.Add(btn);
            }
        }
        #endregion

        
    }
}
