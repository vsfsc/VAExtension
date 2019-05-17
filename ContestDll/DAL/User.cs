using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ContestDll.DAL
{
    public class User
    {
        public static List<Area> GetCity()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<Area> atList = db.Area.Where(at => at.Flag  ==1 && at.AreaLevel ==1).ToList();
                return atList;
            }
        }
        public static List<CSSchoolWorksNum > GetSchoolWorksNum(long ContestID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSSchoolWorksNum> atList = db.CSSchoolWorksNum .Where(at => at.PeriodID  == ContestID).ToList();
                return atList;
            }
        }
        /// <summary>
        /// 获取专家类别
        /// </summary>
        /// <returns></returns>
        public static List<CSExpertWorksTypeDetail>  GetExpertWorksTypeDetail(int roleID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSExpertWorksTypeDetail> atList = db.CSExpertWorksTypeDetail.Where(at => at.RoleID == roleID).ToList();
                return atList;
            }
        }
        /// <summary>
        /// 获取所有的用户按学校和角色
        /// </summary>
        /// <returns></returns>
        public static List<CSUserWithSchoolAndRole>  GetUserBySchoolAndRole(int cityID, int schoolID, int roleID, int stateID,string name ,string account)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSUserWithSchoolAndRole> atList ;//
                if (name != "" && account != "")
                    atList = db.CSUserWithSchoolAndRole.Where(at => at.Name.Contains(name) && at.Account.Contains(account)).ToList();
                else if (name != "")
                    atList = db.CSUserWithSchoolAndRole.Where(at => at.Name.Contains(name)).ToList();
                else if (account != "")
                    atList = db.CSUserWithSchoolAndRole.Where(at => at.Account.Contains(account)).ToList();
                else
                    atList = db.CSUserWithSchoolAndRole.ToList();

                if (stateID != -1)
                    atList = atList.Where(at => at.StateID == stateID).ToList();
                if (cityID > 0)
                    atList = atList.Where(at => at.AreaID == cityID).ToList();
                if (schoolID > 0)
                    atList = atList.Where(at => at.SchoolID == schoolID).ToList();
                if (roleID > 0)
                    atList = atList.Where(at => at.RoleID == roleID).ToList();
                return atList;
            }
        }
        public static List<UserRole> GetUserRoleByUserID(long userID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<UserRole > atList = db.UserRole.Where(at => at.UserID ==userID ).ToList();
                return atList;
            }
        }
        public static List<long?> GetUserIdByPeriodId(long periodId)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List<ContestDll.Works> w = db.Works.Where(p => (long)p.PeriodID == periodId && p.IsSample == 0 && p.Flag > 0).ToList();
                List<long?> wd = w.Select(p => p.CreatedBy).Distinct ().ToList();   
                    //.Select(p => p.CreatedBy).ToList();//.Select(p => p.WorksID == 1).Distinct().ToList(); 
                return wd;
            }
        }
        public static List<School > GetSchool(int AreaID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<School> atList = db.School.Where(at => at.AreaID ==AreaID ).ToList();
                return atList;
            }
        }

        public static List<School> GetSchoolById(int schoolId)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<School> atList = db.School.Where(at => at.SchoolID == schoolId).ToList();
                return atList;
            }
        }
        public static List<Area> GetCity(int parentID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<Area> atList = db.Area.Where(at => at.ParentID  == parentID ).ToList();
                return atList;
            }
        }
        //这个方法不用了
        public static List<Role > GetCareer()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<Role> atList = db.Role.ToList();// Where(p => p.SystemID == 2).ToList();
                return atList;
            }
        }
        public static ContestDll.User GetUserByUserID(long userID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                 ContestDll.User  atList = db.User.SingleOrDefault(at => at.UserID  == userID );
                return atList;
            }
        }
        public static List<ContestDll.User> GetUserByAccount(string account)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll.User > atList = db.User.Where(at => at.Account==account ).ToList();
                return atList;
            }
        }
        public static List<ContestDll.User> GetUserByIDCard(string IDCard)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll.User> atList = db.User.Where(at => at.IDCard ==IDCard ).ToList();
                return atList;
            }
        }
        public static long InsertWorksExpert(ContestEntities db,WorksExpert dr)
        {
                ContestDll.WorksExpert at = new WorksExpert();
                at.Comments = dr.Comments;
                at.Created = dr.Created;
                at.ExpertID = dr.ExpertID;
                at.Flag = 1;
                at.Score = dr.Score;
                at.ScoreState = dr.ScoreState;
                at.WorksID = dr.WorksID;
                db.WorksExpert.Add(at);
                db.SaveChanges();
                return at.WorksExpertID;
        }
        public static long InsertWorksExpert(WorksExpert  dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.WorksExpert at = new WorksExpert();
                at.Comments = dr.Comments;
                at.Created = dr.Created;
                at.ExpertID = dr.ExpertID;
                at.Flag = 1;
                at.Score = dr.Score;
                at.ScoreState = dr.ScoreState;
                at.WorksID = dr.WorksID;
                db.WorksExpert.Add(at);
                db.SaveChanges();
                return at.WorksExpertID;
            }
        }
        public static long InsertUserRole(ContestEntities db,UserRole dr)
        {
            UserRole at = new UserRole();
            at.UserID = dr.UserID;
            at.RoleID = dr.RoleID;
            at.StateID = dr.StateID;
            at.Flag = dr.Flag;
            at.Created = DateTime.Now;
            at.CreatedBy = dr.CreatedBy;
            at.Approved = DateTime.Now;
            at.ApprovedBy = at.ApprovedBy;
            at.ContestID = dr.ContestID;
            db.UserRole.Add(at);
            db.SaveChanges();
            return at.UserRoleID ;
        }
        public static long InsertUser(ContestEntities db , ContestDll.User dr)
        {
                ContestDll.User at = new ContestDll.User();
                at.Account = dr.Account;
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
                db.User.Add(at);
                db.SaveChanges();
                return at.UserID;
        }
        public static long InsertUser( ContestDll.User  dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.User at = new ContestDll.User();
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
        public static List<CSUserWorks> GetUserByWorksID(long WorksID)
        {
            using (ContestEntities db=new ContestEntities ())
            {
               List<CSUserWorks> cs = db.CSUserWorks.Where (p => p.WorksID == WorksID).ToList ();
                return cs;
            }
        }
        public static int UpdateUser(ContestDll.User dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.User at = db.User.SingleOrDefault(p => p.UserID == dr.UserID);
                at.Account = dr.Account;
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
                db.SaveChanges();
                return 1;
            }
        }
        public static int UpdateUserRole(ContestEntities db, UserRole dr)
        {
            UserRole at = db.UserRole.SingleOrDefault(p => p.UserRoleID == dr.UserRoleID);
            at.UserID = dr.UserID;
            at.RoleID = dr.RoleID;
            at.Flag = dr.Flag;
            at.Approved = DateTime.Now;
            at.ApprovedBy = at.ApprovedBy;
            at.StateID = dr.StateID;
            at.Created = DateTime.Now;
            db.SaveChanges();
            return 1;
        }
        public static long InsertUserWorks(UserWorks dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                UserWorks file = new UserWorks();
                file.Flag = dr.Flag;
                file.Relationship = dr.Relationship;
                file.UserID = dr.UserID;
                file.WorksID = dr.WorksID;
                db.UserWorks.Add(file);
                db.SaveChanges();
                return file.UserWorksID;
            }
        }
        public static long UpdateUserWorks(UserWorks dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                UserWorks file = db.UserWorks.SingleOrDefault(p => p.UserWorksID == dr.UserWorksID);
                file.Flag = dr.Flag;
                file.Relationship = dr.Relationship;
                file.UserID = dr.UserID;
                file.WorksID = dr.WorksID;
                db.SaveChanges();
                return 1;
            }
        }
        public static List<CSVUserSocre> GetUsersScore()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSVUserSocre> usList = db.CSVUserSocre.ToList();
                return usList;
            }
       }
    }
}
