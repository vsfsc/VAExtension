using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class PublishProject : LayoutsPageBase
    {
        string thisUrl= SPContext.Current.Web.Url;
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
            btnSubmit.Click += btnSubmit_Click;
            btnSave.Click += btnSave_Click;
            //gvProject.PageIndexChanging += gvProject_PageIndexChanging;
            //gvProject.RowDataBound += gvProject_RowDataBound;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsForm1Valid"] == null)
                {
                    Session["IsForm1Valid"] = "true";
                    //下接初始化代码
                    DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(0), ddlSubjectA);//一级学科绑定
                }
                
                //BindGvData(currentUserId, gvProject);
                BindgvProjectFile();
            }
        }

        private static string CutWebUrl(string myurl)
        {
            string url="";
            string weburl = SPContext.Current.Web.Url;
            if (myurl.Contains(weburl))
            {
                url = myurl.Replace(weburl, "");
            }
            return url;
        }
        private void BindgvProjectFile()
        {
            var cellStrings = new[] { "IconUrl", "Name", "fileUrl", "fileSize", "fileAuthor", "TimeCreated","fileExName"};
            DataTable dt = ProjectDll.DAL.SharePointFileHelper.BindDoclib("项目文档", cellStrings);
            gvmyFiles.DataSource = dt;
            gvmyFiles.DataBind();
        }
        
        protected void gvmyFiles_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var img = (Image) e.Row.FindControl("iconImg");//文件类型图标
                var imgUrl = DataBinder.Eval(e.Row.DataItem, "IconUrl").ToString();
                img.ImageUrl = SPContext.Current.Web.Url +"/"+ imgUrl;

                var fileUrl = CutWebUrl(DataBinder.Eval(e.Row.DataItem, "fileUrl").ToString());//文件地址
                var elink = (HyperLink)e.Row.FindControl("viewDoc");
                elink.NavigateUrl = String.Format(SPContext.Current.Web.Url + "/_layouts/15/WopiFrame.aspx?sourcedoc=" + fileUrl + "&action=VIEW");
                elink.ForeColor = System.Drawing.Color.Blue;
            }
        }
        //创建GridView列的方法
        private void CreatGriew(string[] cellStrings)
        {
            for (int i = 0; i < cellStrings.Length; i++)
            {
                CreateGridColumn(cellStrings[i], cellStrings[i], 20, "", "");
            }
            
        }
        private void CreateGridColumn(string dataField, string headerText, int width, string headerStyle, string itemStyle)
        {
            BoundField bc = new BoundField();
            bc.DataField = dataField;
            bc.HeaderText = headerText;
            bc.HeaderStyle.CssClass = headerStyle;  //若有默认样式，此行代码及对应的参数可以移除
            bc.ItemStyle.CssClass = itemStyle;   //若有默认样式，此行代码及对应的参数可以移除
            gvmyFiles.Columns.Add(bc);  //把动态创建的列，添加到GridView中
            gvmyFiles.Width = new Unit(gvmyFiles.Width.Value + width); //每添加一列后，要增加GridView的总体宽度

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
                        if (ftList.Any(fileType => fileExtension == fileType.TypeName))
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

                                            proj_ProjectFile pFile = ProjectDll.DAL.ProjectFileDAL.JudgeIsExistInDb(fname, currentUserId);
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
        /// 控制按钮显示
        /// </summary>
        /// <param name="i">页面状态号:1.新发布页面;2.</param>
        private void SetControls(int i)
        {
            if (i == 1)
            {
                btnSave.Visible = false;
                btnSubmit.Visible = true;
                prID.Value = "0";
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
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="path"></param>
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
        /// <param name="path"></param>
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
        /// 保存按钮,修改更新项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnSubmit.Visible = true;
            var dr =new Proj_Project
            {
                ProjectID = long.Parse(prID.Value),
                PName = txtName.Value,
                Introduce = txtIntroduce.Value
            };
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
                dr.SubjectID = int.Parse(ddlSubjectC.SelectedValue);
            }
            else
            {
                //当2级学科有值时
                if (long.Parse(ddlSubjectB.SelectedValue) != 0)
                {
                    dr.SubjectID = int.Parse(ddlSubjectB.SelectedValue);
                }
                else
                {
                    //当1级学科有值时
                    if (long.Parse(ddlSubjectA.SelectedValue) != 0)
                    {
                        dr.SubjectID = long.Parse(ddlSubjectA.SelectedValue);
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
            //    dr.SubjectID = int.Parse(ddlSubjectD.SelectedValue);
            //}


            //修改时间和修改人
            dr.ModifiedBy = currentUserId;
            dr.ModifyTime = DateTime.Now;
            dr.Flag = 1;

            ProjectDll.DAL.ProjectDal.UpdateProjectById(dr);
            //BindGvData(currentUserId, gvProject);

            //error.Text = "";
            ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "保存成功！");
            Response.Redirect("Initiate.aspx");
            //Response.Redirect(DAL.Common.SPWeb.Url + "/_layouts/15/ContestWeb/pubContest.aspx");
        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Value))
            {
                ProjectDll.DAL.Common.Alert("项目名称不可为空!");
                txtName.Focus();
                return;
            }
            Proj_Project ds = ProjectDll.DAL.ProjectDal.GetProjectByTitle(txtName.Value.ToString());
            if (ds != null)
            {
                //error.Text = "项目名称已经存在";
                txtName.Focus();
                return;
            }
            var dr = new Proj_Project
            {
                PName = txtName.Value,
                Introduce = txtIntroduce.Value,
                Sponsor = currentUserId,
                CreatedDate = DateTime.Now
            };

            //发布时间和发布人

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
                dr.SubjectID = long.Parse(ddlSubjectC.SelectedValue);
            }
            else
            {
                //当2级学科有值时
                if (long.Parse(ddlSubjectB.SelectedValue) != 0)
                {
                    dr.SubjectID = long.Parse(ddlSubjectB.SelectedValue);
                }
                else
                {
                    //当1级学科有值时
                    if (long.Parse(ddlSubjectA.SelectedValue) != 0)
                    {
                        dr.SubjectID = long.Parse(ddlSubjectA.SelectedValue);
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
            //    dr.SubjectID = long.Parse(ddlSubjectD.SelectedValue);
            //}
            dr.PCode = "P" + DateTime.Now.ToString("yy") + ProjectDll.BLL.RandomKeys.Number(6);//+ string.Format("{0:D6}", dr.ProjectID));//项目编码
            dr.Flag = 1;
            ProjectDll.DAL.ProjectDal.NewProject(dr);
            if (Session["IsForm1Valid"].ToString() == "true")
            {

                Session["IsForm1Valid"] = "false";
                Server.Transfer("MyProjects.aspx");
            }
            //BindGvData(currentUserId, gvProject);
            //ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "添加成功！");
            //Response.Redirect(ProjectDll.DAL.Common.SPWeb.Url + "/_layouts/15/Project/Initiate.aspx");
        }
        /// <summary>
        ///  the subjectDDLs data bind.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="ddl">The DDL.</param>
        private static void DdlSubjectDataBind(List<Subject> dt, DropDownList ddl)
        {
            if (dt.Count == 0)
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
