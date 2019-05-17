using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using System.Web.UI.WebControls;
using ProjectDll;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;

namespace Project.Layouts.Project
{
    public partial class Projects : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //gvProjects.RowDataBound += gvProjects_RowDataBound;
            if (!IsPostBack)
            {
                BindGvApplyJoinProjects(3, gvProjects);
                
            }
        }
        private void BindGvApplyJoinProjects(int stateId, GridView gv)
        {
            List<Proj_ProjectState> dt = ProjectDll.BLL.ProjectBll.GetProjectStates(stateId).OrderByDescending(ps => ps.CreatedDate).ToList();
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gvProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProjects.PageIndex = e.NewPageIndex;
            BindGvApplyJoinProjects(3, gvProjects);
        }

        /// <summary>
        /// 处理申请加入项目，对于已经加入项目不能再加入，跳转到项目子网站；否则申请加入项目进入申请页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvProjects_RowDataBound(object sender, GridViewRowEventArgs e)  
        {
            // bool endTag = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
                // long ProID = Convert.ToInt64(e.Row.Cells[0].Text);
                string strProID = DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString();
                long ProID = Convert.ToInt64(strProID);
                List<Proj_ProjectsIAmINorOut> dt = ProjectDll.BLL.ProjectBll.GetProjectsUserINorOut(currentUserId);
                foreach (var item in dt)
                {
                    //    if (endTag)
                    //      break;
                    if (item.ProjectID == ProID)
                    {
                        HyperLink elink = (HyperLink)e.Row.FindControl("PNameLink");
                        //elink.Text = e.Row.Cells[1].Text;
                        elink.NavigateUrl = String.Format("PDetails.aspx?ProjectID=" + e.Row.Cells[0].Text + "&pageTypeId=0");
                        // e.Row.Cells[1].Attributes["onclick"]= String.Format("PDetails.aspx?ProjectID="+ e.Row.Cells[0].Text + "&pageTypeId=2");
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.LightSteelBlue;
                        e.Row.Cells[6].Enabled = false;
                        e.Row.Cells[6].Text = "已加入";
                        //ProIntroduce.Enabled = false;
                        //       endTag = true;
                    }
                }
            }
        }
    }
}
