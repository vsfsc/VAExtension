using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ContestDll
{
    public partial class AdminPage : LayoutsPageBase
    {
        private static long periodId = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(DAL.Common.GetLoginAccount);
        }
        private static int AllotNum = 9;
        /// <summary>
        /// 为该期次的所有作品分配评分对象
        /// </summary>
        /// <param name="periodId">期次ID</param>
        /// <param name="e"></param>
        protected void setgroup_OnClick(object sender, EventArgs e)
        {
            List<long?>   dtWaitedUser = DAL.User.GetUserIdByPeriodId(periodId);//初始化数据表以保存待分配的用户ID
            if (dtWaitedUser.Count  > 0)
            {
                foreach (long userId in dtWaitedUser)
                {
                    List<Works> dtWaitedWorks = DAL.Works.GetWorksToAllot(userId, periodId, AllotNum);
                    if (dtWaitedWorks.Count > 0)
                    {
                        int allottimes = 0;
                        string[] arrayWaitedWorks = DAL.Common.TableTostrArray(dtWaitedWorks);
                        string[] arrayToAllot = DAL.Common.GetRandomsArray(AllotNum, arrayWaitedWorks);
                        for (int j = 0; j < arrayToAllot.Length; j++)
                        {
                            //插入评分分配新纪录
                            CSWorksExpertUser dr = new CSWorksExpertUser();
                            dr.WorksID = Convert.ToInt64(arrayToAllot[j]);
                            dr.ExpertID = userId;
                            dr.Flag = 1;
                            DAL.Works.InsertWorksComments(dr);
                            //为作品分配计数+1
                            Works dtAllotTimes =
                                DAL.Works.GetWorksAllotTimesByWorsID(Convert.ToInt64(arrayToAllot[j]));
                            allottimes = Convert.ToInt32(dtAllotTimes.AllotTimes);
                            Works dr2 = new Works();
                            dr2.AllotTimes = allottimes + 1;//评分次数+1
                            if (allottimes == 8)
                            {
                                dr2.WorksState = 2;//最后一次分配,将作品状态置为2:作品评分中
                            }
                            //else
                            //{
                            //    dr2["WorksState"] = dr2["WorksState"];//分配未完成,保持状态不变
                            //}
                            DAL.Works.UpdateWorksAllotTimes(dr2);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('该期次没有作品待评分');</script>");
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('分配成功');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('该期次下没有用户参与评分');</script>");
            }
        }
    }
}
