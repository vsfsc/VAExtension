using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace ContestDll.DAL
{
    public class WorksType
    {

        public static List<CSWorksTypeScoreStandard> GetWorksTypeScoreStandardByTypeID(int worksTypeID)
        {
            using (ContestEntities db=new ContestEntities ())
            {
               List <CSWorksTypeScoreStandard> ds = db.CSWorksTypeScoreStandard.Where(p => p.WorkTypeID == worksTypeID).ToList ();
               return ds;
            }

        }

        public static ContestDll.WorksType GetWorksTypeByID(int worksTypeID)
        {
            List<ContestDll.WorksType> wt = DAL.Works.GetWorksType();
            List<ContestDll.WorksType> dr = wt.Where(p => p.WorksTypeID == worksTypeID).ToList();
            if (dr.Count > 0)
                return dr[0];
            else
                return null;
        }
        /// <summary>
        /// 无返回ID
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static int InsertWorksTypeScoreStandard(CSWorksTypeScoreStandard  dr)
        {
 using (ContestEntities db = new ContestEntities())
            {
                WorksTypeScoreStandard wtSS = new WorksTypeScoreStandard();
                wtSS.StandardID = dr.StandardID;
                wtSS.Score = dr.Score;
                wtSS.StandardDescription = dr.StandardDescription;
                wtSS.WorkTypeID = dr.WorkTypeID;
                db.WorksTypeScoreStandard.Add(wtSS);
                db.SaveChanges();
                return 1;
            }       
        }
        public static long UpdateWorksType(WorksTypeScoreStandard dr)
        {
            return 1;
        }
        public static long UpdateWorksTypeScoreStandard(CSWorksTypeScoreStandard dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                WorksTypeScoreStandard wtSS = db.WorksTypeScoreStandard.SingleOrDefault(p => p.StandardID == dr.StandardID);
                wtSS.Score = dr.Score;
                wtSS.StandardDescription = dr.StandardDescription;
                wtSS.WorkTypeID = dr.WorkTypeID;
                db.SaveChanges();
                return 1;
            }
        }
    }
}
