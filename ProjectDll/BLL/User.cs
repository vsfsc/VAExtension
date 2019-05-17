using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using System.Data;

namespace ProjectDll.BLL
{

    public class User
    {
        ///// <summary>
        ///// 获取指定项目中所有有效成员(flag=1,已批准加入项目且未退出)
        ///// </summary>
        ///// <param name="projectId"></param>
        ///// <returns></returns>
        public static List<Proj_Member> GetMembersByprojectId(long projectId)
        {
            List<Proj_Member> ds = DAL.User.GetMembersByprojectId(projectId);
            List<Proj_Member> dsTmp = ds.Where(p => p.Flag == 1).ToList();
            return dsTmp;
        }
        public static long GetUserId(SPUser user)
        {
            if (user==null)
            {
                return 0;
            }
            else
            {
                string name = user.LoginName;
                //检查用户
                List<ProjectDll.User> dt = DAL.User.GetUserByAccount(name.Substring(name.LastIndexOf("\\") + 1));
                if (dt.Count == 0)
                {
                    ProjectDll.User dr = new ProjectDll.User();
                    dr.Account = user.LoginName.Split('\\')[1];
                    dr.Name = user.Name;
                    dr.Flag = 1;
                    return DAL.User.InsertUser(dr);
                }
                else
                {
                    return dt[0].UserID;
                }
            }
        }


        public static SPUser GetSPUser(string sLoginName)
        {
            SPUser oUser = null;
            try
            {
                if (!string.IsNullOrEmpty(sLoginName))
                {
                    oUser = SPContext.Current.Web.EnsureUser(sLoginName);
                }
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message;
            }
            return oUser;
        }
        public static bool JudgeUserRight()
        {
            bool isStudent;

            string UserAccount = DAL.Common.GetLoginAccount;
            if (UserAccount.Length == 8 && UserAccount.Substring(0, 2) == "20")
            {
                isStudent = true;
            }
            else
            {
                isStudent = false;
            }
            return isStudent;
        }
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
            var db = new ProjectEntities();//定义数据库连接接口
            ProjectDll.User mUser = db.User.FirstOrDefault(u => u.Account.Equals(account));
            if (mUser != null)
            {
                return mUser.UserID;
            }
            else
            {
                mUser = new ProjectDll.User();
                mUser.Account = account;
                mUser.Created = DateTime.Now;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    DirectoryEntry adUser = AdHelper.GetDirectoryEntryByAccount(account);
                    if (adUser != null)
                    {
                        mUser.Name = adUser.Properties["displayName"][0].ToString();
                        if (adUser.Properties.Contains("telephoneNumber"))
                            mUser.Telephone = adUser.Properties["telephoneNumber"][0].ToString();
                        if (adUser.Properties.Contains("mail"))
                            mUser.Email = adUser.Properties["mail"][0].ToString();
                    }
                });
                db.User.Add(mUser);
                db.SaveChanges();
                return mUser.UserID;
            }
        }
        public static List<Proj_VUserRoles> GetUserRolesByUserId(long userId)
        {
            List<Proj_VUserRoles> ur = DAL.User.GetUserRoles().Where(urs => urs.UserID == userId&urs.SystemID==3).ToList();
            return ur;
        }
        public static List<Role> GetRolesBySystemId(int systemId)
        {
            List<Role> rsList= DAL.User.GetRoles().Where(r => r.SystemID == systemId).ToList();
            return rsList;
        }

        public static List<Proj_VUserRoles> GetUserRolesByStateId(int stateId)
        {
            List<Proj_VUserRoles> ur = DAL.User.GetUserRoles().Where(urs => urs.StateID == stateId & urs.SystemID == 3).ToList();
            return ur;
        }
    }
}
