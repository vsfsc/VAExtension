using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;

namespace ContestDll.BLL
{
    public class Works
    {
        /// <summary>
        /// 作品分到指定专家
        /// </summary>
        /// <param name="drExpert"></param>
        /// <param name="worksType"></param>
        /// <returns></returns>
        public static bool InsertWorksExpert(long worksID, List<long> experts, params object[] finals )
        {
            List<WorksExpert > dsType = DAL.Works.GetWorksExpertByID(0);
            ContestEntities db = new ContestEntities();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    WorksExpert drRow;
                    foreach (long type1 in experts)
                    {
                        drRow = new WorksExpert();
                        drRow.WorksID = worksID;
                        drRow.ExpertID = type1;
                        if (finals.Length > 0)
                        {
                            drRow.Score = (float)finals[0];
                            drRow.Flag = (long)finals[1];
                        }
                        else
                        {
                            drRow.Score = 0;
                            drRow.Flag = 1;
                        }
                        DAL.Works.InsertWorksExpert(db, drRow);

                    }
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
        /// <summary>
        /// 创建列表"workVideo" "workPic",  "workFile"
        /// </summary>
        public static SPList CreateList(string listName)
        {
            SPListCollection col = DAL.Common.SPWeb.Lists;
            Guid guid;
            if (listName == "workPic")
            {
                guid = col.Add(listName, "", SPListTemplateType.PictureLibrary);

            }
            else
            {
                guid = col.Add(listName, "", SPListTemplateType.DocumentLibrary);

            }
            SPList list = col.GetList(guid, false);
            list.Hidden = true;
            list.Update();
            return list;

        }
        public static long GetWorksIDByPriod(long periodID)
        {
            string account = DAL.Common.GetLoginAccount;
            CSUserWorks  ds = DAL.Works.GetWorksByPeriodAndAccount(periodID, account);
            long worksID = 0;
            if (ds != null)
                worksID = ds.WorksID;
            return worksID;
        }
        /// <summary>
        /// 作品评分
        /// </summary>
        /// <returns></returns>
        public static int WorksScore(long worksExpertID, long worksID, long expertID, SortedList<int, int> scoreDeltails, string txtPingYu)
        {
            List<RatingDetails> dtDetail = DAL.Works.GetRatingDetailsByWorksExpertID(worksExpertID);

             ContestEntities db = new ContestEntities();
             using (TransactionScope scope = new TransactionScope())
             {
                 try
                 {
                     int scores = 0;
                     for (int i = 0; i < scoreDeltails.Count; i++)
                     {
                         int standardID = scoreDeltails.Keys[i];
                         RatingDetails dr;
                         List<RatingDetails> drs;
                         drs = dtDetail.Where(p=>p.StandardID ==standardID ).ToList() ;// ("StandardID=" + standardID);
                         if (drs.Count  > 0)
                             dr = drs[0];
                         else
                         {
                             dr = new RatingDetails();
                             dr.WorksExpertID = worksExpertID;
                             dr.StandardID  = standardID;
                             dr.Flag = 1;
                         }
                         dr.Score = scoreDeltails[standardID];
                         if (drs.Count  == 0)
                             DAL.Works.InsertRatingDetails(db, dr);
                         else
                             DAL.Works.UpdateRatingDetails(db, dr);

                         scores += scoreDeltails[standardID];
                     }
                     WorksExpert  drWorksExpert = DAL.Works.GetWorksExpertByID(worksExpertID)[0];
                     drWorksExpert.Score = scores;
                     drWorksExpert.Comments = txtPingYu;
                     drWorksExpert.Modified = DateTime.Now;

                     DAL.Works.UpdateWorksExpert(db, drWorksExpert);
                     scope.Complete();
                     return 1;
                 }
                 catch
                 {
                     scope.Dispose();
                     return 0;
                 }
             }

        }
        public static long SampleScore(long worksExpertID, long worksID, long expertID, SortedList<int, int> scoreDeltails, string txtPingYu)
        {
            ContestEntities db = new ContestEntities();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    float scores = 0;
                    for (int i = 0; i < scoreDeltails.Count; i++)
                    {
                        int standardID = scoreDeltails.Keys[i];
                        scores += scoreDeltails[standardID];
                    }
                    long flag = GetScoreResult(worksID, scores);
                    List<WorksExpert>  ds = DAL.Works.GetWorksExpertByID(worksExpertID);

                    WorksExpert drWorksExpert;
                    if (ds.Count  > 0)
                        drWorksExpert = ds[0];
                    else
                    {
                        drWorksExpert = new WorksExpert();
                        drWorksExpert.WorksID = worksID;
                        drWorksExpert.ExpertID = expertID;
                    }
                    drWorksExpert.Comments = txtPingYu;
                    drWorksExpert.Score = scores;
                    drWorksExpert.Flag = flag;//样式作品评价训练通过
                    if (worksExpertID == 0)
                    {
                        drWorksExpert.Created = DateTime.Now;
                        worksExpertID = DAL.User.InsertWorksExpert(db,drWorksExpert);

                    }
                    else
                    {
                        drWorksExpert.Modified = DateTime.Now;
                        DAL.Works .UpdateWorksExpert(drWorksExpert);
                    }

                    List<RatingDetails> dtDetail = DAL.Works.GetRatingDetailsByWorksExpertID(worksExpertID);
                    for (int i = 0; i < scoreDeltails.Count; i++)
                    {
                        int standardID = scoreDeltails.Keys[i];
                        RatingDetails dr;
                        List<RatingDetails> drs;
                        drs = dtDetail.Where(p=>p.StandardID ==standardID ).ToList ();// ("StandardID=" + standardID);
                        if (drs.Count  > 0)
                            dr = drs[0];
                        else
                        {
                            dr = new RatingDetails();
                            dr.WorksExpertID  = worksExpertID;
                            dr.StandardID = standardID;
                            dr.Flag = 1;
                        }
                        dr.Score = scoreDeltails[standardID];
                        if (drs.Count  == 0)
                            DAL.Works.InsertRatingDetails(db, dr);
                        else
                            DAL.Works.UpdateRatingDetails(db, dr);
                    }
                    ////执行多个操作
                    //var user1 = new User
                    //{
                    //    Name = "bomo",
                    //    Age = 21,
                    //    Gender = "male"
                    //};
                    //db.User.Add(user1);
                    //db.SaveChanges();

                    //var user2 = new User
                    //{
                    //    Name = "toroto",
                    //    Age = 20,
                    //    Gender = "female"
                    //};
                    //db.User.Add(user2);
                    db.SaveChanges();
                    //提交事务
                    scope.Complete();
                    return flag;
                }
                catch (Exception ex)
                {
                    scope.Dispose ();
                    return 0;
                }
            }
        }
        /// <summary>
        /// 老师给出的样例分real single 
        /// 样例作品评价训练，3-不通过，4-通过
        /// </summary>
        /// <param name="worksID"></param>
        /// <returns></returns>
        public static long GetScoreResult(long worksID, Single studentScore)
        {
            CSWorksWorksType ds = DAL.Works.GetWorksByWorksID(worksID);
            Single standScore = 0;
            if (ds != null && ds.Score != null)
                standScore = (Single)ds.Score;
            if (studentScore >= standScore * 0.95 && studentScore <= standScore * 1.05)
                return 4;
            else
                return 3;
        }
    }
}
