using System;
using System.Data;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Linq;
using System.Collections;
using System.Web.UI.WebControls;
using ProjectDll;
using System.Collections.Generic;
namespace Project.Layouts.Project
{
    public partial class MyProjects : LayoutsPageBase
    {
        //readonly long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
        private long CurrentUserId
        {
            get
            {
                if (ViewState["UserID"]==null )
                    ViewState["UserID"] = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
                return (long)ViewState["UserID"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGvMyPubProject(CurrentUserId, gvMyPub);  //我发布的项目
                BindGvMyJoinProject(CurrentUserId, gvMyJoin);  //我参与的项目
            }
        }

        private void BindGvMyPubProject(long userId,GridView gv)
        {
            List<Proj_ProjectsCreatedByUser> dt = ProjectDll.BLL.ProjectBll.GetProjectsCreatedByUserId(userId).OrderByDescending(p=>p.CreatedDate).ToList();
            gv.DataSource = dt;
            gv.DataKeyNames = new string[] { "ProjectID" }; 
            gv.DataBind();
        }
        private void BindGvMyJoinProject(long userId, GridView gv)
        {
            List<Proj_ProjectsIAmINorOut> dt = ProjectDll.BLL.ProjectBll.GetProjectsUserINorOut(userId).OrderByDescending(j=>j.ApplyInTime).ToList();
            gv.DataSource = dt;
            gv.DataKeyNames = new string[] {"ProjectID" }; 
            gv.DataBind();
        }

        protected void gvMyPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
            gvMyPub.PageIndex = e.NewPageIndex;
            BindGvMyPubProject(currentUserId, gvMyPub);
        }

        protected void gvMyJoin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            long currentUserId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);
            gvMyJoin.PageIndex = e.NewPageIndex;
            BindGvMyJoinProject(currentUserId, gvMyJoin);
        }

        #region 我发布的项目GridView行事件        
        /// <summary>
        /// Handles the OnRowDataBound event of the gvMyPub control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gvMyPub_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            const string listTitle = "项目文档";
            if (e.Row.RowType == DataControlRowType.DataRow)//数据行
            {
                string strIsMatch = DataBinder.Eval(e.Row.DataItem, "IsMatch").ToString();
                int proIsMatch = 0;
                if (strIsMatch!="")
                {
                    proIsMatch = Convert.ToInt32(strIsMatch);
                }
                if (ViewState["gvPubEdit"] != null)
                {
                    if (e.Row.RowIndex == (int)ViewState["gvPubEdit"])
                    {

                        var list = (DropDownList)e.Row.Cells[8].Controls[1]; //FindControl("ddProIsMach");
                        list.SelectedValue = proIsMatch.ToString();
                    }
                    else
                    {
                        var lbIsMach = (Label)e.Row.Cells[8].Controls[1];// e.Row.FindControl("lbIsMach");
                        switch (proIsMatch)
                        {
                            case 0:
                                lbIsMach.Text = "拒绝对接";
                                break;
                            case 1:
                                lbIsMach.Text = "等待对接";
                                break;
                            default:
                                lbIsMach.Text = "对接完成";
                                break;
                        }
                    }
                }
                else
                {
                    var lbIsMach = (Label)e.Row.Cells[8].Controls[1];// e.Row.FindControl("lbIsMach");
                    switch (proIsMatch)
                    {
                        case 0:
                            lbIsMach.Text = "拒绝对接";
                            break;
                        case 1:
                            lbIsMach.Text = "等待对接";
                            break;
                        default:
                            lbIsMach.Text = "对接完成";
                            break;
                    }

                }

                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C6E2FF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");
                 
                //string projName = e.Row.Cells[1].Text;//项目名即子网站名称
                string webTitle = DataBinder.Eval(e.Row.DataItem, "PName").ToString();
                //int flag = 0;
                var elink = (HyperLink)e.Row.FindControl("DocManagement");
                if (!SPContext.Current.Web.Webs[webTitle].Exists)
                {
                    elink.Text = "项目未审批";
                    elink.Enabled = false;
                }
                else
                {
                    SPWeb childweb = SPContext.Current.Web.Webs[webTitle];
                    if (childweb.Lists.Cast<SPList>().Any(list => list.Title == listTitle))
                    {
                        string url = SPContext.Current.Web.Webs[webTitle].Lists[listTitle].DefaultViewUrl;
                        elink.NavigateUrl = String.Format(url);
                    }
                }
                //保持列不变形
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    //方法一：
                    //e.Row.Cells[i].Text = "&nbsp;" + e.Row.Cells[i].Text + "&nbsp;";
                    e.Row.Cells[i].Wrap = false;
                    //方法二：
                    //e.Row.Cells[i].Text = "<nobr>&nbsp;" + e.Row.Cells[i].Text + "&nbsp;</nobr>";            
                }   
            }
        }

        /// <summary>
        /// Handles the OnRowCancelingEdit event of the gvMyPub control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected void gvMyPub_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ViewState["gvPubEdit"] = null;
            gvMyPub.EditIndex = -1;
            BindGvMyPubProject(CurrentUserId, gvMyPub);  //我发布的项目
        }
        /// <summary>
        /// Handles the OnRowEditing event of the gvMyPub control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected void gvMyPub_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            ViewState["gvPubEdit"] = e.NewEditIndex; 
            gvMyPub.EditIndex = e.NewEditIndex;
            BindGvMyPubProject(CurrentUserId, gvMyPub);  //我发布的项目
        }
        /// <summary>
        /// Handles the OnRowUpdating event of the gvMyPub control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewUpdateEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected void gvMyPub_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var dataKey = gvMyPub.DataKeys[e.RowIndex];
            if (dataKey != null)
            {
                var projectId = (long)dataKey.Value;
                List<Proj_Project> projectDt = ProjectDll.BLL.ProjectBll.GetProjectByID(projectId);
                Proj_Project dr = projectDt[0];

                var ddlIsMatch = (DropDownList)gvMyPub.Rows[e.RowIndex].FindControl("ddlProIsMach");
                dr.IsMatch = int.Parse(ddlIsMatch.SelectedValue);
                ProjectDll.DAL.ProjectDal.UpdateProjectById(dr);
                
            }
            ViewState["gvPubEdit"] = null;
            gvMyPub.EditIndex = -1;
            BindGvMyPubProject(CurrentUserId, gvMyPub);
        }
        #endregion
    }
}
