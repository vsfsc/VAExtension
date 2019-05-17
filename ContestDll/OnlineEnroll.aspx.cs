using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
namespace ContestDll
{
    
    public partial class OnlineEnroll : LayoutsPageBase
    {
        #region 控件定义
         protected  System.Web.UI.WebControls.Label error;
         protected  System.Web.UI.WebControls.DropDownList ddlPeriod;
         protected  System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator4;
         protected  System.Web.UI.WebControls.HyperLink HyperLink1;
         protected  System.Web.UI.WebControls.TextBox txtScore;
         protected  System.Web.UI.WebControls.RangeValidator rVScore;
         protected  System.Web.UI.WebControls.RequiredFieldValidator rFScore;
         protected  System.Web.UI.WebControls.Label lblTeacher;
         protected  System.Web.UI.WebControls.Label lblNum;
         protected  System.Web.UI.WebControls.Label lblWorksNum;
         protected  System.Web.UI.WebControls.HiddenField hfEndTime;
         protected  System.Web.UI.WebControls.Label lblSubmit;
         protected  System.Web.UI.WebControls.Label lblScorelbl;
         protected System.Web.UI.WebControls.Label lblPublic;
         protected System.Web.UI.WebControls.Literal lilRequire;
         protected System.Web.UI.WebControls.TextBox txtWorksName;
         protected System.Web.UI.WebControls.DropDownList ddlOneWorksType;
         protected System.Web.UI.WebControls.DropDownList ddlTwoWorksType;
         protected Microsoft.SharePoint.WebControls.PeopleEditor user;
         protected System.Web.UI.WebControls.TextBox txtSubmitProfile;
         protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
         protected System.Web.UI.WebControls.Label errorSubmitProfile;
         protected System.Web.UI.WebControls.TextBox txtDesignIdeas;
         protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator5;
         protected System.Web.UI.WebControls.Label errorDesignIdeas;
         protected System.Web.UI.WebControls.TextBox txtKeyPoints;
         protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
         protected System.Web.UI.WebControls.Label errorKeyPoints;
         protected System.Web.UI.WebControls.TextBox txtDemo1URL;
         protected System.Web.UI.WebControls.FileUpload imageFileUpload;
         protected System.Web.UI.WebControls.Button btnImageUpload;
         protected System.Web.UI.WebControls.GridView gvImage;
         protected System.Web.UI.WebControls.FileUpload fileUpload;
        protected System.Web.UI.WebControls.Button btnUpFile;
         protected System.Web.UI.WebControls.Label jiangJieError;
         protected System.Web.UI.WebControls.GridView gvWorks;
         protected System.Web.UI.WebControls.Button btnSubmit;
         protected System.Web.UI.WebControls.Button btnSave;
        #endregion
        #region 变量
            private SPWeb web; //当前网站
            private SPSite site;
            protected Button btnDel; //删除按钮
            protected Button btnDown; //下载按钮
            private List<WorksType>   WorksTypeDt; //作品类别
            private List<WorksType> WorksType
            {
                get
                {
                    if (WorksTypeDt == null)
                        WorksTypeDt = DAL.Works.GetWorksType();
                    return WorksTypeDt;
                }
            }
           
            #endregion
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
          
            /// <summary>
            /// 附件
            /// </summary>
            private    List<WorksFile>  GetWorksFileDt
            {
                get
                {
                    if (ViewState["WorksFileDt"] == null)
                        return (new List<WorksFile>());
                    else
                        return (List<WorksFile>)ViewState["WorksFileDt"];
                }
                set
                {
                    ViewState["WorksFileDt"] = value;
                }
            }
            /// <summary>
            /// 删除作品
            /// </summary>
            public List<WorksFile> GetDelImageDt
            {
                get
                {
                    if (ViewState["delImageDt"] == null)
                        return new List<WorksFile>();
                    else
                        return (List<WorksFile>)ViewState["delImageDt"];
                }
                set
                {
                    ViewState["delImageDt"] = value;
                }
            }
            public List<WorksFile> GetDelWorksDt
            {
                get
                {
                    if (ViewState["delWorksDt"] == null)
                        return new List<WorksFile>();
                    else
                        return (List<WorksFile>)ViewState["delWorksDt"];
                }
                set
                {
                    ViewState["delWorksDt"] = value;
                }
            }
            /// <summary>
            /// 效果图
            /// </summary>
            public List<WorksFile>  GetImageFileDt
            {
                get
                {
                    if (ViewState["imageFileDt"] == null)
                         return new List<WorksFile>();
                    else
                        return (List<WorksFile>)ViewState["imageFileDt"];
                }
                set
                {
                    ViewState["imageFileDt"] = value;
                }
            }
            public List<CSPeriodsWorksType> GetPeriodsWorksType
            {
                get
                {
                    if (ViewState["periodWorksType"] == null)
                        return DAL.Periods.GetPeriodByCourseID();
                    else
                        return (List<CSPeriodsWorksType>)ViewState["periodWorksType"];
                }
                set
                {
                    ViewState["periodWorksType"] = value;
                }
            }
            #endregion
            #region 事件
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
                btnSave.Click += new EventHandler(btnSave_Click);
                btnUpFile.Click += btnUpFile_Click;
                gvWorks.RowCommand += gvWorks_RowCommand;
                //
                btnImageUpload.Click += btnImageUpload_Click;
                gvImage.RowCommand += gvImage_RowCommand;
                //
                ddlOneWorksType.SelectedIndexChanged += ddlOneWorksType_SelectedIndexChanged;
                ddlPeriod.SelectedIndexChanged += ddlPeriod_SelectedIndexChanged;
                btnSubmit.Click += btnSubmit_Click;
                //btnIsSample.Click += btnIsSample_Click;



            }
            /// <summary>
            /// 加载
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    long courseID = BLL.Course.GetCourseID();
                    ViewState["CourseID"] = courseID;
                    WorksTypeDt = DAL.Works.GetWorksType();

                    BindOneWorksType(WorksTypeDt);
                    BindTwoWorksType(WorksTypeDt, ddlOneWorksType.SelectedValue);
                    //加载期次
                    LoadPeriods();
                    //加载页面信息
                    LoadPeriodPage();
                    JudgeIsSampleButton();
                }
            }
            /// <summary>
            /// 期次更改
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadPeriodPage();
            }
            /// <summary>
            /// 效果图命令
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void gvImage_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "Del")
                {
                    GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                    List<WorksFile> dt = (List<WorksFile>)ViewState["imageFile"];
                    //获取删除dataview数据
                    if (long.Parse(drv.Cells[0].Text) != 0)
                    {
                        //if不是刚传的，删除数据库，更新viewstate
                        WorksFile  delDr =new WorksFile ();// GetDelImageDt;
                        delDr.WorksFileID = long.Parse(drv.Cells[0].Text);
                        delDr.Type = int.Parse(drv.Cells[4].Text);
                        delDr.FileName = drv.Cells[1].Text;

                        delDr.FilePath = drv.Cells[2].Text;
                        delDr.ModifiedBy = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                        delDr.Modified = DateTime.Now;
                        if (drv.Cells[3].Text != "")
                        {
                            delDr.FileSize = int.Parse(drv.Cells[3].Text);
                        }
                        else
                        {
                            delDr.FileSize = 0;
                        }
                        delDr.Flag = 0;

                        List<WorksFile> delImageDt = GetDelImageDt;
                        delImageDt.Add(delDr);
                        GetDelImageDt = delImageDt;
                       
                    }
                    //删除当前viewstate
                    for (int j = dt.Count-1; j >=0; j--)
                    {
                        if (dt[j].FileName == drv.Cells[1].Text.Replace("amp;", ""))
                        {
                            dt.RemoveAt(j);
                        }
                    }
                    ViewState["imageFile"] = dt;
                    BindGridView(dt , gvImage);
                    //btnSubmit.Focus();

                    if (ddlPeriod.SelectedValue != "")
                    {
                        LoadUI(long.Parse(ddlPeriod.SelectedValue));
                        JudgeScore();
                    }
                }
                if (e.CommandName == "Down")
                {
                    GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        DAL.Common.DownLoadFileByStream(myWeb.Url + "/" + drv.Cells[2].Text, drv.Cells[1].Text, Response);
                    });
                }

            }
            /// <summary>
            /// 行命令
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void gvWorks_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "Del")
                {
                    GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                    List<WorksFile> dt = (List<WorksFile>)ViewState["workFile"];

                    //获取删除dataview数据
                    if (long.Parse(drv.Cells[0].Text) != 0)
                    {
                        //if不是刚传的，删除数据库，更新viewstate

                        WorksFile delDr = new WorksFile();

                        delDr.WorksFileID = long.Parse(drv.Cells[0].Text);
                        delDr.Type = int.Parse(drv.Cells[4].Text);
                        delDr.FileName = drv.Cells[1].Text;

                        delDr.FilePath= drv.Cells[2].Text;
                        delDr.ModifiedBy = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                        delDr.Modified = DateTime.Now;
                        if (drv.Cells[3].Text != "")
                        {
                            delDr.FileSize = int.Parse(drv.Cells[3].Text);
                        }
                        else
                        {
                            delDr.FileSize = 0;
                        }
                        delDr.Flag = 0;
                        List<WorksFile> delDt = GetDelWorksDt;
                        delDt.Add(delDr);

                        GetDelWorksDt = delDt;
                    }
                    //删除当前viewstate
                    for (int j = dt.Count -1; j >=0; j--)
                    {
                        if (dt[j].FileName == drv.Cells[1].Text.Replace("amp;", ""))
                        {
                            dt.RemoveAt(j);
                        }
                    }

                    ViewState["workFile"] = dt;
                    //gvWork
                    BindGridView(dt, gvWorks);
                    //btnSubmit.Focus();
                    if (ddlPeriod.SelectedValue != "")
                    {
                        LoadUI(long.Parse(ddlPeriod.SelectedValue));
                        JudgeScore();
                    }
                }
                if (e.CommandName == "Down")
                {
                    GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        DAL.Common.DownLoadFileByStream(myWeb.Url + "/" + drv.Cells[2].Text, drv.Cells[1].Text, Response);
                    });
                }
            }
            /// <summary>
            /// 组别改变事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void ddlOneWorksType_SelectedIndexChanged(object sender, EventArgs e)
            {
                BindTwoWorksType(WorksType , ddlOneWorksType.SelectedValue);
            }
            /// <summary>
            /// 保存
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void btnSave_Click(object sender, EventArgs e)
            {
                if (ddlPeriod.SelectedValue != "")
                {
                    LoadUI(long.Parse(ddlPeriod.SelectedValue));
                    JudgeScore();
                }

                Works dr = new Works();
                Save(dr, 0);
                //  GoToPage();
            }
            /// <summary>
            /// 提交
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void btnSubmit_Click(object sender, EventArgs e)
            {
                if (ddlPeriod.SelectedValue != "")
                {
                    LoadUI(long.Parse(ddlPeriod.SelectedValue));
                    JudgeScore();
                }
                if (JudgeWordCount())
                {
                    return;
                }
                //2014-11-12
                //提交时判断有没有上传文档，没有不让提交
                if (((List<WorksFile>)ViewState["workFile"]).Count == 0 || ((List<WorksFile>)ViewState["imageFile"]).Count == 0)
                {
                    error.Text = "请注意，提交作品时请确保已上传作品文件和讲解视频";
                }
                else
                {

                    Works dr = new Works();
                    Save(dr, 1);

                    SetControls(false);
                }
            }
            /// 视频讲解上传控件
            /// </summary>                                  
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// 
            void btnUpFile_Click(object sender, EventArgs e)
            {
                bool allowUpload = false;
                string[] extensionType = { ".mp4", ".mp3" };
                try
                {
                    if (fileUpload.HasFile == false)
                    {
                        DAL.Common.ShowMessage(this.Page, this.GetType(), "请选择文件");
                    }
                    else
                    {
                        string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                        //判断文件类型是否符合要求
                        foreach (string var in extensionType)
                        {
                            if (fileExtension == var)
                            {
                                allowUpload = true;
                                break;
                            }
                        }
                        if (allowUpload)
                        {
                            //通过后缀判断存放在哪个文档库
                            if (fileExtension == ".mp4" || fileExtension == ".mp3")
                            {
                                //  //文档1  图片2  文档视频3  讲解视频4 
                                this.AddWorkFile(myWeb, "workVideo", fileUpload, "workFile", gvWorks, 4);

                            }
                        }
                        else
                        {
                            DAL.Common.ShowMessage(this.Page, this.GetType(), "只能上传指定格式的文件！");
                        }
                    }
                }
                catch
                {
                }
                //
                if (ddlPeriod.SelectedValue != "")
                {
                    LoadUI(long.Parse(ddlPeriod.SelectedValue));
                    JudgeScore();
                }
            }
            private void JudgeIsSampleButton()
            {
                if (ddlPeriod.SelectedValue != "")
                {
                    HyperLink1.Visible = true;
                    //btnIsSample.Visible = true;
                }
                else
                {
                    HyperLink1.Visible = false;
                    //  btnIsSample.Visible = false;
                }
            }
            /// <summary>
            /// 效果图上传
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void btnImageUpload_Click(object sender, EventArgs e)
            {
                //文档1  图片2  文档视频3  讲解视频4 
                bool allowUpload = false;
                string[] extensionType = { ".docx", ".doc", ".pdf", ".ppt", ".pptx", ".xls", ".xlsx", ".mp4", ".accdb", ".png", ".jpg", ".gif", ".bmp", ".mp3" };
                try
                {
                    if (imageFileUpload.HasFile == false)
                    {
                        DAL.Common.ShowMessage(this.Page, this.GetType(), "请选择文件");
                    }
                    else
                    {
                        string fileExtension = Path.GetExtension(imageFileUpload.FileName).ToLower();
                        //判断文件类型是否符合要求
                        foreach (string var in extensionType)
                        {
                            if (fileExtension == var)
                            {
                                allowUpload = true;
                                break;
                            }
                        }
                        if (allowUpload)
                        {
                            //文档1  图片2  文档视频3  讲解视频4   
                            //通过后缀判断存放在哪个文档库
                            if (fileExtension == ".mp4" || fileExtension == ".mp3")
                            {
                                this.AddWorkFile(myWeb, "workVideo", imageFileUpload, "imageFile", gvImage, 3);

                            }
                            //".png", ".jpg", ".gif", ".bmp",
                            else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".gif" || fileExtension == ".bmp")
                            {
                                this.AddWorkFile(myWeb, "workPic", imageFileUpload, "imageFile", gvImage, 2);
                            }
                            else
                            {
                                this.AddWorkFile(myWeb, "workFile", imageFileUpload, "imageFile", gvImage, 1);
                            }

                        }
                        else
                        {
                            DAL.Common.ShowMessage(this.Page, this.GetType(), "只能上传指定格式的文件！");
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                if (ddlPeriod.SelectedValue != "")
                {
                    LoadUI(long.Parse(ddlPeriod.SelectedValue));
                    JudgeScore();
                }
            }
            #endregion
            #region 方法
            private void Save(Works   dr, int WorksState)
            {
                //

                //重新判断
                long workID = BLL.Works.GetWorksIDByPriod(long.Parse(ddlPeriod.SelectedValue));
            
                //判断是否提交


                CSWorksWorksType  dt =  DAL.Works.GetWorksByWorksID(workID);
                if (dt!=null)
                {
                    //判断
                    if (!JudgeSubmit(dt))
                    {
                        SetControls(false);
                        error.Text = "请注意，该作品已提交不可更改！";
                        return;
                    }
                }

                dr.PeriodID = long.Parse(ddlPeriod.SelectedValue);
                dr.WorksID = workID;
                dr.WorksName = txtWorksName.Text;
                if (Request.QueryString["IsSample"] != null)
                {
                    //再次判断当前用户登陆角色

                    if (BLL.User.JudgeUserRight())
                    {
                        dr.IsSample = 0;
                    }
                    else
                    {
                        dr.IsSample = 1;
                    }

                    if (txtScore.Text != "")
                    {
                        dr.Score =float.Parse ( txtScore.Text);
                    }
                }
                else
                {
                    dr.IsSample  = 0;

                }
                //dr["WorksCode"] = GetWorksCode();
                if (ddlTwoWorksType.SelectedValue == "0")
                {
                    dr.WorksTypeID = int.Parse(ddlOneWorksType.SelectedValue);
                }
                else
                {
                    dr.WorksTypeID = int.Parse(ddlTwoWorksType.SelectedValue);
                }
                // dr["ContestID"]=ContestID;

                dr.SubmitProfile = txtSubmitProfile.Text;
                dr.DesignIdeas = txtDesignIdeas.Text;
                dr.KeyPoints = txtKeyPoints.Text;
                dr.DemoURL = txtDemo1URL.Text;
                dr.Flag = 1;

                dr.WorksState = WorksState;



                //时间和人
                dr.CreatedBy = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                dr.Created = DateTime.Now;


                //插入work
                if (workID != 0)
                {
                    // workID = long.Parse(ViewState["WorksID"].ToString());
                    DAL.Works.UpdateWorksInfo(dr);

                }
                else
                {
                    workID = DAL.Works.InsertWorks(dr);
                    dr.WorksID = workID;
                }
                dr.WorksCode = GetWorksCode(workID);
                DAL.Works.UpdateWorksCode(dr);
                //本次作品的人员
                List<CSUserWorks > UserWorkDt = DAL.User.GetUserByWorksID(workID);
                //插入User
                ArrayList list = user.ResolvedEntities;
                long UserID;
                UserWorks userWorkDr = new UserWorks();// UserWorksDt.NewRow();
                userWorkDr.WorksID = workID;
                //
                if (GetUserCount() == 0)
                {
                    userWorkDr.Relationship = 0;
                }
                else
                {
                    userWorkDr.Relationship = 1;
                }
                //添加登陆用户
                SPUser member = SPContext.Current.Web.CurrentUser;
                UserID = BLL.User.GetUserID(member);
                userWorkDr.UserID = UserID;
                userWorkDr.Flag = 1;
                userWorkDr.WorksID = workID;
                List<CSUserWorks> drs = UserWorkDt.Where(p => p.UserID == UserID).ToList();// ("UserID=" + UserID.ToString());
                if (drs.Count == 0)
                {

                    DAL.User.InsertUserWorks(userWorkDr);
                }

                else
                    DAL.User.UpdateUserWorks(userWorkDr);
                //处理队员
                List<long> delUserID = new List<long>();
                string sql = "UserID<>" + UserID.ToString();
                delUserID.Add(UserID);
                //获取id和显示名称
                foreach (Microsoft.SharePoint.WebControls.PickerEntity p in list)
                {
                    //排除当前用户


                    userWorkDr =new UserWorks ();
                    member = myWeb.EnsureUser(p.Key);
                    if (member.LoginName != SPContext.Current.Web.CurrentUser.LoginName)
                    {
                        //roleID 1教师 2学生
                        UserID = BLL.User.GetUserID(member);

                        userWorkDr.Relationship = 2;
                        userWorkDr.UserID = UserID;
                        userWorkDr.Flag = 1;
                        userWorkDr.WorksID = workID;
                        //xqx
                        sql += " and UserID<>" + UserID.ToString();
                        delUserID.Add(UserID);
                        drs = UserWorkDt.Where(u=>u.UserID ==UserID ).ToList ();// ("UserID=" + UserID.ToString());
                        if (drs.Count  == 0)
                        {

                            DAL.User.InsertUserWorks(userWorkDr);
                        }

                        else
                            DAL.User.UpdateUserWorks(userWorkDr);
                    }
                }
                //删除
                drs = UserWorkDt;//.Where(s=>s.UserID != sql).ToString();
                foreach (long delID in delUserID)
                    drs = drs.Where(s => s.UserID != delID).ToList();
                foreach (CSUserWorks  dr1 in drs)
                {
                    userWorkDr.UserID = dr1.UserID;
                    userWorkDr.Flag = 0;
                    DAL.User.UpdateUserWorks(userWorkDr);
                }

                //插入效果图
                if (ViewState["imageFile"] == null)
                {
                    ViewState["imageFile"] = GetImageFileDt;
                }

                foreach (WorksFile drr in (List<WorksFile>)ViewState["imageFile"])
                {
                    if (drr.WorksFileID.ToString() == "0")
                    {
                        drr.WorksID= workID;
                        DAL.Works.InsertWorksImages(drr);

                    }
                }

                //插入视频讲解
                if (ViewState["workFile"] == null)
                {
                    ViewState["workFile"] = GetWorksFileDt;
                }
                foreach (WorksFile drr in (List<WorksFile>)ViewState["workFile"])
                {
                    if (drr.WorksFileID.ToString() == "0")
                    {
                        drr.WorksID = workID;
                        DAL.Works.InsertWorksImages(drr);
                    }
                }

                //提交已删除的数据     
                List<WorksFile> delFiles = GetDelWorksDt; 
                    foreach (WorksFile ddr in delFiles )
                    {
                        DAL.Works.UpdateWorksFile(ddr);
                    }
              
                //提交已删除的图片数据
                    List<WorksFile> delImges = GetDelImageDt;
                    foreach (WorksFile idr in delImges )
                    {
                        DAL.Works.UpdateWorksFile(idr);
                    }
                
                GoToPage(workID);

            }
            /// <summary>
            /// 绑定组别第一级
            /// </summary>
            /// <param name="dt"></param>
            public void BindOneWorksType(List<WorksType>  dt)
            {
                List<WorksType > drs = dt.Where (p=>p.LevelID ==0).ToList ();// ("LevelID=0");
                ddlOneWorksType.DataSource = drs;
                ddlOneWorksType.DataTextField = "WorksTypeName";
                ddlOneWorksType.DataValueField = "WorksTypeID";
                ddlOneWorksType.DataBind();
            }
            /// <summary>
            /// 绑定组别第二级
            /// </summary>
            /// <param name="dt"></param>
            /// <param name="WorksTypeID"></param>
            public void BindTwoWorksType(List<WorksType>   dt, string WorksTypeID)
            {
                List<WorksType> dts = dt.Where(p => p.ParentID == int.Parse(WorksTypeID)).ToList();
                ddlTwoWorksType.DataSource = dts;
                ddlTwoWorksType.DataTextField = "WorksTypeName";
                ddlTwoWorksType.DataValueField = "WorksTypeID";
                ddlTwoWorksType.DataBind();
            }
            /// <summary>
            /// 上传附件
            /// </summary>
            public void AddWorkFile(SPWeb web, string docLibName, FileUpload fUpload, String ViewStateName, GridView gv, int type)
            {
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite ElevatedsiteColl = new SPSite(mySite.ID))
                        {
                            using (SPWeb ElevatedSite = ElevatedsiteColl.OpenWeb(myWeb.ID))
                            {
                                ElevatedSite.AllowUnsafeUpdates = true;
                                SPList list = ElevatedSite.Lists.TryGetList(docLibName);
                                //如果没有文档库，生成文档库
                                if (list == null)
                                {

                                    list =  BLL.Works.CreateList(docLibName);
                                }
                                SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                                if (fUpload.HasFile)
                                {
                                    //数据库
                                    if (ViewState[ViewStateName] == null)
                                    {
                                        ViewState[ViewStateName] = GetWorksFileDt;
                                    }
                                    //判断是否存在 
                                    if (JudgeSize(fUpload.PostedFile.ContentLength))
                                    {
                                        error.Text = "超出上传文档大小限制！";
                                        // DAL.Common.ShowMessage(this.Page, this.GetType(), "超出上传文档大小限制！");
                                    }
                                    else
                                    {
                                        string fn = type.ToString().PadLeft(2, '0') + DateTime.Now.Year + ddlPeriod.SelectedValue.PadLeft(4, '0') + "-" + DAL.Common.GetLoginAccount + "-" + Path.GetFileName(fUpload.PostedFile.FileName);
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
                                                isDocHave = JudgeIsExistInDoc(docLib, fn, type);
                                                isDBHave = JudgeIsExist(fn, ViewStateName, iLength);
                                                //判断文档库
                                                if (isDocHave)
                                                {
                                                    error.Text = "请注意，该文件已存在，上传后自动覆盖已有文件！";

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
                                                    error.Text = "";
                                                    //数据库

                                                    List<WorksFile> dt = (List<WorksFile>)ViewState[ViewStateName];
                                                    WorksFile dr = new WorksFile();
                                                    dr.WorksFileID = 0;
                                                    dr.WorksID = 0;
                                                    dr.Type = type;
                                                    dr.FileName = fn;
                                                    dr.FilePath = "/" + docLibName + "/" + fn;
                                                    dr.FileSize = iLength;
                                                    dr.Flag = 1;

                                                    dt.Add(dr);
                                                    ViewState[ViewStateName] = dt;
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
                                ElevatedSite.AllowUnsafeUpdates = false;
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
                BindGridView((List<WorksFile>)ViewState[ViewStateName], gv);
            }
            /// <summary>
            /// 判断当前作品是否存在
            /// </summary>
            /// <param name="drr"></param>
            /// <param name="viewstateName"></param>
            /// <returns></returns>
            public bool JudgeIsExist(string fileName, string viewstateName, int length)
            {

                bool isExist = false;
                List<WorksFile> dt = GetWorksFileDt;
                for (int i = 0; i < dt.Count ; i++)
                {
                    if (dt[i].FileName == fileName)
                    {
                        isExist = true;
                        //更新
                        WorksFile delDr = new WorksFile();
                        if (dt[i].WorksFileID.ToString() != "0")
                        {
                            delDr.WorksFileID = dt[i].WorksFileID;
                            delDr.Type = dt[i].Type;
                            delDr.FilePath = dt[i].FilePath;
                            delDr.ModifiedBy = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                            delDr.Modified = DateTime.Now;
                            delDr.FileSize = length;
                            delDr.Flag = 1;
                            DAL.Works.UpdateWorksFileForSize(delDr);
                        }
                    }
                }
                return isExist;
            }

            public bool JudgeIsExistInDoc(SPList docLib, string fileName, int type)
            {
                //得判断资产库

                bool isExist = false;
                if (type == 3 || type == 4)
                {

                    for (int i = 0; i < docLib.RootFolder.SubFolders.Count; i++)
                    {
                        if (docLib.RootFolder.SubFolders[i].Files[0].Name == fileName)
                        {
                            isExist = true;
                            break;

                        }
                    }
                    //不开启资产库
                    for (int i = 0; i < docLib.RootFolder.Files.Count; i++)
                    {
                        if (docLib.RootFolder.Files[i].Name == fileName)
                        {
                            isExist = true;
                            break;
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < docLib.RootFolder.Files.Count; i++)
                    {
                        if (docLib.RootFolder.Files[i].Name == fileName)
                        {
                            isExist = true;
                            break;
                        }
                    }
                }
                return isExist;
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
                int size = int.Parse(System.Configuration.ConfigurationManager.AppSettings["uploadSize"]);
                // 52428801
                if (i > size)
                {
                    isSize = true;
                }
                return isSize;
            }
            /// <summary>
            /// 绑定works
            /// </summary>
            private void BindGridView(List<WorksFile>  dt, GridView gv)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            /// <summary>
            /// 获取work表中的作品数据
            /// </summary>
            private void LoadInfo()
            {
                long workID = long.Parse(ViewState["WoksID"].ToString());

                CSWorksWorksType dt =DAL.Works.GetWorksByWorksID(workID);
                txtWorksName.Text = dt.WorksName;

                txtSubmitProfile.Text = dt.SubmitProfile;
                txtDesignIdeas.Text = dt.DesignIdeas;
                txtKeyPoints.Text = dt.KeyPoints;
                txtDemo1URL.Text = dt.DemoURL;

                if (Request.QueryString["IsSample"] != null)
                {
                    txtScore.Text = dt.Score.ToString ();
                }

                //判断
                if (!JudgeSubmit(dt))
                {
                    SetControls(false);
                    error.Text = "请注意，该作品已提交不可更改！";
                }
                else
                {

                    SetControls(true);
                    error.Text = "";
                }
                //加载队员


                List<CSUserWorks > userDt = BLL.User.GetGroupMemberByWorksID(workID);
                StringBuilder sb = new StringBuilder();
                foreach (CSUserWorks dr in userDt )
                {
                    sb.Append("ccc\\" + dr.Account + ",");
                }
                user.CommaSeparatedAccounts = sb.ToString();

                JudgePeriodWorkTypeIsEnable();
                //LoadPeriodWorkType();
                //当存的不是不限时
                if (dt.LevelID.ToString () != "")
                {
                    //有小类
                    if (dt.LevelID.ToString() != "0")
                    {
                        BindOneWorksType(WorksType );
                        //加载作品类别
                        ddlOneWorksType.SelectedIndex = -1;
                        ddlOneWorksType.Items.FindByValue(dt.ParentID.ToString()).Selected = true;


                        BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                        ddlTwoWorksType.SelectedIndex = -1;
                        ddlTwoWorksType.Items.FindByValue(dt.WorksTypeID.ToString()).Selected = true;
                       
                    }
                    else
                    {
                        BindOneWorksType(WorksType);
                        //加载作品类别

                        ddlOneWorksType.SelectedIndex = -1;
                        ddlOneWorksType.Items.FindByValue(dt.WorksTypeID.ToString()).Selected = true;

                        BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                        ddlTwoWorksType.SelectedIndex = -1;
                        ddlTwoWorksType.Items.FindByValue("0").Selected = true;
                     
                    }
                }

            }
            private void LoadPeriodWorkType()
            {

                //当是新数据时
                //判断期次的类别
                List<CSPeriodsWorksType >  PWorkTypeDr = GetCurrPeriodsData();
                if (PWorkTypeDr[0].LevelID .ToString() != "")
                {
                    if (PWorkTypeDr[0].LevelID.ToString() != "0")
                    {
                        //期次里有小类
                        BindOneWorksType(WorksType);
                        //加载作品类别
                        ddlOneWorksType.SelectedIndex = -1;
                        ddlOneWorksType.Items.FindByValue(PWorkTypeDr[0].ParentID.ToString()).Selected = true;


                        BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                        ddlTwoWorksType.SelectedIndex = -1;
                        ddlTwoWorksType.Items.FindByValue(PWorkTypeDr[0].WorksTypeID.ToString()).Selected = true;
                        //
                        ddlTwoWorksType.Visible = true;
                        ddlOneWorksType.Enabled = false;
                        ddlTwoWorksType.Enabled = false;
                    }
                    else
                    {

                        //期次里有小类里有大类
                        BindOneWorksType(WorksType);
                        //加载作品类别
                        ddlOneWorksType.SelectedIndex = -1;
                        ddlOneWorksType.Items.FindByValue(PWorkTypeDr[0].WorksTypeID.ToString()).Selected = true;

                        BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                        ddlOneWorksType.Enabled = false;
                        ddlTwoWorksType.Enabled = true;
                    }
                }
                else
                {
                    //期次里没设置的
                    BindOneWorksType(WorksType);
                    //加载作品类别
                    BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                    ddlOneWorksType.Enabled = true;
                    ddlTwoWorksType.Enabled = true;
                }
            }
            private void JudgePeriodWorkTypeIsEnable()
            {

                //先判断期次的 控制控件可操作性

                List<CSPeriodsWorksType >  PWorkTypeDr = GetCurrPeriodsData();
                if (PWorkTypeDr[0].LevelID.ToString() != "")
                {
                    if (PWorkTypeDr[0].LevelID.ToString() != "0")
                    {
                        ddlTwoWorksType.Visible = true;
                        ddlOneWorksType.Enabled = false;
                        ddlTwoWorksType.Enabled = false;
                    }
                    else
                    {
                        ddlOneWorksType.Enabled = false;
                        ddlTwoWorksType.Enabled = true;
                    }
                }
                else
                {
                    ddlOneWorksType.Enabled = true;
                    ddlTwoWorksType.Enabled = true;
                }

            }
            /// <summary>
            /// 加载期次
            /// </summary>
            private void LoadPeriods()
            {
                //加载期次
                List<CSPeriodsWorksType> dt = DAL.Periods.GetPeriodByCourseID();
                //ViewState["Periods"] = dt;
                ddlPeriod.Items.Clear();
                DateTime endTime;
                foreach (CSPeriodsWorksType  dr in dt )
                {
                    endTime =DateTime.Parse ( dr.EndSubmit.ToString ());
                    if (DateTime.Now > dr.StartSubmit && DateTime.Now < endTime.AddDays(1))
                    {
                        if (Request.QueryString["PeriodID"] != null)
                        {
                            //判断是否是样例上传
                            string pID = Request.QueryString["PeriodID"].ToString();
                            if (dr.PeriodID.ToString () == pID)
                            {
                                ddlPeriod.Enabled = false;
                                ddlPeriod.Items.Add(new ListItem(dr.PeriodTitle , dr.PeriodID.ToString()));
                            }
                        }
                        else
                        {
                            ddlPeriod.Items.Add(new ListItem(dr.PeriodTitle , dr.PeriodID.ToString ()));
                        }
                    }
                }

            }
            private void LoadGVInfo(string viewStateName, GridView gv, int type)
            {
                if (ViewState["WoksID"] == null)
                {
                    ViewState["WoksID"] = 0;
                }
                if (ViewState[viewStateName] == null)
                {
                    ViewState[viewStateName] = GetWorksFileDt;
                }
                ViewState[viewStateName] = DAL.Works.GetWorksFile(long.Parse(ViewState["WoksID"].ToString()), type);

                BindGridView((List<WorksFile>)ViewState[viewStateName], gv);
            }
            /// <summary>
            /// 判断当前是否可以修改报名
            /// </summary>
            /// <returns></returns>
            private bool JudgeTime(long PeriodID)
            {
                bool isLoad = true;
                CSPeriodsWorksType dt = DAL.Periods.GetPeriodsByID(PeriodID);
                ddlPeriod.Items.Clear();

                if (DateTime.Now > DateTime.Parse(dt.StartSubmit.ToString ())&& DateTime.Now < DateTime.Parse(dt.EndSubmit.ToString()))
                {
                    return false;
                }
                return isLoad;

            }
            private bool JudgeSubmit(CSWorksWorksType   dr)
            {
                bool isSubmit = true;
                if (dr.WorksState==1 )
                {
                    isSubmit = false;
                }
                return isSubmit;

            }
            /// <summary>
            /// 设置控件不可用
            /// </summary>
            private void SetControls(bool istrue)
            {

                txtWorksName.Enabled = istrue;
                //
                //ddlOneWorksType.Enabled = istrue;
                //ddlTwoWorksType.Enabled = istrue;

                txtSubmitProfile.Enabled = istrue;
                txtDesignIdeas.Enabled = istrue;
                txtKeyPoints.Enabled = istrue;
                btnSave.Enabled = istrue;
                btnSubmit.Enabled = istrue;
                txtDemo1URL.Enabled = istrue;
                //
                btnUpFile.Enabled = istrue;
                btnImageUpload.Enabled = istrue;
                gvImage.Enabled = istrue;
                gvWorks.Enabled = istrue;
                user.Enabled = istrue;



            }
            /// <summary>
            /// 获取作品编码
            /// </summary>
            /// <returns></returns>
            public string GetWorksCode(long worksID)
            {
                StringBuilder sCode = new StringBuilder();
                //contestID(2)+worksID(6) 共8位作品编码
                long pID = long.Parse(ddlPeriod.SelectedValue);
                if (pID < 10000)
                {
                    sCode.Append(DateTime.Now.Year.ToString().Substring(2) + pID.ToString().PadLeft(4, '0') + worksID.ToString().PadLeft(6, '0'));
                }
                else
                {
                    sCode.Append(DateTime.Now.Year.ToString().Substring(2) + pID.ToString() + worksID.ToString().PadLeft(6, '0'));
                }
                return sCode.ToString();

            }
            /// <summary>
            /// 控制队员个数
            /// </summary>
            /// <param name="PeriodID"></param>
            public void LoadUI(long PeriodID)
            {

                int i = 0;
                string WorksTypeID = "0";
                if (ddlPeriod.SelectedValue != "")
                {
                    CSPeriodsWorksType dt =  DAL.Periods.GetPeriodsByID(PeriodID);
                    if (dt!=null)
                    {
                        i = int.Parse(dt.Number.ToString());

                        WorksTypeID = dt.WorksTypeID.ToString();
                    }
                    user.MaximumEntities = i - 1;

                    if ((i - 1) < 1)
                    {
                        user.Enabled = false;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "showUser", "<script>document.getElementById('showUser').style.display='none'</script>");

                    }
                    else
                    {

                        user.Enabled = true;
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "hideUser", "<script>document.getElementById('showUser').style.display='block'</script>");
                    }

                }
            }
            /// <summary>
            /// 判断控件
            /// </summary>
            private void Judge()
            {

                if (JudgeTime(long.Parse(ddlPeriod.SelectedValue)))
                {
                    SetControls(true);
                }
                else
                {
                    SetControls(false);
                }
            }
            /// <summary>
            /// 作品显示
            /// </summary>
            private void LoadPeriodPage()
            {
                error.Text = "";
                long worksID = 0;
                //加载work
                if (ddlPeriod.SelectedValue != "")
                {
                    worksID = BLL.Works.GetWorksIDByPriod(long.Parse(ddlPeriod.SelectedValue));
                    //判断团队人
                    LoadUI(long.Parse(ddlPeriod.SelectedValue));
                    //显示基准分数
                    JudgeScore();
                    //加载样例
                    HyperLink1.NavigateUrl = "Comments.aspx?PeriodID=" + ddlPeriod.SelectedValue + "&&IsSample=1";
                    if (worksID != 0)
                    {
                        // ViewState["WoksID"] = DESEncrypt.DesDecrypt(Request.QueryString["WorksID"].ToString());
                        ViewState["WoksID"] = worksID;
                        LoadInfo();
                        LoadGVInfo("workFile", gvWorks, 4);
                        LoadGVInfo("imageFile", gvImage, 1);

                        //2014-12-6
                        ViewState["delImageDt"] = null;
                        ViewState["delWorksDt"] = null;

                    }
                    else
                    {

                        //判断期次的workType
                        LoadPeriodWorkType();
                        //重置数据
                        ResetControls();
                        ViewState["WoksID"] = 0;
                        //GetWorksFileDt = null;
                        //GetImageFileDt = null;
                        ViewState["workFile"] = null;
                        ViewState["imageFile"] = null;
                        ////2014-12-6
                        //GetDelImageDt = null;
                        //GetWorksDt = null;
                        ViewState["delImageDt"] = null;
                        ViewState["delWorksDt"] = null;
                        LoadGVInfo("workFile", gvWorks, 4);
                        LoadGVInfo("imageFile", gvImage, 1);
                        SetControls(true);
                        //LoadUI(long.Parse(ddlPeriod.SelectedValue));

                    }
                    //加载时间和作品要求
                    LoadPeriodsRequire();
                    //加载人数和倒计时


                }
                else
                {
                    SetControls(false);
                    error.Text = "当前阶段没有作品的需求！";
                }
                ViewState["WorksID"] = worksID;

            }
            /// <summary>
            /// 重置控件
            /// </summary>
            private void ResetControls()
            {

                txtWorksName.Text = "";
                txtSubmitProfile.Text = "";
                txtDesignIdeas.Text = "";
                txtKeyPoints.Text = "";
                txtDemo1URL.Text = "";
                user.Entities.Clear();

            }
            /// <summary>
            /// 显示期次需求
            /// </summary>
            private void LoadPeriodsRequire()
            {
                lblSubmit.Text = "";
                lblScorelbl.Text = "";
                lblPublic.Text = "";
                lilRequire.Text = "";
                lblTeacher.Text = "";
                //
                List<CSPeriodsWorksType>   dr = GetCurrPeriodsData();
                //
                lblSubmit.Text = transFormat(dr[0].StartSubmit.ToString()) + "-" + transFormat(dr[0].EndSubmit.ToString());
                lblScorelbl.Text = transFormat(dr[0].StartScore.ToString()) + "-" + transFormat(dr[0].EndScore.ToString());
                lblPublic.Text = transFormat(dr[0].StartPublic.ToString()) + "-" + transFormat(dr[0].EndPublic.ToString());

                lilRequire.Text = "<div style='width:700px'>" + dr[0].Require.ToString() + "</div>";
                lblNum.Text = dr[0].Number.ToString();

                if (dr[0].CreatedBy.ToString() != "")
                {

                    ContestDll.User  userDt = DAL.User.GetUserByUserID(long.Parse(dr[0].CreatedBy .ToString()));
                    if (userDt!=null)
                    {
                        lblTeacher.Text = userDt.Name;

                    }
                }
                //加载上传人数
                lblWorksNum.Text = DAL.Works.GetWorksNumByPeriodID(long.Parse(ddlPeriod.SelectedValue)).ToString();
               
                //设置倒计时
                if (dr[0].EndSubmit.ToString() != "")
                {
                    hfEndTime.Value = DateTime.Parse(dr[0].EndSubmit.ToString()).AddDays(1).ToString().Replace("-", "/");

                }
                else
                {
                    hfEndTime.Value = DateTime.Now.ToString().Replace("-", "/");
                }

            }
            /// <summary>
            /// 获取当前期次数据
            /// </summary>
            /// <returns></returns>
            private List<CSPeriodsWorksType> GetCurrPeriodsData()
            {
                List<CSPeriodsWorksType> dt = GetPeriodsWorksType;
                List<CSPeriodsWorksType> dr = dt.Where (p=>p.PeriodID == long.Parse (ddlPeriod.SelectedValue)).ToList ();
                return dr;
            }
            /// <summary>
            /// 转换格式
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            private string transFormat(string time)
            {
                return DateTime.Parse(time).ToString("d").Replace('-', '.');
            }
            /// <summary>
            /// 判断显示分数
            /// </summary>
            private void JudgeScore()
            {

                if (Request.QueryString["IsSample"] != null)
                {
                    ddlPeriod.Enabled = false;

                    rFScore.Enabled = true;
                    rVScore.Enabled = true;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "showScore", "<script>document.getElementById('showScore').style.display='block'</script>");
                }
                else
                {
                    rFScore.Enabled = false;
                    rVScore.Enabled = false;

                    ddlPeriod.Enabled = true;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "showScore", "<script>document.getElementById('showScore').style.display='none'</script>");
                }


            }
            private void GoToPage(long worksID)
            {
                if (Request.QueryString["IsSample"] != null || Request.QueryString["PeriodID"] != null)
                {
                    //  Response.Redirect(myWeb.Url + "/_layouts/15/WorkEvaluate/OnlineEnroll.aspx?IsSample=1&&PeriodID=" + Request.QueryString["PeriodID"].ToString());
                    Response.Redirect("Comments.aspx?View=1&&WorksID=" + worksID);
                }
                else
                {
                    // Response.Redirect(myWeb.Url + "/_layouts/15/WorkEvaluate/OnlineEnroll.aspx");
                    Response.Redirect("Comments.aspx?View=1&&WorksID=" + worksID);
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
                    DelListItem(fileName, "workVideo");
                }
                else if (type == "2")
                {
                    DelListItem(fileName, "workPic");
                }
                else
                {
                    DelListItem(fileName, "workFile");
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
                            if (listName == "workVideo")
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

            private bool JudgeWordCount()
            {
                bool isSave = false;
                if (txtKeyPoints.Text.Length < 100 || txtKeyPoints.Text.Length > 800)
                {
                    isSave = true;
                    errorKeyPoints.Text = "请注意字数限制";
                    errorKeyPoints.Focus();
                }
                else
                {
                    errorKeyPoints.Text = "";

                }
                if (txtDesignIdeas.Text.Length < 100 || txtDesignIdeas.Text.Length > 2000)
                {
                    isSave = true;
                    errorDesignIdeas.Text = "请注意字数限制";
                    errorDesignIdeas.Focus();
                }
                else
                {
                    errorDesignIdeas.Text = "";

                }

                if (txtSubmitProfile.Text.Length < 50 || txtSubmitProfile.Text.Length > 500)
                {
                    isSave = true;
                    errorSubmitProfile.Text = "请注意字数限制";
                    errorSubmitProfile.Focus();
                }
                else
                {
                    errorSubmitProfile.Text = "";
                }
                //此外不需要判断
                //团队上传,队长是成员以外的人，所以人数加1
                //if (((List<WorksFile>)ViewState["workFile"]).Count != GetUserCount() + 1)
                //{
                //    isSave = true;
                //    jiangJieError.Text = "请注意，上传讲解视频的数量应与团队人数相符";
                //}
                return isSave;
            }

            private int GetUserCount()
            {
                ArrayList list = user.ResolvedEntities;
                SPUser member;
                int num = 0;
                foreach (Microsoft.SharePoint.WebControls.PickerEntity p in list)
                {
                    member = myWeb.EnsureUser(p.Key);
                    if (member.LoginName != SPContext.Current.Web.CurrentUser.LoginName)
                    {
                        num++;
                    }

                }
                return num;

            }

            #endregion

        }
    
}
