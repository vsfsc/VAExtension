using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace ContestDll
{
    public class UserManagement : LayoutsPageBase
    {
        #region 控件定义
        protected global::System.Web.UI.ScriptManager ScriptManager1;
        protected global::System.Web.UI.UpdatePanel up1;
        protected global::System.Web.UI.WebControls.RadioButtonList rblRole;
        protected global::System.Web.UI.WebControls.TextBox txtAccountNew;
        protected global::System.Web.UI.WebControls.TextBox txtPwd;
        protected global::System.Web.UI.WebControls.TextBox txtNameNew;
        protected global::System.Web.UI.WebControls.RadioButtonList rblSex;
        protected global::System.Web.UI.WebControls.TextBox txtId;
        protected global::System.Web.UI.WebControls.DropDownList ddlCityNew;
        protected global::System.Web.UI.WebControls.DropDownList ddlSchollNew;
        protected global::System.Web.UI.WebControls.TextBox txtEmail;
        protected global::System.Web.UI.WebControls.TextBox txtTelephone;
        protected global::System.Web.UI.WebControls.Button btnSave;
        protected global::System.Web.UI.WebControls.CheckBoxList cblRole;
        protected global::System.Web.UI.WebControls.Button btnOk;
        protected global::System.Web.UI.WebControls.DropDownList ddlRole;
        protected global::System.Web.UI.WebControls.TextBox txtAccount;
        protected global::System.Web.UI.WebControls.TextBox txtName;
        protected global::System.Web.UI.WebControls.DropDownList ddlCity;
        protected global::System.Web.UI.WebControls.DropDownList ddlSchool;
        protected global::System.Web.UI.WebControls.Button btnSearch;
        protected global::System.Web.UI.WebControls.GridView gvUser;
        protected global::System.Web.UI.UpdatePanel UpdatePanel1;
        protected global::System.Web.UI.WebControls.Button btnAddExpert;
        protected global::System.Web.UI.WebControls.Button btnAddUser;
        protected global::System.Web.UI.WebControls.Button btnAddRole;
        protected global::System.Web.UI.WebControls.Button btnCancelRole;
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (!DAL.Common.IsWebAdmin && !DAL.Common.LoginRole.Contains(8))//8为竞赛管理员
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('没有权限');top.location.href='" + DAL.Common.SPWeb.Url + "'</script>");
                        return;
                    }
                }
                catch
                {
                     
                }
                InitControl();
                gvUser.DataKeyNames = new string[] { "UserID" };
                gvUser.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);

            }
            ddlCity.SelectedIndexChanged += new EventHandler(ddlCity_SelectedIndexChanged);
            ddlCityNew.SelectedIndexChanged += new EventHandler(ddlCityNew_SelectedIndexChanged);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            btnAddUser.Click += new EventHandler(btnAddUser_Click);
            btnAddExpert.Click += new EventHandler(btnAddExpert_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            gvUser.PageIndexChanging += new GridViewPageEventHandler(gvUser_PageIndexChanging);
            gvUser.RowCommand += new GridViewCommandEventHandler(gvUser_RowCommand);
            gvUser.RowDataBound += new GridViewRowEventHandler(gvUser_RowDataBound);
            btnAddRole.Click += new EventHandler(btnAddRole_Click);
            btnCancelRole.Click += new EventHandler(btnCancelRole_Click);
            btnOk.Click += new EventHandler(btnOk_Click);
            if (!Page.IsPostBack)
                btnSearch_Click(null, null);
            txtPwd.Attributes["value"] = txtPwd.Text;

        }
        /// <summary>
        /// 设置或取消角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOk_Click(object sender, EventArgs e)
        {
            int roleID = int.Parse(ViewState["userRole"].ToString());
            string strTitle;
            if (roleID == 1)
                strTitle = "设置角色";
            else
                strTitle = "取消角色";
            Approve("", strTitle, roleID);
        }
        /// <summary>
        /// 取消已经设置的角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancelRole_Click(object sender, EventArgs e)
        {
            ViewState["userRole"] = 2;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "divScript", "ShowYes1()", true);
        }
        //添加新的角色
        void btnAddRole_Click(object sender, EventArgs e)
        {
            ViewState["userRole"] = 1;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "divScript", "ShowYes1()", true);
        }

        void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "selectx(this)");

            }

        }
        //城市变换事件
        void ddlCityNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            CityChanged(int.Parse(ddlCityNew.SelectedValue), ddlSchollNew, 0);

        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        private void UpdateSave()
        {
            ContestDll.User dr = DAL.User.GetUserByUserID((long)ViewState["editDsUserID"]);
            dr.SchoolID =  int.Parse(ddlSchollNew.SelectedValue);
            dr.Sex =   rblSex.SelectedValue=="1"?true:false ;
            dr.IDCard  = txtId.Text;

            dr.Telephone  = txtTelephone.Text;
            dr.Email  = txtEmail.Text;
            dr.ModifiedBy  =DAL.Common. LoginID;
            dr.Modified = DateTime.Now;
            DAL.User.UpdateUser(dr);
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('保存成功');document.getElementById('divManageUser').style.display='none'", true);
            ViewState.Remove("ShowInfo");
            btnSearch_Click(null, null);
        }
        //保存
        void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAccountNew.ReadOnly == true)
            {
                UpdateSave();
                return;
            }
            List<User> ds =  DAL.User.GetUserByAccount(txtAccountNew.Text);
            if (ds.Count  > 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('用户已存在');document.forms(0).txtAccountNew.select()", true);
                return;
            }
            ds =  DAL.User.GetUserByIDCard(txtId.Text);
            if (ds.Count > 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('身份证号码已存在');document.forms(0).txtId.select()", true);
                return;
            }

            ContestDll.User dr = new ContestDll.User();
            dr.Account  = txtAccountNew.Text;
            dr.Name  = txtNameNew.Text;
            dr.IDCard  = txtId.Text;
            dr.Sex  =  rblSex.SelectedValue=="1"?true :false ;
            dr.Telephone  = txtTelephone.Text;
            dr.Email = txtEmail.Text;
            dr.Flag = 1;
            dr.SchoolID= int.Parse (ddlSchollNew.SelectedValue);

            UserRole drRole = new UserRole();
            drRole.CreatedBy =DAL.Common.LoginID;
            drRole.Created = DateTime.Now;
            drRole.RoleID = int.Parse(rblRole.SelectedValue);
            drRole.StateID = 1;
            drRole.Flag = 1;
            drRole.ContestID =DAL.Common.GetContestID(Page );
            //这里加的专家和管理员无需审批
            try
            {
                bool enableUser = true;// ViewState["ShowInfo"] != null;
                bool succeed = SaveAD(enableUser);
                if (succeed)
                {

                    BLL.User.InsertUser(dr, drRole); ;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "message", "alert('保存成功');document.getElementById('divManageUser').style.display='none'", true);
                    Clear();
                    ViewState.Remove("ShowInfo");

                }
                else
                {
                   DAL.Common.ShowMessage(Page, this.GetType(), "帐号已经存在！");
                }
            }
            catch
            {
               DAL.Common.ShowMessage(Page, this.GetType(), "保存失败！");
            }
        }
        //保存AD
        private bool SaveAD(bool userEnabled)
        {
            bool retValue = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    string ouName = System.Configuration.ConfigurationManager.AppSettings["adPath"];
                    ouName =DAL. ADHelper.GetDirectoryEntryOfOU("", ouName);
                    retValue = DAL.ADHelper.AddUser(txtAccountNew.Text, txtNameNew.Text, txtEmail.Text, txtTelephone.Text, txtPwd.Text, ouName, rblRole.SelectedItem.Text, ddlSchollNew.SelectedItem.Text, userEnabled);

                });
                return retValue;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 清空内容
        /// </summary>
        private void Clear()
        {
            txtAccountNew.Text = "";
            txtNameNew.Text = "";
            rblSex.SelectedIndex = 0;
            txtPwd.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            txtId.Text = "";

        }
        //城市下面的学校
        void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CityChanged(int.Parse(ddlCity.SelectedValue), ddlSchool, 1);
        }
        /// <summary>
        /// 城市变了，下面的学校也要变动
        /// </summary>
        /// <param name="cityID"></param>
        private void CityChanged(int cityID, DropDownList ddlSchool, int search)
        {
            List<School>  dsSchool1 = DAL.User.GetSchool (cityID);
            if (search == 1)
            {
                List<School> dsSchool = new List<School>();
                School dr = new School();
                dr.SchoolID  = 0;
                dr.SchoolName  = "全部";
                dsSchool.Add(dr);
                dsSchool.AddRange(dsSchool1);
                dsSchool1 = dsSchool.ToList();
            }


            ddlSchool.DataSource = dsSchool1;
            ddlSchool.DataTextField = "SchoolName";
            ddlSchool.DataValueField = "SchoolID";
            ddlSchool.DataBind();
            ShowButton();

        }
        //查询\审批通过的
        void btnSearch_Click(object sender, EventArgs e)
        {
            List<CSUserWithSchoolAndRole>  dsSearch = null;
            int schoolID = 0;
            if (ddlSchool.SelectedIndex > -1)
                schoolID = int.Parse(ddlSchool.SelectedValue);
            dsSearch = DAL.User.GetUserBySchoolAndRole(int.Parse(ddlCity.SelectedValue), schoolID, int.Parse(ddlRole.SelectedValue), -1,txtName.Text,txtAccount.Text );
            

            try
            {
                ViewState["dsSearch"] = dsSearch ;
                GridBind(dsSearch );
            }
            catch
            {
               DAL. Common.ShowMessage(Page, this.GetType(), "查询出错！");
            }
            ShowButton();
        }
        void btnAddExpert_Click(object sender, EventArgs e)
        {
            SetUserState(false);
            Clear();
            btnSave.Visible = true;
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "divScript", "ShowYes()", true);
            ViewState["ShowExpert"] = "1";
        }

        void btnAddUser_Click(object sender, EventArgs e)
        {
            OpenWindow(UpdatePanel1, "UserInfo.aspx");
            ShowButton();

        }

        //按钮事件
        void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值 
            //此获取的值为GridView中绑定数据库中的主键值,取值方法是选中的行中的第一列的值,drv.RowIndex取得是选中行的索引 
            long userID = (long)gvUser.DataKeys[drv.RowIndex].Value ;
            //if (gvUser.DataKeys[drv.RowIndex].Values["UserRoleID"] == null)
            //    userID = (long)gvUser.DataKeys[drv.RowIndex].Values["UserID"];
            //else
            //    userRoleID = (long)gvUser.DataKeys[drv.RowIndex].Values["UserRoleID"];
            List<CSUserWithSchoolAndRole> dsSearch = (List<CSUserWithSchoolAndRole>)ViewState["dsSearch"];
            CSUserWithSchoolAndRole drs = dsSearch.SingleOrDefault(p => p.UserID ==userID );
            List<ContestDll.User> dsUser = DAL.User.GetUserByAccount(drs.Account);
            ViewState["cityID"] = drs.AreaID;
            ViewState["RoleID"] = drs.RoleID;
            if (e.CommandName == "ViewDetail")
                DAL.Common.OpenWindow(Page, "UserInfo.aspx?Edit=0&&UserID=" + drs.UserID + "&&CityID=" + drs.AreaID + "&&Role=" + drs.RoleID+"&&SchollID"+ drs.SchoolID );
            else if (e.CommandName == "ViewDelete")
            {
                DAL.Common.OpenWindow(Page, "UserInfo.aspx?Edit=1&&UserID=" + drs.UserID + "&&CityID=" + drs.AreaID + "&&Role=" + drs.RoleID + "&&SchollID" + drs.SchoolID);

               

            }
            ShowButton();
        }

        //页码更改事件
        void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            List<CSUserWithSchoolAndRole> dt = (List<CSUserWithSchoolAndRole>)ViewState["dsSearch"];
            GridBind(dt );
        }
        #endregion
        #region "方法"
        //设置用户的多个角色或取消角色
        private void Approve(string strBody, string strTitle, int stateID)
        {
            try
            {
                long loginID = DAL.Common.LoginID ;
                List<long> userIDs = new List<long>();
                foreach (GridViewRow row in gvUser.Rows)
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("ckb");
                    if (myCheckBox.Checked)
                    {
                        long userID = long.Parse(gvUser.DataKeys[row.RowIndex].Value.ToString());
                        List<CSUserWithSchoolAndRole> dsSearch = (List<CSUserWithSchoolAndRole>)ViewState["dsSearch"];//结果只是个查询
                        CSUserWithSchoolAndRole   dr = dsSearch.SingleOrDefault(p=>p.UserID == userID);
                        userIDs.Add( (int)dr.UserID );
                    }
                }
                List<int> userRoles = new List<int>();
                foreach (ListItem chk in cblRole.Items)
                {
                    if (chk.Selected)
                        userRoles.Add(int.Parse(chk.Value));
                }
                int result = BLL.User.UpdateUserRole(userIDs, userRoles, stateID, DAL.Common.GetContestID(Page), loginID);
                if (result > 0)
                {
                    DAL.Common.ShowMessage(Page, this.GetType(), strTitle + "保存成功 " + result + " 人");
                    btnSearch_Click(null, null);
                }
                else
                {
                    DAL.Common.ShowMessage(Page, this.GetType(), "操作失败");
                }
            }
            catch
            {
               DAL.Common.ShowMessage(Page, this.GetType(), "操作失败");
            }
        }

        //查看或编辑用户信息
        private void SetUserState(bool readOnly)
        {
            rblRole.Enabled = !readOnly;
            txtAccountNew.ReadOnly = readOnly;
            txtPwd.ReadOnly = readOnly;
            txtId.ReadOnly = readOnly;
            txtAccountNew.ReadOnly = readOnly;
            txtNameNew.ReadOnly = readOnly;
            rblSex.Enabled = !readOnly;
            txtTelephone.ReadOnly = readOnly;
            txtEmail.ReadOnly = readOnly;
            ddlCityNew.Enabled = !readOnly;
            ddlSchollNew.Enabled = !readOnly;

        }
        /// <summary>浏览
        /// </summary>
        /// <param name="dr"></param>
        private void FillUserInfo(User  dr)
        {
            txtAccountNew.Text = dr.Account;
            txtNameNew.Text = dr.Name ;
            txtId.Text = dr.IDCard ;
            if (!string.IsNullOrEmpty(dr.Sex.ToString()))
                rblSex.SelectedValue =  (bool)dr.Sex  ? "1" : "0";
            txtTelephone.Text = dr.Telephone;
            txtEmail.Text = dr.Email;
            ddlCityNew.SelectedValue = ViewState["cityID"].ToString();
            ddlCityNew_SelectedIndexChanged(null, null);
            ddlSchollNew.SelectedValue = dr.SchoolID.ToString();
        }
        //按钮显示
        private void ShowButton()
        {
            if (ViewState["ShowInfo"] != null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ShowYes()</script>");
                ViewState.Remove("ShowInfo");
            }
            else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>ShowNo()</script>");

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GridBind(List<CSUserWithSchoolAndRole> dv)
        {
            gvUser.DataSource = dv;
            gvUser.DataBind();
            ShowButton();
        }
        //绑定控件
        private void InitControl()
        {
            List<Area>  dsCity1 = DAL.User.GetCity();
            ddlCityNew.DataTextField = "AreaName";
            ddlCityNew.DataValueField = "AreaID";
            ddlCityNew.DataSource = dsCity1.ToList();
            ddlCityNew.DataBind();
            ddlCityNew_SelectedIndexChanged(null, null);

            List<Area> dsCity = new List<Area>();
            Area dr = new Area();
            dr.AreaID = 0;
            dr.AreaName = "全部";
            dsCity.Add(dr);
            dsCity.AddRange(dsCity1.ToList());
            ddlCity.DataTextField = "AreaName";
            ddlCity.DataValueField = "AreaID";
            ddlCity.DataSource = dsCity.ToList ();
            ddlCity.DataBind();
            ddlCity_SelectedIndexChanged(null, null);

        }

        #endregion
        #region "方法"
        public bool ShowMessage(UpdatePanel UpdatePanel1, string msg)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateScript", "alert('" + msg + "')", true);
            return true;
        }
        public void OpenWindow(UpdatePanel UpdatePanel1, string fileName)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "openScript", "window.open('" + fileName + "','_blank')", true);
        }
        #endregion
    }
}
