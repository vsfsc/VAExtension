using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ProjectDll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project.Layouts.Project
{


    public partial class PManage : LayoutsPageBase
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
                hfID.Value = projectId.ToString();
                if (projectId>0)//编辑项目
                {
                    
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
                        lbPageTitle.Text = "编辑项目:" + pr.PName;
                        lbPName.Text = pr.PName;
                        long? subId = pr.SubjectID;
                        if (subId>0)//学科ID存在
                        {
                            long sid = long.Parse(subId.ToString());
                            Subject thissubject = ProjectDll.BLL.SubjectBll.GetSubjectById(sid).ToList().FirstOrDefault();
                            if (thissubject != null)
                            {
                                lbSubject.Text = thissubject.SubjectName;
                            }
                        }
                        btnSubjectChange.Visible = true;
                        txtIntro.Value = pr.Introduce;
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
                    btnSave.Visible = false;
                }
                
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

       
        private void ShowLixiangFile(long projId)
        {
            proj_ProjectFile pFile = ProjectDll.BLL.ProjectFileBll.GetFileByProjIdandPurpose(projId, 1).FirstOrDefault();
            if (pFile!=null)
            {
                hdfFile.Value = pFile.FileID.ToString();
                fileInfo.InnerHtml = " 名称: <b> " + pFile.FileName + "  </b> 上传时间: <b> " + pFile.Created+" </b>";
                fileInfo.Attributes.CssStyle.Add("color", "black");
                lixiangFile.Visible = false;
                lbDocType.Visible = false;
                btnNewFile.Visible = true;
                btnFileCancel.Visible = false;
            }
            else
            {
                fileInfo.InnerHtml = "你还未上传立项报告,请及时上传!";
                fileInfo.Attributes.CssStyle.Add("color", "red");
                lixiangFile.Visible = true;
                lbDocType.Visible = true;
                btnNewFile.Visible = true;
                btnNewFile.Text = "现在上传";
            }

        }
        /// <summary>
        /// 新建项目按钮,增加记录
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            if (ddlSubjectC.Items.Count<=0)
            {
                ProjectDll.DAL.Common.Alert("还未选择项目所属学科!");
                ddlSubjectA.Focus();
                return;
            }
            else
            {
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
            }
            #endregion

            #region 判断项目立项报告是否准备好上传

            if (lixiangFile.HasFile&&fileInfo.Visible==false)
            {
                ProjectDll.DAL.Common.Alert("你要上传的文档尚未提交!");
                lixiangFile.Focus();
                return;
            }
            //var allowUpload = IsAllowUpload(lixiangFile);
            //if (allowUpload)
            //{
            //    AddWorkFile("ProjectReports", lixiangFile, 1);
            //}
            //else
            //{
            //    ProjectDll.DAL.Common.Alert("你还未选择立项报告，或者您上传的报告文档格式不对或大小超出限制！");
            //    lixiangFile.Focus();
            //    return;
            //}
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
            ProjectDll.DAL.ProjectDal.NewProject(dr);
            //ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "添加成功！");
            //Response.Redirect(ProjectDll.DAL.Common.SPWeb.Url + "/_layouts/15/Project/MyProjects.aspx");
            Response.Write("<script language='javascript'>alert('新项目发布成功');window.location='" + ProjectDll.DAL.Common.SPWeb.Url + "'/_layouts/15/Project/MyProjects.aspx'</script>");
            #endregion
        }

        private bool IsAllowUpload(FileUpload fu)
        {
            //判断项目立项报告是否准备好上传
            var allowUpload = false;
            if (lixiangFile.HasFile)
            {
                var extension = Path.GetExtension(lixiangFile.FileName);//获取文件扩展名,确认文件格式
                if (extension != null)
                {
                    string fileExtension = extension.ToLower();
                    //判断文件类型是否符合要求
                    string[] extensionType = { ".docx", ".doc", ".pdf", ".ppt", ".pptx", ".xls", ".xlsx" };
                    allowUpload = extensionType.Any(exStr => fileExtension == exStr);
                    allowUpload = !JudgeSize(lixiangFile.PostedFile.ContentLength);
                }
            }
            return allowUpload;

        }
        /// <summary>
        /// 保存项目按钮,更新记录
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

            if (hdfFile.Value!="0")
            {
                long fileId = long.Parse(hdfFile.Value);
                proj_ProjectFile myfile =ProjectDll.BLL.ProjectFileBll.GetFileByProjIdandPurpose(fileId, 1).FirstOrDefault();

                ProjectDll.DAL.ProjectFileDAL.DelFileById(fileId, currentUserId, DateTime.Now);
                //if (myfile != null)
                //{
                //    string fileName = myfile.FileName;
                    
                //    DelListItem(fileName, "ProjectReports");
                //}
                var allowUpload = IsAllowUpload(lixiangFile);

                if (allowUpload)
                {
                    AddWorkFile("ProjectReports", lixiangFile, 1);
                }
                else
                {
                    ProjectDll.DAL.Common.Alert("你还未选择新版立项报告，或者您上传的报告文档格式不对或大小超出限制！");
                    lixiangFile.Focus();
                }
            }
            
            #endregion

            #region 所有表单信息验证已通过,生成项目记录
            var dr = new Proj_Project();

            //判断是新建还是更新,新建则调用NewProject,否则调用UpdataProject
            long pid = GetUrlSendProjectId;//项目ID初始化
            if (pid > 0)//有项目ID传值,说明是编辑页面,则执行更新功能
            {
                dr = new Proj_Project
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

            Response.Write("<script language='javascript'>alert('项目更新成功!');window.location='" + ProjectDll.DAL.Common.SPWeb.Url + "'/_layouts/15/Project/MyProjects.aspx'</script>");
            #endregion
        }

      
        protected void gvmyFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        { //执行删除
            var dataKey = gvmyFile.DataKeys[e.RowIndex];
            if (dataKey != null)
            {
                long fId = (long)dataKey.Value;
                string fName = ProjectDll.BLL.ProjectFileBll.GetFileByFileId(fId).FileNameInDocLib;
                //1.数据库删除记录
                ProjectDll.DAL.ProjectFileDAL.DelFileById(fId, currentUserId, DateTime.Now);
                //2.文档库删除文件
                DelListItem(fName, "ProjectReports");
            }
            gvmyFile.Visible =false;
            lixiangFile.Visible = true;
            btnNewFile.Visible = true;
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="docLibName">Name of the document library.</param>
        /// <param name="fUpload">The fupload.</param>
        /// <param name="type">The type.</param>
        private void AddWorkFile(string docLibName, FileUpload fUpload,  int type)
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
                            if (list == null) //如果没有文档库，先创建文档库
                            {
                                list = ProjectDll.DAL.ProjectFileDAL.CreateList(docLibName);
                                urlStr = urlStr + "\\" + docLibName;
                            }
                            var docLib = (SPDocumentLibrary)list;
                            if (fUpload.HasFile)
                            {
                                string fName = Path.GetFileName(fUpload.PostedFile.FileName);
                                string fLength = fUpload.PostedFile.ContentLength.ToString();
                                int? fTypeId = ProjectDll.BLL.ProjectFileBll.GetFileTypeByFileExtension(Path.GetExtension(fName).ToLower()).FileTypeID;
                                string fName0InDocLib = "";
                                string fName1InDocLib = "";
                                //判断数据库中是否存在该文件
                                proj_ProjectFile pFile = ProjectDll.DAL.ProjectFileDAL.JudgeIsExistInDb(fName, currentUserId);
                                //判断数据库
                                if (pFile != null)
                                {
                                    fName0InDocLib = pFile.FileNameInDocLib;//原有文档库文件名
                                    fName1InDocLib = (int.Parse(fName0InDocLib.Substring(0, 2)) + 1).ToString() +
                                                     DateTime.Now.Year +  "-" +
                                                     ProjectDll.BLL.User.GetAccount() + "-" + fName;//新文档库文件名
                                    urlStr += "\\" + fName1InDocLib;
                                    proj_ProjectFile drFile=new proj_ProjectFile
                                    {
                                        FileID = pFile.FileID,
                                        FileName = fName,
                                        FileNameInDocLib = fName1InDocLib,
                                        FilePath = urlStr,
                                        FileSize = fLength,
                                        Modified = DateTime.Now,
                                        ModifiedBy = currentUserId,
                                        Flag= 1
                                    };
                                    ProjectDll.DAL.ProjectFileDAL.UpdataFile(drFile);
                                    //数据库中有记录，更新数据库
                                }
                                else
                                {
                                    //如果没有,则新建记录插入数据库
                                    fName1InDocLib = (int.Parse(fName0InDocLib.Substring(0, 2)) + 1).ToString() +
                                                     DateTime.Now.Year + "-" +
                                                     ProjectDll.BLL.User.GetAccount() + "-" + fName;//新文档库文件名
                                    urlStr += "\\" + fName1InDocLib;
                                    proj_ProjectFile drFile = new proj_ProjectFile
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
                                    ProjectDll.DAL.ProjectFileDAL.InsertProjectFile(drFile);
                                }
                                
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
                                        bool isDocHave = ProjectDll.DAL.ProjectFileDAL.JudgeIsExistInDocLib(docLib, fName0InDocLib, type);
                                        //判断文档库
                                        if (isDocHave)
                                        {
                                            //ProjectDll.DAL.Common.ShowMessage(this.Page, this.GetType(), "请注意，该文件已存在，上传后自动覆盖已有文件！");
                                            DelListItem(fName0InDocLib, docLibName);
                                            rootFolder.Files.Add(fName1InDocLib, filecontent, true);
                                        }
                                        else
                                        {
                                            rootFolder.Files.Add(fName1InDocLib, filecontent, true);
                                        }
                                        //urlStr += "\\" + fName1InDocLib;


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

        public void SendFile(string fileName, Uri uri, CredentialCache credentialCache)
        {
            CookieContainer cookies = new CookieContainer();
            // cast the WebRequest to a HttpWebRequest since we're using HTTPS
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Credentials = credentialCache;
            httpWebRequest.CookieContainer = cookies;
            WebResponse webResponse = httpWebRequest.GetResponse();
            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest2.Credentials = credentialCache;
            httpWebRequest2.CookieContainer = cookies;
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest2.Method = "POST";

            // Build up the post message header
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"file\"; filename=\"");
            sb.Append(Path.GetFileName(fileName));
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array
            // ensuring the boundary appears on a line by itself
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            httpWebRequest2.ContentLength = length;

            Stream requestStream = httpWebRequest2.GetRequestStream();

            // Write out our post header
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // Write out the file contents
            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // Write out the trailing boundary
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

            WebResponse webResponse2 = httpWebRequest2.GetResponse();

        }

        protected void gvLixiang_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="UpNew")
            {
                hdfFile.Value = e.CommandArgument.ToString();
                lixiangFile.Visible = true;
                btnFileCancel.Visible = true;
            }
        }

        protected void btnFileCancel_OnClick(object sender, EventArgs e)
        {
            btnFileCancel.Visible = false;
            btnNewFile.Visible = true;
        }

        protected void btnNewFile_OnClick(object sender, EventArgs e)
        {
            AddWorkFile("ProjectReports", lixiangFile, 1);
            lixiangFile.Visible = true;
            fileInfo.Visible = false;
            btnFileCancel.Visible = true;
        }
    }
}
