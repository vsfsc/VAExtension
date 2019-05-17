using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using System.Web.UI.WebControls;
using ProjectDll;
using System.Collections.Generic;

namespace Project.Layouts.Project
{
    public partial class PApprove : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGvDate(1, gvWait);  //待审批项目
                BindGvDate(3, gvPass);  //已审批项目
                BindGvDate(2, gvBack);  //已驳回项目
            }
        }
        private void BindGvDate(int stateId,GridView gv)
        {
            List<Proj_ProjectState> dt = ProjectDll.BLL.ProjectBll.GetProjectStates(stateId);
            gv.DataSource = dt;
            gv.DataBind();
        }
        private void BindGridView(IEnumerable dt, GridView gv)
        {
            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void gvWait_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWait.PageIndex = e.NewPageIndex;
            BindGvDate(1, gvWait);
        }

        protected void gvPass_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWait.PageIndex = e.NewPageIndex;
            BindGvDate(3, gvPass);
        }

        protected void gvBack_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWait.PageIndex = e.NewPageIndex;
            BindGvDate(2, gvBack);
        }
     
    }
}
