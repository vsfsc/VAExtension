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

namespace LearningActivity.Layouts.LearningActivity
{
    public partial class LA_My : LayoutsPageBase
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
                long userId = DAL.UserDAL.GetUserId();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ud"])) //判断Url中是否传递了用户ID,如果有,则是查看他人,如果无,则是查看自己
                {
                    userId = long.Parse(Request.QueryString["ud"]);
                }
            }   

                var myla = DAL.LearningActivityDal.GetlearningActivityByUser(userId);
                if (myla.Count <= 0)//该用户未创建过活动
                {
                    nolaDiv.Visible = true;
                    mycontent.Visible = false;
                }
                else
                {
                    MyatHtml(userId);
                    string laCount = LaCount(userId);
                    if (!string.IsNullOrEmpty(laCount))
                    {
                        stat1.InnerHtml = laCount;
                    }

                    ArrayList cLoca, dLoca, cType, dType;
                    DateTime dtNow = DateTime.Now;
                    DateTime dt7DaysAgo = dtNow.AddDays(-7);
                    startDT.SelectedDate = dt7DaysAgo;
                    endDT.SelectedDate = dtNow;
                    //默认加载近一周之内的数据

                    //1 按日期统计
                    var dateStrings = DAL.Common.DtStrings(dt7DaysAgo, dtNow);
                    var c1 = DAL.Statistics.MyLaCountsEveryDay(userId, dt7DaysAgo, dtNow);
                    var c2 = DAL.Statistics.MyLaCountsEveryDay(0, dt7DaysAgo, dtNow);
                    var d1 = DAL.Statistics.MyLaDuringsEveryDay(userId, dt7DaysAgo, dtNow);
                    var d2 = DAL.Statistics.MyLaDuringsEveryDay(0, dt7DaysAgo, dtNow);

                    ctDay.Series[0].Points.DataBindXY(dateStrings, d1);
                    ctDay.Series[1].Points.DataBindXY(dateStrings, d2);
                    ctDay.Series[2].Points.DataBindXY(dateStrings, c1);
                    ctDay.Series[3].Points.DataBindXY(dateStrings, c2);
                    //2 按地点统计饼图
                    // ctLoca.Series[0] ;//次数饼图
                    // ctLoca.Series[1];//持续时长饼图
                    cLoca = DAL.Statistics.MyLaCountsEveryLocation(userId, dt7DaysAgo, dtNow);
                    var cLocaStrings = (string[])cLoca[0];
                    var clocaInts = (int[])cLoca[1];
                    ctLoca.Series[0].Points.DataBindXY(cLocaStrings, clocaInts);

                    dLoca = DAL.Statistics.MyLaDuringsEveryLocation(userId, dt7DaysAgo, dtNow);
                    var dLocStrings = (string[])dLoca[0];
                    var dLocaInts = (int?[])dLoca[1];
                    ctLoca.Series[1].Points.DataBindXY(dLocStrings, dLocaInts);
                    ctLoca.Titles.Add("按地点统计用户的记录数(单位:条)和累计时长(单位:分钟)");

                    //3 按类别统计饼图
                    //ctTypeCount.Series["s1"].ChartType = SeriesChartType.Pie;//次数饼图
                    //ctTypeDuring.Series["s1"].ChartType = SeriesChartType.Pie;//持续时长饼图
                    cType = DAL.Statistics.MyLaCountsEveryType(userId, dt7DaysAgo, dtNow);

                    var cTypeStrings = (string[])cType[0];
                    var cTypeInts = (int[])cType[1];
                    ctType.Series[0].Points.DataBindXY(cTypeStrings, cTypeInts);
                    dType = DAL.Statistics.MyLaDuringsEveryType(userId, dt7DaysAgo, dtNow);

                    var dTypeStrings = (string[])dType[0];
                    var dTypeInts = (int?[])dType[1];
                    ctType.Series[1].Points.DataBindXY(dTypeStrings, dTypeInts);

                    ctType.Titles.Add("按类别统计用户的记录数(单位:条)和累计时长(单位:分钟)");
                    //ViewChart(ctLocaCount, cDictionary, "分地点活动记录次数统计");
                }

        }

        protected void startDT_OnDateChanged(object sender, EventArgs e)
        {
            long userId = DAL.UserDAL.GetUserId();

            DateTime dtS = startDT.SelectedDate;
            DateTime dtE = endDT.SelectedDate;
            if (DAL.Common.DateDiff(dtS, dtE) < 3)
            {
                DAL.Common.Alert("起始日期与结束日期时间之差不能小于 3 天");
                endDT.Focus();
            }
            else
            {
                string[] dateStrings = DAL.Common.DtStrings(dtS, dtE);
                int[] c1 = DAL.Statistics.MyLaCountsEveryDay(userId, dtS, dtE);
                int[] c2 = DAL.Statistics.MyLaCountsEveryDay(0, dtS, dtE);
                IEnumerable<int> d1 = DAL.Statistics.MyLaDuringsEveryDay(userId, dtS, dtE);
                IEnumerable<int> d2 = DAL.Statistics.MyLaDuringsEveryDay(0, dtS, dtE);

                ctDay.Series[2].Points.DataBindXY(dateStrings, c1);
                ctDay.Series[3].Points.DataBindXY(dateStrings, c2);

                ctDay.Series[0].Points.DataBindXY(dateStrings, d1);
                ctDay.Series[1].Points.DataBindXY(dateStrings, d2);

                //2 按地点统计饼图
                // ctLoca.Series[0] ;//次数饼图
                // ctLoca.Series[1];//持续时长饼图
                ArrayList cLoca, dLoca, cType, dType;
                cLoca = DAL.Statistics.MyLaCountsEveryLocation(userId, dtS, dtE);
                var cLocaStrings = (string[])cLoca[0];
                var clocaInts = (int[])cLoca[1];
                ctLoca.Series[0].Points.DataBindXY(cLocaStrings, clocaInts);

                dLoca = DAL.Statistics.MyLaDuringsEveryLocation(userId, dtS, dtE);
                var dLocStrings = (string[])dLoca[0];
                var dLocaInts = (int?[])dLoca[1];
                ctLoca.Series[1].Points.DataBindXY(dLocStrings, dLocaInts);
                ctLoca.Titles.Add("按地点统计用户的记录数(单位:条)和累计时长(单位:分钟)");

                //3 按类别统计饼图
                //ctTypeCount.Series["s1"].ChartType = SeriesChartType.Pie;//次数饼图
                //ctTypeDuring.Series["s1"].ChartType = SeriesChartType.Pie;//持续时长饼图
                cType = DAL.Statistics.MyLaCountsEveryType(userId, dtS, dtE);

                var cTypeStrings = (string[])cType[0];
                var cTypeInts = (int[])cType[1];
                ctType.Series[0].Points.DataBindXY(cTypeStrings, cTypeInts);
                dType = DAL.Statistics.MyLaDuringsEveryType(userId, dtS, dtE);

                var dTypeStrings = (string[])dType[0];
                var dTypeInts = (int?[])dType[1];
                ctType.Series[1].Points.DataBindXY(dTypeStrings, dTypeInts);

                ctType.Titles.Add("按类别统计用户的记录数(单位:条)和累计时长(单位:分钟)");
            }
        }

        protected void endDT_OnDateChanged(object sender, EventArgs e)
        {
            long userId = DAL.UserDAL.GetUserId();
            DateTime dtS, dtE;
            if (startDT.IsDateEmpty)
            {
                DAL.Common.Alert("请先选择起始日期!");
                startDT.Focus();
                return;
            }
            dtS = startDT.SelectedDate;
            dtE = endDT.SelectedDate;
            if (DAL.Common.DateDiff(dtS, dtE) < 3)
            {
                DAL.Common.Alert("起始日期与结束日期时间之差不能小于 3 天");
                endDT.Focus();
            }
            else
            {
                string[] dateStrings = DAL.Common.DtStrings(dtS, dtE);
                int[] c1 = DAL.Statistics.MyLaCountsEveryDay(userId, dtS, dtE);
                int[] c2 = DAL.Statistics.MyLaCountsEveryDay(0, dtS, dtE);
                var d1 = DAL.Statistics.MyLaDuringsEveryDay(userId, dtS, dtE);
                var d2 = DAL.Statistics.MyLaDuringsEveryDay(0, dtS, dtE);


                //ctDayCount.Series[0].Points.DataBindXY(dateStrings, c1);
                //ctDayCount.Series[1].Points.DataBindXY(dateStrings, c2);


                var yValues1 = d1 as int[] ?? d1.ToArray();
                var yValues2 = d2 as int[] ?? d2.ToArray();
                ctDay.Series[0].Points.DataBindXY(dateStrings, yValues1);
                ctDay.Series[1].Points.DataBindXY(dateStrings, yValues2);
                ctDay.Series[2].Points.DataBindXY(dateStrings, c1);
                ctDay.Series[3].Points.DataBindXY(dateStrings, c2);



                //2 按地点统计饼图
                // ctLoca.Series[0] ;//次数饼图
                // ctLoca.Series[1];//持续时长饼图
                ArrayList cLoca, dLoca, cType, dType;
                cLoca = DAL.Statistics.MyLaCountsEveryLocation(userId, dtS, dtE);
                var cLocaStrings = (string[])cLoca[0];
                var clocaInts = (int[])cLoca[1];
                ctLoca.Series[0].Points.DataBindXY(cLocaStrings, clocaInts);

                dLoca = DAL.Statistics.MyLaDuringsEveryLocation(userId, dtS, dtE);
                var dLocStrings = (string[])dLoca[0];
                var dLocaInts = (int?[])dLoca[1];
                ctLoca.Series[1].Points.DataBindXY(dLocStrings, dLocaInts);
                ctLoca.Titles.Add("按地点统计用户的记录数(单位:条)和累计时长(单位:分钟)");

                //3 按类别统计饼图
                //ctTypeCount.Series["s1"].ChartType = SeriesChartType.Pie;//次数饼图
                //ctTypeDuring.Series["s1"].ChartType = SeriesChartType.Pie;//持续时长饼图
                cType = DAL.Statistics.MyLaCountsEveryType(userId, dtS, dtE);

                var cTypeStrings = (string[])cType[0];
                var cTypeInts = (int[])cType[1];
                ctType.Series[0].Points.DataBindXY(cTypeStrings, cTypeInts);
                dType = DAL.Statistics.MyLaDuringsEveryType(userId, dtS, dtE);

                var dTypeStrings = (string[])dType[0];
                var dTypeInts = (int?[])dType[1];
                ctType.Series[1].Points.DataBindXY(dTypeStrings, dTypeInts);

                ctType.Titles.Add("按类别统计用户的记录数(单位:条)和累计时长(单位:分钟)");
            }
        }

        //饼图   dt数据结构为 columndata(数据)  columnname(文本) 这两列
        private void ViewChart(Chart ctChart, Dictionary<string, int> dictionary, string title)
        {
            ctChart.Series.Clear();
            ctChart.Legends.Clear();

            ctChart.Series.Add(new Series("Data"));
            ctChart.Legends.Add(new Legend("Stores")); //右边标签列

            ctChart.Series["Data"].ChartType = SeriesChartType.Pie;
            ctChart.Series["Data"]["PieLabelStyle"] = "Inside";//Inside 数值显示在圆饼内 Outside 数值显示在圆饼外 Disabled 不显示数值

            ctChart.Series["Data"]["PieLineColor"] = "Black";


            //ctChart.Series["Data"].IsValueShownAsLabel = true;
            //ctChart.Series["Data"].IsVisibleInLegend = true;
            //ctChart.Series["Data"].ShadowOffset = 1;//阴影偏移量

            ctChart.Series["Data"].ToolTip = "#VAL{D} 次";//鼠标移动到上面显示的文字
            ctChart.Series["Data"].BackSecondaryColor = Color.DarkCyan;
            ctChart.Series["Data"].BorderColor = Color.DarkOliveGreen;
            ctChart.Series["Data"].LabelBackColor = Color.Transparent;
            for (int i = 0; i < dictionary.Count; i++)
            {

            }
            foreach (KeyValuePair<string, int> kv in dictionary)
            {

                int ptIdx = ctChart.Series["Data"].Points.AddY(Convert.ToDouble(kv.Value.ToString()));
                DataPoint pt = ctChart.Series["Data"].Points[ptIdx];
                pt.LegendText = kv.Key.ToString() + " " + "#PERCENT{P2}" + " [ " + "#VAL{D} 次" + " ]";//右边标签列显示的文字
                pt.Label = kv.Key.ToString() + " " + "#PERCENT{P2}" + " [ " + "#VAL{D} 次" + " ]"; //圆饼外显示的信息


                //  pt.LabelToolTip = "#PERCENT{P2}";
                //pt.LabelBorderColor = Color.Red;//文字背景色 
            }

            // ctChart.Series["Data"].Label = "#PERCENT{P2}"; //
            ctChart.Series["Data"].Font = new Font("Segoe UI", 8.0f, FontStyle.Bold);
            ctChart.Series["Data"].Legend = "Stores"; //右边标签列显示信息
            ctChart.Legends["Stores"].Alignment = StringAlignment.Center;
            //ctChart.Legends["Stores"].HeaderSeparator =Chart.Charting.LegendSeparatorStyle.ThickLine;


            ctChart.Titles[0].Text = title;
            ctChart.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            ctChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            int intCount = dictionary.Values.Sum();
            ctChart.Titles[1].Text = "总次数: " + intCount.ToString("0.00") + " 人";

            /*
            　#VALX      显示当前图例的X轴的对应文本(或数据)
　　#VAL, #VALY,  显示当前图例的Y轴的对应文本(或数据)
　　#VALY2, #VALY3, 显示当前图例的辅助Y轴的对应文本(或数据)
　　#SER:      显示当前图例的名称
　　#LABEL       显示当前图例的标签文本
　　#INDEX      显示当前图例的索引
　　#PERCENT       显示当前图例的所占的百分比
　　#TOTAL      总数量
　　#LEGENDTEXT      图例文本
             */
        }

        /// <summary>
        /// 准备数据源
        /// </summary>
        /// <returns>数据源</returns>
        public DataTable PrepareData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("subject", typeof(string));
            dt.Columns.Add("score", typeof(float));
            dt.Rows.Add("数学", 80);
            dt.Rows.Add("语文", 89);
            dt.Rows.Add("英语", 97);
            dt.Rows.Add("物理", 78);
            dt.Rows.Add("化学", 76);
            return dt;
        }

        /// <summary>
        /// 图表绑定
        /// </summary>
        /// <param name="chartType">图表类型</param>
        public void ChartBind(SeriesChartType chartType)
        {
            DataTable dt = PrepareData();
            ctDay.Series[0].Points.DataBind(dt.DefaultView, "subject", "score", "LegendText=subject,YValues=score,ToolTip=subject");
            ctDay.Series[0].ChartType = chartType;//图表类型
            ctDay.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctId">ChartID</param>
        /// <param name="i"></param>
        /// <param name="chartType"></param>
        /// <param name="dt"></param>
        public void BindChart(Chart ctId,int i,SeriesChartType chartType,DataTable dt)
        {
            string subject = dt.Columns[0].ColumnName;//项目列
            double amount = double.Parse(dt.Columns[1].ColumnName);//数据列
            ctDay.Series[i].Points.DataBind(dt.DefaultView, "subject", "score", "LegendText=subject,YValues=score,ToolTip=subject");
            ctDay.Series[i].ChartType = chartType;//图表类型
            ctDay.DataBind();
        }

       

        /// <summary>
        /// 下拉框选择
        /// </summary>
        protected void ddlSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeriesChartType chartType;
            string value = "";// this.ddlSelectType.SelectedValue;
            switch (value)
            {
                case "Column": chartType = SeriesChartType.Column; break;//柱状图
                case "Pie": chartType = SeriesChartType.Pie; break;//饼图
                case "Line": chartType = SeriesChartType.Line; break;//折线图
                default: chartType = SeriesChartType.Column; break;
            }
            ChartBind(chartType);
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
            if (userId!=0)
            {
                temphtml += "<th>我的记录数</th>";
            }
            else
            {
                temphtml += "<th>所有用户记录数</th>";
            }
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId ,0).Count.ToString()+ "</td>";//今日活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 1).Count.ToString() + "</td>";//昨日活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 6).Count.ToString() + "</td>";//一周内活动记录数
            temphtml += "<td>" + DAL.LearningActivityDal.GetLAbyUserIdandDate(userId, 29).Count.ToString() + "</td>";//30天内活动记录数
            temphtml += "<td>" +DAL.LearningActivityDal.GetlearningActivityByUser(userId).Count.ToString()+ "</td>";//所有时间范围内的活动记录
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
            List<QuerylearningActivity> myLa = DAL.LearningActivityDal.GetlearningActivityByUser(userId);

            HtmlContainerControl divActity;
            int aIndex = 0;
            foreach (var myItem in myLa)
            {
                divActity = new HtmlGenericControl("div");
                divActity.ID = "divAcity" + aIndex.ToString();

                ulhtml = "<span class='myspan1'>" + string.Format("{0:yyyy-MM-dd HH:mm}", myItem.开始时间) + "</span>";
                divActity.Controls.Add(new LiteralControl(ulhtml));

                ulhtml = "<span class='myspan2'>" + myItem.活动类型.ToString() + "&nbsp;&nbsp;" + myItem.活动地点.ToString() + " &nbsp;&nbsp;"+ myItem.持续时长 + "′&nbsp;&nbsp;</span>";
                divActity.Controls.Add(new LiteralControl(ulhtml));
                long currenUserId = DAL.UserDAL.GetUserId();
                
                if (currenUserId==userId)
                {
                    addActivity.Visible = true;
                    ImageButton btnEdits = new ImageButton();
                    btnEdits.ImageUrl = "./images/edit.png";
                    btnEdits.ToolTip = "修改本条活动记录";
                    btnEdits.Width = 24;
                    btnEdits.CommandArgument = myItem.LearningActivityID.ToString();
                    btnEdits.ID = "btnEdits" + myItem.LearningActivityID.ToString();
                    btnEdits.Click += btnEdit_Click;

                    divActity.Controls.Add(btnEdits);

                    divActity.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));

                    ImageButton iBtnDel = new ImageButton();
                    iBtnDel.ImageUrl = "./images/delete.png";
                    iBtnDel.ToolTip = "删除本条活动记录";
                    iBtnDel.Width = 24;
                    iBtnDel.CommandArgument = myItem.LearningActivityID.ToString();
                    iBtnDel.CommandName = aIndex.ToString();
                    iBtnDel.ID = "btnDel" + myItem.LearningActivityID.ToString();
                    iBtnDel.OnClientClick = "javascript:return confirm('确定删除本条活动记录吗?');";
                    iBtnDel.Click += btnDel_Click;
                    divActity.Controls.Add(iBtnDel);
                }
                else
                {
                    addActivity.Visible = false;
                }

                ulhtml = Getdetails("学习对象", myItem.学习对象.ToString());
                ulhtml += Getdetails("对象描述", myItem.对象描述.ToString());
                ulhtml += Getdetails("学习内容", myItem.学习内容.ToString());
                ulhtml += Getdetails("活动描述", myItem.活动描述.ToString());
                if (myItem.作品地址 != "" && myItem.作品地址.Length>5)
                {
                    ulhtml += Getdetails("作品地址", "<a href='" + myItem.作品地址 + "'target='_blank'>点击查看</a>");
                }
                divActity.Controls.Add(new LiteralControl(ulhtml));

                //为生成的div添加样式
                if (aIndex % 2 == 0)
                {
                    divActity.Attributes.Add("class", "divSingle");
                }
                else
                {
                    divActity.Attributes.Add("class", "divDouble");
                }
                myat.Controls.Add(divActity);
                aIndex = aIndex + 1;

            }

        }

        void btnDel_Click(object sender, EventArgs e)
        {
            long userId = DAL.UserDAL.GetUserId();
            ImageButton btn = (ImageButton)sender;
            DAL.LearningActivityDal.DelActivity(long.Parse(btn.CommandArgument));
            myat.Controls.Clear();
            MyatHtml(userId);
            //myat.Controls.RemoveAt(int.Parse (btn.CommandName));
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string atId = HttpUtility.UrlEncode(btn.CommandArgument);
            Response.Redirect("LA_Activity.aspx?ad=" + atId);//  跳转到新增活动页面

        }

        private static string Getdetails(string biaoqian, string neirong)
        {
            string detailsHtml = "<div class='onlineEnroll-model'>";
            detailsHtml +="<div class='wt'>"+biaoqian+":</div>";//标签
            detailsHtml += "<div style='word-break: break-all; word-wrap:break-word;'>&nbsp;&nbsp;" + neirong + "</div>";//内容
            detailsHtml += "</div>";
            return detailsHtml;
        }

        protected void addActivity_OnClick(object sender, EventArgs e)
        {
            //Response.Write("<script>window.opener=null;window.close();</script>");
            Response.Redirect("LA_Activity.aspx");//  跳转到新增活动页面
        }

        protected void showReport_OnClick(object sender, EventArgs e)
        {
            DAL.Common.Alert("");
            //bg.Visible = true;
            //show.Visible = true;
        }
    }
}
