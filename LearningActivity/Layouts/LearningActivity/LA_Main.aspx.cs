using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using LADLL;
using LearningActivity.DAL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Newtonsoft.Json;

namespace LearningActivity.Layouts.LearningActivity
{
    public partial class LA_Main : LayoutsPageBase
    {
        int Indexstore = 0;//存储当前索引
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Indexstore = 0;//第一次加载页面时将Indexstore清零
            }
            long userId = DAL.UserDAL.GetUserId();
            var myla = DAL.LearningActivityDal.GetlearningActivityByUser(userId);
            if (myla.Count <= 0)//该用户的活动数为0
            {
                nolaDiv.Visible = true;
                tabsDiv.Visible = false;

            }
            else
            {//若用户存在记录则加载记录列表
                nolaDiv.Visible = false;
                
                tabsDiv.Visible = true;
                MyatHtml(userId);//生成活动记录列表页面
                string laCount = LaCount(userId);
                if (!string.IsNullOrEmpty(laCount))
                {
                    stat1.InnerHtml = laCount;
                }              
            }

            //获取DrawEcharts3.0.js传来的数据并将所求数据传回使用echarts可视化
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
                    case "LocationPieData":
                        {
                            DateTime dtNow = DateTime.Now;
                            GetLocationPieData(userId, dtNow.AddDays(-10),dtNow);

                        }
                        break;
                    case"TypePieData":
                        {
                            DateTime dtNow = DateTime.Now;
                            GetTypePieData(userId, dtNow.AddDays(-10), dtNow);
                        }                        
                       break;
                }
            }    
        }

        private void GetTypePieData(long userId, DateTime start, DateTime end)
        {
            List<string> LegendList = new List<string>();//获取legend字符串数组
            List<DAL.PieData> myCountsList = new List<DAL.PieData>();//Y轴我的记录数            
            List<DAL.PieData> myDurationList = new List<DAL.PieData>();//Y轴我的时长
            ////填充数据
            ArrayList cType, dType;
            cType = DAL.Statistics.MyLaCountsEveryType(userId, start, end);
            var cTypeStrings = (string[])cType[0];
            var cTypeInts = (int[])cType[1];

            dType = DAL.Statistics.MyLaDuringsEveryType(userId, start, end);
            var dTypeStrings = (string[])dType[0];
            var dTypeInts = (int[])dType[1];
            for (int i = 0; i < cTypeStrings.Length; i++)
            {
                DAL.PieData piedata1 = new PieData();
                piedata1.name = cTypeStrings[i];
                piedata1.value = cTypeInts[i];
                myCountsList.Add(piedata1);

                DAL.PieData piedata2 = new PieData();
                piedata2.name = cTypeStrings[i];
                piedata2.value = dTypeInts[i];
                myDurationList.Add(piedata2);
            }
            var newObj = new
            {
                legend = cTypeStrings,
                myCounts = myCountsList,
                myDuration = myDurationList
            };
            Response.Write(JsonConvert.SerializeObject(newObj, Formatting.None));//传输数据到JS
            Response.Flush();
            Response.End();
        }

        private void GetLocationPieData(long userId, DateTime start,DateTime end)
        {            
            List<string> LegendList = new List<string>();//获取legend字符串数组
            List<DAL.PieData> myCountsList = new List<DAL.PieData>();//Y轴我的记录数            
            List<DAL.PieData> myDurationList = new List<DAL.PieData>();//Y轴我的时长
            ////填充数据
            ArrayList cLoca, dLoca;
            cLoca = DAL.Statistics.MyLaCountsEveryLocation(userId, start, end);
            var cLocaStrings = (string[])cLoca[0];
            var clocaInts = (int[])cLoca[1];

            dLoca = DAL.Statistics.MyLaDuringsEveryLocation(userId, start, end);
            var dLocStrings = (string[])dLoca[0];
            var dLocaInts = (int[])dLoca[1];
            for (int i = 0; i < cLocaStrings.Length; i++)
            {
                //LegendList.Add(cLocaStrings[i]);

                DAL.PieData piedata1 = new PieData();
                piedata1.name = cLocaStrings[i];
                piedata1.value = clocaInts[i];
                myCountsList.Add(piedata1);

                DAL.PieData piedata2 = new PieData();
                piedata2.name = cLocaStrings[i];
                piedata2.value = dLocaInts[i];
                myDurationList.Add(piedata2);
            }
            var newObj = new
            {
                legend = cLocaStrings,
                myCounts = myCountsList,
                myDuration = myDurationList
            };
            Response.Write(JsonConvert.SerializeObject(newObj, Formatting.None));//传输数据到JS
            Response.Flush();
            Response.End();
        }
        private static string LaCount(long userId)
        {
            string laCount = "";
            laCount = "<table class='altrowstable' id='alternatecolor'>";
            laCount += "<tr><th>数据统计</th><th>今日</th><th>昨天</th><th>近一周</th><th>近30天</th><th>所有时间</th></tr>";
            laCount += UserLaCount(userId);//指定用户记录数统计行
            laCount += UserLaCount(0);//所有用户记录数统计行
            laCount += UserLaDurings(userId);
            laCount += UserLaDurings(0);
            laCount += "</table>";
            return laCount;
        }
        private static string UserLaCount(long userId)
        {
            string temphtml = "";
            temphtml = "<tr>";
            if (userId != 0)
            {
                temphtml += "<th>我的记录数</th>";
            }
            else
            {
                temphtml += "<th>所有用户记录数</th>";
            }
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 0).Count.ToString() + "</td>";//今日活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 1).Count.ToString() + "</td>";//昨日活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 6).Count.ToString() + "</td>";//一周内活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 29).Count.ToString() + "</td>";//30天内活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetlearningActivityByUser(userId).Count.ToString() + "</td>";//所有时间范围内的活动记录
            temphtml += "</tr>";
            return temphtml;
        }
        private static string UserLaDurings(long userId)
        {
            string temphtml = "";
            temphtml = "<tr>";
            if (userId != 0)
            {
                temphtml += "<th>我的累计时长</th>";
            }
            else
            {
                temphtml += "<th>所有用户累计时长</th>";
            }
            temphtml += "<td>" + DAL.LearningActivityDal.GetlaDuringsByDate(userId, 0) + "</td>";//今日活动累计时长
            temphtml += "<td>" + DAL.LearningActivityDal.GetlaDuringsByDate(userId, 1) + "</td>";//昨日活动累计时长
            temphtml += "<td>" + DAL.LearningActivityDal.GetlaDuringsByDate(userId, 6) + "</td>";//一周内活动累计时长
            temphtml += "<td>" + DAL.LearningActivityDal.GetlaDuringsByDate(userId, 29) + "</td>";//30天内活动累计时长
            temphtml += "<td>" + DAL.LearningActivityDal.GetlaDurings(DAL.LearningActivityDal.GetLAbyUserIdandDay(userId, "")) + "</td>";//所有时间内活动累计时长
            temphtml += "</tr>";
            return temphtml;
        }


        private void MyatHtml(long userId)
        {
            string ulhtml = "";
            //获取时间段内的活动列表
            List<QuerylearningActivity> myLa = DAL.LearningActivityDal.GetlearningActivityByUser(userId);
            HtmlContainerControl divActity;
            HtmlContainerControl divActityTop;
            int aIndex = Indexstore;//当前要加载的活动索引
           // foreach (var myItem in myLa)
            for (int i = aIndex; i < myLa.Count; i++)
            {
                var myItem = myLa[i];
                divActityTop = new HtmlGenericControl("div");
                divActity = new HtmlGenericControl("div");
                divActity.ID = "divAcity" + aIndex.ToString();
                divActityTop.ID = "divAcityTop" + aIndex.ToString();

                ulhtml = "<span class='myspan1'>" + string.Format("{0:yyyy-MM-dd HH:mm}", myItem.开始时间) + "</span>";
                divActityTop.Controls.Add(new LiteralControl(ulhtml));

                ulhtml = "<span class='myspan2'>" + myItem.活动类型.ToString() + "&nbsp;&nbsp;" + myItem.活动地点.ToString() + " &nbsp;&nbsp;" + myItem.持续时长 + "′&nbsp;&nbsp;</span>";
                divActityTop.Controls.Add(new LiteralControl(ulhtml));
                long currenUserId = DAL.UserDAL.GetUserId();

                if (currenUserId == userId)
                {
                    // addActivity.Visible = true;
                    addActivityBtn.Visible = true;

                    ImageButton iBtnDel = new ImageButton();
                    iBtnDel.ImageUrl = "./images/delete.png";
                    iBtnDel.ToolTip = "删除本条活动记录";
                    iBtnDel.Width = 24;
                    iBtnDel.CommandArgument = myItem.LearningActivityID.ToString();
                    iBtnDel.CommandName = "ItemDel" + aIndex.ToString();
                    iBtnDel.ID = "btnDel" + myItem.LearningActivityID.ToString();
                    iBtnDel.OnClientClick = "javascript:return confirm('确定删除本条活动记录吗?');";
                    iBtnDel.Click += btnDel_Click;
                    iBtnDel.Attributes.Add("class", "btn");
                    divActityTop.Controls.Add(iBtnDel);
                    divActityTop.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));


                    ImageButton btnEdits = new ImageButton();
                    btnEdits.ImageUrl = "./images/edit.png";
                    btnEdits.ToolTip = "修改本条活动记录";
                    btnEdits.Width = 24;
                    btnEdits.CommandArgument = myItem.LearningActivityID.ToString();
                    btnEdits.ID = "btnEdits" + myItem.LearningActivityID.ToString();
                    btnEdits.Click += btnEdit_Click;
                    btnEdits.Attributes.Add("class", "btn");
                    divActityTop.Controls.Add(btnEdits);

                }
                else
                {
                    //addActivity.Visible = false;
                    addActivityBtn.Visible = false;
                }

                ulhtml = Getdetails("学习对象", myItem.学习对象.ToString());
                ulhtml += Getdetails("对象描述", myItem.对象描述.ToString());
                ulhtml += Getdetails("学习内容", myItem.学习内容.ToString());
                ulhtml += Getdetails("活动描述", myItem.活动描述.ToString());
                if (myItem.作品地址 != "" && myItem.作品地址.Length > 5)
                {
                    ulhtml += Getdetails("作品地址", "<a href='" + myItem.作品地址 + "'target='_blank'>点击查看</a>");
                }
                divActity.Controls.Add(new LiteralControl(ulhtml));

                //为生成的div添加样式
                if (aIndex % 2 == 0)
                {
                    divActityTop.Attributes.Add("class", "divSingleTop");
                    divActity.Attributes.Add("class", "divSingle");
                }
                else
                {
                    divActityTop.Attributes.Add("class", "divDoubleTop");
                    divActity.Attributes.Add("class", "divDouble");
                }
                myat.Controls.Add(divActityTop);
                myat.Controls.Add(divActity);

                if (aIndex>0&&aIndex % 5 == 0)//判断加载的条数够了没有，每次显示20条
                {
                    aIndex = aIndex + 1;
                    break;
                }
                aIndex = aIndex + 1;

            }
            Indexstore = aIndex;
            ImageButton showNextListBtn = new ImageButton();
            showNextListBtn.Visible = true;
            showNextListBtn.ImageUrl = "./images/arrow_down.png";
            showNextListBtn.ToolTip = "显示更多";
            showNextListBtn.Click += showNextListBtn_Click;
            myat.Controls.Add(showNextListBtn);

        }

        private void showNextListBtn_Click(object sender, ImageClickEventArgs e)
        {
            //DAL.Common.Alert("索引到：" + Indexstore);
            long userId = DAL.UserDAL.GetUserId();
            ImageButton btn = (ImageButton)sender;//获取按钮
            if (btn.Visible == true)
            {
                btn.Visible = false;
                MyatHtml(userId);
                //btn.Visible = true;
            }
            //DAL.Common.Alert("索引到：" + Indexstore);          
        }

        void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            //DAL.Common.Alert(e.ToString());
            long userId = DAL.UserDAL.GetUserId();
            ImageButton btn = (ImageButton)sender;
            DAL.LearningActivityDal.DelActivity(long.Parse(btn.CommandArgument));
            myat.Controls.Clear();
            MyatHtml(userId);
            // myat.Controls.RemoveAt(int.Parse (btn.CommandName));

        }

        void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string atId = HttpUtility.UrlEncode(btn.CommandArgument);
            Response.Redirect("LA_Activity.aspx?ad=" + atId);//  跳转到新增活动页面

        }

        private static string Getdetails(string biaoqian, string neirong)
        {
            string detailsHtml = "<div class='onlineEnroll-model'>";
            //detailsHtml += "<div class='wt'>" + biaoqian + ":</div>";//标签
            detailsHtml += "<div class='wt'>" + biaoqian + ":&nbsp;&nbsp;" + neirong + "</div>";//内容
            detailsHtml += "</div>";
            return detailsHtml;
        }     
        protected void addActivity_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("LA_Activity.aspx");//  跳转到新增活动页面
        }

        protected void addActivityBtn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("LA_Activity.aspx");//  跳转到新增活动页面
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
            else
            {
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
                var count1 = 0;
                var count2 = 0;
                var duration1 = 0;
                var duration2 = 0;
               // count1 = DAL.LearningActivityDal.GetLACountsbyUserIdandDuration(userId, dtStart.AddDays(i), dtStart.AddDays(i + interval_x - 1));
                //count2 = DAL.LearningActivityDal.GetLACountsbyUserIdandDuration(0, dtStart.AddDays(i), dtStart.AddDays(i + interval_x - 1));
                for (int j = i; j < i + interval_x; j++)
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
