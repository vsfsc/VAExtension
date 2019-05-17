using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ContestDll.DAL
{
    public class Periods
    {

        public static long UpdatePeriodsByID(ContestDll. Periods  dr)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                ContestDll.Periods pd = db.Periods.SingleOrDefault(p => p.PeriodID == dr.PeriodID);
                pd.Modified = dr.Modified;
                pd.ModifiedBy = dr.ModifiedBy;
                pd.Number = dr.Number;
                pd.PeriodStandard = dr.PeriodStandard;
                pd.PeriodTitle = dr.PeriodTitle;
                pd.Require = dr.Require;
                pd.StartPublic = dr.StartPublic;
                pd.StartScore = dr.StartScore;
                pd.StartSubmit = dr.StartSubmit;
                pd.WorksTypeID = dr.WorksTypeID;
                db.SaveChanges();
            }
            return 1;
        }
        public static long DelPeriodsByID(ContestDll.Periods dr, long editorId,DateTime ediTime)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.Periods pd = db.Periods.SingleOrDefault(p => p.PeriodID == dr.PeriodID);
                pd.ModifiedBy = editorId;
                pd.Modified = ediTime;
                pd.Flag = 0;
                db.SaveChanges();
                return 1;
            }

        }
        public static List<CSPeriodsCourse> GetPeriodsList()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSPeriodsCourse> ps = db.CSPeriodsCourse.ToList();
                return ps;
            }
        }
        public static CSPeriodsWorksType  GetPeriodsByID(long PeriodID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                CSPeriodsWorksType  ps = db.CSPeriodsWorksType .FirstOrDefault(p => p.PeriodID == PeriodID);
                return ps;
            }
        }
        public static ContestDll. Periods  GetPeriodsByTitle(string periodsTitle)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.Periods ps = db.Periods.FirstOrDefault(p => p.PeriodTitle == periodsTitle);
                return ps;
            }
        }
        public static long InsertPeriods(ContestDll.Periods dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.Periods pd = new ContestDll.Periods();
                pd.Flag = dr.Flag;
                pd.CourseID = dr.CourseID;
                pd.Created = dr.Created;
                pd.CreatedBy = dr.CreatedBy;
                pd.EndPublic = dr.EndPublic;
                pd.EndScore = dr.EndScore;
                pd.EndSubmit = dr.EndSubmit;
                pd.Modified = dr.Modified;
                pd.ModifiedBy = dr.ModifiedBy;
                pd.Number = dr.Number;
                pd.PeriodStandard = dr.PeriodStandard;
                pd.PeriodTitle = dr.PeriodTitle;
                pd.Require = dr.Require;
                pd.StartPublic = dr.StartPublic;
                pd.StartScore = dr.StartScore;
                pd.StartSubmit = dr.StartSubmit;
                pd.WorksTypeID = dr.WorksTypeID;
                db.Periods.Add(pd);
                db.SaveChanges();
                return pd.PeriodID; ;
            }
        }
        /// <summary>
        /// 给竞赛无关
        /// </summary>
        /// <returns></returns>
        public static List <CSPeriodsWorksType>  GetPeriodByCourseID()
        {
            long courseID = BLL.Course.GetCourseID();
           using (ContestEntities db=new ContestEntities ())
           {
               List<CSPeriodsWorksType> pw = db.CSPeriodsWorksType.Where(p => p.CourseID == courseID && p.Flag >0).ToList();
               return pw;
           }
        }
        /// <summary>
        /// 获取所有期次
        /// </summary>
        /// <returns></returns>
        public static List <ContestDll.Periods > GetPeriods()
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List <ContestDll.Periods> pd = db.Periods.ToList();
                return pd;
            }
        }
        /// <summary>
        /// 根据用户ID获取该用户创建的所有期次
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static List<CSPeriodsCourse> GetPeriodByUserId(long userId)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSPeriodsCourse> pd = db.CSPeriodsCourse.Where(p=>p.CreatedBy==userId ) .ToList();
                return pd;
            }
        }

        //public static IQueryable<ContestEntities.Periods> Get1StPeriodByBeginStr(string CodeBiginStr)
        //{
        //    using (ContestEntities db = new ContestEntities())
        //    {
        //        var pd = db.Periods.Where(p => p.PeriodID == 1).OrderByDescending(p=>p.PeriodID).Take(1);
        //        return pd;
        //    }
        //}
    }
}
