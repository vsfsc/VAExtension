using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ProjectDll;

namespace Project.Layouts.Project
{
    public partial class AllProjects : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            const int stateId = 5;
            BindGvMyPubProject(stateId, gvPublicProject);  //我发布的项目
        }

        private void BindGvMyPubProject(int stateId, GridView gv)
        {
            List<Proj_ProjectState> dt = ProjectDll.BLL.ProjectBll.GetProjectStates(stateId);
            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void gvPublicProject_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            const int stateId = 5;
            gvPublicProject.PageIndex = e.NewPageIndex;
            BindGvMyPubProject(stateId, gvPublicProject);
        }
    }
}
