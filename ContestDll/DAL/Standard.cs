using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ContestDll.DAL
{
    public class Standard
    {
        /// <summary>
        /// 更新期次标准
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long UpdatePeriodStandard(CSPeriodScoreStandard  dr)
        {
            using (ContestEntities db = new ContestEntities())
            {

                PeriodStandard loginUser = db.PeriodStandard.Where(p => p.WorkTypeID == dr.WorkTypeID && p.PeriodID == dr.PeriodID && p.StandardID == dr.StandardID).SingleOrDefault();
                if (loginUser != null)
                {
                    loginUser.StandardDescription = dr.StandardDescription;
                    loginUser.Score = dr.Score;
                    loginUser.Modified = dr.Modified;
                    loginUser.ModifiedBy = dr.ModifiedBy;
                    loginUser.Flag = dr.Flag;
                    db.SaveChanges();
                    return 1;
                }
                return 0;

            }
        }
        /// <summary>
        /// 作品评分明细
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertPeriodStandard(PeriodStandard  dr)
        {
           using ( ContestEntities db = new ContestEntities())
           {
               PeriodStandard  pS= new PeriodStandard();
               pS.WorkTypeID = dr.WorkTypeID;
               pS.PeriodID = dr.PeriodID;
               pS.Score = dr.Score;
               pS.StandardDescription = dr.StandardDescription;
               pS.StandardID = dr.StandardID;
               pS.Created = dr.Created;
               pS.CreatedBy = dr.CreatedBy;
               pS.Flag = dr.Flag;
               db.PeriodStandard.Add(pS);
               db.SaveChanges();
               return 1 ;
           }
        }
    }
}
