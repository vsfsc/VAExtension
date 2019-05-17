using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ProjectDll;


namespace Project.Layouts.Project
{
    using System.Net;
    using System.Net.Mime;

    public partial class PStart : LayoutsPageBase
    {


        #region 属性
        readonly long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
        private SPWeb web; //当前网站
        private SPSite site;
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
        private long GetUrlSendProjectId
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long projectId = GetUrlSendProjectId;
                if (projectId > 0)//编辑项目
                {
                    hfID.Value = projectId.ToString();
                    txtPName.Visible = false;
                    ddlSubjectA.Visible = false;
                    ddlSubjectB.Visible = false;
                    ddlSubjectC.Visible = false;

                    lbPName.Visible = true;
                    lbSubject.Visible = true;
                    var ds = ProjectDll.BLL.ProjectBll.GetProjectByID(projectId);
                    Proj_Project pr = ds.ToList().FirstOrDefault();
                    if (pr != null)
                    {
                        lbPageTitle.Text = "编辑项目:" + pr.PName;//页面标题
                        lbPName.Text = pr.PName;//项目名称
                        long? subId = pr.SubjectID;//学科绑定
                        if (subId > 0)//学科ID存在
                        {
                            long sid = long.Parse(subId.ToString());
                            Subject thissubject = ProjectDll.BLL.SubjectBll.GetSubjectById(sid).ToList().FirstOrDefault();
                            if (thissubject != null)
                            {
                                lbSubject.Text = thissubject.SubjectName;
                            }
                        }
                        btnSubjectChange.Visible = true;
                        txtIntro.Value = pr.Introduce;//项目简介
                        btnNewFile.Visible = true;
                        ShowLixiangFile(projectId);

                        btnSubmit.Visible = false;
                        btnSave.Visible = true;
                    }
                }
                else//新建项目
                {
                    lbPName.Visible = false;
                    lbSubject.Visible = false;
                    hfID.Value = null;
                    btnSubjectChange.Visible = false;
                    btnSubjectCancelChange.Visible = false;

                    txtPName.Visible = true;
                    txtPName.Text = "";
                    txtPName.Focus();

                    ddlSubjectA.Visible = true;
                    DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(0), ddlSubjectA);//一级学科绑定

                    btnSubmit.Visible = true;
                }

            }
        }
        private void ShowLixiangFile(long projId)
        {
            proj_ProjectFile pFile = ProjectDll.BLL.ProjectFileBll.GetFileByProjIdandPurpose(projId, 1).FirstOrDefault();
            if (pFile != null)
            {
                hdfFile.Value = pFile.FileID.ToString();
                BindMyFile(pFile.FileNameInDocLib);
                lbDocType.Visible = false;
            }
            else
            {
                fileDiv.InnerHtml = "你还未上传立项报告,请及时上传!";
                fileDiv.Attributes.CssStyle.Add("color", "red");
                lixiangFile.Visible = true;
                lbDocType.Visible = true;
                btnNewFile.Visible = true;
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
        protected void btnSubjectChange_OnClick(object sender, EventArgs e)
        {
            ddlSubjectA.Visible = true;
            DdlSubjectDataBind(ProjectDll.BLL.SubjectBll.GetSubjectsByParentId(0), ddlSubjectA);//一级学科绑定
            lbSubject.Visible = false;
            btnSubjectCancelChange.Visible = true;
            btnSubjectChange.Visible = false;
        }
        protected void btnSubjectCancel_OnClick(object sender, EventArgs e)
        {
            ddlSubjectA.Visible = false;
            ddlSubjectB.Visible = false;
            ddlSubjectC.Visible = false;
            btnSubjectChange.Visible = true;
            btnSubjectCancelChange.Visible = false;
            lbSubject.Visible = true;
        }
        /// <summary>
        /// 上传或加载页面时绑定已经上传的立项报告
        /// </summary>
        /// <param name="fNameInDocLib">The f name in document library.</param>
        private void BindMyFile(string fNameInDocLib)
        {
            fileDiv.Visible = true;
            proj_ProjectFile myPFile = ProjectDll.BLL.ProjectFileBll.GetFileByFileNameInDocLib(fNameInDocLib);
            if (myPFile != null)
            {
                fileDiv.Controls.Clear();
                var hccFiles = new HtmlGenericControl("div");
                string fileInfoStr = "<span> 已上传报告：</span>";
                fileInfoStr += myPFile.FileName + "（" + ProjectDll.DAL.Common.HumanReadableFilesize(double.Parse(myPFile.FileSize)) + "）&nbsp; &nbsp;";
                hccFiles.Controls.Add(new LiteralControl(fileInfoStr));
                var iBtnDel = new ImageButton
                {
                    ImageUrl = "./images/delete.png",
                    ToolTip = "点击删除该文件并重新上传",
                    Width = 24,
                    CausesValidation = false,
                    CommandArgument = myPFile.FileID.ToString(),
                    CommandName = "fileDel" + myPFile.FileID,
                    ID = "btnDel" +  myPFile.FileID,
                    OnClientClick = "javascript:return confirm('确定删除该项目报告并重新上传吗?');"
                };
                iBtnDel.Click += btnDelFile_Click;
                iBtnDel.Attributes.Add("class", "btn");
                hccFiles.Controls.Add(iBtnDel);
                fileDiv.Controls.Add(hccFiles);
            }

        }
        void btnDelFile_Click(object sender, ImageClickEventArgs e)
        {
            long userId = ProjectDll.BLL.User.GetUserId();
            var btn = (ImageButton)sender;
            btn.CausesValidation = false;
            long fileId = long.Parse(btn.CommandArgument);
            proj_ProjectFile pf = ProjectDll.BLL.ProjectFileBll.GetFileByFileId(fileId);
            string fNameInDocLib = pf.FileNameInDocLib;
            DelListItem(fNameInDocLib, "ProjectReports");//删除文档库文件
            ProjectDll.DAL.ProjectFileDAL.DelFileById(fileId,userId,DateTime.Now);//删除数据库项
            fileDiv.InnerHtml = "你还未上传立项报告,请及时上传!";
            lixiangFile.Visible = true;
            btnNewFile.Visible = true;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="docLibName">Name of the document library.</param>
        /// <param name="fUpload">The fupload.</param>
        /// <param name="type">The type.</param>
        private void AddWorkFile(string docLibName, FileUpload fUpload, int type)
        {
            long userId = ProjectDll.BLL.User.GetUserId();
            try
            {
                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //{
                    using (var elevatedsiteColl = new SPSite(MySite.ID))
                    {
                        using (var elevatedSite = elevatedsiteColl.OpenWeb(MyWeb.ID))
                        {
                            string urlStr = elevatedSite.Url;
                            elevatedSite.AllowUnsafeUpdates = true;
                            SPList list = elevatedSite.Lists.TryGetList(docLibName);
                            if (list == null) //如果没有文档库，先创建文档库
                            {
                                list = ProjectDll.DAL.ProjectFileDAL.CreateList(docLibName);
                            }
                            urlStr += "//" + docLibName;
                            var docLib = (SPDocumentLibrary)list;
                            if (fUpload.HasFile)
                            {
                                string fName = Path.GetFileName(fUpload.PostedFile.FileName);
                                string fLength = fUpload.PostedFile.ContentLength.ToString();
                                string fTypeString = Path.GetExtension(fName).ToLower();
                                fTypeString = fTypeString.TrimStart('.');
                                int? fTypeId = ProjectDll.BLL.ProjectFileBll.GetFileTypeByFileExtension(fTypeString).FileTypeID;
                                string fName0InDocLib = "";

                                //第一步: 数据库操作
                                var pFile = ProjectDll.BLL.ProjectFileBll.GetFileByCreatedBy(userId).First(f => f.ProjectID==0||f.ProjectID==null);//最新上传的且与项目无关联的
                                //判断数据库是否存在记录
                                string fName1InDocLib = "";
                                if (pFile != null)//存在记录，则更新数据库记录
                                {
                                    fName0InDocLib = pFile.FileNameInDocLib;//原有文档库文件名
                                    //文档库中文件名的命名规则:(新编号+上传日期)-上传者账户名-文件名
                                    fName1InDocLib = (int.Parse(fName0InDocLib.Substring(0, 2)) + 1).ToString("00") +DateTime.Now.Year+ProjectDll.BLL.RandomKeys.Number(4)+ "-" +ProjectDll.BLL.User.GetAccount() + "-" + fName;//新文档库文件名
                                    urlStr += "//" + fName1InDocLib;

                                    var drFile = new proj_ProjectFile
                                    {
                                        FileID = pFile.FileID,
                                        ProjectID = GetUrlSendProjectId,
                                        FileName = fName,
                                        FileNameInDocLib = fName1InDocLib,
                                        FilePath = urlStr,
                                        FileSize = fLength,
                                        FileTypeID = fTypeId,
                                        Modified = DateTime.Now,
                                        ModifiedBy = currentUserId,
                                        Flag = 1
                                    };
                                    ProjectDll.DAL.ProjectFileDAL.UpdataFile(drFile);
                                    hdfFile.Value = pFile.FileID.ToString();
                                    BindMyFile(fName1InDocLib);
                                }
                                else//不存在记录,则新建数据库记录并插入数据库
                                {
                                    //文档库中文件名的命名规则:(编号+上传日期)-上传者账户名-文件名
                                    fName1InDocLib = "01" + DateTime.Now.Year+ProjectDll.BLL.RandomKeys.Number(4) + "-" +
                                                     ProjectDll.BLL.User.GetAccount() + "-" + fName;//新文档库文件名
                                    urlStr += "//" + fName1InDocLib;
                                    var drFile = new proj_ProjectFile
                                    {
                                        FileName = fName,
                                        FileNameInDocLib = fName1InDocLib,
                                        FileTypeID = fTypeId,
                                        FilePurpose = 1,
                                        FilePath = urlStr,
                                        FileSize = fLength,
                                        Created = DateTime.Now,
                                        CreatedBy = currentUserId,
                                        Flag = 1
                                    };
                                    long fid=ProjectDll.DAL.ProjectFileDAL.InsertProjectFile(drFile);
                                    hdfFile.Value = fid.ToString();
                                    BindMyFile(fName1InDocLib);
                                }


                                //第二步: 文档库操作
                                Stream stm = fUpload.PostedFile.InputStream;
                                int iLength = (int)stm.Length;
                                if (iLength > 0)
                                {

                                    SPFolder rootFolder = docLib.RootFolder;
                                    Byte[] filecontent = new byte[iLength];
                                    stm.Read(filecontent, 0, iLength);
                                    try
                                    {
                                        bool isDocHave = ProjectDll.DAL.ProjectFileDAL.JudgeIsExistInDocLib(docLib, fName0InDocLib, type);//判断文档库是否存在该文件

                                        if (isDocHave)//存在,则删除文档库中同名文档
                                        {
                                            //ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "请注意，该文件已存在，上传后自动覆盖已有文件！");
                                            DelListItem(fName0InDocLib, docLibName);

                                        }
                                        //上传文档
                                        rootFolder.Files.Add(fName1InDocLib, filecontent, true);
                                    }
                                    catch (Exception ex)
                                    {
                                        ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "出错了，请报告给管理员！");
                                    }
                                    finally
                                    {
                                        stm.Close();
                                    }


                                }
                            }
                            elevatedSite.AllowUnsafeUpdates = false;
                        }
                    }
                //});
            }
            catch (Exception ex)
            {
                ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "出错了，请报告给管理员！");
            }
            finally
            {

            }
            //BindGridView((DataTable)ViewState[viewStateName], gv);
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
        /// 删除附件项
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="listName"></param>
        private void DelListItem(string fileName, string listName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite elevatedsiteColl = new SPSite(MySite.ID))
                {
                    using (SPWeb elevatedSite = elevatedsiteColl.OpenWeb(MyWeb.ID))
                    {
                        elevatedSite.AllowUnsafeUpdates = true;
                        SPList list = elevatedSite.Lists.TryGetList(listName);
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
                        elevatedSite.AllowUnsafeUpdates = false;
                    }
                }
            });
        }


        protected void btnNewFile_OnClick(object sender, EventArgs e)
        {
            if (IsAllowUpload(lixiangFile))
            {
                AddWorkFile("ProjectReports", lixiangFile, 1);
            }
            else
            {
                ProjectDll.DAL.Common.Alert("你还未选择立项报告，或者您上传的报告文档格式不对或大小超出限制！");
                lixiangFile.Focus();
            }

        }
        private bool IsAllowUpload(FileUpload fu)
        {
            //判断项目立项报告是否准备好上传
            bool allowUpload = false;
            if (fu.HasFile)
            {
                string extension = Path.GetExtension(fu.FileName);//获取文件扩展名,确认文件格式
                if (extension != null)
                {
                    //判断文件类型是否符合要求
                    string[] extensionType = { ".docx", ".doc", ".pdf", ".ppt", ".pptx", ".xls", ".xlsx" };
                    allowUpload = extensionType.Any(exStr => extension.ToLower() == exStr);
                    allowUpload = !JudgeSize(fu.PostedFile.ContentLength);
                }
            }
            return allowUpload;

        }
        protected void NewProject_OnClick(object sender, EventArgs e)
        {
            #region 表单信息(项目名称)
            //项目名称
            if (string.IsNullOrEmpty(txtPName.Text))//判断项目名称是否为空
            {
                ProjectDll.DAL.Common.Alert("项目名称不可为空!");
                txtPName.Focus();
                return;
            }
            var ds = ProjectDll.DAL.ProjectDal.GetProjectByTitle(txtPName.Text.Trim(' '));
            if (ds != null)//判断项目名称是否重复
            {
                ProjectDll.DAL.Common.Alert("项目名称已经存在,请重新填写!");
                txtPName.Focus();
                return;
            }
            //学科选择
            long subjectId;
            if (ddlSubjectC.Items.Count <= 0)
            {
                ProjectDll.DAL.Common.Alert("还未选择项目所属学科!");
                ddlSubjectA.Focus();
                return;
            }
            switch (ddlSubjectC.SelectedValue)
            {
                case null:
                    ProjectDll.DAL.Common.Alert("还未选择项目所属学科!");
                    ddlSubjectA.Focus();
                    return;
                default:
                    subjectId = long.Parse(ddlSubjectC.SelectedValue);
                    break;
            }

            #endregion

            #region 判断项目立项报告是否准备好上传

            if (lixiangFile.HasFile && fileDiv.Visible == false)
            {
                ProjectDll.DAL.Common.Alert("你要上传的文档尚未提交!");
                lixiangFile.Focus();
                return;
            }

            #endregion

            #region 所有表单信息验证已通过,生成项目记录
            //新建项目,调用NewProject
            var dr = new Proj_Project
            {
                PName = txtPName.Text,//项目名称
                PCode = "P" + DateTime.Now.ToString("yy") + ProjectDll.BLL.RandomKeys.Number(6),//项目编码
                Introduce = txtIntro.Value,//项目简介
                Sponsor = currentUserId,//发布人
                CreatedDate = DateTime.Now,//发布时间
                SubjectID = subjectId,//学科ID
                IsMatch = 0,
                Flag = 1//显示隐藏标记
            };
            long pid=ProjectDll.DAL.ProjectDal.NewProject(dr);//插入新项目记录,返回新的项目id

            if (hdfFile.Value != "0")//更新立项报告关联的项目ID
            {
                var file = new proj_ProjectFile
                {
                    FileID = long.Parse(hdfFile.Value),
                    ProjectID = pid,
                    Modified = DateTime.Now,
                    ModifiedBy = currentUserId,
                    Flag = 1
                };
                ProjectDll.DAL.ProjectFileDAL.UpdataFile(file);
            }
            Page.RegisterStartupScript("温馨提示", String.Format("<script language=\"javascript\">alert(\"{0}\");window.location.replace(\"{1}\")</script>", "项目: “" + txtPName.Text + "”发布成功!", "MyProjects.aspx"));
            //Response.Write("<script language='javascript'>alert('新项目发布成功');window.location='" + ProjectDll.DAL.Common.SPWeb.Url + "'/_layouts/15/Project/MyProjects.aspx'</script>");
            #endregion
        }
        protected void SaveProject_OnClick(object sender, EventArgs e)
        {
            #region 表单更新信息(学科ID,项目介绍,修改时间,修改人)
            //学科选择
            long subjectId;
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
            #endregion

            #region 判断项目立项报告是否修改

            if (hdfFile.Value != "0")
            {
                long fileId = long.Parse(hdfFile.Value);
                proj_ProjectFile myfile =ProjectDll.BLL.ProjectFileBll.GetFileByProjIdandPurpose(fileId, 1).FirstOrDefault();//
                if (myfile != null) DelListItem(myfile.FileNameInDocLib, "ProjectReports");
                ProjectDll.DAL.ProjectFileDAL.DelFileById(fileId, currentUserId, DateTime.Now);
            }

            #endregion

            #region 所有表单信息验证已通过,生成项目记录

            //判断是新建还是更新,新建则调用NewProject,否则调用UpdataProject
            long pid = GetUrlSendProjectId;//项目ID初始化
            if (pid > 0)//有项目ID传值,说明是编辑页面,则执行更新功能
            {
                var dr = new Proj_Project
                {
                    ProjectID = pid,//即将更新的项目Id
                    Introduce = txtIntro.Value,//项目简介
                    ModifiedBy = currentUserId,//发布人
                    ModifyTime = DateTime.Now,//发布时间
                    SubjectID = subjectId,//学科ID
                    Flag = 1//显示隐藏标记
                };
                ProjectDll.DAL.ProjectDal.UpdateProjectById(dr);
            }
            Response.Redirect("MyProjects.aspx");
            Page.RegisterStartupScript("温馨提示", String.Format("<script language=\"javascript\">alert(\"{0}\");window.location.replace(\"{1}\")</script>", "项目: “" + lbPName.Text + "”更新成功!", "MyProjects.aspx"));
            //System.Web.HttpContext.Current.Response.Write(String.Format("<script language=\"javascript\">alert(\"{0}\");window.location.replace(\"{1}\")</script>", "项目更新成功!", "MyProjects.aspx"));
            //Response.Write("<script language='javascript'>alert('项目更新成功!');window.location='" + ProjectDll.DAL.Common.SPWeb.Url + "'/_layouts/15/Project/MyProjects.aspx'</script>");
            #endregion
        }
        public void UploadFileToDocLib(SPWeb web, string docLibName, FileUpload fUpload, int itemId)
        {
            SPList list = web.Lists.TryGetList(docLibName);
            SPDocumentLibrary docLib = (SPDocumentLibrary)list;
            if (fUpload.HasFile)
            {
                string fn = System.IO.Path.GetFileName(fUpload.PostedFile.FileName);
                System.IO.Stream stm = fUpload.PostedFile.InputStream;
                int iLength = (int)stm.Length;
                if (iLength > 0)
                {
                    SPFolder rootFolder = docLib.RootFolder;
                    Byte[] filecontent = new byte[iLength];
                    stm.Read(filecontent, 0, iLength);
                    SPFile f = rootFolder.Files.Add(fn, filecontent);
                    SPListItem item = f.Item;
                    item["ItemID"] = itemId;
                    item.SystemUpdate();
                    stm.Close();
                }
            }
        }
    }
}
