using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ContestDll;

namespace ContestWeb.Layouts.ContestWeb
{
    public partial class PDetails : LayoutsPageBase
    {
        private long PeriodId
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["PeriodID"]))
                {
                    return long.Parse(Request.QueryString["PeriodID"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showDetails();
            }
            
        }
        private void showDetails()
        {
            long pid = PeriodId;
            
            //if (pid== ContestDll.BLL.Course.GetCourseID())
            //{
            //}
            CSPeriodsCourse pd = ContestDll.BLL.Period.GetPeridsByPId(pid);
            if (pd == null)
            {
                Response.Write("<script>alert('你要浏览的比赛不存在！');location.href='Contests.aspx';</script>");
            }
            else
            {
                PTitle.InnerText = pd.PeriodTitle;
                lbContestTitle.Text = pd.PeriodTitle;
                PCourseTitle.InnerText=pd.CourseName;
                PPublisher.InnerText = pd.UserName;
                string pstate = ContestDll.BLL.Period.GetPeridState(PeriodId, DateTime.Now);
                if (pstate != "报名提交期")
                {
                    SignupLink.Visible = false;
                }
                SignupLink.PostBackUrl = "OnlineEnroll.aspx?PeriodID=" + PeriodId;
                PState.InnerText =pstate;
                PSaizhi.InnerText = "单人赛";
                lbforCount.InnerText = "参赛人数：";
                if (pd.Number>1)
                {
                    PSaizhi.InnerHtml = "团体赛   &nbsp;  团队人数限制 <b style='color:red;'>≤" + pd.Number+" </b> 人";
                    lbforCount.InnerText = "参赛团队数：";
                }
                PType.InnerHtml = pd.WorksTypeName;
                PMemberCount.InnerText = pd.WorksCount.ToString();
                string PTimeSets = "<span style='color:green;padding-top:20px;'>报名阶段：" + String.Format("{0:yyyy-MM-dd}", pd.StartSubmit)+ "&nbsp;00:00&nbsp;------&nbsp;" + String.Format("{0:yyyy-MM-dd}", pd.EndSubmit)+ "&nbsp;23:59</span><br/>";
                PTimeSets += "<span style='color:red;padding-top:10px;'>评比阶段：" + String.Format("{0:yyyy-MM-dd}", pd.StartScore ) + "&nbsp;00:00&nbsp;------&nbsp;" + String.Format("{0:yyyy-MM-dd}", pd.EndScore) + " &nbsp;23:59</span><br/>";
                PTimeSets += "<span style='color:blue;padding-top:10px;'>公示阶段：" + String.Format("{0:yyyy-MM-dd}", pd.StartPublic) + "&nbsp;00:00&nbsp;------&nbsp;" + String.Format("{0:yyyy-MM-dd}", pd.EndPublic )+ "&nbsp;23:59</span><br/>";
                PTimeSheet.InnerHtml = PTimeSets;
                PReq.InnerHtml = pd.Require;
            }
        }
    }
}
