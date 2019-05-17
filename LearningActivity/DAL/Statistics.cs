using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LADLL;

namespace LearningActivity.DAL
{
    /// <summary>
    /// 统计个人活动记录
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// 统计用户周期(指定时间范围)内记录过的活动地点
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static long[] GetAddress(long userId, DateTime dtStart, DateTime dtEnd)
        {
            List<QuerylearningActivity> la = DAL.LearningActivityDal.GetlearningActivityByUser(userId);
            long[] locaIds = la.Where(a =>a.开始时间 > dtStart&&a.开始时间<dtEnd).Select(a => a.LocationID).Distinct().ToArray();
            return locaIds;
        }

        /// <summary>
        /// 统计用户记录过的活动类型
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static long[] GetTypes(long userId, DateTime dtStart, DateTime dtEnd)
        {
            List<QuerylearningActivity> la = LearningActivityDal.GetlearningActivityByUser(userId);
            long[] typeIds = la.Select(a => a.ActivityTypeID).Distinct().ToArray();
            return typeIds;
        }

        /// <summary>
        /// 统计用户记录过活动的每个地点的活动数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static ArrayList MyLaCountsEveryLocation(long userId, DateTime dtStart, DateTime dtEnd)
        {
            ArrayList cLocaArray = new ArrayList();
            //Dictionary<string,int> dictLa=new Dictionary<string, int>();
            long[] ads = GetAddress(userId,dtStart,dtEnd);
            if (ads.Length>0)
            {
                string[] nameStrs = new string[ads.Length];
                int[] countInts = new int[ads.Length];
                int i = 0;
                foreach (long locationId in ads)
                {
                    nameStrs[i] = LocationDAL.GetLoctionById(locationId)[0].Address;
                    countInts [i]= LearningActivityDal.GetActivitiesByLocation(userId, locationId).Count;
                    //dictLa.Add(locaName,laCount);
                    
                    i++;
                }
                cLocaArray.Add(nameStrs);
                cLocaArray.Add(countInts);
            }
            //else
            //{
            //    DAL.Common.Alert("您未曾记录过任何地点的活动!");
            //}
            return cLocaArray;
        }
        /// <summary>
        /// 返回地点和记录数组成的两列数据表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static DataTable DtMyLaCountsEveryLocation(long userId, DateTime dtStart, DateTime dtEnd)
        {
            DataTable cLocaDt = new DataTable();
            cLocaDt.Columns.Add("地点", typeof(string));
            cLocaDt.Columns.Add("记录数", typeof(int));
            long[] ads = GetAddress(userId, dtStart, dtEnd);
            if (ads.Length > 0)
            {
                //int i = 0;
                foreach (long locationId in ads)
                {
                    string nameStrs = LocationDAL.GetLoctionById(locationId)[0].Address;
                    int countInts =LearningActivityDal.GetActivitiesByLocation(userId, locationId).Count;
                    //dictLa.Add(locaName,laCount);
                    if (countInts!=0)
                    {
                        cLocaDt.Rows.Add(nameStrs, countInts);
                    }
                }
              
            }
            return cLocaDt;
        }
        /// <summary>
        /// 统计用户记录过活动的每个地点的活动累计时长(选定时间范围内)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static ArrayList MyLaDuringsEveryLocation(long userId,DateTime dtStart,DateTime dtEnd)
        {
            ArrayList dLocaArray = new ArrayList();
            //var dictLa = new Dictionary<string, int?>();
            long[] ads = GetAddress(userId,dtStart,dtEnd);
            if (ads.Length > 0)
            {
                string[] nameStrs=new string[ads.Length];
                int[] duringInts=new int[ads.Length];
                int i = 0;
                foreach (long locationId in ads)
                {
                    string locaName = DAL.LocationDAL.GetLoctionById(locationId)[0].Address;
                    List<QuerylearningActivity> laList = DAL.LearningActivityDal.GetActivitiesByLocation(userId, locationId);
                    int? durings = 0;
                    foreach (var la in laList)
                    {
                        durings += la.持续时长;
                    }
                    //dictLa.Add(locaName, durings);
                    nameStrs[i] = locaName;
                    duringInts[i] = (int)durings;
                    i++;
                }
                dLocaArray.Add(nameStrs);
                dLocaArray.Add(duringInts);
            }
            //else
            //{
            //    DAL.Common.Alert("您未曾记录过任何地点的活动!");
            //}
            return dLocaArray;
        }

        /// <summary>
        /// 返回指定周期内地点和记录累计时长组成的两列数据表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static DataTable DtMyLaDuringsEveryLocation(long userId, DateTime dtStart, DateTime dtEnd)
        {
            DataTable dtDLoca = new DataTable();
            long[] ads = GetAddress(userId, dtStart, dtEnd);
            if (ads.Length > 0)
            {
                foreach (long locationId in ads)
                {
                    string locaName = DAL.LocationDAL.GetLoctionById(locationId)[0].Address;
                    List<QuerylearningActivity> laList = DAL.LearningActivityDal.GetActivitiesByLocation(userId, locationId);
                    int? durings = 0;
                    foreach (var la in laList)
                    {
                        durings += la.持续时长;
                    }
                  
                    dtDLoca.Rows.Add(locaName, durings);
                }
            }
            return dtDLoca;
        }

        /// <summary>
        /// 统计用户记录过活动的每个活动类型的活动次数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static ArrayList MyLaCountsEveryType(long userId, DateTime dtStart, DateTime dtEnd)
        {
            ArrayList cTypeArrayList=new ArrayList();
            //Dictionary<string, int> dictLa = new Dictionary<string, int>();
            long[] tps = GetTypes(userId,dtStart,dtEnd);
            if (tps.Length > 0)
            {
                string[] nameStrs=new string[tps.Length];
                int[] countInts=new int[tps.Length];
                int i = 0;
                foreach (long typeId in tps)
                {
                    nameStrs[i] = DAL.ActivityTypeDAL.GetTypeById(typeId).Action;
                    countInts[i] = DAL.LearningActivityDal.GetActivitiesByType(userId, typeId).Count;
                    i++;
                    //dictLa.Add(typeName, laCount);
                }
                cTypeArrayList.Add(nameStrs);
                cTypeArrayList.Add(countInts);
            }
            return cTypeArrayList;
        }

        public static DataTable DtMyLaCountsEveryType(long userId, DateTime dtStart, DateTime dtEnd)
        {
            DataTable dtcType = new DataTable();
            //Dictionary<string, int> dictLa = new Dictionary<string, int>();
            long[] tps = GetTypes(userId, dtStart, dtEnd);
            if (tps.Length > 0)
            {
                foreach (long typeId in tps)
                {
                    string nameStrs = DAL.ActivityTypeDAL.GetTypeById(typeId).Action;
                    int countInts = LearningActivityDal.GetActivitiesByType(userId, typeId).Count;
                    if (countInts!=0)
                    {
                        dtcType.Rows.Add(nameStrs, countInts);
                    }
                }
            }
            return dtcType;
        }

        /// <summary>
        /// 统计用户记录过活动的每个活动类型的活动累计时长
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static ArrayList MyLaDuringsEveryType(long userId, DateTime dtStart, DateTime dtEnd)
        {
            ArrayList dTypeArrayList = new ArrayList();
            //Dictionary<string, int?> dictLa = new Dictionary<string, int?>();
            long[] tps = GetTypes(userId,dtStart,dtEnd);
            if (tps.Length > 0)
            {
                string[] nameStrs = new string[tps.Length];
                int[] duringInts = new int[tps.Length];
                int i = 0;
                foreach (long typeId in tps)
                {
                    string typeName = DAL.ActivityTypeDAL.GetTypeById(typeId).Action;
                    List<QuerylearningActivity> laList = DAL.LearningActivityDal.GetActivitiesByType(userId, typeId);
                    int? durings = 0;
                    foreach (var la in laList)
                    {
                        durings += la.持续时长;
                    }
                    nameStrs[i] = typeName;
                    duringInts[i] = (int)durings;
                    i++;
                    //dictLa.Add(typeName, durings);
                }
                dTypeArrayList.Add(nameStrs);
                dTypeArrayList.Add(duringInts);
            }
            return dTypeArrayList;
        }

        /// <summary>
        /// 统计用户记录过活动的每个活动类型的活动累计时长,生成DataTable
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static DataTable DtMyLaDuringsEveryType(long userId, DateTime dtStart, DateTime dtEnd)
        {
            DataTable dtDType = new DataTable();
            //Dictionary<string, int?> dictLa = new Dictionary<string, int?>();
            long[] tps = GetTypes(userId, dtStart, dtEnd);
            if (tps.Length > 0)
            {
                foreach (long typeId in tps)
                {
                    string typeName = DAL.ActivityTypeDAL.GetTypeById(typeId).Action;
                    List<QuerylearningActivity> laList = DAL.LearningActivityDal.GetActivitiesByType(userId, typeId);
                    int? durings = 0;
                    foreach (var la in laList)
                    {
                        durings += la.持续时长;
                    }
                    dtDType.Rows.Add(typeName, durings);
                }
            }
            return dtDType;
        }


        /// <summary>
        /// 统计指定时间段的一个周期内用户每天的活动数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static int[] MyLaCountsEveryDay(long userId,DateTime dtStart,DateTime dtEnd)
        {
            int timeSpan = DAL.Common.DateDiff(dtStart, dtEnd);
            int[] arrIntsLa = new int[timeSpan];
            for (int i = 0; i < timeSpan; i++)
            {
                string dtTime = string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(i));
                arrIntsLa[i] = DAL.LearningActivityDal.GetLAbyUserIdandDay(userId, dtTime).Count;
            }
            return arrIntsLa;
        }

        /// <summary>
        /// 统计指定时间段的一个周期内用户每天的活动累计时长
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="dtStart">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static IEnumerable<int> MyLaDuringsEveryDay(long userId,DateTime dtStart, DateTime dtEnd)
        {
            int timeSpan = DAL.Common.DateDiff(dtStart, dtEnd);
            var dictLa = new int[timeSpan];
            for (int i = 0; i < timeSpan; i++)
            {
                string dtTime = string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(i));
                var la = DAL.LearningActivityDal.GetLAbyUserIdandDay(userId, dtTime);
                long laCount = la.Count;
                int? during=0;
                for (int j = 0; j < laCount; j++)
                {
                    during += la[j].持续时长;
                }
                dictLa[i] = (int) during;
            }
            return dictLa;
        }

        public static int[] MyLaDurationEveryDay(long userId, DateTime dtStart, DateTime dtEnd)
        {
            int timeSpan = DAL.Common.DateDiff(dtStart, dtEnd);
            var dictLa = new int[timeSpan];
            for (int i = 0; i < timeSpan; i++)
            {
                string dtTime = string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(i));
                var la = DAL.LearningActivityDal.GetLAbyUserIdandDay(userId, dtTime);
                long laCount = la.Count;
                int? during = 0;
                for (int j = 0; j < laCount; j++)
                {
                    during += la[j].持续时长;
                }
                dictLa[i] = (int)during;
            }
            return dictLa;
        }
        public static int MyLaDurationToDay(long userId, DateTime NowDate)
        {    //获取NowDate这天的活动总时长       
                string dtTime = string.Format("{0:yyyy-MM-dd}", NowDate);
                var la = DAL.LearningActivityDal.GetLAbyUserIdandDay(userId, dtTime);//获得当天活动数组
                long laCount = la.Count;
                int? during = 0;
                for (int j = 0; j < laCount; j++)
                {
                    during += la[j].持续时长;
                }               
                return (int)during;
        }

    }
}
