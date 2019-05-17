using System;
using System.Linq;
using LADLL;
using Microsoft.SharePoint;
using System.DirectoryServices;

namespace LearningActivity.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// 获取当前登录的用户的账号
        /// </summary>
        /// <returns></returns>
        public static string GetAccount()
        {
            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            string loginName = currentUser.LoginName;
            loginName = loginName.Substring(loginName.IndexOf('\\') + 1);
            string account = loginName.Replace(@"i:0#.w|", "");
            return account;
        }
        /// <summary>
        /// 获取当前用户ID,如果数据库中没有数据，根据活动目录写入数据库中
        /// </summary>
        /// <returns>mUser.UserID</returns>
        public static long GetUserId()
        {
            string account = GetAccount();
            var db = new LAActivityEntities();//定义数据库连接接口
            User mUser = db.User.FirstOrDefault(u => u.Account.Equals(account));
            if (mUser != null)
            {
                return mUser.UserID ;
            }
            else
            {
                mUser = new User();
                mUser.Account = account;
                mUser.Created = DateTime.Now;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    DirectoryEntry adUser = ADHelper.GetDirectoryEntryByAccount(account);
                    if (adUser != null)
                    {
                        mUser.Name = adUser.Properties["displayName"][0].ToString();
                        if (adUser.Properties.Contains("telephoneNumber"))
                            mUser.Telephone  = adUser.Properties["telephoneNumber"][0].ToString();
                        if (adUser.Properties.Contains("mail"))
                           mUser.Email = adUser.Properties["mail"][0].ToString();
                    }
                });
                db.User.Add(mUser);
                db.SaveChanges();
                return mUser.UserID ;
            }
        }
    }
}
