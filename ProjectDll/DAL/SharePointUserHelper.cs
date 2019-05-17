using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace ProjectDll.DAL
{
    public class SharePointUserHelper
    {
        /// <summary>
        /// 判断组是否存在
        /// </summary>
        /// <param name="web"></param>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public bool IsExistGroup(SPWeb web, string groupname)
        {
            try
            {
                foreach (SPGroup grouplist in web.SiteGroups)//判断组是否存在
                {
                    if (grouplist.ToString().ToLower() == groupname.ToLower())
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 新建组
        /// </summary>
        /// <param name="web"></param>
        /// <param name="groupname"></param>
        /// <param name="member"></param>
        /// <param name="spuser"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool AddGroup(SPWeb web, string groupname, SPMember member, SPUser spuser, string description)
        {
            try
            {
                if (!IsExistGroup(web, groupname))
                {
                    web.SiteGroups.Add(groupname, member, spuser, description);//新建组
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 判断指定组是否存在用户
        /// </summary>
        /// <param name="web"></param>
        /// <param name="username">Domin\\Name形式</param>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public bool IsExistUser(SPWeb web, string username, string groupname)
        {
            try
            {
                foreach (SPUser userlist in web.SiteGroups[groupname].Users)//判断指定组是否存在用户
                {
                    if (userlist.ToString().ToLower() == username.ToLower())
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据指定的组新建用户
        /// </summary>
        /// <param name="web"></param>
        /// <param name="loginname">登录名:Domin\\Name形式</param>
        /// <param name="groupname">组名称</param>
        /// <param name="email">Email</param>
        /// <param name="cnname">中文名</param>
        /// <param name="notes">用户说明</param>
        /// <returns>bool</returns>
        public bool AddUserToGroup(SPWeb web, string loginname, string groupname, string email, string cnname, string notes)
        {
            try
            {
                if (!IsExistUser(web, loginname, groupname))
                {
                    web.SiteGroups[groupname].AddUser(loginname, email, cnname, notes);//新建用户
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 组权限分配与定义(New)
        /// </summary>
        /// <param name="web"></param>
        /// <param name="groupname"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool AddGroupToRoles(SPWeb web, string groupname, string[] roles)
        {
            try
            {
                string[] _roles = roles;
                int rolemun = _roles.Length;

                if (IsExistGroup(web, groupname))
                {
                    //改变站点继承权
                    if (!web.HasUniqueRoleDefinitions)
                    {
                        web.RoleDefinitions.BreakInheritance(true, true);//复制父站点角色定义并且保持权限
                    }

                    //站点继承权改变后重新设置状态
                    web.AllowUnsafeUpdates = true;

                    //组权限分配与定义(New)
                    SPRoleDefinitionCollection roleDefinitions = web.RoleDefinitions;
                    SPRoleAssignmentCollection roleAssignments = web.RoleAssignments;
                    SPMember memCrossSiteGroup = web.SiteGroups[groupname];
                    SPPrincipal myssp = (SPPrincipal)memCrossSiteGroup;
                    SPRoleAssignment myroles = new SPRoleAssignment(myssp);
                    SPRoleDefinitionBindingCollection roleDefBindings = myroles.RoleDefinitionBindings;
                    if (rolemun > 0)
                    {
                        for (int i = 0; i < rolemun; i++)
                        {
                            roleDefBindings.Add(roleDefinitions[_roles[i]]);
                        }
                    }
                    roleAssignments.Add(myroles);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
