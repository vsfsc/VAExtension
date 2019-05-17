using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using System.Data;
using System.Transactions;

namespace ContestDll.BLL
{

    public class User
    {
        /// <summary>
        /// 批量设置或取消权限
        /// </summary>
        /// <param name="userRoleIDs"></param>
        /// <param name="userRoles"></param>
        /// <param name="state"></param>
        /// <param name="contestID"></param>
        /// <returns></returns>
        public static int UpdateUserRole(List<long> userIDs, List<int> userRoles, int state, long contestID, long approveBy)
        {
            int i = 0;
            ContestEntities db = new ContestEntities();

            foreach (long userID in userIDs)
            {
                List<UserRole> dsRole = DAL.User.GetUserRoleByUserID(userID);

                using (TransactionScope trans = new TransactionScope())
                    try
                    {
                        foreach (UserRole dr in dsRole)
                        {
                            if (userRoles.Contains(dr.RoleID) && dr.ContestID ==contestID )
                            {
                                dr.StateID = state;
                                dr.ContestID = contestID;
                                dr.Approved = DateTime.Now;
                                dr.ApprovedBy = approveBy;
                                userRoles.Remove(dr.RoleID);
                                DAL.User.UpdateUserRole(db, dr);
                            }
                        }
                        UserRole drRow;
                        if (state == 1)
                        {
                            foreach (int roleID in userRoles)
                            {
                                drRow = new UserRole();
                                drRow.UserID = userID;
                                drRow.RoleID = roleID;
                                drRow.Flag = 1;
                                drRow.StateID = state;
                                drRow.CreatedBy = approveBy;
                                drRow.Created = DateTime.Now;
                                drRow.Approved = DateTime.Now;
                                drRow.ApprovedBy = approveBy;
                                drRow.ContestID = contestID;
                                DAL.User.InsertUserRole(db, drRow);
                            }
                        }
                        trans.Complete();
                        i++;
                    }
                    catch (Exception ex)
                    {
                        DAL.Common.WriteLog(ex.ToString());
                        trans.Dispose();
                    }
            }
            return i;
        }
        public static bool InsertUser(ContestDll.User  drUser,UserRole drUserRole)
        {
            List<WorksExpert> dsType = DAL.Works.GetWorksExpertByID(0);
            ContestEntities db = new ContestEntities();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    long userID = DAL.User.InsertUser(db, drUser);
                    drUserRole.UserID = userID;
                    DAL.User.InsertUserRole(db, drUserRole);

                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return false;
                }
            }
        }
        ///// <summary>
        ///// 获取团队成员
        ///// </summary>
        ///// <param name="worksID"></param>
        ///// <returns></returns>
        public static List<CSUserWorks> GetGroupMemberByWorksID(long worksID)
        {
            List<CSUserWorks > ds = DAL.User.GetUserByWorksID(worksID);
            List<CSUserWorks> dsTmp = ds.Where(p=>p.Relationship ==2  ).ToList();
            return dsTmp;
        }
        public static long GetUserID(SPUser user)
        {
            string name = user.LoginName;
            //检查用户
            List<ContestDll.User> dt = DAL.User.GetUserByAccount(name.Substring(name.LastIndexOf("\\") + 1));
            if (dt.Count == 0)
            {
                ContestDll.User dr = new ContestDll.User();
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
        public static List<CSVUserSocre> GetUserScoreByUserId(long userId)
        {
            List<CSVUserSocre> uslist = DAL.User.GetUsersScore().Where(us => us.UserID == userId).ToList();
            return uslist;
        }
        public static long[] GetContestUser()
        {
                List<CSVUserSocre> usList = DAL.User.GetUsersScore();
                long[] ArrUserId = usList.Select(us => us.UserID).Distinct().ToArray();
                return ArrUserId;
        }
        public static List<CSVUserSocre> GetUserScoreByPeriodId(long periodId)
        {
            List<CSVUserSocre> uslist = DAL.User.GetUsersScore().Where(us => us.PeriodID == periodId).OrderByDescending(a => a.Score).ToList();
            return uslist;
        }
    }
}
