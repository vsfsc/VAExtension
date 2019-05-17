using System;
using System.Web;
using System.Runtime.InteropServices;
using System.DirectoryServices;
using System.Data;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using iSmart.DAL;
using System.Security.Principal;
using System.DirectoryServices.ActiveDirectory;

namespace iSmart.Layouts.iSmart
{
    public partial class EditUserInfo : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //InitControl();
                SPUser currentUser = SPContext.Current.Web.CurrentUser;
                string loginName = currentUser.LoginName;
                loginName = loginName.Substring(loginName.IndexOf('\\') + 1);

                string account = loginName.Replace(@"i:0#.w|", "");
                txtAccount.Text = account;
                DataSet ds = DAL.User.GetUserByAccount(account);
                if (ds.Tables[0].Rows.Count > 0)//数据库中已有该账户信息,则直接从数据库中读取
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    //string[] drcolStrings = drRow..ToString();
                    txtName.Text = dr["Name"].ToString();
                    txtId.Text = dr["IDCard"].ToString();
                    rblSex.SelectedValue = dr["Sex"].ToString();
                    txtTelephone.Text = dr["Telephone"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                }
                else//该用户信息只在AD中存在,用户信息从AD中读取
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                      {
                          DirectoryEntry adUser = ADHelper.GetDirectoryEntryByAccount(account);
                          if (adUser != null)
                          {
                              txtName.Text = adUser.Properties["displayName"][0].ToString();
                              if (adUser.Properties.Contains("telephoneNumber"))
                                  txtTelephone.Text = adUser.Properties["telephoneNumber"][0].ToString();
                              if (adUser.Properties.Contains("mail"))
                                  txtEmail.Text = adUser.Properties["mail"][0].ToString();
                          }
                      });
                }
            }
            btnSave.Click += btnSave_Click;
            //ddlProvince.SelectedIndexChanged += ddlProvince_SelectedIndexChanged;
            //ddlCity.SelectedIndexChanged += ddlCity_SelectedIndexChanged;
        }
        /// <summary>
        /// 提交修改用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string account = txtAccount.Text.Trim().Replace(" ", "");
                DataSet ds = DAL.User.GetUserByAccount(account);
                bool succeed = SaveAd(account, txtName.Text.Trim(), txtEmail.Text.Trim(), txtTelephone.Text.Trim());
                if (succeed == true)//AD修改成功,然后对数据库进行操作
                {
                    if (ds.Tables[0].Rows.Count > 0)//在数据库中找到了当前用户的记录,准备更新操作
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dr["Account"] = account;
                        dr["Name"] = txtName.Text.Trim();
                        dr["IDCard"] = txtId.Text.Trim();
                        dr["Sex"] = int.Parse(rblSex.SelectedValue);
                        dr["Telephone"] = txtTelephone.Text.Trim();
                        dr["Email"] = txtEmail.Text.Trim();
                        dr["Flag"] = 1;
                        dr["Modified"] = DateTime.Now;
                        DAL.User.UpdateUser(dr);//更新当前用户记录
                    }
                    else//该用户不存在数据库中,准备添加当前用户为新用户
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["Account"] = account;
                        dr["Name"] = txtName.Text.Trim();
                        dr["IDCard"] = txtId.Text.Trim();
                        dr["Sex"] = int.Parse(rblSex.SelectedValue);
                        dr["Telephone"] = txtTelephone.Text.Trim();
                        dr["Email"] = txtEmail.Text.Trim();
                        dr["Flag"] = 1;
                        dr["Created"] = DateTime.Now;
                        DAL.User.InsertUser(dr);//添加新用户纪录
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('修改成功');top.location.href='" + SPContext.Current.Site.Url + "'</script>");
                }
                else
                {
                    Common.ShowMessage(Page, this.GetType(), "用户信息更新失败！");
                }
            }
            catch (Exception ex)
            {
                Common.ShowMessage(Page, this.GetType(), "用户信息更新失败！" + ex.ToString());
            }
        }
        private bool SaveAd(string userAccount, string userName, string userEmail, string userTelephone)
        {
            bool retValue = true;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    retValue = ADHelper.EditUser(userAccount, userName, userEmail, userTelephone);
                });
                return retValue;
            }
            catch
            {
                return retValue;
            }
        }
        #region 方法
        public const int Logon32LogonInteractive = 2;
        public const int Logon32ProviderDefault = 0;

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
        private bool ImpersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, Logon32LogonInteractive,
                    Logon32ProviderDefault, ref token) != 0)
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
        private void UndoImpersonation()
        {
            impersonationContext.Undo();
        }
        #endregion

        #region 教育信息数据绑定
        //private void InitControl()
        //{
        //    DataSet dsCity = DAL.User.GetCity(0);

        //    ddlProvince.DataTextField = "AreaName";
        //    ddlProvince.DataValueField = "AreaID";
        //    ddlProvince.DataSource = dsCity;
        //    ddlProvince.DataBind();
        //    ddlProvince_SelectedIndexChanged(null, null);

        //}
        //void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsCity = DAL.User.GetCity(int.Parse(ddlProvince.SelectedValue));
        //    ddlCity.DataTextField = "AreaName";
        //    ddlCity.DataValueField = "AreaID";
        //    ddlCity.DataSource = dsCity;
        //    ddlCity.DataBind();
        //    ddlCity_SelectedIndexChanged(null, null);

        //}

        //void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet dsSchool = DAL.User.GetSchool(int.Parse(ddlCity.SelectedValue), 0);
        //    ddlSchool.DataSource = dsSchool;
        //    ddlSchool.DataTextField = "SchoolName";
        //    ddlSchool.DataValueField = "SchoolID";
        //    ddlSchool.DataBind();
        //}
        #endregion

        
    }
}
