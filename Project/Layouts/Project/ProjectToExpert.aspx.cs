using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ProjectDll;

namespace Project.Layouts.Project
{
    public partial class ProjectToExpert : LayoutsPageBase
    {
        readonly long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGvMyPubProject(currentUserId, gvToMy);  //我发布的项目
        }

        private void BindGvMyPubProject(long expertId, GridView gv)
        {
            List<Proj_ToExpertProj> dt = ProjectDll.BLL.ProjectBll.GetProjectsToExpertByExpertId(expertId);
            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void gvToMy_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvToMy.PageIndex = e.NewPageIndex;
            BindGvMyPubProject(currentUserId, gvToMy);
        }
    }
}
