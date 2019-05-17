using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using ProjectDll;

namespace Project.Layouts.Project
{
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public partial class Test : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    BindGvData(gvMyRoles);
            //    BindGvData(gvApprovingRoles);                
            //}
            ApprovingRolesDiv.Controls.Clear();
            BindMyApplyingRoles();
            MyRolesDiv.Controls.Clear();
            BindMyRoles();
            NewRolesDiv.Controls.Clear();
            BindNewRole();
        }
        //private void BindGvData(GridView gv)
        //{
        //    long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
        //    string gvId = gv.ID;
        //    //lbGvTitle.Text = gvId;
        //    if (gvId == "gvMyRoles")//已批准
        //    {
        //        List<Proj_VUserRoles> myRolesList = ProjectDll.BLL.User.GetUserRolesByUserId(userId).Where(r => r.StateID == 1).ToList();//当前用户已申请切被审批通过的角色列表（-1已拒绝，0待审批，1已通过）
        //        gv.DataSource = myRolesList;
        //        gv.DataBind();
        //    }
        //    else if (gvId == "gvApprovingRoles")//审批中
        //    {
        //        List<Proj_VUserRoles> appovingRolesList = ProjectDll.BLL.User.GetUserRolesByUserId(userId).Where(r => r.StateID == 0).ToList();//当前用户已申请切被审批通过的角色列表（-1已拒绝，0待审批，1已通过）

        //        gv.DataSource = appovingRolesList;
        //        gv.DataBind();
        //    }
           
        //}
        private static DataTable DiffArrays(List<Proj_VUserRoles> myRList, List<Role> allRList)
        {
            List<int> list = new List<int>();
            int[] myRoleIds = myRList.Select(ur => ur.RoleID).ToArray();
            DataTable dt = new DataTable("RolesTable");
            dt.Columns.Add("RoleID", type: Type.GetType("System.Int32"));
            dt.Columns.Add("RoleName", type: Type.GetType("System.String"));
            DataColumn[] dc = { dt.Columns["RoleID"]};
            dt.PrimaryKey = dc;

            foreach (var aRole in allRList)
            {
                int roleId = aRole.RoleID;
                if (((IList)myRoleIds).Contains(roleId))
                {
                    continue;
                }
                DataRow dr = dt.NewRow();
                dr["RoleID"] = roleId;
                dr["RoleName"] = aRole.RoleName;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //void gvNewRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
        //    if (e.CommandName == "cmdApply")
        //    {
        //        GridViewRow gvr = ((Control)e.CommandSource).BindingContainer as GridViewRow;
        //        if (gvr != null)
        //        {
        //            //var dataKey = gvNewRoles.DataKeys[gvr.RowIndex];
        //            //if (dataKey != null)
        //            //{
        //            //    int roleId = int.Parse(dataKey.Value.ToString());//  DataBinder.Eval(gvMyProjMatch.Rows[gvr.RowIndex].DataItem, "MatchID").ToString();
        //            //    var dr = new UserRole();
        //            //    dr.UserID = userId;
        //            //    dr.RoleID = roleId;
        //            //    dr.Created = DateTime.Now;
        //            //    dr.CreatedBy = userId;
        //            //    dr.StateID = 0;
        //            //    dr.Flag = 1;
        //            //    ProjectDll.DAL.User.ApplyNewRole(dr);
        //            //}
        //            string matchID = gvNewRoles.DataKeys[gvr.RowIndex].Value.ToString();
        //            //int roleId = int.Parse(DataBinder.Eval(gvNewRoles.Rows[gvr.RowIndex].DataItem, "RoleID").ToString());
        //            int roleId = int.Parse(matchID);
        //            var dr = new UserRole();
        //            dr.UserID = userId;
        //            dr.RoleID = roleId;
        //            dr.Created = DateTime.Now;
        //            dr.CreatedBy = userId;
        //            dr.StateID = 0;
        //            dr.Flag = 1;
        //            ProjectDll.DAL.User.ApplyNewRole(dr);
        //        }
               
        //        BindGvData(gvMyRoles);
        //        BindGvData(gvApprovingRoles);
        //        BindGvData(gvNewRoles);
        //    }
        //}
        private void BindNewRole()//可申请角色
        {
            
            long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
            List<Proj_VUserRoles> myRList = ProjectDll.BLL.User.GetUserRolesByUserId(userId).OrderBy(ur => ur.RoleID).ToList();//我已申请的角色
            List<Role> allRList = ProjectDll.BLL.User.GetRolesBySystemId(3).OrderBy(ur => ur.RoleID).ToList();//获取项目管理系统中的所有用户角色

            int[] myRoleIds = myRList.Select(ur => ur.RoleID).ToArray();
            int aIndex = 0;
            foreach (var aRole in allRList)
            {
                int roleId = aRole.RoleID;
                if (((IList)myRoleIds).Contains(roleId))
                {
                    continue;
                }
                HtmlContainerControl roleDiv;
                roleDiv=new HtmlGenericControl("div");
                roleDiv.ID = "RoleDiv" + aIndex.ToString();
                Button btnApplyNewRole = new Button();
                btnApplyNewRole.ToolTip = "申请该角色";
                btnApplyNewRole.Text = aRole.RoleName;
                btnApplyNewRole.CommandArgument = roleId.ToString();
                btnApplyNewRole.ID = "btnApplyNewRole" + roleId.ToString();
                btnApplyNewRole.Click += btnApplyNewRole_Click;

                btnApplyNewRole.Attributes.Add("class", "btnRole");
                roleDiv.Controls.Add(btnApplyNewRole);
                NewRolesDiv.Controls.Add(roleDiv);
                aIndex = aIndex + 1;
            }           
        }
        private void BindMyRoles()//已获得角色
        {
            
            long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
            List<Proj_VUserRoles> appovedRolesList = ProjectDll.BLL.User.GetUserRolesByUserId(userId).Where(r => r.StateID == 1).ToList();//当前用户已申请且审批通过的角色列表（-1已拒绝，0待审批，1已通过）
            int aIndex = 0;
            string htmlContent = "";
            foreach (var aRole in appovedRolesList)
            {
                int roleId = aRole.RoleID;              
                HtmlContainerControl roleDiv;
                roleDiv = new HtmlGenericControl("div");
                roleDiv.ID = "RoleDiv" + aIndex.ToString();
                htmlContent = "<span class='myspan'>" + aRole.RoleName + "</span>";
                roleDiv.Controls.Add(new LiteralControl(htmlContent));                
                MyRolesDiv.Controls.Add(roleDiv);
                aIndex = aIndex + 1;
            }
        }
        private void BindMyApplyingRoles()//待审批角色
        {
           
            long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
            List<Proj_VUserRoles> appovingRolesList = ProjectDll.BLL.User.GetUserRolesByUserId(userId).Where(r => r.StateID == 0).ToList();//当前用户已申请且等待审批的角色列表（-1已拒绝，0待审批，1已通过）
            int aIndex = 0;
            string htmlContent = "";
            foreach (var aRole in appovingRolesList)
            {
                int roleId = aRole.RoleID;
                HtmlContainerControl roleDiv;
                roleDiv = new HtmlGenericControl("div");
                roleDiv.ID = "RoleDiv" + aIndex.ToString();
                htmlContent = "<span class='applyspan'>" + aRole.RoleName + "</span>";
                roleDiv.Controls.Add(new LiteralControl(htmlContent));
                ApprovingRolesDiv.Controls.Add(roleDiv);
                aIndex = aIndex + 1;
            }
        }
        void btnApplyNewRole_Click(object sender, EventArgs e)
        {
            long userId = ProjectDll.BLL.User.GetUserId(SPContext.Current.Web.CurrentUser);//当前用户Id
            Button btn = (Button) sender;
            string roleId = HttpUtility.UrlEncode(btn.CommandArgument);
            var dr = new UserRole();
            dr.UserID = userId;
            dr.RoleID = int.Parse(roleId);
            dr.Created = DateTime.Now;
            dr.CreatedBy = userId;
            dr.StateID = 0;
            dr.Flag = 1;
            ProjectDll.DAL.User.ApplyNewRole(dr);
            
            ApprovingRolesDiv.Controls.Clear();
            BindMyApplyingRoles();
            MyRolesDiv.Controls.Clear();
            BindMyRoles();
            NewRolesDiv.Controls.Clear();
            BindNewRole();
        }

        protected void btnTest_OnClick(object sender, EventArgs e)
        {
            SPSite mySite = SPContext.Current.Site;
            SPWeb rootWeb = mySite.RootWeb;
            string rootUrl = rootWeb.Url.ToString();
            string username = rootWeb.CurrentUser.LoginName;
            username = username.Split('\\')[1];
            string blogSite = rootUrl + "/personal/" + username + "/Blog";//两种形式的博客网站地址
            if (!ProjectDll.DAL.Common.UrlCheck(blogSite))
            {
                blogSite = rootUrl + "/my/personal/" + username + "/Blog";
                if (!ProjectDll.DAL.Common.UrlCheck(blogSite))
                {
                    return;//两种博客均不可访问,说明博客网站不存在,则放弃创建新类别.
                }
            }
            
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (var oSiteCollection = new SPSite(blogSite))
                    {
                        using (SPWeb oWebsite = oSiteCollection.OpenWeb())
                        {
                            oWebsite.AllowUnsafeUpdates = true;
                            SPList postTypeList = oWebsite.Lists["类别"];
                            if (tbTest.Text.Trim() != "")
                            {
                                SPListItem item = postTypeList.AddItem();
                                string str = tbTest.Text;
                                item["标题"] = str;
                                item.Update();
                            }
                            oWebsite.AllowUnsafeUpdates = false;
                            SPListItemCollection items = GetItems(postTypeList);
                            gvTypes.DataSource = items.GetDataTable();
                            gvTypes.DataBind();
                        }
                    }
                    
                });
            }
            catch(Exception ex)
            {
                ProjectDll.DAL.Common.Alert(ex.ToString());
            }
            
        }

        private SPListItemCollection GetItems(SPList list)
        {
            SPQuery spQuery=new SPQuery();
            spQuery.Folder = list.RootFolder;
            spQuery.ViewAttributes = "Scope=\"RecursiveAll\"";
            SPListItemCollection itemCollection = list.GetItems(spQuery);
            return itemCollection;
        }
    }
}
