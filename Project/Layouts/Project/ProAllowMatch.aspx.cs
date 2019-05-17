using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using System.Web.UI.WebControls;
using ProjectDll;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;

namespace Project.Layouts.Project
{
    public partial class ProAllowMatch : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGvAllMatchProjects(1, gvAllowMatch);   //获取可进行对接的项目信息，IsMatch=1,代表等待进行对接
                BindGvMatchInfo(gvMatchInfo); //获取已经申请对接的信息
            }
        }
        private void BindGvAllMatchProjects(int IsMatch,GridView gv)
        {
            List<Proj_ProjectState> dt = ProjectDll.BLL.ProjectBll.GetMatchProjects(IsMatch); //筛选出可对接的项目，IsMacth=1--待对接
            long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
            dt = dt.Where(ps => ps.Sponsor != currentUserId).ToList();  //再次筛选出除了自己的项目可对接的项目
            gv.DataSource = dt;
            gv.DataBind();
        }
        private void BindGvMatchInfo(GridView gv)
        {
            long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
            List<Proj_MatchResult> dt = ProjectDll.BLL.ProjectBll.GetMyProjMatchInfo(currentUserId);
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gvAllowMatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllowMatch.PageIndex = e.NewPageIndex;
            BindGvAllMatchProjects(1, gvAllowMatch);
        }

        protected void gvAllowMatch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
                string strProID = DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString();
                long ProID = Convert.ToInt64(strProID);
                List<Proj_Match> dt = ProjectDll.BLL.ProjectBll.GetProjectsrMatchByUser(currentUserId);
                foreach (var item in dt)
                {
                    if (item.ProjectID == ProID)
                    {
                        //HyperLink elink = (HyperLink)e.Row.FindControl("PNameLink");                       
                        //elink.NavigateUrl = String.Format("PDetails.aspx?ProjectID=" + strProID + "&pageTypeId=0");                      
                        //e.Row.Cells[6].ForeColor = System.Drawing.Color.LightSteelBlue;
                        //e.Row.Cells[6].Enabled = false;
                        //e.Row.Cells[6].Text = "已申请";
                        e.Row.Visible = false;                      
                    }                                    
                }
            }
        }

        protected void gvMatchInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string matchState = e.Row.Cells[6].Text;
                int state = Int32.Parse(matchState);
                if (state != 1)
                {
                    e.Row.Cells[3].Text = "--";
                    e.Row.Cells[4].Text = "--";
                }
                switch (state)
                {
                    case 0: { e.Row.Cells[6].Text = "等待对方回应"; } break;
                    case 1: { e.Row.Cells[6].Text = "接受对接"; } break;
                    case -1: { e.Row.Cells[6].Text = "拒绝对接"; } break;
                }
            }
        }
    }
}
