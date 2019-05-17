using System;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace ContestDll
{
    public partial class Comments : System.Web.UI.Page
    {
        #region 显示控件定义
        protected Label lblWorksName;
        protected Label lblWorksCode;
        protected Label lblWorksType;
        protected Label lblSubmitProfile;

        protected GridView gvComments;
        protected HtmlGenericControl divWorksFile;
        protected Label lblPersons;
        protected HtmlGenericControl divScore;
        //点评
        protected HtmlGenericControl divPublicComments;//公式期点评
        protected HiddenField HiddenField2;
        protected Button btnSubmitShow;
        protected TextBox txtComments;
        protected HiddenField HiddenField1Show;
        protected Label lblDemoURL;
        protected HtmlGenericControl divDesignIdeas;
        protected HtmlGenericControl divViewShow;
        protected HtmlGenericControl divDocView;
        protected HtmlGenericControl divKeyPoints;
        protected HtmlGenericControl divDovVideo;
        #endregion
        #region 评分控件定义
        protected HtmlGenericControl divWorksScore;
        protected Button btnSubmit;
        protected Button btnSave;
        protected Label lblTotalScore;
        protected HtmlGenericControl divContent;
        protected Button btnClose;
        protected TextBox txtScorePingYu;
        protected HiddenField HiddenField1;
        protected UpdatePanel UpdatePanel1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser == null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('请先登录');document.location.replace('" + DAL.Common.SPWeb.Url + "');", true);
                return;
            }
            if (WorksID == 0) return;
            btnSubmit.Click += new EventHandler(btnSubmint_Click);
            btnClose.Click += new EventHandler(btnClose_Click);
            if (HiddenField1.Value != "-1")
                lblTotalScore.Text = HiddenField1.Value;
            InitControl();
            //查看比较评过的分数
            if (!Page.IsPostBack)
            {
                if (IsViewScore)
                {
                    ViewComments(false);
                }
                else if (PeriodTime == 2)
                {
                    ViewComments(true);
                }
            }
            //作品相关的

            btnSubmitShow.Click += new EventHandler(btnSubmitShow_Click);
            if (!Page.IsPostBack)
            {
                InitControlWorksShow();
            }
            FillDocView();//doc
            ShowControlDisplay();

        }
        #region 评分相关的
        #region 属性
        /// <summary>
        /// 返回当前作品其次的时间段1、评价训练2、评分、3、公示点评
        /// </summary>
        private int PeriodTime
        {
            get
            {
                if (ViewState["periodTime"] == null)
                {
                    ViewState["periodTime"] = BLL.Course.JudgeDate(PeriodID);
                }
                return (int)ViewState["periodTime"];
            }
        }
        private List<WorksType> dsWorksType;
        private List<WorksType> DSWorksType
        {
            get
            {
                if (dsWorksType == null)
                {
                    dsWorksType = DAL.Works.GetWorksType();
                }
                return dsWorksType;
            }
            set
            {
                dsWorksType = value;
            }
        }
             
        private long PeriodID
        {
            get
            {
                if (Request.QueryString["PeriodID"] != null && Request.QueryString["PeriodID"] != "")
                    return long.Parse(Request.QueryString["PeriodID"]);
                else
                    return 0;
            }
        }
        private int IsSample
        {
            get
            {
                if (Request.QueryString["IsSample"] != null)
                    return int.Parse(Request.QueryString["IsSample"]);
                else
                    return 0;
            }
        }
        /// <summary>
        /// 评分和点评控件何时显示
        /// </summary>
        private void ShowControlDisplay()
        {
            if (IsViewWorks)
            {
                divPublicComments.Attributes.Add("style", "display:none");
                divWorksScore.Attributes.Add("style", "display:none");
                return;
            }
            if (IsSample == 0)
            {
                if (PeriodTime == 3)
                {
                    divPublicComments.Attributes.Add("style", "display:");
                    divWorksScore.Attributes.Add("style", "display:none");
                }
                else if (PeriodTime == 2)
                {
                    divPublicComments.Attributes.Add("style", "display:none");
                    divWorksScore.Attributes.Add("style", "display:");
                }
                else
                {
                    divPublicComments.Attributes.Add("style", "display:none");
                    divWorksScore.Attributes.Add("style", "display:none");
                }
            }
            else
            {
                if (PeriodTime != 1)
                {
                    divPublicComments.Attributes.Add("style", "display:none");
                    divWorksScore.Attributes.Add("style", "display:none");
                }


            }
        }
        List<CSWorksToEvaluate> dsWorksToEvaluage;
        private List<CSWorksToEvaluate> DsCurrentWorks1
        {
            get
            {
                return dsWorksToEvaluage;
            }
            set
            {
                dsWorksToEvaluage = value;
            }
        }
        private List<CSWorksWorksType> DsCurrentWorks
        {
            get
            {
                    if (IsViewWorks)
                    {
                        CSWorksWorksType dsTmp;
                        List<CSWorksWorksType> dsTmp1=new List<CSWorksWorksType> ();
                        dsTmp = DAL.Works.GetWorksByWorksID(WorksID);
                        dsTmp1.Add(dsTmp);
                        return dsTmp1;
                    }

                    List<CSWorksWorksType>  dsSearch=null;
                    List<CSWorksToEvaluate> dsSearch1=null; 
                    List<CSWorksWorksType >  drs;
                    List<CSWorksToEvaluate> drs1;
                    if (PeriodTime == 3)
                        dsSearch = DAL.Works.GetWorksByPeriodID(PeriodID);
                    else
                        dsSearch1 = DAL.Works.GetWorksToEvaluate(DAL.Common.LoginID, PeriodID, IsSample);
                    List<CSWorksWorksType> dsSample=null ;
                    List<CSWorksToEvaluate> dsSample1=null;  
                    if (IsSample == 1 && PeriodTime == 1)
                    {
                        SamplePassed = 0;
                        if (dsSearch!=null && dsSearch.Count ==0 || dsSearch1!=null && dsSearch1.Count ==0 )//样例作品
                        {
                            dsSample  = DAL.Works.GetSampleWorksByPeriodID(PeriodID);
                        }
                        else
                        {//未通过的
                            dsSample  = dsSearch.Where(p=>p.Flag ==4).ToList();// Select("Flag=4");
                            if (dsSample .Count > 0)
                            {
                                SamplePassed = 1;
                                ShowMessage(UpdatePanel1, "您已经通过评价训练！");
                                btnSubmit.Enabled = false;
                            }
                            else
                            {
                                //sui ji sample
                                dsSample = DAL.Works.GetSampleWorksByPeriodID(PeriodID);
                                if (dsSample.Count == 0)
                                {
                                    dsSample = dsSearch.Where(p => p.Flag == 3).ToList();
                                }
                                else
                                {
                                    foreach (CSWorksWorksType dr in dsSample)
                                    {
                                        drs1 = dsSearch1.Where(p=>p.WorksID ==dr.WorksID && p.ExpertID ==DAL.Common.LoginID ).ToList ();// ("WorksID=" + dr["WorksID"] + " and ExpertID=" + DAL.Common.LoginID); //是否已经点评过
                                        if (drs1.Count  == 0)
                                        {
                                            dsSample1 = drs1;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (IsSample == 0)
                    {
                        if (dsSearch != null)
                        {
                            drs = dsSearch.Where(p => p.WorksID == WorksID).ToList();
                            dsSample = drs;
                        }
                        else if (dsSearch1 != null)
                        {
                            drs1 = dsSearch1.Where(p=>p.WorksID ==WorksID ).ToList ();
                            dsSample1 = drs1;
                        }
                    }
                    DsCurrentWorks1 = dsSample1;
                    return dsSample;
                 
            }
        }
       
        private int samplePassed = 0;
        private int SamplePassed
        {
            get
            {
                return samplePassed;
            }
            set
            {
                samplePassed = value;

            }
        }
        //查看作品，不查看分数
        private bool IsViewWorks
        {
            get
            {

                if (Request.QueryString["View"] == "1")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 样例通过可以查看分数
        /// </summary>
        private bool IsViewScore
        {
            get
            {

                if (SamplePassed == 1)
                    return true;
                else
                    return false;
            }
        }
                    List<RatingDetails> dsDetails;
        private List<RatingDetails> DSScoreDetails
        {
            get
            {
                if (dsDetails == null)
                {
                    List<CSWorksToEvaluate> ds = DsCurrentWorks1;
                    long worksExpertID = 0;
                    if (ds != null && ds.Count > 0)
                        worksExpertID = ds[0].WorksExpertID;
                    dsDetails = DAL.Works.GetRatingDetailsByWorksExpertID(worksExpertID);
                 }
                return dsDetails;
            }
        }
        //获取所有类别
        private List<ScoreStandard> dsStandardSubLevel;
        private List<ScoreStandard> DSScoreStandardSubLevel
        {
            get
            {
                if (dsStandardSubLevel == null)
                {
                    dsStandardSubLevel = DAL.Works.GetScoreStandardSubLevel();
                }
                return dsStandardSubLevel;
            }
        }

        #endregion
        #region 事件

        void btnClose_Click(object sender, EventArgs e)
        {
        }
        void btnSubmint_Click(object sender, EventArgs e)
        {
            string txtPingYu = txtScorePingYu.Text;
            if (txtPingYu.Length < 30)
            {
                ShowMessage(UpdatePanel1, "评语不能少于30字");
                return;
            }
            SortedList<int, int> scores1 = new SortedList<int, int>();//按添加时的顺序
            foreach (Control ctr in this.divContent.Controls)
            {
                if (ctr.GetType() == typeof(Panel))
                {
                    string id = ctr.ID;
                    string standardID = id.Substring(3);
                    Panel panel = (Panel)ctr;
                    foreach (Control subCtr in panel.Controls)
                    {
                        if (subCtr.GetType() == typeof(TextBox))
                        {
                            TextBox txtBox = (TextBox)subCtr;
                            scores1.Add(int.Parse(standardID), int.Parse(txtBox.Text));
                        }
                    }
                }
            }
            if (DsCurrentWorks != null && DsCurrentWorks.Count > 0 || DsCurrentWorks1 != null && DsCurrentWorks1.Count > 0)
            {
                if (IsSample == 1)
                    SaveSampleScore(scores1);
                else
                    SaveWorksScore(scores1);
            }

        }
        /// <summary>
        /// 保存样例成绩
        /// </summary>
        private void SaveSampleScore(SortedList<int, int> scores1)
        {
            List<CSWorksToEvaluate>  ds1 = DsCurrentWorks1;
            List<CSWorksWorksType> ds = DsCurrentWorks;
            long result = 0;
            if (ds!=null && ds.Count >0 || ds1!=null && ds1.Count >0)
            {
                long worksExpertID = 0;
                long userID = DAL.Common.LoginID;
                long worksID=0;
                if (ds != null && ds.Count > 0)
                    worksID = ds[0].WorksID;
                else if (ds1 != null && ds1.Count > 0)
                {
                    worksID = (long)ds1[0].WorksID;
                    userID = (long)ds1[0].ExpertID;
                    worksExpertID = ds1[0].WorksExpertID;
                }
                else if (userID == 0)
                {
                    userID = BLL.User.GetUserID(DAL.Common.SPWeb.CurrentUser);
                }

                result = BLL.Works.SampleScore(worksExpertID, worksID, userID, scores1, txtScorePingYu.Text);
                if (result == 4)
                {
                    btnSubmit.Enabled = false;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('评价练习已经通过');window.opener ='';window.open('','_self');window.close()", true);
                }
                else if (result == 3)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('评价练习没有通过，请继续评价');document.location.replace('Comments.aspx?PeriodID=" + PeriodID + "&&IsSample=1')", true);
                }
                else if (result == 0)
                {
                    ShowMessage(UpdatePanel1, "提交失败");
                }
            }
        }
        /// <summary>
        /// 保存作品评分
        /// </summary>
        private void SaveWorksScore(SortedList<int, int> scores1)
        {
            List<CSWorksToEvaluate> ds1 = DsCurrentWorks1;
            int result = 0;
            if ( ds1!=null && ds1.Count >0)
            {
                CSWorksToEvaluate DRCurrentWorks = ds1[0];
                result = BLL.Works.WorksScore( DRCurrentWorks.WorksExpertID , (long)DRCurrentWorks.WorksID , (long)DRCurrentWorks.ExpertID, scores1, txtScorePingYu.Text);
                if (result == 1)
                {
                    ViewState["DsWorks"] = null;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('提交成功');window.opener ='';window.open('','_self');window.close()", true);
                }
                else if (result == 0)
                {
                    ShowMessage(UpdatePanel1, "提交失败");
                }
            }
        }
        /// <summary>
        /// 保存输入错误的分的ID值
        /// </summary>
        /// <param name="errorID"></param>
        /// <param name="rightID"></param>
        /// <returns></returns>
        private void GetErrorInput(string errorID, string rightID)
        {
            List<string> lsts = (List<string>)ViewState["ErrorIds"];
            if (lsts == null)
                lsts = new List<string>();
            if (errorID != "")
            {
                if (!lsts.Contains(errorID))
                    lsts.Add(errorID);
            }
            else
            {
                if (lsts.Contains(rightID))
                    lsts.Remove(rightID);
            }
            ViewState["ErrorIds"] = lsts;
        }

        //分数更改事件
        void txtBox_TextChanged(object sender, EventArgs e)
        {
            string id = ((TextBox)sender).ClientID;
            string indx = id.Substring(6, id.IndexOf("_") - 6);
            int max = int.Parse(id.Substring(id.IndexOf("_") + 1));
            int def = 0;
            string err = "只能输入0和" + max.ToString() + "之间的分数";
            Panel panel = (Panel)Page.Form.FindControl("div" + indx);
            Image img = (Image)panel.FindControl("img" + indx);
            img.Style.Remove("visibility");
            img.ImageUrl = "images/cuo.png";
            try
            {
                def = int.Parse(((TextBox)sender).Text);
                if (def >= 0 && def <= max)
                {
                    img.ImageUrl = "images/dui.png";
                    GetErrorInput("", indx);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script defer>alert('" + err + "');document.getElementById('" + id + "').select();</script>");
                    GetErrorInput(indx, "");
                    return;
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script defer>alert('" + err + "');document.getElementById('" + id + "').select();</script>");
                GetErrorInput(indx, "");
                return;
            }

            int total = ViewState["total"] == null ? 0 : int.Parse(ViewState["total"].ToString());
            int oleScore = ViewState[id] == null ? 0 : int.Parse(ViewState[id].ToString());
            lblTotalScore.Text = (total - oleScore + def).ToString();
            ViewState["total"] = lblTotalScore.Text;
            ViewState[id] = ((TextBox)sender).Text;
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script defer>document.getElementById('" + id + "').focus();</script>");

        }

        #endregion
        #region 方法
       
        private int GetTopWorksTypeID(int worksTypeID)
        {
            List<WorksType>   dsWorksType = DSWorksType;
            List<WorksType> drs = dsWorksType.Where(p=>p.WorksTypeID ==worksTypeID ).ToList ();// ("WorksTypeID=" + worksTypeID);
            int parentID = (int) drs[0].ParentID ;
            while (parentID > 0)
            {
                drs = dsWorksType.Where(p=>p.WorksTypeID ==parentID ).ToList ();// ("WorksTypeID=" + parentID);
                if (drs.Count > 0)
                    parentID =(int) drs[0].ParentID;
            }

            return  drs[0].WorksTypeID;
        }
        /// <summary>
        /// 初始化控件pingfen
        /// </summary>
        private void InitControl()
        {
            if (IsViewWorks)
                return;
            divContent.Controls.Clear();
            int worksTypeID = 0;
            List<CSWorksWorksType>   ds = DsCurrentWorks;
            List<CSWorksToEvaluate > ds1 = DsCurrentWorks1; 
            if (ds!=null && ds.Count >0)
                worksTypeID = (int)ds[0].WorksTypeID;
            else if (ds1!=null && ds1.Count >0)
                worksTypeID = (int)ds1[0].WorksTypeID;
            
            else
            {
                btnSubmit.Enabled = false;
                return;
            }
            List<WorksType> dsWorksType = DSWorksType;
            List<string> itmes = new List<string>();
            List<WorksType> dv = dsWorksType.Where(p => p.WorksTypeID == worksTypeID).ToList();
            if (dv.Count == 0) return;
            itmes.Add(dv[0].WorksTypeName);
            GetWorksTypeTopLevel(dsWorksType, (int)dv[0].ParentID, ref itmes);
            //作品类别、期次ID
            int topWorksTypeID = GetTopWorksTypeID(worksTypeID);
            List<CSPeriodScoreStandard> dsScoreStandard = DAL.Works.GetScoreStandardByWorksType(topWorksTypeID, PeriodID);
            List<CSWorksTypeScoreStandard> dsTypeScoreStandard;
            int i = 1;
            foreach (CSPeriodScoreStandard dr in dsScoreStandard)
            {
                FillScoreStandard(dr, null, i.ToString());
                i += 1;
            }

            if (dsScoreStandard.Count == 0)
            {
                dsTypeScoreStandard = DAL.WorksType.GetWorksTypeScoreStandardByTypeID(topWorksTypeID);
                i = 1;
                foreach (CSWorksTypeScoreStandard dr in dsTypeScoreStandard)
                {
                    FillScoreStandard(null, dr, i.ToString());
                    i += 1;
                }
            }
        }
        //浏览的分数界面
        private void ViewComments(bool allowEdit)
        {

            btnSubmit.Visible = allowEdit;
            List<CSWorksWorksType> ds = DsCurrentWorks;
            List<CSWorksToEvaluate> ds1 = DsCurrentWorks1;
            float score = 0;
            string comments="";
            if (ds != null && ds.Count > 0)
            {
                if (ds[0].Score != null)
                    score = (float)ds[0].Score;
                comments = ds[0].Comment;
            }
            else if (ds1 != null && ds1.Count > 0)
            {
                if (ds1[0].Score != null)
                    score = (float)ds1[0].Score;
                comments = ds1[0].Comments;
            }

            lblTotalScore.Text = score.ToString();
            txtScorePingYu.Text = comments;
            txtScorePingYu.ReadOnly = !allowEdit;
            List<RatingDetails> dsDetails = DSScoreDetails;
            List<RatingDetails> drs;
            try
            {
                Single totalScore = 0;
                foreach (Control ctr in this.divContent.Controls)
                {
                    if (ctr.GetType() == typeof(Panel))
                    {
                        string id = ctr.ID;
                        string standardID = id.Substring(3);
                        Panel panel = (Panel)ctr;
                        foreach (Control subCtr in panel.Controls)
                        {
                            if (subCtr.GetType() == typeof(TextBox))
                            {
                                TextBox txtBox = (TextBox)subCtr;

                                drs = dsDetails.Where(p => p.StandardID == int.Parse(standardID)).ToList();
                                score = (float)drs[0].Score;
                                txtBox.Text = score.ToString();
                                totalScore += score;
                                txtBox.ReadOnly = !allowEdit;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        //获取大类别
        private void GetWorksTypeTopLevel(List<WorksType> dsWorksType, int parentID, ref List<string> itmes)
        {
            List<WorksType> dv = dsWorksType.Where(p => p.WorksTypeID == parentID).ToList(); 
            if (dv.Count > 0)
            {
                itmes.Add(dv[0].WorksTypeName);
                GetWorksTypeTopLevel(dsWorksType, (int)dv[0].ParentID, ref itmes);
            }

        }
        //评分标准
        private void FillScoreStandard(CSPeriodScoreStandard  dr,CSWorksTypeScoreStandard  dr1, string i)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<div class=\"comments-scores-model\">");
            int standardID = dr == null ? dr1.StandardID  : dr.StandardID ;
            int score=dr == null ? (int)dr1.Score : (int)dr.Score;
            string subStandard = dr == null ? dr1.StandardDescription : dr.StandardDescription;
            string standardName = dr == null ? dr1.StandardName : dr.StandardName;

            //评分
            Panel div = new Panel();
            div.ID = "div" +standardID ;// dr["StandardID"];
            div.CssClass = "comments-scores-modelTitle";
            div.Controls.Add(new LiteralControl(standardName + "（" + score + "分）"));

            TextBox txtBox = new TextBox();
            txtBox.ID = "txtFen" + standardID + "_" + score;
            txtBox.Text = "0";
            txtBox.CssClass = "txtcss";
            txtBox.Attributes.Add("onfocus", "onft" + i + "(this)");
            txtBox.Attributes.Add("onblur", "onbt" + i + "(this)");
            txtBox.Attributes.Add("onchange", "checkInput(this)");
            div.Controls.Add(txtBox);
            Image img = new Image();
            img.Style.Add("visibility", "hidden");
            img.ID = "img" + standardID ;
            img.ImageUrl = "images/dui.png";
            div.Controls.Add(img);
            str.Append("<div class=\"comments-scores-divHidden\" id=\"HiddenDiv" + i + "\">");
            str.AppendLine("<ul>");
            string[] subsValue = subStandard.Split('\n');
            foreach (string strValue in subsValue)
            {
                str.AppendLine("<li>" + strValue + "</li>");
            }
            str.AppendLine("</ul>");
            str.AppendLine("</div>");
            divContent.Controls.Add(div);
            divContent.Controls.Add(new LiteralControl(str.ToString()));
            divContent.Controls.Add(new LiteralControl("</div>"));
        }
        #endregion
        #region 引用的方法
        public bool ShowMessage(UpdatePanel UpdatePanel1, string msg)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateScript", "alert('" + msg + "')", true);
            return true;
        }
        #endregion
        #endregion
        #region 作品相关的
        #region 属性
        private List<CSWorksExpertUser> dsWorksComments;
        private List<CSWorksExpertUser> GetDSWorksComments
        {
            get
            {
                if (dsWorksComments == null)
                {
                    dsWorksComments = DAL.Works.GetWorksCommentsByWorksID(WorksID);
                  
                }
                return dsWorksComments;
            }
        }
        private List<Works> dsWorksSubmit; 
        private List<Works> GetDSWorksSubmit
        {
            get
            {
                if (dsWorksSubmit == null)
                {

                    dsWorksSubmit = DAL.Works.GetWorksSubmitByID(WorksID);
                }
                return dsWorksSubmit;
            }
        }
        private List<WorksFile> worksDoc;
        /// <summary>
        /// get doc
        /// </summary>
        private List<WorksFile>  GetWorksDoc
        {
            get
            {
                if (worksDoc == null)
                {
                    worksDoc = BLL.WorksType.GetWorksFileByTypeID(WorksID, 1);
                }
                return worksDoc;
            }
        }
        private List<WorksFile> worksVideo;
        //获取文档视频
        private List<WorksFile> GetWorksDocVideo
        {
            get
            {
                if (worksVideo == null)
                {
                    worksVideo = BLL.WorksType.GetWorksFileByTypeID(WorksID, 3);
                }
                return worksVideo;
            }
        }
        private List<WorksFile> worksViewVideo;
        //获取演示视频
        private List<WorksFile> GetWorksViewVideo
        {
            get
            {
                if (worksViewVideo == null)
                {
                    worksViewVideo = BLL.WorksType.GetWorksFileByTypeID(WorksID, 4);
                }
                return worksViewVideo;
            }
        }
        private List<WorksFile> worksFile;
        //类型2为图片
        private List<WorksFile> GetDSWorksFile
        {
            get
            {
                if (worksFile == null)
                {
                    worksFile = BLL.WorksType.GetWorksFileByTypeID(WorksID, 2);
                }
                return worksFile;
            }
        }
        private long WorksID
        {
            get
            {
                if (ViewState["worksID"] == null)
                {
                    long worksID = 0;
                    if (Request.QueryString["WorksID"] != null)
                        worksID = long.Parse(Request.QueryString["WorksID"]);
                    else if (IsSample == 1 && PeriodTime == 1)
                    {
                        List<CSWorksWorksType> ds = DsCurrentWorks;
                        List<CSWorksToEvaluate> ds1 = DsCurrentWorks1;
                        if (ds != null && ds.Count > 0)
                            worksID = (long)ds[0].WorksID;
                        else if (ds1 != null && ds1.Count > 0)
                            worksID = (long)ds1[0].WorksID;
                    }
                    ViewState["worksID"] = worksID;
                }
                return (long)ViewState["worksID"];
            }
        }
        #endregion
        #region 事件
        void btnSubmitShow_Click(object sender, EventArgs e)
        {
            Single score = Single.Parse(HiddenField2.Value);
            List<CSWorksExpertUser> dt = GetDSWorksComments;
            CSWorksExpertUser dr = new CSWorksExpertUser();
            dr.WorksID  = WorksID;
            dr.ExpertID  = DAL.Common.LoginID;
            dr.Flag  = 2;
            dr.Score  = score;
            dr.Comments = txtComments.Text;
            dr.Created = DateTime.Now;
            try
            {
                long resultID = DAL.Works.InsertWorksComments(dr);
                if (resultID > 0)
                {
                    FillComments();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('点评成功');document.location.replace('Comments.aspx?PeriodID=" + PeriodID + "&&WoksID=" + WorksID + ")</script>");
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('点评失败');</script>");

            }
        }
        #endregion
        #region 方法
        private void InitControlWorksShow()
        {
            List<CSWorksWorksType> ds= DsCurrentWorks;
            List <CSWorksToEvaluate> ds1=DsCurrentWorks1;
            if (ds!=null && ds.Count  == 0 || ds1!=null && ds1.Count ==0) return;
            List<Works>  dsWorks = GetDSWorksSubmit;
            string worksName="";
            string worksTypeName="";
            if (ds!=null && ds.Count  >0)
            {
                worksName = ds[0].WorksName;
                worksTypeName = ds[0].WorksTypeName;
            }
            else if (ds1 != null && ds1.Count > 0)
            {
                worksName = ds1[0].WorksName;
                worksTypeName = ds1[0].WorksTypeName;
            }
            if (dsWorks.Count > 0)
            {
                Works dr = dsWorks[0];
                lblWorksName.Text = worksName;
                lblWorksCode.Text = dr.WorksCode==null ? "" : dr.WorksCode ;
                lblWorksType.Text = worksTypeName;
                try
                {
                    lblSubmitProfile.Text = dr.SubmitProfile==null ? "" : dr.SubmitProfile;
                    divKeyPoints.Controls.Clear();
                    if (dr.KeyPoints !=null)
                        divKeyPoints.Controls.Add(new LiteralControl(dr.KeyPoints));
                    lblDemoURL.Text = dr.DemoURL==null ? "" : dr.DemoURL;
                    divDesignIdeas.Controls.Clear();
                    if ( dr.DesignIdeas!=null)
                        divDesignIdeas.Controls.Add(new LiteralControl(dr.DesignIdeas));
                }
                catch
                {
                }
                FillVideoShow();//video
                FillWorksFile();//picture
                FillComments();//dianping
            }
        }
        private void FillDocView()
        {
            string txtDocView = "<iframe src='" + DAL.Common.SPWeb.Url + "/_layouts/15/WopiFrame.aspx?sourcedoc=qqqqqq1111&action=embedview'  width='600px' height='470px'></iframe>";

            divDocView.Controls.Clear();
            List<WorksFile >  ds = GetWorksDoc;
            string txtUrl;
            string subWebUrl = SPContext.Current.Web.Url;
            int urlIndex = subWebUrl.IndexOf("/", 8);
            if (urlIndex >0)
                subWebUrl = subWebUrl.Substring(urlIndex);
            else
                subWebUrl ="/";
            LinkButton btn = new LinkButton();
            foreach (WorksFile dr in ds )
            {
                txtUrl = dr.FilePath ;
                string docUrl = subWebUrl + txtUrl;
                txtDocView = txtDocView.Replace("qqqqqq1111", docUrl);//加上子网站的链接
                divDocView.Controls.Add(new LiteralControl(txtDocView));
                btn = new LinkButton();
                btn.ID = "btn" + dr.WorksFileID;
                btn.CommandArgument = docUrl;
                btn.Click += btn_Click;
                btn.Text = "下载到本地";
                divDocView.Controls.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string fileName = btn.CommandArgument;
            string docName = fileName.Substring(fileName.LastIndexOf("/") + 1);
            string saveFileName = DealWorkfileName(docName);
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
                        using (SPWeb web = site.OpenWeb(DAL.Common.SPWeb.ID))
                        {
                            SPFile file = web.GetFile(url);
                            DAL.Common.DownLoadFileByStream(file, Response, fileName);

                        }
                    }
                });
            }
            catch (Exception ex)
            { }
        }
        private void FillVideoShow()
        {
            List<WorksFile>  ds = GetWorksDocVideo;//3
            string txtVideo1 = "";
            int i = 1;
            divDovVideo.Controls.Clear();
            foreach (WorksFile dr in ds )
            {
                txtVideo1 = FillVideoControl("文档视频：" +  dr.FileName,  dr.FilePath, i.ToString(), "true");
                divDovVideo.Controls.Add(new LiteralControl(txtVideo1));
                i += 1;
            }
            ds = GetWorksViewVideo;//4
            divViewShow.Controls.Clear();
            foreach (WorksFile dr in ds )
            {
                txtVideo1 = FillVideoControl("讲解视频：" +  dr.FileName,  dr.FilePath, i.ToString(), "false");
                divViewShow.Controls.Add(new LiteralControl(txtVideo1));
                i += 1;
            }
        }
        private string FillVideoControl(string mediaTitle, string mediaUrl, string i, string autoPlay)
        {
            StringBuilder txtContent = new StringBuilder();
            txtContent.AppendLine("<div id=\"MediaPlayerHost" + i + "\">");
            txtContent.AppendLine("<script type=\"text/javascript\" >");
            txtContent.AppendLine("{");
            txtContent.AppendLine("mediaPlayer.createMediaPlayer(");
            txtContent.AppendLine("document.getElementById('MediaPlayerHost" + i + "'),");
            txtContent.AppendLine("'MediaPlayerHost',");
            txtContent.AppendLine("'600px', '470px',");
            txtContent.AppendLine("{");
            txtContent.AppendLine("displayMode: 'Inline',");
            txtContent.AppendLine("mediaTitle: '" + DealWorkfileName(mediaTitle) + "',");
            txtContent.AppendLine("mediaSource: '" + DAL.Common.SPWeb.Url + mediaUrl + "',");
            string txtExtend = mediaTitle.Substring(mediaTitle.LastIndexOf(".") + 1);
            string previewFile = DAL.Common.SPWeb.Url + "/_layouts/15/WorkEvaluate/images/";
            if (txtExtend.ToLower() == "mp3")
                previewFile += "yinpin.jpg";
            else
                previewFile += "shipin.jpg";
            txtContent.AppendLine("previewImageSource: '" + previewFile + "',");
            txtContent.AppendLine("autoPlay: " + autoPlay + ",");
            txtContent.AppendLine("loop: false,");
            txtContent.AppendLine("mediaFileExtensions: 'wmv;wma;avi;mpg;mp3;',");
            txtContent.AppendLine(" silverlightMediaExtensions: 'wmv;wma;mp3;'");
            txtContent.AppendLine("});");
            txtContent.AppendLine("}");
            txtContent.AppendLine("</script>");
            txtContent.AppendLine("</div>");
            return txtContent.ToString();
        }
        private string DealWorkfileName(string fileName)
        {
            string txtFile = fileName.Substring(0, fileName.IndexOf("-")) + fileName.Substring(fileName.LastIndexOf("-") + 1);
            return txtFile;
        }
        //作品展示图
        private void FillWorksFile()
        {
            List<WorksFile>  ds = GetDSWorksFile;
            StringBuilder txtContent = new StringBuilder();
            foreach (WorksFile dr in ds)
            {
                txtContent.AppendLine("<img src='" + DAL.Common.SPWeb.Url + dr.FilePath + "' width='500px'/><br />" + DealWorkfileName(dr.FileName));
                txtContent.AppendLine("<br/>");
            }
            if (txtContent.ToString().Length > 0)
                txtContent.Remove(txtContent.Length - 5, 5);
            divWorksFile.Controls.Clear();
            divWorksFile.Controls.Add(new LiteralControl(txtContent.ToString()));
        }
        //师生点评
        private void FillComments()
        {
            divScore.Controls.Clear();
            List<CSWorksExpertUser> ds = GetDSWorksComments;
            List<CSWorksExpertUser> dv = ds.Where(p => p.ExpertID == DAL.Common.LoginID).ToList();
            if (dv.Count > 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>YiComments();</script>");


            lblPersons.Text = ds.Count.ToString();
            gvComments.DataSource = ds;
            gvComments.DataBind();
            float avg = 0;
            //string sScore = ds..Compute("AVG(Score)", "").ToString();
            foreach (CSWorksExpertUser dr in ds)
            {
                if (dr.Score != null)
                    avg += (float)dr.Score;
            }
            if (ds.Count > 0)
                avg = avg / ds.Count;
            int score = (int)avg;
            string txt = "";
            for (int i = 0; i < score; i++)
            {
                txt += "<img src='images/star_red.gif'/>";
            }
            int j = score;
            if (avg > score)
            {
                j = j + 1;
                txt += "<img src='images/star_red2.gif'/>";

            }
            for (int i = j; i < 5; i++)
            {
                txt += "<img src='images/star_gray.gif'/>";

            }
            divScore.Controls.Add(new LiteralControl(txt));
        }
        #endregion
        #endregion

    }
}
