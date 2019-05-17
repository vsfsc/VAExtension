//所有竞赛项目页面后台
using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using ContestDll;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;

namespace ContestDll
{
    public partial class Contests : LayoutsPageBase
    {
        #region 控件定义 
        protected GridView gvContests;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {                
                BindAllPeriods(gvContests);
            }
        }
        private void BindAllPeriods(GridView gv)
        {
            long courseId = BLL.Course.GetContestWebID();
            List<CSPeriodsCourse> dt = BLL.Period.GetPeridsByCourseId(courseId).OrderByDescending(ps => ps.Created).ToList();
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gvPeriods_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContests.PageIndex = e.NewPageIndex;
            BindAllPeriods(gvContests);
        }

        /// <summary>
        /// 绑定比赛列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPeriods_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // bool endTag = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long currentUserId = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                string strPID = DataBinder.Eval(e.Row.DataItem, "PeriodID").ToString();
                Label lbPState = (Label)e.Row.FindControl("lbState");
                lbPState.Text = BLL.Period.GetPeridState(Convert.ToInt64(strPID), DateTime.Now);
                lbPState.ForeColor = System.Drawing.Color.DodgerBlue;

                HyperLink pHyperLink = (HyperLink)e.Row.FindControl("PNameLink");
                pHyperLink.Text = DataBinder.Eval(e.Row.DataItem, "PeriodTitle").ToString();
                
                //SPSite currentSite=SPContext.Current.Site;
                SPWeb currentWeb = SPContext.Current.Web;
                string periodSiteUrl=currentWeb.Url+"/"+DataBinder.Eval(e.Row.DataItem, "PeriodCode").ToString();
                pHyperLink.NavigateUrl = periodSiteUrl;
                //if (DAL.Common.UrlCheck(periodSiteUrl))
                //{
                    pHyperLink.ForeColor = System.Drawing.Color.RoyalBlue;
                    
                    //pHyperLink.Target = "_blank";
                    pHyperLink.Attributes.Add("onmouseover ", "this.style.color= 'Red ' ");
                    pHyperLink.Attributes.Add("onmouseout ", "this.style.color= 'blue ' ");
                //}
                //else
                //{
                //    pHyperLink.ForeColor = System.Drawing.Color.SlateGray;
                //    pHyperLink.Enabled = false;
                    
                //}
                
            }
        }

    }
}
