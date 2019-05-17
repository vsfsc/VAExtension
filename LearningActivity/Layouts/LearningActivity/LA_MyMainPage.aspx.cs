using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using LearningActivity.DAL;
using LADLL;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services; 

namespace LearningActivity.Layouts.LearningActivity
{
    public partial class LA_MyMainPage : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request["type"];
            string startDate = Request["startDate"];
            string endDate = Request["endDate"];
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "AWeekBarData":
                        {
                            DateTime dtNow = DateTime.Now;
                            string strStart = string.Format("{0:yyyy-MM-dd}", dtNow.AddDays(-7));
                            string strEnd = string.Format("{0:yyyy-MM-dd}", dtNow);
                            GetCustomDate(strStart, strEnd);
                        }                      
                        break;
                    case "AMonthBarData":
                        {
                            DateTime dtNow = DateTime.Now;
                            string strStart = string.Format("{0:yyyy-MM-dd}", dtNow.AddDays(-30));
                            string strEnd = string.Format("{0:yyyy-MM-dd}", dtNow);
                            GetCustomDate(strStart, strEnd);                        
                        }
                        break;
                    case "CustomData":
                        GetCustomDate(startDate, endDate);
                        break;
                }
            }            
            
        }       
        private void GetCustomDate(string startDate, string endDate)
        {
            int interval_x = 1;
            long userId = DAL.UserDAL.GetUserId();
            List<string> DTList = new List<string>();//获取要在X轴显示的事件链表时间字符串
            List<int> myCountsList = new List<int>();//Y轴我的记录数
            List<int> TotalCountsList = new List<int>();//Y轴总的记录数
            List<int> myDurationList = new List<int>();//Y轴我的时长
            List<int> TotalDurationList = new List<int>();//Y轴总的时长

            DateTime dtEnd = Convert.ToDateTime(endDate);
            DateTime dtStart = Convert.ToDateTime(startDate);
            int days = DAL.Common.DateDiff(dtStart, dtEnd);
            if (days % 15 == 0)
            {
                interval_x = days / 15;
            }
            else{
                interval_x = days / 15 + 1;
            }           
            for (int i = 0; i < days; )
            {
                //X抽填充
                string dtItem = string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(i));
                DTList.Add(dtItem + "(+" + interval_x + ")");
                
                //时间段
                var startT = string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(i));
                var endT = string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(i + interval_x - 1));

                //Y轴填充：统计这个时间段的记录数和时长
                var count1 = 0 ;
                var count2 = 0 ;
                var duration1 = 0;
                var duration2 = 0;
                for (int j = i; j < i+interval_x; j++)
                {
                    count1 = count1 + DAL.LearningActivityDal.GetLACountsbyUserIdandDate(userId, string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(j)));
                    count2 = count2 + DAL.LearningActivityDal.GetLACountsbyUserIdandDate(0, string.Format("{0:yyyy-MM-dd}", dtStart.AddDays(j)));
                    duration1 = duration1 + DAL.Statistics.MyLaDurationToDay(userId, dtStart.AddDays(j));
                    duration2 = duration2 + DAL.Statistics.MyLaDurationToDay(0, dtStart.AddDays(j));
                } 
                myCountsList.Add(count1);
                TotalCountsList.Add(count2);
                myDurationList.Add(duration1);
                TotalDurationList.Add(duration2);

                i = i + interval_x;
            }            
            var newObj = new
            {
                category = DTList,
                myCounts = myCountsList,
                totalCounts = TotalCountsList,
                myDuration = myDurationList,
                totalDuration = TotalDurationList
            };
            Response.Write(JsonConvert.SerializeObject(newObj));//传输数据到JS
            Response.Flush();
            Response.End();
            
        }
       
    }
}
