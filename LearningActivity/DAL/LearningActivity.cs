using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.UI.WebControls;
using LADLL;

namespace LearningActivity.DAL
{
    public static class LearningActivityDal
    {
       public static List<QuerylearningActivity> GetAllLA()
        {
            using (var db = new LAActivityEntities())
            {
                return db.QuerylearningActivity.OrderByDescending(q => q.开始时间).ToList();                
            }
        }
        /// <summary>
        /// 新增学习活动记录
        /// </summary>
        /// <param name="aTypeId">类别ID</param>
        /// <param name="objectId">学习对象ID</param>
        /// <param name="durings">持续时长</param>
        /// <param name="startDateTime">开始时间</param>
        /// <param name="locationId">学习地点</param>
        /// <param name="worksUrl">作品URL</param>
        /// <param name="others">其他描述</param>
        /// <returns></returns>
        public static long AddAct(long aTypeId, long objectId, int durings,DateTime startDateTime,long locationId,string worksUrl,string others)
        {
            var db = new LAActivityEntities();
            var la = new LADLL.LearningActivity
            {
                UserID = UserDAL.GetUserId(),
                ActivityTypeID = aTypeId,
                LearningObjectID = objectId,
                Start = startDateTime,
                During = durings,
                LocationID = locationId,
                WorksUrl = worksUrl,
                Others = others,
                CreatDate = DateTime.Now,
                
                Flag = 1
            };
            db.LearningActivity.Add(la);
            db.SaveChanges();
            return la.LearningActivityID;
        }
        /// <summary>
        /// 获取指定用户的活动列表
        /// </summary>
        /// <returns></returns>
        public static List<QuerylearningActivity> GetlearningActivityByUser(long userId)
        {
            List<QuerylearningActivity> qActivities = GetAllLA();
            using (var db = new LAActivityEntities())
            {
                //qActivities = db.QuerylearningActivity.OrderByDescending(q => q.开始时间).ToList();
                //qActivities = GetAllLA;
                if (userId!=0)
                {
                    qActivities = qActivities.Where(q => q.UserID == userId).ToList();
                }
            }
            return qActivities ;
        }
        /// <summary>
        /// 获取用户在指定时间点同期活动(重复活动)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dt">指定时间点</param>
        /// <returns></returns>
        public static List<QuerylearningActivity> GetMySameActivities(long userId,DateTime dt)
        {
            List<QuerylearningActivity> qActivities = GetlearningActivityByUser(userId);//首先查询该用户的所有活动
            qActivities = qActivities.Where(qa=>qa.开始时间<dt).OrderBy(qa => Math.Abs(DAL.Common.TimeToSeconds(qa.开始时间点) - DAL.Common.TimeToSeconds(dt.TimeOfDay.ToString()))).ToList();
            List<QuerylearningActivity> sameActivities=new List<QuerylearningActivity>();
            if (qActivities.Count>=3)
            {
                for (int i = 0; i < 3; i++)
                {
                    sameActivities.Add(qActivities[i]);
                }
            }
            else
            {
                for (int i = 0; i < qActivities.Count; i++)
                {
                    sameActivities.Add(qActivities[i]);
                }
            }
            return sameActivities;
        }
        public static void DelActivity(long id)
        {
            using (var db = new LAActivityEntities())
            {
                var result = from la in db.LearningActivity where la.LearningActivityID == id select la;
                var target = result.FirstOrDefault<LADLL.LearningActivity>();
                if (target != null)
                {
                    target.Flag = 0;
                    target.ModifyDate = DateTime.Now;
                }
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 更新指定ID的活动记录
        /// </summary>
        /// <param name="laid">指定活动ID</param>
        /// <param name="aTypeId">活动类型ID</param>
        /// <param name="objectId">学习对象ID</param>
        /// <param name="locationId">活动地点ID</param>
        /// <param name="startDateTime">起始时间</param>
        /// <param name="durings">持续时长</param>
        /// <param name="worksUrl">作品Url</param>
        /// <param name="others">活动描述</param>
        public static void UpdateLearningActivity(long laid, long aTypeId, long objectId, long locationId, DateTime startDateTime, int durings, string worksUrl, string others)
        {
            var db = new LAActivityEntities();
            
            var result = from la in db.LearningActivity where la.LearningActivityID==laid select la;
            var target = result.FirstOrDefault<LADLL.LearningActivity>();
            if (target != null)
            {
                target.ActivityTypeID = aTypeId;
                target.LearningObjectID = objectId;
                target.LocationID = locationId;
                target.Start = startDateTime;
                target.During = durings;
                target.WorksUrl = worksUrl;
                target.Others = others;
                target.ModifyDate = DateTime.Now;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 获取某个用户近几天之内的所有活动
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="days">统计天数</param>
        public static List<LADLL.LearningActivity> GetLAbyUserIdandDate(long userid, int days)
        {
            DateTime dt = DateTime.Now.AddDays(-days).Date;
            using (var db = new LAActivityEntities())
            {
                List<LADLL.LearningActivity> userActivities;
                if (userid==0)
                {
                    userActivities = db.LearningActivity.Where(la => la.Start > dt).ToList();
                }
                else
                {
                    userActivities = db.LearningActivity.Where(la => la.UserID == userid && la.Start > dt).ToList();
                }
                return userActivities;
            }
        }
        //public static List<LADLL.QuerylearningActivity> GetlearningActivityByUserAndDuration(long userId, DateTime startDate, DateTime endDate)
        //{
        //    using (var db = new LAActivityEntities())
        //    {
        //        List<QuerylearningActivity> userActivities = db.QuerylearningActivity.ToList();//获取所有用户所有活动
        //        if (userId == 0)
        //        {
        //            userActivities = userActivities.Where(ua => ua.开始时间 >= startDate && ua.开始时间<endDate).ToList();
        //        }
        //        else
        //        {
        //            userActivities = userActivities.Where(ua => ua.UserID == userId && ua.开始时间 >= startDate && ua.开始时间 < endDate).ToList();
        //        }
        //        return userActivities;
        //    }
        //}
        /// <summary>
        /// 筛选用户的活动,若指定用户ID(userId!=0),则筛选该用户的活动,若指定日期(dateString!=""),则筛选该日的活动
        /// </summary>
        /// <param name="userId">指定用户ID</param>
        /// <param name="dateString">指定日期</param>
        /// <returns></returns>
        public static List<LADLL.QuerylearningActivity> GetLAbyUserIdandDay(long userId, string dateString)
        {
            List<QuerylearningActivity> userActivities = GetAllLA();
            using (var db = new LAActivityEntities())
            {
               // List<QuerylearningActivity> userActivities = db.QuerylearningActivity.ToList();//获取所有用户所有活动
                if (dateString!="")//指定了活动开始日期
                {
                    userActivities = userActivities.Where(ua => ua.开始日期 == dateString).ToList();
                }
                
                if (userId != 0)//指定了用户ID
                {
                    userActivities = userActivities.Where(ua => ua.UserID == userId).ToList();
                }
                return userActivities;
            }
        }
        public static int GetLACountsbyUserIdandDate(long userId, string dateString)
        {
            List<QuerylearningActivity> userActivities = GetAllLA();
            using (var db = new LAActivityEntities())
            {
               // List<QuerylearningActivity> userActivities = db.QuerylearningActivity.ToList();//获取所有用户所有活动
                if (dateString != "")//指定了活动的时间段
                {
                    //userActivities = db.QuerylearningActivity.Where(ua => Convert.ToDateTime(ua.开始日期) >= Convert.ToDateTime(StartDate)).ToList();
                    //userActivities = db.QuerylearningActivity.Where(ua => Convert.ToDateTime(ua.开始日期) <= Convert.ToDateTime(EndDate)).ToList();
                    //userActivities = db.QuerylearningActivity.Where(ua => DateTime.Compare(Convert.ToDateTime(ua.开始日期), Convert.ToDateTime(StartDate)) >=0 ).ToList();
                    //userActivities = db.QuerylearningActivity.Where(ua => DateTime.Compare(Convert.ToDateTime(ua.开始日期), Convert.ToDateTime(EndDate)) <= 0).ToList();
                    userActivities = userActivities.Where(ua => ua.开始日期 == dateString).ToList();
                }

                if (userId != 0)//指定了用户ID
                {
                    userActivities = userActivities.Where(ua => ua.UserID == userId).ToList();
                }
                return userActivities.Count;
            }
        }
        public static int GetLACountsbyUserIdandDuration(long userId, DateTime startdate, DateTime enddate)
        {
            List<QuerylearningActivity> userActivities = GetAllLA();
            using (var db = new LAActivityEntities())
            {
                if (string.Format("{0:yyyy-MM-dd}", startdate) != "")//指定了活动的时间段
                {
                    //userActivities = db.QuerylearningActivity.Where(ua => Convert.ToDateTime(ua.开始日期) >= Convert.ToDateTime(StartDate)).ToList();
                    //userActivities = db.QuerylearningActivity.Where(ua => Convert.ToDateTime(ua.开始日期) <= Convert.ToDateTime(EndDate)).ToList();
                    //userActivities = db.QuerylearningActivity.Where(ua => DateTime.Compare(Convert.ToDateTime(ua.开始日期), Convert.ToDateTime(StartDate)) >=0 ).ToList();
                    //userActivities = db.QuerylearningActivity.Where(ua => DateTime.Compare(Convert.ToDateTime(ua.开始日期), Convert.ToDateTime(EndDate)) <= 0).ToList();
                    userActivities = userActivities.Where(ua => ua.开始时间 >= startdate&&ua.开始时间<=enddate).ToList();
                }

                if (userId != 0)//指定了用户ID
                {
                    userActivities = userActivities.Where(ua => ua.UserID == userId).ToList();
                }
                return userActivities.Count;
            }
        }

        public static string GetlaDurings(List<LADLL.QuerylearningActivity> laList)
        {
            long? durings = 0;
            if (laList.Count > 0)
            {
                foreach (var la in laList)
                {
                    durings += la.持续时长;
                }
            }
            if (durings >= 60)
            {
                return (durings / 60).ToString() + "小时" + (durings % 60).ToString() + "分钟";
            }
            else if (durings > 0 && durings < 60)
            {
                return durings.ToString() + "分钟";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 获取指定用户在指定时间内的活动累计时长
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="days">统计天数</param>
        /// <returns></returns>
        public static string GetlaDuringsByDate(long userid, int days)
        {
            long? durings = 0;
            List< LADLL.LearningActivity> laList= GetLAbyUserIdandDate(userid, days);
            if (laList.Count>0)
            {
                foreach (var la in laList)
                {
                    durings+=la.During;
                }
            }
            if (durings>=60)
            {
                return  (durings/60).ToString()+"小时"+ (durings%60).ToString()+"分钟";
            }
            else if (durings > 0&&durings<60)
            {
                return durings.ToString() + "分钟";
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 获取指定用户(userId!=0在指定活动地点(locaId)的活动
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="locaId">活动地点ID</param>
        /// <returns></returns>
        public static List<LADLL.QuerylearningActivity> GetActivitiesByLocation(long userId, long locaId)
        {
            var userActivities = new List<LADLL.QuerylearningActivity>();
            using (var db = new LAActivityEntities())
            {
                userActivities = db.QuerylearningActivity.Where(la=>la.LocationID==locaId).ToList();
                if (userId!=0)
                {
                    userActivities =userActivities.Where(la => la.UserID ==userId).ToList();
                }
            }
            return userActivities;
        }
        /// <summary>
        /// 获取指定用户(userId!=0在指定活动类型(typeId)的活动
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="typeId">活动类型ID</param>
        /// <returns></returns>
        public static List<LADLL.QuerylearningActivity> GetActivitiesByType(long userId, long typeId)
        {
            var userActivities = new List<LADLL.QuerylearningActivity>();
            using (var db = new LAActivityEntities())
            {
                userActivities = db.QuerylearningActivity.Where(la => la.ActivityTypeID == typeId).ToList();
                if (userId != 0)
                {
                    userActivities = userActivities.Where(la => la.UserID == userId).ToList();
                }
            }
            return userActivities;
        }
        /// <summary>
        /// 查询指定活动ID的学习活动
        /// </summary>
        /// <param name="laId">活动ID</param>
        /// <returns></returns>
        public static List<LADLL.LearningActivity> GetActivityById(long laId)
        {
            List<LADLL.LearningActivity> laList;
            using (var db = new LAActivityEntities())
            {
                laList = db.LearningActivity.Where(la => la.LearningActivityID == laId).ToList();
            }
            return laList;
        }

        public static List<laCountsbyUser> GetLaCountsbyUsers()
        {
            List<LADLL.laCountsbyUser> laList;
            using (var db = new LAActivityEntities())
            {
                laList = db.laCountsbyUser.OrderByDescending(la=>la.counts).ToList();
            }
            return laList;
        }
    }
}
