using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestDll.BLL
{
    public class WorksScoring
    {
        /// <summary>
        /// 计算指定期次下作品的分值并更新到作品表
        /// </summary>
        /// <param name="periodID"></param>
        public static int ComputerAllScoresByPeriod(long periodID)
        {
            List<CSWorksWorksType> ds = DAL.Works.GetWorksByPeriodID(periodID);
            Single result;
            ContestEntities db = new ContestEntities();
            using (TransactionScope trans =new TransactionScope ())
                try
                {
                    foreach (CSWorksWorksType dr in ds )
                    {
                        result = ComputerPerworksScore( dr.WorksID);
                        if (result > 0)
                        {
                            dr.Score = result;
                            dr.WorksState = 3;
                            DAL.Works.UpdateWorksSubmit(db, dr);
                        }
                    }
                    trans.Complete();
                    return 1;
                }
                catch (Exception ex)
                {
                    
                    return 0;
                }
        }
        /// <summary>
        /// 计算成绩(去掉0分)，去掉最高、最低两个成绩，其他求平均分
        /// </summary>
        public static Single ComputerPerworksScore(long worksID)
        {
            List<WorksExpert>  ds = DAL.Works.GetWorksExpertByWorksID(worksID);
            List<WorksExpert> drs = ds.Where (p=>p.Score>0 ).ToList ();
            if (drs.Count  == 0)
                return 0;
            int i, j;
            if (drs.Count > 6)
            {
                i = 2;
                j = drs.Count - 3;

            }
            else if (drs.Count > 4)
            {
                i = 1;
                j = drs.Count - 2;
            }
            else
            {
                i = 0;
                j = drs.Count - 1;
            }
            Single scores = 0;
            WorksExpert dr;
            int k = i;
            while (i <= j)
            {
                dr = drs[i];
                scores += (Single)dr.Score;
                i += 1;
            }
            scores = scores / (j - k + 1);
            return scores;
        }
    }
}
