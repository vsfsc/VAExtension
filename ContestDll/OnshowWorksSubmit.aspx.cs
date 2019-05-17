using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Data;
using System.Collections;

namespace ContestDll
{
    public partial class OnshowWorksSubmit : LayoutsPageBase
    {
        #region 控件定义
        protected Label lblWorksName;
        protected Label lblWorksCode;
        protected Label lblWorksType;
        protected Label lblSubmitProfile;
        protected Label lblInstallationGuide;

        protected Label lblComment;
        protected GridView gvComments;
        protected HtmlGenericControl divWorksFile;
        protected Label lblPersons;
        protected HtmlGenericControl divScore;
        protected Button btnSubmit;
        protected TextBox txtComments;
        protected HiddenField HiddenField1;
        protected Label lblDemoURL;
        protected HtmlGenericControl divDesignIdeas;
        protected HtmlGenericControl divViewShow;
        protected HtmlGenericControl divKeyPoints;
        #endregion
        #region 属性
     
        private List<CSWorksExpertUser>  GetDSWorksComments
        {
            get
            {
                if (ViewState["dsWorksComments"] == null)
                {

                    List<CSWorksExpertUser> ds = DAL.Works.GetWorksCommentsByWorksID(WorksID);
                    ViewState["dsWorksComments"] = ds;
                }
                return (List<CSWorksExpertUser>)ViewState["dsWorksComments"];
            }
        }
        private List<Works> GetDSWorksSubmit
        {
            get
            {
                if (ViewState["dsWorksSubmit"] == null)
                {

                    List<Works> ds = DAL.Works.GetWorksSubmitByID(WorksID);
                    ViewState["dsWorksSubmit"] = ds;
                }
                return (List<Works>)ViewState["dsWorksSubmit"];
            }
        }
        //类型2为图片
        private List<WorksFile> GetDSWorksFile
        {
            get
            {
                if (ViewState["dsWorksFile"] == null)
                {
                    List<WorksFile> ds = DAL.Works.GetWorksFile(WorksID, 2);
                    ViewState["dsWorksFile"] = ds;
                }
                return (List<WorksFile>)ViewState["dsWorksFile"];
            }
        }
        private long WorksID
        {
            get
            {
                if (ViewState["worksID"] == null)
                {
                    long worksID = 0;
                    string id = Request.QueryString["WorksID"];
                    if (string.IsNullOrEmpty(id))
                        worksID = 0;
                    else
                        worksID = long.Parse(id);
                    ViewState["worksID"] = worksID;
                }
                return (long)ViewState["worksID"];
            }
        }
        private long ContestID
        {
            get
            {
                if (ViewState["contestID"] == null)
                {
                    long worksID = 0;
                    string id = Request.QueryString["ContestID"];
                    if (string.IsNullOrEmpty(id))
                        worksID = 0;
                    else
                        worksID = long.Parse(id);
                    ViewState["contestID"] = worksID;
                }
                return (long)ViewState["contestID"];
            }
        }
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            if (Convert.ToBoolean(Session["IsSubmit"]))
            {

                //如果表单数据提交成功，就设“Session["IsSubmit"]”为false  

                Session["IsSubmit"] = false;

                //显示提交成功信息  

                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('点评成功');YiComments();</script>");

            }
            if (!Page.IsPostBack)
                InitControl();
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            int score = int.Parse(HiddenField1.Value);
            List<CSWorksExpertUser> dt = GetDSWorksComments;
            CSWorksExpertUser dr = new CSWorksExpertUser();
            dr.WorksID = WorksID;
            dr.UserID =DAL.Common.LoginID;
            dr.Flag = 1;
            dr.Score = score;
            dr.Comments = txtComments.Text;
            dr.Created = DateTime.Now;
            try
            {
                long resultID = DAL.Works.InsertWorksComments(dr);
                if (resultID > 0)
                {
                    ViewState["dsWorksComments"] = null;
                    FillComments();
                    Session["IsSubmit"] = true;
                    Response.Redirect("OnshowWorksSubmit.aspx");
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('点评失败');</script>");

            }
        }
        #endregion
        #region 方法
        private string GetWorksTypeName(int typeID)
        {
            WorksType dr = DAL.WorksType.GetWorksTypeByID(typeID);
            if (dr != null)
                return dr.WorksTypeName;
            else
                return "";
        }
        private void InitControl()
        {
            List<Works>  dsWorks = GetDSWorksSubmit;
            if (dsWorks.Count > 0)
            {
                Works  dr = dsWorks[0];
                lblWorksName.Text = dr.WorksName;
                lblWorksCode.Text = dr.WorksCode;
                lblWorksType.Text =GetWorksTypeName ((int)dr.WorksTypeID);
                try
                {
                    lblSubmitProfile.Text = dr.SubmitProfile;
                    divKeyPoints.Controls.Clear();
                    divKeyPoints.Controls.Add(new LiteralControl(dr.KeyPoints));
                    lblInstallationGuide.Text = dr.InstallationGuide;
                    lblComment.Text = dr.Comment;
                    lblDemoURL.Text = dr.DemoURL;
                    divDesignIdeas.Controls.Clear();
                    divDesignIdeas.Controls.Add(new LiteralControl(dr.DesignIdeas ));
                }
                catch
                {
                }
                FillVideoShow(dr.DemoURL.Trim());
                FillWorksFile();
                FillComments();
            }
        }
        //视频演示（只限tudou sohu sina youku）
        //|| tmpdemoUrl.Contains("sina")
        private void FillVideoShow(string demoUrl)
        {
            string txtVideo = "<embed src=\"http://player.youku.com/player.php/sid/XMzQ4NTI0MzQ0/v.swf\" allowFullScreen=\"true\" quality=\"high\" width=\"480\" height=\"400\" align=\"middle\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\"></embed>";
            string txtVideo1 = "视频无法显示";
            divViewShow.Controls.Clear();
            if (demoUrl.Length > 0)
            {
                string tmpdemoUrl = demoUrl.ToLower();
                int start;
                int end;
                int s1 = 0;
                int s2 = 0;
                string txtType = "tudou";
                try
                {
                    if (tmpdemoUrl.Contains("tudou") || tmpdemoUrl.Contains("sohu") || tmpdemoUrl.Contains("youku"))
                    {
                        if (tmpdemoUrl.Contains("tudou"))
                        {
                            txtType = "tudou";
                            //http://www.tudou.com/programs/view/JccfI6enRlM/
                            txtVideo = "<embed src=\"http://www.tudou.com/v/XMzQ4NTI0MzQ0/&resourceId=0_05_05_99/v.swf\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" wmode=\"opaque\" width=\"480\" height=\"400\"></embed>";
                            txtVideo1 = GetDemoUrl(tmpdemoUrl, demoUrl, txtType);
                            if (txtVideo1.Contains("/?"))
                            {
                                txtVideo1 = txtVideo1.Substring(0, txtVideo1.IndexOf("/?") + 1);
                            }
                            if (txtVideo1.EndsWith(".html"))
                            {
                                txtVideo = "<embed src=\"http://www.tudou.com/l/XMzQ4NTI0MzQ0/&rpid=111905378&resourceId=111905378_05_05_99&iid=140062282/v.swf\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" wmode=\"opaque\" width=\"480\" height=\"400\"></embed>";
                                txtVideo1 = txtVideo1.Substring(0, txtVideo1.LastIndexOf("/"));
                            }

                            if (!txtVideo1.EndsWith("/"))
                                txtVideo1 = txtVideo1 + "/";
                            s1 = txtVideo1.LastIndexOf("/", txtVideo1.Length - 2);
                            s2 = txtVideo1.Length - 2;
                        }
                        else if (tmpdemoUrl.Contains("sohu"))
                        {
                            txtType = "sohu";
                            //http://my.tv.sohu.com/u/vw/20027857
                            txtVideo = "<object width= height=506><param name=\"movie\" value=\"http://share.vrs.sohu.com/my/v.swf&topBar=1&id=XMzQ4NTI0MzQ0&autoplay=false\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><param name=\"wmode\" value=\"Transparent\"></param><embed width=640 height=506 wmode=\"Transparent\" allowfullscreen=\"true\" allowscriptaccess=\"always\" quality=\"high\" src=\"http://share.vrs.sohu.com/my/v.swf&topBar=1&id=XMzQ4NTI0MzQ0&autoplay=false\" type=\"application/x-shockwave-flash\"/></embed></object>";
                            txtVideo1 = GetDemoUrl(tmpdemoUrl, demoUrl, txtType);
                            s1 = txtVideo1.LastIndexOf("/");
                            s2 = txtVideo1.Length - 1;
                        }
                        else
                        {
                            txtType = "youku";
                            //http://v.youku.com/v_show/id_XMzkzODUzODA0.html
                            txtVideo1 = GetDemoUrl(tmpdemoUrl, demoUrl, txtType);
                            s1 = txtVideo1.IndexOf("id_") + 2;
                            s2 = txtVideo1.LastIndexOf(".html") - 1;
                        }

                        txtVideo1 = txtVideo1.Substring(s1 + 1, s2 - s1);
                        divViewShow.Controls.Add(new LiteralControl(txtVideo.Replace("XMzQ4NTI0MzQ0", txtVideo1)));
                        return;
                    }
                }
                catch
                {

                }
                divViewShow.Controls.Add(new LiteralControl(txtVideo1));
            }

        }
        private string GetDemoUrl(string tmpdemoUrl, string demoUrl, string txtType)
        {
            int start = tmpdemoUrl.LastIndexOf("http:", tmpdemoUrl.IndexOf(txtType));
            int end = tmpdemoUrl.IndexOf(";", start + 1);
            if (end < 0)
                end = tmpdemoUrl.IndexOf(" ", start + 1);
            if (end < 0)
                end = tmpdemoUrl.Length;
            string txtVideo1 = demoUrl.Substring(start, end - start).Trim();
            return txtVideo1;

        }
        //作品展示图
        private void FillWorksFile()
        {
            List<WorksFile>  ds = GetDSWorksFile;
            StringBuilder txtContent = new StringBuilder();
            foreach (WorksFile  dr in ds )
            {
                txtContent.AppendLine("<img src='" + dr.FilePath + "' width='500px'/><br />" + dr.FileName);
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
            List<CSWorksExpertUser>  ds = GetDSWorksComments;
            List<CSWorksExpertUser> dv = ds.Where(p => p.UserID == DAL.Common.LoginID).ToList();
            if (dv.Count > 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>YiComments();</script>");

            lblPersons.Text = ds.Count.ToString();
            gvComments.DataSource = dv;
            gvComments.DataBind();
            float avg = 0;
            int tCount = 0;
            foreach (CSWorksExpertUser dr in ds)
            {
                if (dr.Score.HasValue && dr.Score > 0)
                {
                    avg = avg + (float)dr.Score;
                    tCount = tCount + 1;
                }
            }
            if (tCount >0)
                avg = avg /tCount ;
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
    }
}
