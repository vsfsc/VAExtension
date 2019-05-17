using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.DirectoryServices.ActiveDirectory;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ContestDll.DAL;

namespace ContestDll
{
    public partial class UserInfo : LayoutsPageBase
    {
        #region 控件定义
        protected System.Web.UI.WebControls.TextBox txtAccount;
        protected System.Web.UI.WebControls.TextBox txtPwd;
        protected System.Web.UI.WebControls.HiddenField HiddenField1;
        protected System.Web.UI.WebControls.TextBox txtPwd1;
        protected System.Web.UI.WebControls.TextBox txtName;
        protected System.Web.UI.WebControls.RadioButtonList rblSex;
        protected System.Web.UI.WebControls.TextBox txtId;
        protected System.Web.UI.WebControls.DropDownList ddlCareer;
        protected System.Web.UI.WebControls.TextBox txtTelephone;
        protected System.Web.UI.WebControls.TextBox txtEmail;
        protected System.Web.UI.WebControls.DropDownList ddlProvince;
        protected System.Web.UI.WebControls.DropDownList ddlCity;
        protected System.Web.UI.WebControls.DropDownList ddlSchool;
        protected System.Web.UI.WebControls.TextBox txtCheckCode;
        protected System.Web.UI.WebControls.Button btnSave;
        protected System.Web.UI.WebControls.Button btnClose;
        #endregion
        #region 属性
        protected int editState
        {
            get
            {
                if (Request.QueryString["Edit"] != null)
                    if (Request.QueryString["Edit"] == "0")
                        return 0;
                    else
                        return 1;
                else
                    return 2;
            }
        }
        #endregion
        #region 事件
        protected override bool AllowAnonymousAccess
        {
            get
            {
                return true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitControl();
                if (editState != 2)
                    FillUserInfo();
            }
            if (editState ==0)
            {//浏览用户信息
                btnSave.Enabled = false;
                ddlCareer.Enabled = false;
                ddlCity.Enabled = false;
                ddlProvince.Enabled = false;
                ddlSchool.Enabled = false;
                txtAccount.ReadOnly = true;
                txtCheckCode.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtId.ReadOnly = true;
                txtName.ReadOnly = true;
                txtPwd.ReadOnly = true;
                txtPwd1.ReadOnly = true;
                txtTelephone.ReadOnly = true;
                
            }
            else
            {//新加或编辑用户信息
                if (editState == 1)
                {
                    txtAccount.ReadOnly = true;
                    txtPwd.ReadOnly = true;
                    txtPwd1.ReadOnly = true;
                }
                btnSave.Click += btnSave_Click;
                ddlProvince.SelectedIndexChanged += ddlProvince_SelectedIndexChanged;
                ddlCity.SelectedIndexChanged += ddlCity_SelectedIndexChanged;
                txtPwd.Attributes["value"] = txtPwd.Text;
                txtPwd1.Attributes["value"] = txtPwd1.Text;
            }
        }
        private void EditSave()
        {
            User dr = DAL.User.GetUserByUserID(long.Parse(Request.QueryString["UserID"]));
            dr.Telephone = txtTelephone.Text;
            dr.Email = txtEmail.Text;
            dr.IDCard = txtId.Text;
            dr.Sex = rblSex.SelectedValue == "1" ? true : false;
            dr.SchoolID =int.Parse ( ddlSchool.SelectedValue);
            dr.StateID = 1;
            dr.Modified = DateTime.Now;
            dr.ModifiedBy = DAL.Common.LoginID;
            DAL.User.UpdateUser(dr);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (editState ==1)
                {
                    EditSave();
                    DAL.ADHelper.EditUser(txtAccount.Text, txtName.Text, txtEmail.Text, txtTelephone.Text);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('保存成功');if (window.opener.document.getElementById('btnSearch')!=null) window.opener.document.getElementById('btnSearch').click();window.close()</script>");

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('保存成功');window.close()</script>");

                    return;
                }
                string account = txtAccount.Text.Trim().Replace(" ", "");
                List<ContestDll.User > ds = DAL.User.GetUserByAccount(account);
                if (ds.Count  > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('用户已存在');document.forms(0).txtAccount.select()</script>");
                    return;
                }
                else
                {
                    bool succeed = SaveAD(account, txtName.Text.Trim(), txtEmail.Text.Trim(), txtTelephone.Text.Trim(), txtPwd.Text.Trim(), ddlCareer.SelectedItem.Text , ddlSchool.SelectedItem == null ? "其它" : ddlSchool.SelectedItem.Text, true);
                    if (succeed)
                    {
                        ContestDll.User dr = new ContestDll.User();
                        dr.Account = account;
                        if (ddlSchool.SelectedItem != null)
                            dr.SchoolID = int.Parse(ddlSchool.SelectedValue);
                        dr.Name = txtName.Text.Trim();
                        dr.IDCard = txtId.Text.Trim();
                        dr.Sex = rblSex.SelectedValue == "1" ? true : false;
                        dr.Telephone = txtTelephone.Text.Trim();
                        dr.Email = txtEmail.Text.Trim();
                        dr.Flag = 1;
                        dr.RoleID = int.Parse(ddlCareer.SelectedValue);
                        DAL.User.InsertUser(dr);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('注册成功');top.location.href='" + SPContext.Current.Site.Url + "'</script>");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>LoginSys('" + ADHelper.Domain + "','" + account + "','" + txtPwd.Text.Trim() + "');</script>");

                    }
                    else
                    {
                        Common.ShowMessage(Page, this.GetType(), "用户已经存在！");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowMessage(Page, this.GetType(), "注册失败！" + ex.ToString());
            }
        }

        #endregion
        #region 编辑查看数据用到的方法
        private void FillUserInfo()
        {
             User  drUser = DAL.User.GetUserByUserID(long.Parse ( Request.QueryString["UserID"]));
             txtAccount.Text = drUser.Account;
             txtName.Text = drUser.Name;
             txtEmail.Text = drUser.Email;
             txtId.Text = drUser.IDCard;
             txtTelephone.Text = drUser.Telephone;
        }
        private int GetProvinceID(int cityID)
        {
            List<Area> ds = DAL.User.GetCity();
            ds = ds.Where(p => p.AreaID == cityID).ToList();
            return (int)ds[0].ParentID;
        }
        #endregion
        //绑定控件
        #region 数据加载
        private void InitControl()
        {
            List<Role> ds = DAL.User.GetCareer();
            List<Role> dsCareer = new List<Role>();
            Role dr=new Role ();
            dr.RoleID =0;
            dr.RoleName ="";
            dsCareer.Add(dr);
            dsCareer.AddRange(ds);
            ddlCareer.DataSource = dsCareer;
            ddlCareer.DataTextField = "RoleName";
            ddlCareer.DataValueField = "RoleID";
            ddlCareer.DataBind();

            List<Area> dsCity = DAL.User.GetCity(0);

            ddlProvince.DataTextField = "AreaName";
            ddlProvince.DataValueField = "AreaID";
            ddlProvince.DataSource = dsCity;
            ddlProvince.DataBind();

            if (editState == 2)//new
                ddlCareer.Items[0].Selected = true;
            else//edit or view
            {
                ddlCareer.SelectedValue = Request.QueryString["Role"] == null ? "0" : Request.QueryString["Role"];
                ddlProvince.SelectedValue =GetProvinceID (int.Parse ( Request.QueryString["CityID"])).ToString (); 

            }
            ddlProvince_SelectedIndexChanged(null, null);

        }
        void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Area> dsCity = DAL.User.GetCity(int.Parse(ddlProvince.SelectedValue));
            ddlCity.DataTextField = "AreaName";
            ddlCity.DataValueField = "AreaID";
            ddlCity.DataSource = dsCity;
            ddlCity.DataBind();
            if (editState != 2)
                ddlCity.SelectedValue = Request.QueryString["CityID"];
            ddlCity_SelectedIndexChanged(null, null);

        }

        void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<School> dsSchool = DAL.User.GetSchool(int.Parse(ddlCity.SelectedValue));
            ddlSchool.DataSource = dsSchool;
            ddlSchool.DataTextField = "SchoolName";
            ddlSchool.DataValueField = "SchoolID";
            ddlSchool.DataBind();
            if (editState != 2)
                ddlSchool.SelectedValue = Request.QueryString["SchollID"];

        }
        #endregion
        #region 方法
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);
        private bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }
        private bool SaveAD(string userAccount, string txtName, string txtEmail, string txtTelephone, string txtPwd, string roleName, string schoolName, bool userEnabled)
        {
            bool retValue = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    string domain = ADHelper.Domain;
                    string strConst = HiddenField1.Value;
                    if (impersonateValidUser("administrator", domain, strConst.Substring(strConst.IndexOf(" ") + 1)))
                    {
                        string ouName = strConst.Substring(0, strConst.IndexOf(" "));// "iSmart";// System.Configuration.ConfigurationManager.AppSettings["adPath"];
                        ouName = ADHelper.GetDirectoryEntryOfOU("", ouName);
                        retValue = ADHelper.AddUser(userAccount, txtName, txtEmail, txtTelephone, txtPwd, ouName, roleName , schoolName, userEnabled);
                        undoImpersonation();
                    }
                    else
                    {
                        //Your impersonation failed. Therefore, include a fail-safe mechanism here.
                    }

                });
                return retValue;
            }
            catch
            {
                return false;
            }
        }
        private void SendEmail()
        {
            string content = "您注册的用户名：";// +txtLoginName.Text.Trim() + "<br>" + "您注册的密码：" + tbnewPass.Text.Trim() + "<br>" + "登录用户名：  <FONT size=5>" + domain + "\\" + txtLoginName.Text.Trim() + "</FONT>";

            Common.SendMail("training@mail.neu.edu.cn", "training", "110004cc", new string[] { txtEmail.Text.Trim() }, "注册成功", content);
            string message = "注册信息已经发送到您的邮件，24小时后您的账号将激活，注意查收";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('" + message + "');location.href='" + SPContext.Current.Web.Url + "'</script>");

        }
        /// <summary>
        /// 生成验证码的随机数
        /// </summary>
        /// <returns>返回五位随机数</returns>
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < 5; i++)//可以任意设定生成验证码的位数
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            return checkCode;
        }
        #endregion
    }
}
