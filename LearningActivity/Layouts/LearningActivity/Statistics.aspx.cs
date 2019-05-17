using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace LearningActivity.Layouts.LearningActivity
{
    public partial class Statistics : LayoutsPageBase
    {
        readonly long userId = DAL.UserDAL.GetUserId();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList cLoca,dLoca,cType,dType;
            DateTime dtNow = DateTime.Now;
            DateTime dt7DaysAgo = dtNow.AddDays(-7);
            startDT.SelectedDate = dt7DaysAgo;
            endDT.SelectedDate = dtNow;
            //默认加载近一周之内的数据
            if (!IsPostBack)
            {
                //1 按日期统计
                var dateStrings = DAL.Common.DtStrings(dt7DaysAgo, dtNow);
                var c1 = DAL.Statistics.MyLaCountsEveryDay(userId, dt7DaysAgo, dtNow);
                var c2 = DAL.Statistics.MyLaCountsEveryDay(0,dt7DaysAgo, dtNow);
                var d1 = DAL.Statistics.MyLaDuringsEveryDay(userId, dt7DaysAgo, dtNow);
                var d2 = DAL.Statistics.MyLaDuringsEveryDay(0,dt7DaysAgo, dtNow);
                
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
            //if (!endDT.IsDateEmpty)
            //{
            //    DAL.Common.Alert("请先选择起始日期!");
            //    endDT.Focus();
            //    return;
            //}
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
                int[] c1 = DAL.Statistics.MyLaCountsEveryDay(userId,dtS, dtE);
                int[] c2 = DAL.Statistics.MyLaCountsEveryDay(0,dtS, dtE);
                IEnumerable<int> d1 = DAL.Statistics.MyLaDuringsEveryDay(userId,dtS, dtE);
                IEnumerable<int> d2 = DAL.Statistics.MyLaDuringsEveryDay(0,dtS, dtE);

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
            DateTime dtS, dtE;
            if (startDT.IsDateEmpty)
            {
                DAL.Common.Alert("请先选择起始日期!");
                startDT.Focus();
                return;
            }
            dtS = startDT.SelectedDate;
            dtE = endDT.SelectedDate;
            if (DAL.Common.DateDiff(dtS,dtE)<3)
            {
                DAL.Common.Alert("起始日期与结束日期时间之差不能小于 3 天");
                endDT.Focus();
            }
            else
            {
                string[] dateStrings = DAL.Common.DtStrings(dtS,dtE);
                int[] c1 = DAL.Statistics.MyLaCountsEveryDay(userId,dtS, dtE);
                int[] c2 = DAL.Statistics.MyLaCountsEveryDay(0,dtS, dtE);
                var d1 = DAL.Statistics.MyLaDuringsEveryDay(userId,dtS, dtE);
                var d2 = DAL.Statistics.MyLaDuringsEveryDay(0,dtS, dtE);


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
        private void ViewChart(Chart ctChart,Dictionary<string,int> dictionary, string title)
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
            foreach (KeyValuePair<string,int> kv in dictionary)
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

            int intCount =dictionary.Values.Sum();
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

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("LA_My.aspx");
        }
    }
}
