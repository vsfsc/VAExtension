using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ProjectDll;

namespace Project.Layouts.Project
{

    public partial class Initiate : LayoutsPageBase
    {
        readonly long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
        private SPWeb web; //当前网站
        private SPSite site;

        #region 属性
        /// <summary>
        /// 当前网站
        /// </summary>
        private SPWeb MyWeb
        {
            get
            {
                if (web == null)
                {
                    web = SPContext.Current.Web;
                }
                return web;
            }
        }
        private SPSite MySite
        {
            get
            {
                if (site == null)
                {
                    site = SPContext.Current.Site;
                }
                return site;
            }
        }
        #endregion
        //public Proj_Project ProjectDt
        //{
        //    get
        //    {
        //        Proj_Project projectDt=new Proj_Project();
        //        if (projectDt == null)
        //        {
        //            projectDt = new Proj_Project();
        //        }
        //        return projectDt;
        //    }

        //}
        //private Proj_Project worksDt;
        #region 初始化事件
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }
        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            btnSubmit.Click +=btnSubmit_Click;
            
            btnSave.Click += btnSave_Click;
            
            gvProject.PageIndexChanging += gvProject_PageIndexChanging;
            //gvProject.RowDataBound += gvProject_RowDataBound;

        }
        #endregion
        void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((HyperLink)e.Row.Cells[5].FindControl("lnkStandard")).NavigateUrl = "ScoreStandard.aspx?PeriodID=" + e.Row.Cells[0].Text;
                ((HyperLink)e.Row.Cells[4].FindControl("lnkUpload")).NavigateUrl = "OnlineEnroll.aspx?IsSample=1&&PeriodID=" + e.Row.Cells[0].Text;
            }
        }
        /// <summary>
        /// Handles the PageIndexChanging event of the gvProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewPageEventArgs"/> instance containing the event data.</param>
        void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProject.PageIndex = e.NewPageIndex;
            BindGvData(currentUserId, gvProject);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGvData(currentUserId, gvProject);
                hfID.Value = null;
                DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(0),ddlSubjectA);//一级学科绑定
            }
        }
        
        /// <summary>
        ///  the subjectDDLs data bind.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="ddl">The DDL.</param>
        private static void DdlSubjectDataBind(List<Subject> dt,DropDownList ddl)
        {
            if (dt.Count==0)
            {
                ddl.Visible = false;
            }
            else
            {
                dt = dt.OrderBy(d => d.SubjectName).ToList();
                ddl.Visible = true;
                ddl.DataSource = dt;
                ddl.DataTextField = "SubjectName";
                ddl.DataValueField = "SubjectID";
                ddl.DataBind();
            }
        }

        /// <summary>
        /// 作品上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpFile_OnClick(object sender, EventArgs e)
        {
            //1文档类  2图片类  3音频类  4视频类 5其他类 
            bool allowUpload = false;
            List<FileType> ftList = ProjectDll.DAL.ProjectFileDAL.GetFileTypes().Where(ft => ft.ParentID != 0).ToList();
            string[] extensionType = { ".docx", ".doc", ".pdf", ".ppt", ".pptx", ".xls", ".xlsx", ".mp4", ".accdb", ".png", ".jpg", ".gif", ".bmp", ".mp3" };
            try
            {
                if (fuWorks.HasFile == false)
                {
                    ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "请选择文件");
                }
                else
                {
                    var extension = Path.GetExtension(fuWorks.FileName);//获取文件扩展名,确认文件格式
                    if (extension != null)
                    {
                        string fileExtension = extension.ToLower();
                        if (ftList.Any(fileType => fileExtension==fileType.TypeName))
                        {
                            allowUpload = true;
                        }
                        //判断文件类型是否符合要求
                        foreach (var exStr in extensionType)
                        {
                            if (fileExtension == exStr)
                            {
                                allowUpload = true;
                                break;
                            }
                        }
                        if (allowUpload)
                        {
                            //文档1  图片2  音视频3   
                            //通过后缀判断存放在哪个文档库
                            if (fileExtension == ".mp4" || fileExtension == ".mp3")//音视频媒体类作品
                            {
                                this.AddWorkFile(MyWeb, "MediaWorks", fuWorks, "imageFile", gvFiles, 3);

                            }
                            //".png", ".jpg", ".gif", ".bmp",
                            else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".gif" || fileExtension == ".bmp")//图片类作品
                            {
                                this.AddWorkFile(MyWeb, "ImageWorks", fuWorks, "imageFile", gvFiles, 2);
                            }
                            else//文档类作品
                            {
                                this.AddWorkFile(MyWeb, "DocWorks", fuWorks, "imageFile", gvFiles, 1);
                            }

                        }
                        else
                        {
                            ProjectDll.DAL.Common.Alert("只能上传指定格式的文件！<br/>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProjectDll.DAL.Common.Alert(ex.ToString());
            }
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="web">The web.</param>
        /// <param name="docLibName">Name of the document library.</param>
        /// <param name="fUpload">The fupload.</param>
        /// <param name="viewStateName">Name of the view state.</param>
        /// <param name="gv">The GridView.</param>
        /// <param name="type">The type.</param>
        private void AddWorkFile(SPWeb web, string docLibName, FileUpload fUpload, String viewStateName, GridView gv, int type)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite elevatedsiteColl = new SPSite(MySite.ID))
                    {
                        using (SPWeb elevatedSite = elevatedsiteColl.OpenWeb(MyWeb.ID))
                        {
                            string urlStr = elevatedSite.Url;
                            elevatedSite.AllowUnsafeUpdates = true;
                            SPList list = elevatedSite.Lists.TryGetList(docLibName);
                            //如果没有文档库，生成文档库
                            if (list == null)
                            {
                                list = ProjectDll.DAL.ProjectFileDAL.CreateList(docLibName);
                                urlStr = urlStr + "\\" + docLibName;
                            }
                            var docLib = (SPDocumentLibrary)list;
                            if (fUpload.HasFile)
                            {

                                //数据库

                                //判断是否存在 
                                if (JudgeSize(fUpload.PostedFile.ContentLength))//判断文件大小是否超出限制
                                {
                                    ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "超出上传文档大小限制！");
                                }
                                else
                                {
                                    string fname = type.ToString().PadLeft(2, '0') + DateTime.Now.Year + ddlSubjectC.SelectedValue.PadLeft(4, '0') + "-" + ProjectDll.BLL.User.GetAccount() + "-" + Path.GetFileName(fUpload.PostedFile.FileName);
                                    Stream stm = fUpload.PostedFile.InputStream;
                                    int iLength = (int)stm.Length;
                                    if (iLength > 0)
                                    {

                                        SPFolder rootFolder = docLib.RootFolder;
                                        Byte[] filecontent = new byte[iLength];
                                        stm.Read(filecontent, 0, iLength);
                                        try
                                        {

                                            SPFile f;
                                            //如果在文档库中有,删除文档库中同名文档后，上传文档
                                            bool isDocHave = false;

                                            isDocHave = ProjectDll.DAL.ProjectFileDAL.JudgeIsExistInDocLib(docLib, fname, type);
                                            proj_ProjectFile isDBHave = ProjectDll.DAL.ProjectFileDAL.JudgeIsExistInDb(fname, currentUserId);
                                            //判断文档库
                                            if (isDocHave)
                                            {
                                                ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "请注意，该文件已存在，上传后自动覆盖已有文件！");
                                                DelList(fname, type.ToString());
                                                f = rootFolder.Files.Add(fname, filecontent, true);

                                            }
                                            else
                                            {
                                                f = rootFolder.Files.Add(fname, filecontent, true);
                                            }
                                            urlStr += "\\" + fname;

                                            ////判断数据库
                                            //if (isDBHave)
                                            //{
                                            //    //如果数据库中有，更新数据库
                                            //}
                                            //else
                                            //{
                                            //    //如果没有插入数据库

                                            //    //数据库

                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                            ProjectDll.DAL.Common.Alert(ex.ToString());
                                        }
                                        finally
                                        {
                                            stm.Close();
                                        }

                                    }
                                }
                            }
                            elevatedSite.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ProjectDll.DAL.Common.Alert(ex.ToString());
                // DAL.Common.ShowMessage(this.Page, this.GetType(), "出错了，请报告给管理员！");
            }
            finally
            {

            }
            //BindGridView((DataTable)ViewState[viewStateName], gv);
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        private void DelList(string fileName, string type)
        {
            if (type == "4" || type == "3")
            {
                DelListItem(fileName, "MediaWorks");
            }
            else if (type == "2")
            {
                DelListItem(fileName, "ImageWorks");
            }
            else
            {
                DelListItem(fileName, "DocWorks");
            }
        }

        /// <summary>
        /// 删除附件项
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="listName"></param>
        private void DelListItem(string fileName, string listName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite ElevatedsiteColl = new SPSite(MySite.ID))
                {
                    using (SPWeb ElevatedSite = ElevatedsiteColl.OpenWeb(MyWeb.ID))
                    {
                        ElevatedSite.AllowUnsafeUpdates = true;
                        SPList list = ElevatedSite.Lists.TryGetList(listName);
                        SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                        SPFolder rootFolder = docLib.RootFolder;
                        if (listName == "MediaWorks")
                        {

                            for (int i = 0; i < rootFolder.SubFolders.Count; i++)
                            {
                                if (rootFolder.SubFolders[i].Files[0].Name == fileName)
                                {
                                    rootFolder.SubFolders[i].Delete();

                                }
                            }
                            //不开启资产库
                            for (int i = 0; i < rootFolder.Files.Count; i++)
                            {
                                if (rootFolder.Files[i].Name == fileName)
                                {
                                    rootFolder.Files[i].Delete();
                                }
                            }

                        }
                        else
                        {
                            for (int i = 0; i < rootFolder.Files.Count; i++)
                            {
                                if (rootFolder.Files[i].Name == fileName)
                                {
                                    rootFolder.Files[i].Delete();
                                }
                            }
                        }
                        ElevatedSite.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        /// <summary>
        /// 判断是否超出大小
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool JudgeSize(long i)
        {
            bool isSize = false;
            //更改大小
            int size = int.Parse(ConfigurationManager.AppSettings["uploadSize"]);
            // 52428801
            if (i > size)
            {
                isSize = true;
            }
            return isSize;
        }

        /// <summary>
        /// Handles the NextStep Button event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WizardNavigationEventArgs"/> instance containing the event data.</param>
        protected void OnNextClick(object sender, WizardNavigationEventArgs e)
        {
            //wzdProjectInfo.BorderWidth = Unit.Pixel((int)(wzdProjectInfo.BorderWidth.Value + 1));
            btnSave.Visible = false;
            btnSubmit.Visible = true;
            var projectDt = new Proj_Project();
            var dr = projectDt;
            
            dr.PName = txtName.Text;//项目名称
            dr.Introduce = txtIntroduce.Text;//项目简介

            long subjectId;//学科ID
            //3级学科判断
            //if (ddlSubjectC.SelectedValue!="")
            //{
                if (ddlSubjectC.SelectedValue != null && int.Parse(ddlSubjectC.SelectedValue) != 0)
                {
                    subjectId = int.Parse(ddlSubjectC.SelectedValue);
                }
                else
                {
                    //2级学科判断
                    if (ddlSubjectB.SelectedValue != null && int.Parse(ddlSubjectB.SelectedValue) != 0)
                    {
                        subjectId = int.Parse(ddlSubjectB.SelectedValue);
                    }
                    else
                    {
                        //1级学科判断
                        subjectId = ddlSubjectA.SelectedValue != null && int.Parse(ddlSubjectA.SelectedValue) != 0 ? int.Parse(ddlSubjectA.SelectedValue) : 0;
                    }
                }

                dr.SubjectID = subjectId;
            //}
            //修改时间和修改人
            dr.ModifiedBy = currentUserId;
            dr.ModifyTime = DateTime.Now;
            dr.Flag = 1;

            if (hfID!=null)
            {
                dr.ProjectID = long.Parse(hfID.Value);
                ProjectDll.DAL.ProjectDal.UpdateProjectById(dr);
                BindGvData(currentUserId, gvProject);
                error.Text = "";
                //ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "保存成功！");
            }
            else
            {
                ProjectDll.DAL.ProjectDal.NewProject(dr);
                BindGvData(currentUserId, gvProject);
                error.Text = "";
                //ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "保存成功！");
            }
            
        }
        /// <summary>
        /// 保存按钮,项目信息修改后保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnSubmit.Visible = true;
            var projectDt = new Proj_Project();
            var dr = projectDt;
            dr.ProjectID = long.Parse(hfID.Value);
            dr.PName = txtName.Text;

            dr.Introduce = txtIntroduce.Text;

            long subjectId;
            //添加作品类别
            //if (long.Parse(ddlProjectTypeB.SelectedValue) == 0)
            //{
            //    dr.TypeID = int.Parse(ddlProjectTypeA.SelectedValue);
            //}
            //else
            //{
            //    dr.TypeID = int.Parse(ddlProjectTypeB.SelectedValue);
            //}
            
            //添加作品学科(四级学科属性)
            //当4级学科无值时
            //if (long.Parse(ddlSubjectD.SelectedValue) == 0)
            //{
                //当3级学科有值时
                if (long.Parse(ddlSubjectC.SelectedValue) != 0)
                {
                    subjectId = int.Parse(ddlSubjectC.SelectedValue);
                }
                else
                {
                    //当2级学科有值时
                    if (long.Parse(ddlSubjectB.SelectedValue) != 0)
                    {
                        subjectId = int.Parse(ddlSubjectB.SelectedValue);
                    }
                    else
                    {
                        //当1级学科有值时
                        subjectId = long.Parse(ddlSubjectA.SelectedValue) != 0 ? int.Parse(ddlSubjectA.SelectedValue) : 0;
                    }

                }
            //}
            //else
            //{
            //    //当4级学科有值时
            //    subjectId = int.Parse(ddlSubjectD.SelectedValue);
            //}

            dr.SubjectID = subjectId;
            //修改时间和修改人
            dr.ModifiedBy = currentUserId; 
            dr.ModifyTime = DateTime.Now;
            dr.Flag = 1;

            ProjectDll.DAL.ProjectDal.UpdateProjectById(dr);
            BindGvData(currentUserId,gvProject);
         
            error.Text = "";
            ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "保存成功！");
            Response.Redirect("Initiate.aspx");
            //Response.Redirect(DAL.Common.SPWeb.Url + "/_layouts/15/ContestWeb/pubContest.aspx");
        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))//判断项目名称是否为空
            {
                ProjectDll.DAL.Common.Alert("项目名称不可为空!");
                txtName.Focus();
                return;
            }
            var ds = ProjectDll.DAL.ProjectDal.GetProjectByTitle(txtName.Text.Trim(' '));
            if (ds != null)//判断项目名称是否重复
            {
                error.Text = "项目名称已经存在";
                txtName.Focus();
                return;
            }
            long subjectId;
            //添加作品类别
            //当小类不限时(小类别无值)
            //if (long.Parse(ddlProjectTypeB.SelectedValue) == 0)
            //{
            //    //当大类有值时
            //    if (long.Parse(ddlProjectTypeA.SelectedValue) != 0)
            //    {
            //        dr.TypeID = int.Parse(ddlProjectTypeA.SelectedValue);
            //    }
            //    else
            //    {

            //        dr.TypeID = 0;
            //    }
            //}
            //else
            //{
            //    //当小类有值时
            //    dr.TypeID = int.Parse(ddlProjectTypeB.SelectedValue);
            //}

            //添加作品学科(四级学科属性)
            //当4级学科无值时
            //if (long.Parse(ddlSubjectD.SelectedValue) == 0)
            //{
            //当3级学科有值时

            if (long.Parse(ddlSubjectC.SelectedValue) != 0)
            {
                subjectId = long.Parse(ddlSubjectC.SelectedValue);
            }
            else
            {
                //当2级学科有值时
                if (long.Parse(ddlSubjectB.SelectedValue) != 0)
                {
                    subjectId = long.Parse(ddlSubjectB.SelectedValue);
                }
                else
                {
                    //当1级学科有值时
                    if (long.Parse(ddlSubjectA.SelectedValue) != 0)
                    {
                        subjectId = long.Parse(ddlSubjectA.SelectedValue);
                    }
                    else
                    {
                        ProjectDll.DAL.Common.Alert("还未选择项目所属学科!");
                        ddlSubjectA.Focus();
                        return;
                    }
                }
            }
            //}
            //else
            //{
            //    //当4级学科有值时
            //    subjectId = long.Parse(ddlSubjectD.SelectedValue);
            //}
            var dr = new Proj_Project
            {
                PName = txtName.Text,//项目名称
                PCode =  "P" +DateTime.Now.ToString("yy")+ ProjectDll.BLL.RandomKeys.Number(6),//项目编码
                Introduce = txtIntroduce.Text,//项目简介
                Sponsor = currentUserId,//发布人
                CreatedDate = DateTime.Now,//发布时间
                SubjectID = subjectId,//学科ID
                Flag = 1//显示隐藏标记
            };
            ProjectDll.DAL.ProjectDal.NewProject(dr);
            BindGvData(currentUserId, gvProject);
            ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "添加成功！");
            Response.Redirect(ProjectDll.DAL.Common.SPWeb.Url + "/_layouts/15/Project/Initiate.aspx");
        }
        /// <summary>
        /// Handles the RowCommand event of the gvPeriod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        void gvPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                Proj_Project dr = new Proj_Project();
                dr.ProjectID = long.Parse(drv.Cells[0].Text);
                DateTime dtNow = DateTime.Now;
                ProjectDll.DAL.ProjectDal.DelProjectById(dr, currentUserId, dtNow);
                BindGvDate();
                SetControls(1);
            }

            if (e.CommandName == "EditProject")
            {
                GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                List<Proj_Project> dt = ProjectDll.BLL.ProjectBll.GetProjectByID(long.Parse(drv.Cells[0].Text));
                if (dt.Count>0)
                {
                    txtName.Text = dt.First().PName;
                    txtIntroduce.Text = dt.First().Introduce;
                }
                

                //txtRequire.Text = dt.Require;

                //txtNum.Text = dt.Number.ToString();
               
                ////当存的不是不限时
                //if (dt.LevelID.ToString() != "")
                //{
                //    //有小类
                //    if (dt.LevelID.ToString() != "0")
                //    {
                //        BindOneWorksType(WorksType);

                //        //加载作品类别
                //        ddlOneWorksType.SelectedIndex = -1;
                //        ddlOneWorksType.Items.FindByValue(dt.ParentID.ToString()).Selected = true;
                //        BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                //        ddlTwoWorksType.SelectedIndex = -1;
                //        ddlTwoWorksType.Items.FindByValue(dt.WorksTypeID.ToString()).Selected = true;

                //    }
                //    else
                //    {
                //        BindOneWorksType(WorksTypeDt);
                //        //加载作品类别

                //        ddlOneWorksType.SelectedIndex = -1;
                //        ddlOneWorksType.Items.FindByValue(dt.WorksTypeID.ToString()).Selected = true;
                //        BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                //        ddlTwoWorksType.SelectedIndex = -1;
                //        ddlTwoWorksType.Items.FindByValue("0").Selected = true;
                //    }
                //}
                //else
                //{

                //    BindOneWorksType(WorksTypeDt);
                //    //加载作品类别
                //    BindTwoWorksType(WorksTypeDt, ddlOneWorksType.SelectedValue);

                //}
                hfID.Value = drv.Cells[0].Text;
                BindGvData(currentUserId, gvProject);
                SetControls(2);
            }
        }

        /// <summary>
        /// 控制按钮显示
        /// </summary>
        /// <param name="i">页面状态号:1.新发布页面;2.</param>
        private void SetControls(int i)
        {
            if (i == 1)
            {
                btnSave.Visible = false;
                btnSubmit.Visible = true;
                hfID.Value = "0";
            }
            else if (i == 2)
            {
                btnSave.Visible = true;
                btnSubmit.Visible = false;
            }
            else if (i == 3)
            {
                btnSubmit.Enabled = false;
                btnSave.Enabled = false;

            }
        }
        //
        private void BindGvDate()
        {
            List<Proj_Project> dt = ProjectDll.DAL.ProjectDal.GetProjects();
            BindGridView(dt, gvProject);
        }

        /// <summary>
        /// 绑定数据到GridView控件
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="gv">GridView控件</param>
        private void BindGridView(IEnumerable dt, GridView gv)
        {
            gv.DataSource = dt;
            gv.DataBind();
        }
        /// <summary>
        /// Binds the gv data.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="gv">The gv.</param>
        private void BindGvData(long userId, GridView gv)
        {
            List<Proj_ProjectsCreatedByUser> dt = ProjectDll.BLL.ProjectBll.GetProjectsCreatedByUserId(userId);
            if (dt.Count>0)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                myProjectsDiv.Visible = false;
            }
           
        }
        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSubjectA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlSubjectA_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubjectC.Visible = false;
            //ddlSubjectD.Visible = false;
            if (long.Parse(ddlSubjectA.SelectedValue) != 0)
            {
                DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(long.Parse(ddlSubjectA.SelectedValue)), ddlSubjectB);//二级学科绑定
            }
            else
            {
                ddlSubjectB.Visible = false;
            }
            
        }
        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSubjectB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlSubjectB_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlSubjectD.Visible = false;
            if (long.Parse(ddlSubjectB.SelectedValue) != 0)
            {
                DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(long.Parse(ddlSubjectB.SelectedValue)), ddlSubjectC);//三级学科绑定
            }
            else
            {
                ddlSubjectC.Visible = false;
            }
        }

        //protected void ddlSubjectC_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (long.Parse(ddlSubjectC.SelectedValue) != 0)
        //    {
        //        DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(long.Parse(ddlSubjectC.SelectedValue)), ddlSubjectD);//四级学科绑定
        //    }
        //    else
        //    {
        //        ddlSubjectD.Visible = false;
        //    }
        //}
        
    }
}
