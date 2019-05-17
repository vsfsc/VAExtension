using System;
using System.Web;
using System.Runtime.InteropServices;

using System.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using iSmart.DAL;
using System.Security.Principal;
using System.DirectoryServices.ActiveDirectory;

namespace iSmart.Layouts.iSmart
{
    public partial class Registration : LayoutsPageBase
    {
        #region 属性
        private string CurrentIP
        {
            get
            {
                // look for current selected user in ViewState
                object currentIP = this.ViewState["_CurrentIP"];
                if (currentIP == null)
                {
                    HttpRequest request = HttpContext.Current.Request; 
                    string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"]; 
                    if (string.IsNullOrEmpty(result)) { 
                        result = request.ServerVariables["REMOTE_ADDR"]; } 
                    if (string.IsNullOrEmpty(result)) { 
                        result = request.UserHostAddress; } 
                    if (string.IsNullOrEmpty(result)) { 
                        result = "0.0.0.0"; }
                    this.ViewState["_CurrentIP"] = result;
                    return result;
                }
                else
                    return (string)currentIP;
            }

            set
            {
                this.ViewState["_CurrentIP"] = value;
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
            }
            btnSave.Click += btnSave_Click;
            ddlProvince.SelectedIndexChanged += ddlProvince_SelectedIndexChanged;
            ddlCity.SelectedIndexChanged +=ddlCity_SelectedIndexChanged;
            txtPwd.Attributes["value"] = txtPwd.Text;
            txtPwd1.Attributes["value"] = txtPwd1.Text;
        }

       
        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string account = txtAccount.Text.Trim().Replace(" ", "");
                DataSet ds = DAL.User.GetUserByAccount(account);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('用户已存在');document.forms(0).txtAccount.select()</script>");
                    return;
                }
                else
                {
                    bool succeed = SaveAD(account, txtName.Text.Trim(), txtEmail.Text.Trim(), txtTelephone.Text.Trim(), txtPwd.Text.Trim(), rblRole.SelectedValue, ddlSchool.SelectedItem == null ? "其它" : ddlSchool.SelectedItem.Text, true);
                    if (succeed)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["Account"] = account;
                        dr["Name"] = txtName.Text.Trim();
                        dr["IDCard"] = txtId.Text.Trim();
                        dr["Sex"] = int.Parse(rblSex.SelectedValue);
                        dr["Telephone"] = txtTelephone.Text.Trim();
                        dr["Email"] = txtEmail.Text.Trim();
                        dr["Flag"] = 1;
                        dr["RoleID"] = rblRole.SelectedValue ;
                        dr["Created"] = DateTime.Now;
                        //dr["IP"] = CurrentIP;
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
        //绑定控件
        #region 数据加载
        private void InitControl()
        {
            DataSet dsRole = DAL.User.GetRole();
            rblRole.DataSource = dsRole;
            rblRole.DataTextField = "RoleName";
            rblRole.DataValueField = "RoleID";
            rblRole.DataBind();
            rblRole.Items[0].Selected = true;
            DataSet dsCity = DAL.User.GetCity(0);

            ddlProvince.DataTextField = "AreaName";
            ddlProvince.DataValueField = "AreaID";
            ddlProvince.DataSource = dsCity;
            ddlProvince.DataBind();
            ddlProvince_SelectedIndexChanged(null, null);

        }
        void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsCity = DAL.User.GetCity(int.Parse(ddlProvince.SelectedValue));
            ddlCity.DataTextField = "AreaName";
            ddlCity.DataValueField = "AreaID";
            ddlCity.DataSource = dsCity;
            ddlCity.DataBind();
            ddlCity_SelectedIndexChanged(null, null);

        }

        void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsSchool =DAL.User .GetSchool(int.Parse(ddlCity.SelectedValue), 0);
            ddlSchool.DataSource = dsSchool;
            ddlSchool.DataTextField = "SchoolName";
            ddlSchool.DataValueField = "SchoolID";
            ddlSchool.DataBind();
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
        private bool SaveAD(string userAccount,string txtName,string txtEmail,string txtTelephone ,string txtPwd, string roleName,string schoolName, bool userEnabled)
        {
            bool retValue = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    string domain = ADHelper.Domain;
                    string strConst=HiddenField1.Value;
                    if (impersonateValidUser("administrator", domain, strConst.Substring(strConst.IndexOf(" ") + 1)))
                    {
                        string ouName = strConst.Substring(0, strConst.IndexOf(" "));// "iSmart";// System.Configuration.ConfigurationManager.AppSettings["adPath"];
                        ouName = ADHelper.GetDirectoryEntryOfOU("", ouName);
                        retValue = ADHelper.AddUser(userAccount, txtName, txtEmail, txtTelephone, txtPwd, ouName, roleName == "1" ? "iSmartStudent" : "iSmartTeacher", schoolName, userEnabled);
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
