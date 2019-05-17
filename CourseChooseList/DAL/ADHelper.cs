using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

namespace CourseDll
{
    public class ADHelper
    {
        #region 属性
        ///
        /// LDAP绑定路径
        ///
        private static string ADPath
        {
            get
            {
                DirectoryEntry root = new DirectoryEntry("LDAP://rootDSE");
                string path = "LDAP://" + root.Properties["defaultNamingContext"].Value;
                return path;

            }
        }
        #endregion
        #region 方法
        ///// <summary>
        ///// 获取用户的性别
        ///// </summary>
        ///// <param name="accountName">用户登录帐户，不包括域名；如：20053703</param>
        ///// <returns>返回登录用户的性别</returns>
        //public static string GetUserSex(string accountName)
        //{
        //    DirectoryEntry user = GetDirectoryEntryByAccount(accountName);
        //    if (user != null)
        //    {
        //        System.DirectoryServices.PropertyCollection props = user.Properties;
        //        PropertyValueCollection des = props["description"];
        //        if (des.Value != null)
        //        {
        //            string identify = des.Value.ToString().Trim();
        //            string flag;
        //            if (identify.Length == 18)
        //                flag = identify.Substring(16, 1);
        //            else
        //                flag = identify.Substring(14);
        //            if (Convert.ToInt16(flag) % 2 == 0)
        //                return "女";
        //            else
        //                return "男";
        //        }
        //        else
        //            return "";
        //    }
        //    return "";
        //}
        /// <summary>
        /// 获取用户的性别2010-09-07
        /// </summary>
        /// <param name="accountName">用户登录帐户，不包括域名；如：20053703</param>
        /// <returns>返回登录用户的性别</returns>
        public static string GetUserSex(string accountName)
        {
            DirectoryEntry user = GetDirectoryEntryByAccount(accountName);
            if (user != null)
            {
                System.DirectoryServices.PropertyCollection props = user.Properties;
                PropertyValueCollection des = props["description"];
                if (des.Value != null)
                {
                    string identify = des.Value.ToString().Trim();
                    if (identify.Length == 1) return identify;
                    string flag;
                    if (identify.Length == 18)
                        flag = identify.Substring(16, 1);
                    else
                        flag = identify.Substring(14);
                    if (Convert.ToInt16(flag) % 2 == 0)
                        return "女";
                    else
                        return "男";
                }
                else
                    return "";
            }
            return "";
        }

        /// <summary>
        /// 获取用户所在的组（班级）
        /// </summary>
        /// <param name="accountName">用户登录名</param>
        /// <returns></returns>
        public static string[] GetUserGroups(string accountName)
        {
            DirectoryEntry user = GetDirectoryEntryByAccount(accountName);
            if (user != null)
            {
                string groups = "";
                string group;
                System.DirectoryServices.PropertyCollection props = user.Properties;
                PropertyValueCollection values = props["memberOf"];
                foreach (object val in values)
                {
                    group = val.ToString();
                    group = group.Substring(3, group.IndexOf(",") - 3);
                    groups += group + "|";
                }
                groups = groups.Trim('|');
                if (groups.Length > 0)
                    return groups.Split('|');
                else
                    return null;
            }

            return null;
        }
        /// <summary>
        /// 用户是否属于某个组
        /// </summary>
        /// <param name="accountName">用户登录名称，不包括域名 如：xueqingxia</param>
        /// <param name="groupName">组名 如：英语0901</param>
        /// <returns></returns>
        public static bool UserIsMemberof(string accountName, string groupName)
        {
            DirectoryEntry user = GetDirectoryEntryByAccount(accountName);
            if (user != null)
            {
                System.DirectoryServices.PropertyCollection props = user.Properties;
                PropertyValueCollection values = props["memberOf"];
                foreach (object val in values)
                {
                    if (val.ToString().ToLower().Contains(groupName.ToLower()))
                        return true;
                }

            }
            return false;
        }
        ///
        ///根据用户帐号称取得用户的 对象
        ///
        ///用户帐号名 

        ///如果找到该用户，则返回用户的 对象；否则返回 null
        private static DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName)
        {
            DirectoryEntry de = new DirectoryEntry(ADPath);//GetDirectoryObject();
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))";
            deSearch.SearchScope = SearchScope.Subtree;

            try
            {
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
