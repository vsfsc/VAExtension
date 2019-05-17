using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using LADLL;
using LearningActivity.DAL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LearningActivity.Layouts.LearningActivity
{
    public partial class LA_Activity : LayoutsPageBase
    {
        private SPWeb web; //当前网站
        private SPSite site;

        #region 网站集属性
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
                long userId = DAL.UserDAL.GetUserId();
                if (DAL.ActivityTypeDAL.GetTypes().Count > 0)
                {
                    ddlType.DataSource = DAL.ActivityTypeDAL.GetTypes();
                    ddlType.DataTextField = "Action";
                    ddlType.DataValueField = "ActivityTypeID";
                    ddlType.DataBind();
                    ddlType.Items.Add("请选择活动类型");
                    ddlType.Items[this.ddlType.Items.Count - 1].Value = " ";
                    ddlType.SelectedIndex = this.ddlType.Items.Count - 1;
                }
                else
                {
                    ddlType.Items.Add("未存在任何活动类别,请点击右侧按钮添加!");
                    ddlType.Items[0].Value = " ";
                    ddlType.SelectedIndex = 0;
                }
                ddlType.Attributes["onchange"] = "addTxtTanto(this.options[selectedIndex].innerText)";
                if (DAL.LObjectDAL.GetLObjects().Count > 0)
                {
                    ddlObject.DataSource = DAL.LObjectDAL.GetLObjects();
                    ddlObject.DataTextField = "Title";
                    ddlObject.DataValueField = "LearningObjectID";
                    ddlObject.DataBind();
                    ddlObject.Items.Add("请选择活动对象");
                    ddlObject.Items[this.ddlObject.Items.Count - 1].Value = " ";
                    ddlObject.SelectedIndex = this.ddlObject.Items.Count - 1;
                }
                else
                {
                    ddlObject.Items.Add("未有任何活动对象,请点击右侧按钮添加");
                    ddlObject.Items[this.ddlObject.Items.Count - 1].Value = " ";
                    ddlObject.SelectedIndex = this.ddlObject.Items.Count - 1;
                }

                if (DAL.LocationDAL.GetLoctions().Count > 0)
                {
                    ddlLocation.DataSource = DAL.LocationDAL.GetLoctions();
                    ddlLocation.DataTextField = "Address";
                    ddlLocation.DataValueField = "LocationID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Add("请选择活动地点");
                    ddlLocation.Items[this.ddlLocation.Items.Count - 1].Value = " ";
                    ddlLocation.SelectedIndex = this.ddlLocation.Items.Count - 1;
                }
                else
                {
                    ddlLocation.Items.Add("未有任何活动地点,请点击右侧按钮添加!");
                    ddlLocation.Items[this.ddlLocation.Items.Count - 1].Value = " ";
                    ddlLocation.SelectedIndex = this.ddlLocation.Items.Count - 1;
                }

                if (DAL.ActivityTypeDAL.GetTypesByparentId(0).Count > 0)
                {
                    ddlParentType.DataSource = DAL.ActivityTypeDAL.GetTypesByparentId(0);
                    ddlParentType.DataTextField = "Action";
                    ddlParentType.DataValueField = "ActivityTypeID";
                    ddlParentType.DataBind();
                    ddlParentType.Items.Add("请选择所属大类");
                    ddlParentType.Items[this.ddlParentType.Items.Count - 1].Value = " ";
                    ddlParentType.SelectedIndex = this.ddlParentType.Items.Count - 1;
                }

                DTStart.SelectedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(Request.QueryString["ad"]))//判断Url中是否传递了活动ID,如果有,则是编辑活动页面,如果无,则是新建活动页面
                {
                    newlaDiv.Visible = true;
                    long laId = long.Parse(Request.QueryString["ad"]);
                    List<LADLL.LearningActivity> la = DAL.LearningActivityDal.GetActivityById(laId);
                    if (la.Count>0)
                    {
                        if (la[0].UserID != userId)//不是当前用户的活动
                        {
                            DAL.Common.Alert("对不起,你没有权限修改该活动信息!");
                            Response.Redirect("LA_Main.aspx");
                        }
                        else
                        {
                            this.btnSubmit.Visible = false;
                            this.btnSave.Visible = true;
                            this.divTitle.InnerHtml = "<h2>编辑活动信息</h2>";
                            this.btnBack.Text = "取 消";
                            this.ddlType.SelectedValue = la[0].ActivityTypeID.ToString();
                            this.ddlObject.SelectedValue = la[0].LearningObjectID.ToString();
                            this.ddlLocation.SelectedValue = la[0].LocationID.ToString();
                            this.DTStart.SelectedDate = DateTime.Parse(la[0].Start.ToString());
                            this.tbDuring.Text = la[0].During.ToString();
                            this.tbUrl.Text = la[0].WorksUrl;
                            tbOthers.InnerText = la[0].Others;
                        }
                    }
                    else
                    {
                        DAL.Common.Alert("你要修改的活动记录不存在!");
                        Response.Redirect("LA_Main.aspx");
                    }

                }


            }

        }


        #region 附件操作


        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="web">网站</param>
        /// <param name="docLibName">文档库名称</param>
        /// <param name="fUpload">上传控件</param>
        /// <param name="viewStateName">状态图名称</param>
        /// <param name="gv">GridView控件</param>
        /// <param name="type">附件文件类型</param>
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
                            string urlStr = "http://va.neu.edu.cn";//此处网址获取有问题,先写入定值
                            elevatedSite.AllowUnsafeUpdates = true;
                            SPList list = elevatedSite.Lists.TryGetList(docLibName);
                            //如果没有文档库，生成文档库
                            if (list == null)
                            {
                                list = DAL.WorksDAL.CreateList(docLibName);
                                urlStr=urlStr+"\\"+docLibName;
                            }
                            SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                            if (fUpload.HasFile)
                            {

                                //数据库

                                //判断是否存在
                                if (JudgeSize(fUpload.PostedFile.ContentLength))
                                {
                                    DAL.Common.ShowMessage(this.Page, this.GetType(), "超出上传文档大小限制！");
                                }
                                else
                                {
                                    string fn = type.ToString().PadLeft(2, '0') + DateTime.Now.Year + ddlType.SelectedValue.PadLeft(4, '0') + "-" + DAL.UserDAL.GetAccount() + "-" + Path.GetFileName(fUpload.PostedFile.FileName);
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
                                            urlStr += "\\" + fn;
                                            tbUrl.Text = urlStr;
                                            checkUrl.Visible = true;
                                            checkUrl.NavigateUrl = tbUrl.Text;
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
                                            DAL.Common.Alert(ex.ToString());
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
                DAL.Common.Alert(ex.ToString());
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
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="fileName">todo: describe fileName parameter on DelList</param>
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
        /// <param name="listName">列表名</param>
        /// <param name="fileName">文件名</param>
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

        #endregion

        #region 按钮事件

        /// <summary>
        /// 作品上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpFile_OnClick(object sender, EventArgs e)
        {
            //文档1  图片2  文档视频3  讲解视频4
            bool allowUpload = false;
            string[] extensionType = {  ".txt",".docx", ".doc", ".pdf", ".ppt", ".pptx", ".xls", ".xlsx", ".mp4", ".accdb", ".png", ".jpg", ".gif", ".bmp", ".mp3" };
            try
            {
                if (fuWorks.HasFile == false)
                {
                    DAL.Common.ShowMessage(this.Page, this.GetType(), "请选择文件");
                }
                else
                {
                    var extension = Path.GetExtension(fuWorks.FileName);
                    if (extension != null)
                    {
                        string fileExtension = extension.ToLower();
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
                                this.AddWorkFile(myWeb, "MediaWorks", fuWorks, "imageFile", gvFiles, 3);

                            }
                            //".png", ".jpg", ".gif", ".bmp",
                            else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".gif" || fileExtension == ".bmp")//图片类作品
                            {
                                this.AddWorkFile(myWeb, "ImageWorks", fuWorks, "imageFile", gvFiles, 2);
                            }
                            else//文档类作品
                            {
                                this.AddWorkFile(myWeb, "DocWorks", fuWorks, "imageFile", gvFiles, 1);
                            }

                        }
                        else
                        {
                            DAL.Common.Alert("只能上传指定格式的文件！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.Common.Alert(ex.ToString());
            }
        }

        protected void tbUrl_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUrl.Text))
            {
                checkUrl.Visible = true;
                checkUrl.NavigateUrl = tbUrl.Text;
            }
            else
            {
                checkUrl.Visible = false;
                checkUrl.NavigateUrl = "";
            }

        }

        private void tbDuring_Validating(object sender, CancelEventArgs e)
        {
            const string pattern = @"^\d+\.?\d+$";
            string content = ((TextBox)sender).Text;

            if (!(Regex.IsMatch(content, pattern)))
            {
                Common.Alert( "只能输入整数!");
                e.Cancel = true;
            }
            if (int.Parse(content) < 10 || int.Parse(content)>120)
            {
                Common.Alert("持续时长必须在10-120之间，单位：分钟!");
                e.Cancel = true;
            }

        }

        protected void ddlObject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.addObjectDiv.Visible = false;
            this.addLocaDiv.Visible = false;
            this.addTypeDiv.Visible = false;
            if (ddlObject.SelectedIndex!=ddlObject.Items.Count-1)
            {
                long loId = long.Parse(this.ddlObject.SelectedValue);
                this.ddlObjectDiv.InnerHtml = DAL.LObjectDAL.GetLObjectById(loId);
                this.ddlObjectDiv.Visible = true;
            }
            else
            {
                this.ddlObjectDiv.Visible = false;
            }

        }
        /// <summary>
        /// 确认新增学习对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addEnter_OnClick(object sender, EventArgs e)
        {
            string oName = this.tboName.Text;
            string oDesc = this.tboDesc.Text;
            int isShare = 0;
            if (cbObject.Checked)
            {
                isShare = 1;
            }
            long userId = UserDAL.GetUserId();
            //string oCont = this.tboContent.Text;
            DAL.LObjectDAL.AddNewOject(oName,oDesc,isShare,userId,"");
            if (DAL.LObjectDAL.GetLObjects().Count > 0)
            {
                ddlObject.DataSource = DAL.LObjectDAL.GetLObjects();
                ddlObject.DataTextField = "Title";
                ddlObject.DataValueField = "LearningObjectID";
                ddlObject.DataBind();
                ddlObject.Items.Add("请选择活动对象");
                ddlObject.Items[this.ddlObject.Items.Count - 1].Value = " ";
                ddlObject.SelectedIndex = this.ddlObject.Items.Count -2;
            }
            else
            {
                ddlObject.Items.Add("未有任何学习对象,请点击右侧按钮添加");
                ddlObject.Items[this.ddlObject.Items.Count - 1].Value = " ";
                ddlObject.SelectedIndex = this.ddlObject.Items.Count - 2;
            }
            this.ddlObjectDiv.Visible = true;
            this.addObjectDiv.Visible = false;
            this.addObtn.Visible = true;

            long loId = long.Parse(this.ddlObject.SelectedValue);
            this.ddlObjectDiv.InnerHtml = DAL.LObjectDAL.GetLObjectById(loId);
            this.ddlObjectDiv.Visible = true;
        }
        /// <summary>
        /// 取消新增学习对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addCancel_OnClick(object sender, EventArgs e)
        {
            this.ddlObjectDiv.Visible = false;
            this.addObjectDiv.Visible = false;
            this.addObtn.Visible = true;
        }
        /// <summary>
        /// 新增学习对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addObtn_OnClick(object sender, EventArgs e)
        {
            addLocaDiv.Visible = false;
            addTypeDiv.Visible = false;
            ddlObject.SelectedIndex = this.ddlObject.Items.Count - 1;
            this.ddlObjectDiv.Visible = false;
            this.tboName.Text = "";
            this.tboDesc.Text = "";

            //this.tboContent.Text = "";

            this.ddlObjectDiv.Visible = false;
            this.addObjectDiv.Visible = true;
            //this.addObtn.Visible = false;
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("LA_Main.aspx");
        }

        protected void btnaddTypeCancel_OnClick(object sender, EventArgs e)
        {
            addTypeDiv.Visible = false;
        }

        protected void btnaddTypeEnter_OnClick(object sender, EventArgs e)
        {
            long userId = DAL.UserDAL.GetUserId();
            if (string.IsNullOrEmpty(tbTypeName.Text))
            {
                DAL.Common.Alert("请输入活动类别名称!");
                tbTypeName.Focus();
            }
            else
            {
                if (ddlParentType.SelectedIndex==ddlParentType.Items.Count-1)
                {
                    DAL.Common.Alert("你还未选择新增类别所属大类!");

                }
                else
                {
                    string name = tbTypeName.Text;
                    string desc = tbTypeDesc.Text;
                    long pid = long.Parse(ddlParentType.SelectedValue);
                    int isShare = 0;
                    if (cbType.Checked)
                    {
                        isShare = 1;
                    }
                    DAL.ActivityTypeDAL.AddActivityType(name, desc,userId,isShare, pid);
                    addTypeDiv.Visible = false;

                    if (DAL.ActivityTypeDAL.GetTypes().Count > 0)
                    {
                        ddlType.DataSource = DAL.ActivityTypeDAL.GetTypes();
                        ddlType.DataTextField = "Action";
                        ddlType.DataValueField = "ActivityTypeID";
                        ddlType.DataBind();
                        ddlType.Items.Add("请选择所属大类");
                        ddlType.Items[this.ddlType.Items.Count - 1].Value = " ";
                        ddlType.SelectedIndex = this.ddlType.Items.Count - 2;
                    }

                }

            }
        }

        protected void btnaddType_OnClick(object sender, EventArgs e)
        {
            this.ddlObjectDiv.Visible = false;
            addLocaDiv.Visible = false;
            addObjectDiv.Visible = false;
            this.ddlType.SelectedIndex = this.ddlType.Items.Count - 1;
            tbTypeDesc.Text = "";
            tbTypeName.Text = "";
            addTypeDiv.Visible = true;

        }

        protected void btnaddLoca_OnClick(object sender, EventArgs e)
        {
            this.ddlObjectDiv.Visible = false;
            addTypeDiv.Visible = false;
            addObjectDiv.Visible = false;
            this.ddlLocation.SelectedIndex = this.ddlLocation.Items.Count - 1;
            tbLocaDesc.Text = "";
            tbLocaName.Text = "";
            addLocaDiv.Visible = true;
        }

        protected void btnAddLocaEnter_OnClick(object sender, EventArgs e)
        {
            long userId = DAL.UserDAL.GetUserId();
            if (string.IsNullOrEmpty(tbLocaName.Text))
            {
                DAL.Common.Alert("请填写活动地点名称!");
                tbLocaName.Focus();
            }
            else
            {
                string LocaName = tbLocaName.Text;
                string locaDesc = tbLocaDesc.Text;
                long pid = 0;
                int isShare = 0;
                if (cbLocation.Checked)
                {
                    isShare = 1;
                }
                DAL.LocationDAL.AddLocation(LocaName, locaDesc,userId,isShare, pid);
                addLocaDiv.Visible = false;
                if (DAL.LocationDAL.GetLoctions().Count > 0)
                {
                    ddlLocation.DataSource = DAL.LocationDAL.GetLoctions();
                    ddlLocation.DataTextField = "Address";
                    ddlLocation.DataValueField = "LocationID";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Add("请选择活动地点");
                    ddlLocation.Items[this.ddlLocation.Items.Count - 1].Value = " ";
                    ddlLocation.SelectedIndex = this.ddlLocation.Items.Count - 2;
                }

            }

        }

        protected void btnAddLocaCancel_OnClick(object sender, EventArgs e)
        {
            addLocaDiv.Visible = false;
        }

        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            addLocaDiv.Visible = false;
            addObjectDiv.Visible = false;
            addTypeDiv.Visible = false;
            ddlObjectDiv.Visible = false;
        }

        protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            addLocaDiv.Visible = false;
            addObjectDiv.Visible = false;
            addTypeDiv.Visible = false;
            ddlObjectDiv.Visible = false;
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            long userId = DAL.UserDAL.GetUserId();//获取当前用户ID
            //if (userId == 0)
            //{
            //    DAL.Common.Alert("请先登录网站才能记录活动！");
            //    Response.Redirect("../Authenticate.aspx");//专项登录页面
            //    return;
            //}
            if (DTStart.SelectedDate!=DateTime.Parse(lbStart.Text))//还未设定时间
            {
                DAL.Common.Alert("请先点击设定时间按钮,确认开始时间!");
                btnsetStart.Focus();
                return;
            }
            if (ddlType.SelectedIndex == ddlType.Items.Count - 1)
            {
                DAL.Common.Alert("请先选择活动类别!");
                ddlType.Focus();
                return;
            }
            if (ddlObject.SelectedIndex == ddlObject.Items.Count - 1)
            {
                DAL.Common.Alert("请先选择学习对象!");
                ddlObject.Focus();
                return;
            }
            if (ddlLocation.SelectedIndex == ddlLocation.Items.Count - 1)
            {
                DAL.Common.Alert("请先选择活动地点!");
                ddlLocation.Focus();
                return;
            }
            long activityTypeId = long.Parse(ddlType.SelectedValue);
            long learningObjectId = long.Parse(ddlObject.SelectedValue);
            long locationId = long.Parse(ddlLocation.SelectedValue); ;

            int durings = 10;

            if (!string.IsNullOrEmpty(tbDuring.Text))
            {
                durings = int.Parse(tbDuring.Text);
            }
            else
            {
                DAL.Common.Alert("持续时长不能为空!");
                tbDuring.Focus();
                return;
            }

            DTStart.LocaleId = Convert.ToInt32(SPContext.Current.RegionalSettings.LocaleId);
            string others = tbOthers.Value;
            //DateTime tm = DateTime.Parse(ddlHours.SelectedValue + ":" + ddlMinutes.SelectedValue);
            DateTime startDt = DateTime.Parse(lbStart.Text);
            string worksUrl = tbUrl.Text;
            DAL.LearningActivityDal.AddAct(activityTypeId, learningObjectId, durings, startDt, locationId, worksUrl, others);
            Response.Redirect("LA_Main.aspx");
            //DAL.Common.Alert("ok");
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            long laId = long.Parse(Request.QueryString["ad"]);
            long userId = DAL.UserDAL.GetUserId();//获取当前用户ID
            if (lbStart.Text=="")//还未设定时间
            {
                DAL.Common.Alert("请先点击设定时间按钮,确认开始时间!");//当点击这个警告框的确认按钮后出现界面混乱******************************************
                btnsetStart.Focus();
                return;
            }
            if (ddlType.SelectedIndex == ddlType.Items.Count - 1)
            {
                DAL.Common.Alert("请先选择活动类别!");
                ddlType.Focus();
                return;
            }
            if (ddlObject.SelectedIndex == ddlObject.Items.Count - 1)
            {
                DAL.Common.Alert("请先选择学习对象!");
                ddlObject.Focus();
                return;
            }
            if (ddlLocation.SelectedIndex == ddlLocation.Items.Count - 1)
            {
                DAL.Common.Alert("请先选择活动地点!");
                ddlLocation.Focus();
                return;
            }
            long activityTypeId = long.Parse(ddlType.SelectedValue);
            long learningObjectId = long.Parse(ddlObject.SelectedValue);
            long locationId = long.Parse(ddlLocation.SelectedValue); ;
            DTStart.LocaleId = Convert.ToInt32(SPContext.Current.RegionalSettings.LocaleId);
            DateTime startDt = DateTime.Parse(lbStart.Text);
            int durings = 0;

            if (!string.IsNullOrEmpty(tbDuring.Text))
            {
                durings = int.Parse(tbDuring.Text);
            }
            else
            {
                DAL.Common.Alert("持续时长不能为空!");
                tbDuring.Focus();
                return;
            }

            string others = tbOthers.Value;

            string worksUrl = tbUrl.Text;
            DAL.LearningActivityDal.UpdateLearningActivity(laId,activityTypeId, learningObjectId,locationId,startDt, durings,worksUrl, others);
            Response.Redirect("LA_Main.aspx");
        }
        #endregion

        protected void btSelectSame_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["rbSelectSame"]))
            {
                DAL.Common.Alert("请先选中你要重做的活动");
            }
            else
            {
                long acId = long.Parse(Request.Form["rbSelectSame"]);
                List<LADLL.LearningActivity> la = DAL.LearningActivityDal.GetActivityById(acId);
                if (la.Count > 0)
                {
                    this.ddlType.SelectedValue = la[0].ActivityTypeID.ToString();
                    this.ddlObject.SelectedValue = la[0].LearningObjectID.ToString();
                    this.ddlLocation.SelectedValue = la[0].LocationID.ToString();
                    this.tbDuring.Text = la[0].During.ToString();
                    this.tbUrl.Text = la[0].WorksUrl;
                    tbOthers.InnerText = la[0].Others;
                }
                newlaDiv.Visible = true;
                chooseDiv.Visible = false;
            }
        }

        protected void btSelectCancel_OnClick(object sender, EventArgs e)
        {
            newlaDiv.Visible = true;
            chooseDiv.Visible = false;
            DTStart.Enabled = false;
        }

        protected void btnresetStart_OnClick(object sender, EventArgs e)//重设开始时间
        {
            DTStart.SelectedDate = DateTime.Parse(lbStart.Text);
            lbStart.Visible = false;
            DTStart.Visible = true;
            btnresetStart.Visible = false;
            btnsetStart.Visible = true;
            chooseDiv.Visible = false;
        }

        protected void btnsetStart_OnClick(object sender, EventArgs e)
        {
            if (DTStart.IsDateEmpty)
            {
                DAL.Common.Alert("活动开始时间不可为空!");
                DTStart.Focus();
                return;
            }
            else
            {
                long userId = DAL.UserDAL.GetUserId();
                if (string.IsNullOrEmpty(Request.QueryString["ad"])) //Url中没有传递活动ID参数,则页面是新建活动,才做此项处理
                {
                    DateTime dt = DTStart.SelectedDate;
                    //dt = dt.ToLongTimeString();
                    List<LADLL.QuerylearningActivity> myAc = DAL.LearningActivityDal.GetMySameActivities(userId, dt);
                    if (myAc.Count > 0) //判断与选定时间点最近的活动记录数,如果有,则列出;
                    {
                        gvFavs.DataSource = myAc;
                        //gvFavs.Columns.Remove();
                        gvFavs.DataBind();
                        chooseDiv.Visible = true;
                        newlaDiv.Visible = false;
                    }
                    else //如果无,则只能新增
                    {
                        chooseDiv.Visible = false;
                        newlaDiv.Visible = true;
                    }
                }
                lbStart.Visible = true;
                lbStart.Text = DTStart.SelectedDate.ToString();
                DTStart.Visible = false;
                btnsetStart.Visible = false;
                btnresetStart.Visible = true;
            }
        }

        protected void DTStart_OnDateChanged(object sender, EventArgs e)
        {
            lbStart.Text = DTStart.SelectedDate.ToString();
        }

        protected void btSelectNoEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["rbSelectSame"]))
            {
                DAL.Common.Alert("请先选中你要重做的活动");
            }
            else
            {
                long acId = long.Parse(Request.Form["rbSelectSame"]);
                List<LADLL.LearningActivity> la = DAL.LearningActivityDal.GetActivityById(acId);
                if (la.Count > 0)
                {
                    DateTime startDt = DateTime.Parse(lbStart.Text);
                    long activityTypeId = la[0].ActivityTypeID;
                    long learningObjectId = la[0].LearningObjectID;
                    long locationId = la[0].LocationID;
                    int durings = int.Parse(la[0].During.ToString());
                    string worksUrl = la[0].WorksUrl;
                    string others = la[0].Others;

                    DAL.LearningActivityDal.AddAct(activityTypeId, learningObjectId, durings, startDt, locationId, worksUrl, others);
                    Response.Redirect("LA_Main.aspx");
                }
                newlaDiv.Visible = true;
                chooseDiv.Visible = false;
            }

        }
    }
}
