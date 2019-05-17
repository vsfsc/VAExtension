using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.UI.WebControls;
using LearningActivity.DAL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using LADLL;

namespace LearningActivity.Layouts
{
    public partial class LA_Details : LayoutsPageBase
    {
        private SPWeb web; //当前网站
        private SPSite site;

        #region 属性
        /// <summary>
        /// 当前网站
        /// </summary>
        public SPWeb myWeb
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
        public SPSite mySite
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TypeContent.InnerHtml = ActivityTypeDAL.GetTypeHtml();
                ObjectContent.InnerHtml = LObjectDAL.GetLObjectsHtml();
                LocationContent.InnerHtml = LocationDAL.GetLoctionHtml();

                ddlParentType.DataSource = DAL.ActivityTypeDAL.GetTypesByparentId(0);
                ddlParentType.DataTextField = "Action";
                ddlParentType.DataValueField = "ActivityTypeID";
                ddlParentType.DataBind();

                ddlPLocation.DataSource = DAL.LocationDAL.GetLoctionsByParentId(0);
                ddlPLocation.DataTextField = "Address";
                ddlPLocation.DataValueField = "LocationID";
                ddlPLocation.DataBind();

                if (DAL.ActivityTypeDAL.GetTypes().Count > 0)
                {
                    ddlType.DataSource = DAL.ActivityTypeDAL.GetTypes();
                    ddlType.DataTextField = "Action";
                    ddlType.DataValueField = "ActivityTypeID";
                    ddlType.DataBind();
                }
                else
                {
                    ddlType.Items.Add("系统中尚未存在活动类别");
                }
            }
            //addTypeBtn.Click += AddTypes;
            //addObjBtn.Click += addObjects;
            //addLocatonBtn.Click += addLocation;
        }

        private void addLocation(object sender, EventArgs e)
        {
            string locaName = tbAction.Value;
            string locadesc = tbTypeDesc.Value;
            long pId = long.Parse(ddlPLocation.SelectedValue);
            long userID = DAL.UserDAL.GetUserId();
            int isShare = 0;
            DAL.LocationDAL.AddLocation(locaName, locadesc,userID,isShare, pId);
        }

        private void addObjects(object sender, EventArgs e)
        {
            string objTitle = tbObjTitle.Value;
            string objDesc = tbObjDesc.Value;
            string objContent = tbObjContent.Value;
            long userId = DAL.UserDAL.GetUserId();
            int isShare = 0;
            DAL.LObjectDAL.AddNewOject(objTitle, objDesc,isShare,userId, objContent);
        }
        private void AddActivity()
        {
            LAActivityEntities db = new LAActivityEntities();
            LADLL.LearningActivity la = new LADLL.LearningActivity();
            la.UserID = 1;
            db.LearningActivity.Add(la);
        }

        private void AddTypes(object sender, EventArgs e)
        {
            string typeName = tbAction.Value;
            string typedesc = tbTypeDesc.Value;
            long pId = long.Parse(ddlParentType.SelectedValue);
            long userId = DAL.UserDAL.GetUserId();
            int isShare = 0;
            DAL.ActivityTypeDAL.AddActivityType(typeName, typedesc,  userId,isShare, pId);
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            long userId =DAL.UserDAL.GetUserId();//获取当前用户ID
            if (userId==0)
            {
                DAL.Common.Alert("请先登录网站才能记录活动！");
                Response.Redirect("../Authenticate.aspx");//专项登录页面
                return;
            }
            LADLL.LAActivityEntities vaCtx = new LAActivityEntities();
            LADLL.LearningActivity la = new LADLL.LearningActivity();
            la.UserID = UserDAL.GetUserId();
            la.ActivityTypeID = int.Parse(thisTypeID.Value);
            la.LearningObjectID = int.Parse(thisObjID.Value);
            la.LocationID = int.Parse(thisLocaID.Value);
            la.Others = tbOthers.Value;
            la.Start = DTStart.SelectedDate;
            //la.WorksID = WorksDAL.GetWorksId(wName);
            vaCtx.LearningActivity.Add(la);
            vaCtx.SaveChanges();
            

        }


        protected void addTypeBtn_OnClick(object sender, EventArgs e)
        {
            DAL.Common.Alert(ddlParentType.SelectedValue);
            string typeName = tbAction.Value;
            string typedesc = tbTypeDesc.Value;
            long pId = long.Parse(ddlParentType.SelectedValue);
            if (pId==0)
            {
                pId = 0;
            }
            long userId = DAL.UserDAL.GetUserId();
            int isShare = 0;
            DAL.ActivityTypeDAL.AddActivityType(typeName, typedesc, userId, isShare, pId);
        }

        protected void addObjBtn_OnClick(object sender, EventArgs e)
        {
            string objTitle = tbObjTitle.Value;
            string objDesc = tbObjDesc.Value;
            string objContent = tbObjContent.Value;
            long userId = DAL.UserDAL.GetUserId();
            int isShare = 0;
            DAL.LObjectDAL.AddNewOject(objTitle, objDesc, isShare,  userId,objContent);
        }

        protected void addLocatonBtn_OnClick(object sender, EventArgs e)
        {
            string locaName = tbAction.Value;
            string locadesc = tbTypeDesc.Value;
            long pId = long.Parse(ddlPLocation.SelectedValue);
            long userId = DAL.UserDAL.GetUserId();
            int isShare = 0;
            DAL.LocationDAL.AddLocation(locaName, locadesc, userId, isShare, pId);
        }

        
        /// <summary>
        /// 视频音频上传控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnUpFile_Click(object sender, EventArgs e)
        {
            //bool allowUpload = false;
            //string[] extensionType = { ".mp4", ".mp3" };
            //try
            //{
            //    if (fileUpload.HasFile == false)
            //    {
            //        DAL.Common.ShowMessage(this.Page, this.GetType(), "请选择文件");
            //    }
            //    else
            //    {
            //        string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
            //        //判断文件类型是否符合要求
            //        foreach (string var in extensionType)
            //        {
            //            if (fileExtension == var)
            //            {
            //                allowUpload = true;
            //                break;
            //            }
            //        }
            //        if (allowUpload)
            //        {
            //            //通过后缀判断存放在哪个文档库
            //            if (fileExtension == ".mp4" || fileExtension == ".mp3")
            //            {
            //                //  //文档1  图片2  文档视频3  讲解视频4 
            //                this.AddWorkFile(myWeb, "MediaWorks", fileUpload, "DocWorks", gvWorks, 4);

            //            }
            //            //else
            //            //{
            //            //    this.AddWorkFile(myWeb, "DocWorks", fileUpload, "DocWorks", gvWorks, 2);
            //            //}
            //        }
            //        else
            //        {
            //            DAL.Common.ShowMessage(this.Page, this.GetType(), "只能上传指定格式的文件！");
            //        }
            //    }
            //}
            //catch
            //{
            //}
        }

        /// <summary>
        /// 效果图上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnImageUpload_Click(object sender, EventArgs e)
        {
            //文档1  图片2  文档视频3  讲解视频4 
            //bool allowUpload = false;
            //string[] extensionType = { ".docx", ".doc", ".pdf", ".ppt", ".pptx", ".xls", ".xlsx", ".mp4", ".accdb", ".png", ".jpg", ".gif", ".bmp", ".mp3" };
            //try
            //{
            //    if (imageFileUpload.HasFile == false)
            //    {
            //        DAL.Common.ShowMessage(this.Page, this.GetType(), "请选择文件");
            //    }
            //    else
            //    {
            //        string fileExtension = Path.GetExtension(imageFileUpload.FileName).ToLower();
            //        //判断文件类型是否符合要求
            //        foreach (string var in extensionType)
            //        {
            //            if (fileExtension == var)
            //            {
            //                allowUpload = true;
            //                break;
            //            }
            //        }
            //        if (allowUpload)
            //        {
            //            //文档1  图片2  文档视频3  讲解视频4   
            //            //通过后缀判断存放在哪个文档库
            //            if (fileExtension == ".mp4" || fileExtension == ".mp3")
            //            {
            //                this.AddWorkFile(myWeb, "MediaWorks", imageFileUpload, "imageFile", gvImage, 3);

            //            }
            //            //".png", ".jpg", ".gif", ".bmp",
            //            else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".gif" || fileExtension == ".bmp")
            //            {
            //                this.AddWorkFile(myWeb, "ImageWorks", imageFileUpload, "imageFile", gvImage, 2);
            //            }
            //            else
            //            {
            //                this.AddWorkFile(myWeb, "DocWorks", imageFileUpload, "imageFile", gvImage, 1);
            //            }

            //        }
            //        else
            //        {
            //            DAL.Common.ShowMessage(this.Page, this.GetType(), "只能上传指定格式的文件！");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

        }
        /// <summary>
        /// 上传附件
        /// </summary>
        public void AddWorkFile(SPWeb web, string docLibName, FileUpload fUpload, String viewStateName, GridView gv, int type)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite elevatedsiteColl = new SPSite(mySite.ID))
                    {
                        using (SPWeb elevatedSite = elevatedsiteColl.OpenWeb(myWeb.ID))
                        {
                            elevatedSite.AllowUnsafeUpdates = true;
                            SPList list = elevatedSite.Lists.TryGetList(docLibName);
                            //如果没有文档库，生成文档库
                            if (list == null)
                            {

                                list = DAL.WorksDAL.CreateList(docLibName);
                            }
                            SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                            if (fUpload.HasFile)
                            {
                                //数据库
                                //if (ViewState[viewStateName] == null)
                                //{
                                //    ViewState[viewStateName] = GetWorksFileDt;
                                //}
                                //判断是否存在 
                                if (JudgeSize(fUpload.PostedFile.ContentLength))
                                {
                                    DAL.Common.ShowMessage(this.Page, this.GetType(), "超出上传文档大小限制！");
                                }
                                else
                                {
                                    string fn = type.ToString().PadLeft(2, '0') + DateTime.Now.Year + Path.GetFileName(fUpload.PostedFile.FileName);
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
                                            bool isDBHave = false;
                                            isDocHave = DAL.WorksDAL.JudgeIsExistInDoc(docLib, fn, type);
                                            isDBHave = DAL.WorksDAL.JudgeIsExist(fn, viewStateName, iLength);
                                            //判断文档库
                                            if (isDocHave)
                                            {
                                                DAL.Common.ShowMessage(this.Page, this.GetType(), "请注意，该文件已存在，上传后自动覆盖已有文件！");

                                                DelList(fn, type.ToString());
                                                f = rootFolder.Files.Add(fn, filecontent, true);

                                            }
                                            else
                                            {
                                                f = rootFolder.Files.Add(fn, filecontent, true);
                                            }
                                            //判断数据库
                                            if (isDBHave)
                                            {
                                                //如果数据库中有，更新数据库
                                            }
                                            else
                                            {
                                                //如果没有插入数据库
                                                //数据库
                                                DataTable dt = (DataTable)ViewState[viewStateName];
                                                DataRow dr = dt.NewRow();
                                                dr["WorksFileID"] = 0;
                                                dr["WorksID"] = 0;
                                                dr["Type"] = type;
                                                dr["FileName"] = fn;
                                                //dr["FilePath"] = web.ServerRelativeUrl + "/" + docLibName + "/" + fn;
                                                dr["FilePath"] = "/" + docLibName + "/" + fn;
                                                dr["FileSize"] = iLength;
                                                dr["Flag"] = 1;

                                                ((DataTable)ViewState[viewStateName]).Rows.Add(dr.ItemArray);
                                            }
                                        }
                                        catch (Exception ex)
                                        {

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
                error.Text = "出错了，请报告给管理员！";
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
                using (SPSite ElevatedsiteColl = new SPSite(mySite.ID))
                {
                    using (SPWeb ElevatedSite = ElevatedsiteColl.OpenWeb(myWeb.ID))
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
    }
}
