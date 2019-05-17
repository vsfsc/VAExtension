using System;
using System.Web;
using System.Web.UI;
using System.DirectoryServices;
using Microsoft.SharePoint;

namespace iSmart.Layouts.iSmart
{
    public partial class Login : Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string domainName = error.Attributes["Title"];
            bool checkResult = CheckUserPassword(domainName, userid.Value, password.Value);
            if (checkResult)
            {
                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "<script type='text/javascript'>UserValidate('" + userid.Value + "','" + password.Value + "');</script>", true);
                error.InnerText = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>UserValidate('" + domainName + "','" + userid.Value + "','" + password.Value + "');</script>");
            }
            else
                error.InnerText = "用户名或密码错误";
        }
        private bool CheckUserPassword(string domainName, string userName, string pwd)
        {
            try
            {
                string strLDAP = "LDAP://" + domainName;
                using (DirectoryEntry objDE = new DirectoryEntry("", userName, pwd))
                {
                    DirectorySearcher deSearcher = new DirectorySearcher(objDE);
                    deSearcher.Filter = "(&(objectClass=user)(sAMAccountName=" + userName + "))";
                    DirectoryEntry usr = deSearcher.FindOne().GetDirectoryEntry();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void closeBtn_OnClick(object sender, ImageClickEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>top.location.href='" + SPContext.Current.Site.Url + "'</script>");
        }
    }
}
