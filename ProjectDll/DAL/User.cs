using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectDll.DAL
{
    public class User
    {
        //public static List<long?> GetUserIdByPeriodId(long periodId)
        //{
        //    using (ProjectEntities db=new ProjectEntities ())
        //    {
        //        List<ProjectDll.Works> w = db.Works.Where(p => (long)p.PeriodID == periodId && p.IsSample == 0 && p.Flag > 0).ToList();
        //        List<long?> wd = w.Select(p => p.CreatedBy).Distinct ().ToList();   
        //            //.Select(p => p.CreatedBy).ToList();//.Select(p => p.WorksID == 1).Distinct().ToList(); 
        //        return wd;
        //    }
        //}
        public static void GetExpertWorksTypeDetail()
        {

        }

        
        public static List<Proj_VUserWithRole > GetUserBySchoolandRole(int roleID)
        {
             using (ProjectEntities db=new ProjectEntities ())
            {
                List<Proj_VUserWithRole> expert = db.Proj_VUserWithRole.Where(p => p.RoleID==roleID  ).ToList ();
                return expert;
            }
        }
        public static List<School > GetSchool(int AreaID)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<School> atList = db.School.Where(at => at.AreaID ==AreaID ).ToList();
                return atList;
            }
        }

        public static List<School> GetSchoolById(int schoolId)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<School> atList = db.School.Where(at => at.SchoolID == schoolId).ToList();
                return atList;
            }
        }
        public static List<Area> GetCity(int parentID)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Area> atList = db.Area.Where(at => at.ParentID  == parentID ).ToList();
                return atList;
            }
        }
        public static List<Career> GetCareer()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Career> atList = db.Career.ToList();
                return atList;
            }
        }
        public static ProjectDll.User GetUserByUserID(long userID)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                 ProjectDll.User  atList = db.User.SingleOrDefault(at => at.UserID  == userID );
                return atList;
            }
        }
        public static List<ProjectDll.User> GetUserByAccount(string account)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<ProjectDll.User > atList = db.User.Where(at => at.Account==account ).ToList();
                return atList;
            }
        }
        //public static long InsertWorksExpert(ProjectEntities db,WorksExpert dr)
        //{
        //        ProjectDll.WorksExpert at = new WorksExpert();
        //        at.Comments = dr.Comments;
        //        at.Created = dr.Created;
        //        at.ExpertID = dr.ExpertID;
        //        at.Flag = 1;
        //        at.Score = dr.Score;
        //        at.ScoreState = dr.ScoreState;
        //        at.WorksID = dr.WorksID;
        //        db.WorksExpert.Add(at);
        //        db.SaveChanges();
        //        return at.WorksExpertID;
        //}
        //public static long InsertWorksExpert(WorksExpert  dr)
        //{
        //    using (ProjectEntities db = new ProjectEntities())
        //    {
        //        ProjectDll.WorksExpert at = new WorksExpert();
        //        at.Comments = dr.Comments;
        //        at.Created = dr.Created;
        //        at.ExpertID = dr.ExpertID;
        //        at.Flag = 1;
        //        at.Score = dr.Score;
        //        at.ScoreState = dr.ScoreState;
        //        at.WorksID = dr.WorksID;
        //        db.WorksExpert.Add(at);
        //        db.SaveChanges();
        //        return at.WorksExpertID;
        //    }
        //}


        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="dr">用户记录</param>
        /// <returns></returns>
        public static long InsertUser(ProjectDll.User dr)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                ProjectDll.User at = new ProjectDll.User();
                at.Account  = dr.Account ;
                at.Department = dr.Department;
                at.Duty = dr.Duty;
                at.Email = dr.Email;
                at.Department = dr.Department;
                at.Telephone = dr.Telephone;
                at.IDCard = dr.IDCard;
                at.Name = dr.Name;
                at.SchoolID = dr.SchoolID;
                at.Major = dr.Major;
                at.RoleID = dr.RoleID;
                at.Sex = dr.Sex;
                at.StateID = dr.StateID;
                at.Created = DateTime.Now;
                db.User .Add(at);
                db.SaveChanges();
                return at.UserID ;
            }
        }
        /// <summary>
        /// 获取指定项目中所有人员
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Proj_Member> GetMembersByprojectId(long projectId)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_Member> pms = db.Proj_Member.Where(pm => pm.ProjectID == projectId).ToList();
                return pms;
            }
        }
        public static int UpdateUser(DataRow dr)
        {
            //return (DAL.SqlHelper.ExecuteAppointedParameters(DAL.DataProvider.ConnectionString, "UpdateUser", dr));
            return 1;
        }
        /// <summary>
        /// 用户申请加入项目
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertMember(Proj_Member dr)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Member pm = new Proj_Member();
                pm.UserID = dr.UserID;
                pm.ProjectID = dr.ProjectID;
                pm.ApplyInTime = dr.ApplyInTime;
                pm.Flag = dr.Flag;
                db.Proj_Member.Add(pm);
                db.SaveChanges();
                return pm.MemberID;
            }
        }
        /// <summary>
        /// 记录成员在项目中的成绩
        /// </summary>
        /// <param name="mId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static long UpdateMemberScore(long mId,float score)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Member pm = db.Proj_Member.SingleOrDefault(p => p.MemberID == mId);
                pm.Score = score;
                db.SaveChanges();
                return 1;
            }
        }
        /// <summary>
        /// 成员退出
        /// </summary>
        /// <param name="mId"></param>
        /// <param name="outReason"></param>
        /// <returns></returns>
        public static long SetMemberOut(long mId, string outReason)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Member pm = db.Proj_Member.SingleOrDefault(p => p.MemberID == mId);
                pm.OutReason = outReason;
                pm.ApplyOutTime = DateTime.Now;
                db.SaveChanges();
                return 1;
            }
        }

        #region 用户角色与权限
        public static List<Proj_VUserRoles> GetUserRoles()
        {
            using (var db = new ProjectEntities())
            {
                List<Proj_VUserRoles> ur = db.Proj_VUserRoles.ToList();
                return ur;
            }
        }
        public static List<Role> GetRoles()
        {
            using (var db = new ProjectEntities())
            {
                List<Role> rs = db.Role.ToList();
                return rs;
            }
        }

        public static long ApplyNewRole(UserRole dr) //用户申请新角色
        {
            using (var db = new ProjectEntities())
            {
                var ur = new UserRole();
                ur.UserID = dr.UserID;
                ur.RoleID = dr.RoleID;
                ur.Created = dr.Created;
                ur.CreatedBy = dr.CreatedBy;
                ur.StateID = dr.StateID;
                ur.Flag = dr.Flag;
                db.UserRole.Add(ur);
                db.SaveChanges();
                return ur.UserRoleID;
            }
        }

        public static int ApproveNewRole(UserRole dr)//为用户审批新角色
        {
            using (var db = new ProjectEntities())
            {
                UserRole ur = db.UserRole.FirstOrDefault(r => r.UserRoleID == dr.UserRoleID);
                ur.StateID = dr.StateID;//-1拒绝,1同意
                ur.Approved = dr.Approved;
                ur.ApprovedBy = dr.ApprovedBy;
                db.SaveChanges();
                return 1;
            }
        }
        #endregion
    }
}
