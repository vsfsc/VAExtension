using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestDll.BLL
{
    public class Period
    {
        /// <summary>
        /// 根据期次ID获取期次的时间设置,并存入数组dateArr
        /// </summary>
        /// <param name="periodId">期次ID</param>
        /// <returns></returns>
        public static DateTime[] GetPeridTimeSets(long periodId)
        {
            CSPeriodsWorksType dtPeriods = DAL.Periods.GetPeriodsByID(periodId) ;
            DateTime[] dateArr = new DateTime[6];//设置数组保存每个时期的六个时间值(上传开始、结束；评分开始、结束；公示开始、结束。)
            if (dtPeriods!=null)
            {
                dateArr[0] = (DateTime)dtPeriods.StartSubmit;
                dateArr[1] = (DateTime)dtPeriods.EndSubmit;
                dateArr[2] = (DateTime)dtPeriods.StartScore;
                dateArr[3] = (DateTime)dtPeriods.EndScore;
                dateArr[4] = (DateTime)dtPeriods.StartPublic;
                dateArr[5] = (DateTime)dtPeriods.EndPublic;
            }
            return dateArr;
        }
        /// <summary>
        /// 期次作品互评分配
        /// </summary>
        /// <param name="periodId"></param>
        /// <param name="resulDt"></param>
        public static void WorksAlloting(long periodId)
        {
            //long workscount = long.Parse(DAL.Works.GetWorksNumByPeriodID(periodId).Tables[0].Rows[0][0].ToString());//获取当前期次上传的作品数量,不包含样例作品

            long allotNum = BLL.Period.SetallotNum(DAL.Works.GetWorksNumByPeriodID(periodId));
            List<long?> dtWaitedUser = DAL.User.GetUserIdByPeriodId(periodId); //初始化数据表以保存待分配的用户ID
            if (dtWaitedUser.Count > 0)//确定本期次可以参与互评的人数
            {
                for (int i = 0; i < dtWaitedUser.Count; i++)
                {
                    long expertId = (long)dtWaitedUser[i];
                     List<CSVWorksForMe> vWorksForMe=DAL.Works.GetWorksForMeByPeriodId(periodId, expertId);
                    long hasAlloted =vWorksForMe.Count;
                    if (hasAlloted < allotNum)   //获取该用户已经分配的作品数，若未满则继续分配，否则则跳过
                    {
                        List<ContestDll.Works >  dtWaitedWorks = DAL.Works.GetWorksToAllot(expertId, periodId, allotNum);//
                        if (dtWaitedWorks.Count > 0)
                        {
                            string[] arrayWaitedWorks = DAL.Common.TableTostrArray(dtWaitedWorks);//待分配作品表转化为数组

                            long shortOf = allotNum - hasAlloted;//获取该用户还差需要分配的作品数
                            string[] arrayToAllot = DAL.Common.GetRandomsArray(shortOf, arrayWaitedWorks);

                            for (int j = 0; j < arrayToAllot.Length; j++)//插入评分分配新纪录
                            {
                                long worksId = long.Parse(arrayToAllot[j].ToString());
                                CSWorksExpertUser dr = new CSWorksExpertUser();
                                dr.WorksID = worksId; //作品ID
                                dr.ExpertID = expertId; //评分用户ID
                                dr.Flag = 1;
                                long aaa = DAL.Works.InsertWorksComments(dr);
                                //为作品分配计数+1,最后一次重置作品状态为2:已分配,评分中
                                ContestDll.Works  dtAllotTimes = DAL.Works.GetWorksAllotTimesByWorsID(worksId);
                                int allottimes =(int)dtAllotTimes.AllotTimes ;
                                ContestDll.Works drAllotTimes = dtAllotTimes;
                                drAllotTimes.WorksID = worksId;
                                drAllotTimes.AllotTimes = allottimes + 1;
                                if (allottimes == allotNum - 1)
                                {
                                    drAllotTimes.WorksState = 2; //最后一次分配,将作品状态置为2:作品评分中
                                }
                                else
                                {
                                    drAllotTimes.WorksState  = (int)drAllotTimes.WorksState ; //分配未完成,保持状态不变
                                }
                                DAL.Works.UpdateWorksAllotTimes(drAllotTimes);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 根据作品总数确定每个评分人可分配的作品个数
        /// </summary>
        /// <param name="worksCount">作品总数</param>
        /// <returns></returns>
        public static long SetallotNum(int worksCount)
        {
            long allotNum = 0;
            if (worksCount > 2 & worksCount <= 9)//作品总数在2-9之间，个数=总数-1
            {
                allotNum = worksCount - 1;
            }
            else if (worksCount > 9)        //作品总数大于9，个数等于9
            {
                allotNum = 9;
            }
            return allotNum;
        }
        public static string GetPeridState(long PeriodId,DateTime nowDateTime)
        {
            string stateStr = "";
            DateTime[] dtArr = GetPeridTimeSets(PeriodId);
            if (nowDateTime < dtArr[0])
            {
                stateStr= "未开始报名";

            }
            if (dtArr[0] <= nowDateTime && nowDateTime <= dtArr[1].AddDays(1))//上传作品时期
            {
                stateStr= "报名提交期";

            }
            else if (dtArr[1].AddDays(1) < nowDateTime && nowDateTime <= dtArr[3].AddDays(1))
            {
                stateStr= "作品评分期";
               
            }
            else if (dtArr[3].AddDays(1) < nowDateTime && nowDateTime <= dtArr[5].AddDays(1))
            {
                stateStr= "作品公示期";
               
            }
            else if (nowDateTime > dtArr[5])
            {
                stateStr= "竞赛已结束";
                
            }
            return stateStr;
        }
        public static List<CSPeriodsCourse> GetPeridsByCourseId(long courseId)
        {
            List<CSPeriodsCourse> ps = DAL.Periods.GetPeriodsList();
            if (courseId!=0)
            {
                ps = ps.Where(p => p.CourseID == courseId).ToList();
            }            
            return ps;
        }
        public static CSPeriodsCourse GetPeridsByPId(long PId)
        {
            CSPeriodsCourse pd = DAL.Periods.GetPeriodsList().FirstOrDefault(ps=>ps.PeriodID==PId);            
            return pd;
        }
    }
}
