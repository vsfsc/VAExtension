using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Collections;


namespace ContestDll
{
    public class Score : LayoutsPageBase
    {
        #region 控件定义
        protected Label lblWorksNum;
        protected Label lblWorksName;
        protected Label ddlWorksType;
        protected HtmlGenericControl divContent;
        protected DropDownList ddlNumber;
        protected HtmlGenericControl lblTotalScore;
        protected HiddenField HiddenField1;
        protected Button btnSubmit;
        #endregion
        #region  属性
        private List<CSFinalsExpertWillScore> GetCurrentWorks
        {
            get
            {
                List<CSFinalsExpertWillScore> ds = DAL.Works.GetFinalsExpertWillScore(GroupID, DAL.Common.LoginID);
                return ds;
            }
        }

        private List<FinalsScoreStandard> GetFinalsScoreStandard
        {
            get
            {
                List<FinalsScoreStandard> ds = DAL.Works.GetFinalsScoreStandard();
                return ds;
            }
        }
       
        private int GroupID
        {
            get
            {
                if (ViewState["groupID"] == null)
                {
                    List<CSFinalsExpertGroup> dt = DAL.Works.GetExpertGroup(DAL.Common.GetContestID(Page) );
                    long loginID = DAL.Common.LoginID ;
                    List<CSFinalsExpertGroup> drs = dt.Where(p => p.ExpertID == DAL.Common.LoginID).ToList();
                    int groupID = 0;
                    if (drs.Count > 0)
                        groupID = (int)drs[0].GroupID;
                    ViewState["groupID"] = groupID;

                }
                return int.Parse(ViewState["groupID"].ToString());
            }

        }
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            //可以在页面加载时设置页面的缓存为“SetNoStore()”，即无缓存  

            Response.Cache.SetNoStore();

            //Session中存储的变量“IsSubmit”是标记是否提交成功的  

            if (Convert.ToBoolean(Session["IsSubmit"]))
            {
                //如果表单数据提交成功，就设“Session["IsSubmit"]”为false  

                Session["IsSubmit"] = false;

                //显示提交成功信息  


            }
            if (!Page.IsPostBack)
            {
                string url = DAL.Common.SPWeb.Url ;
                try
                {
                    
                    //专家评分操作
                    if (!DAL.Common.IsWebAdmin && ! DAL.Common.LoginRole.Contains(3))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('没有权限');top.location.href='" + url + "'</script>");
                        return;
                    }
                }
                catch
                {
                }
                List<CSFinalsExpertWillScore> dt = GetCurrentWorks;
                if (dt.Count  == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('评分已经结束');top.location.href='" + url + "'</script>");
                    return;

                }
                ddlNumber.DataSource = dt;
                ddlNumber.DataTextField = "Number";
                ddlNumber.DataValueField = "WorksID";
                ddlNumber.DataBind();
                ddlNumber_SelectedIndexChanged(null, null);
            }
            ddlNumber.SelectedIndexChanged += new EventHandler(ddlNumber_SelectedIndexChanged);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            if (HiddenField1.Value != "")
                lblTotalScore.InnerText = HiddenField1.Value;
            InitControl();
        }

        void ddlNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNumber.SelectedValue == "") return;
            CSWorksWorksType dr = DAL.Works.GetWorksByWorksID(long.Parse(ddlNumber.SelectedValue));
            lblWorksName.Text = dr.WorksName ;
            lblWorksNum.Text = dr.WorksCode ;
            ddlWorksType.Text = dr.WorksTypeName ;
        }
        //刷新会提交两次数据
        void btnSubmit_Click(object sender, EventArgs e)

        {
            int total = int.Parse(HiddenField1.Value);
            if (total == 0) return;
            bool result = BLL.Works.InsertWorksExpert(long.Parse (ddlNumber.SelectedValue), new List<long> { DAL.Common.LoginID }, new object[] { total, 3 });
            if (result)
            {
                ddlNumber.Items.RemoveAt(ddlNumber.SelectedIndex);
                HiddenField1.Value = "0";
                Session["IsSubmit"] = true;

                Response.Redirect("Score.aspx");
            }
            else
                DAL.Common.ShowMessage(Page, this.GetType(), "提交失败！");
        }
        #endregion
        #region 方法
        private void InitControl()
        {
            divContent.Controls.Clear();
            StringBuilder txt;
            List<FinalsScoreStandard> dsStandard = GetFinalsScoreStandard;
            HtmlInputText txtScore;
            foreach (FinalsScoreStandard dr in dsStandard)
            {
                txt = new StringBuilder();
                txt.AppendLine("<div class=\"mainDiv\"><div class=\"fl\" style=\"margin-left:150px;width:500px\"><p class=\"ptitle\">");
                txt.AppendLine(dr.StandardName  + "（" + dr.Score.ToString() + "）");
                txt.AppendLine("</p><p class=\"pin\" >");
                txt.AppendLine(dr.Description);
                txt.AppendLine("</p></div>");
                divContent.Controls.Add(new LiteralControl(txt.ToString()));
                txtScore = new HtmlInputText();
                txtScore.ID = "txtFen" + dr.StandardID + "_" + dr.Score;
                txtScore.Attributes.Add("class", "keyboardInput mt h30");
                txtScore.Attributes.Add("onchange", "CaculateScore()");
                //<input type=\"text\" value=\"\" =\\" />);
                divContent.Controls.Add(txtScore);
                divContent.Controls.Add(new LiteralControl("</div><div style=\"height:20px;\">&nbsp;</div>"));
            }
        }
        #endregion
    }
}
