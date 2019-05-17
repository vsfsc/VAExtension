using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProjectDll;
using System.Web.UI;

namespace Project.Layouts.Project
{
    public partial class RoleApprove : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridView(gvRolesApproving);
            BindGridView(gvRolesApproved);
            //if (!IsPostBack)
            //{
            //    BindGridView(gvRolesApproving);
            //    BindGridView(gvRolesApproved);
            //}
        }

        private static void BindGridView(GridView gv)
        {
            long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
            string gvId = gv.ID;

            //lbGvTitle.Text = gvId;
            if (gvId == "gvRolesApproving")//待审批的用户角色
            {
                List<Proj_VUserRoles> myRolesList = ProjectDll.BLL.User.GetUserRolesByStateId(0).ToList();//当前用户已申请切被审批通过的角色列表（-1已拒绝，0待审批，1已通过）
                gv.DataSource = myRolesList;
                gv.DataBind();
            }
            else if (gvId == "gvRolesApproved")//当前用户（管理员）审批的用户
            {
                List<Proj_VUserRoles> appovingRolesList = ProjectDll.BLL.User.GetUserRolesByUserId(1).Where(r => r.ApprovedBy ==userId).ToList();//当前用户已申请切被审批通过的角色列表（-1已拒绝，0待审批，1已通过）

                gv.DataSource = appovingRolesList;
                gv.DataBind();
            }
            else//当前管理员分配的新管理员
            {
                
            }
        }
        void gvRolesApproving_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
            var dr = new UserRole();
            GridView gvr=(GridView)((Control)e.CommandSource).Parent.Parent;
            if (e.CommandName == "RolePass")
            {

                dr.StateID = 1;
            }
            else if (e.CommandName == "RoleNoPass")
            {
                dr.StateID = -1;
            }
            dr.UserRoleID = long.Parse(e.CommandArgument.ToString());
            dr.Approved = DateTime.Now;
            dr.ApprovedBy = userId;
            dr.Flag = 1;
            ProjectDll.DAL.User.ApproveNewRole(dr);
            //BindGvData(gvRolesApproving);
            //BindGvData(gvApprovingRoles);
            //BindGvData(gvNewRoles);
        }
        protected void btnPass_Click(object sender, EventArgs e)
        {
            string rowIndex = (String)((Button)sender).CommandArgument;
            ProjectDll.DAL.Common.Alert(rowIndex);
        }

     
    }
}
