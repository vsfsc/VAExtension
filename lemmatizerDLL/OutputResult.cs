using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;

namespace lemmatizerDLL
{
   /// <summary>
    /// 词级标记说明：-2 超纲词汇，-1未处理词汇，0忽略处理词汇，其他数字对应数字指定的各级词汇
    /// </summary>
    public class OutputResult
    {
        #region 词汇表输出
        /// <summary>
        /// 输出超纲词表
        /// </summary>
        /// <param name="oldDt">原各等级词汇表</param>
        /// <param name="maxIndex">选择的最高等级</param>
        /// <returns>超纲词汇表</returns>
        public static DataTable NewDataTable(DataTable oldDt, int maxIndex)
        {
            DataTable newDt = new DataTable();
            //DataRow[] dr = oldDT.Select();
            foreach (DataRow row in oldDt.Rows)
            {
                //string strName = row["词汇"].ToString(); //取对应字段信息
                //string str1 = row["基本"].ToString();
                //string str2 = row["较高"].ToString();
                //string str3 = row["更高"].ToString();
                //string wordfreq = row["词频"].ToString();
                if (int.Parse(row[1].ToString()) > maxIndex && int.Parse(row[1].ToString()) <= 0)
                {
                    newDt.Rows.Add(row);
                }

            }
            //string sql = string.Empty;
            //for (int i = 1; i <=maxIndex; i++)
            //{

            //}
            DataRow[] rows = oldDt.Select("[level]>0 and [level]<=" + maxIndex);

            DataTable dt2 = oldDt.Copy(); //我印象中有Clone方法可以直接复制架构和数据结构但不复制数据的，不要用copy方法。
            dt2.Rows.Clear();
            foreach (DataRow row2 in rows)
            {
                object[] row3 = row2.ItemArray;
                dt2.Rows.Add(row3);
            }
            return newDt;
        }

        /// <summary>
        /// 输出结果表格
        /// </summary>
        /// <param name="showWordsdt"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public static string ResultTable(DataTable showWordsdt, int maxIndex) //返回数据表格
        {
            string tableHtml = string.Empty;
            int rCount = showWordsdt.Rows.Count;
            int cCount = showWordsdt.Columns.Count;
            string rowStr = string.Empty;
            for (int i = 0; i < rCount; i++)
            {
                tableHtml += "<tr><td>" + (i + 1).ToString() + "</td>";
                for (int j = 0; j < cCount; j++)
                {
                    if (j <= maxIndex && j > 0 && showWordsdt.Rows[i][j].ToString() != "√")
                    {
                        rowStr = "<td>" + showWordsdt.Rows[i][j] + "</td>";
                    }
                }
                for (int j = 0; j < rCount; j++)
                {
                    if (rowStr.IndexOf('√') <= -1)//不包含指定级别的词汇
                    {
                        tableHtml += rowStr;
                    }
                }
                tableHtml += "</tr>";
            }
            return tableHtml;
        }

        #endregion

        #region 输出统计图表(饼图\曲线图)
        protected static List<string[]> Drawpie(int[] dataInts, int maxIndex)
        {
            string[][] s1 = new string[3][];
            List<string[]> pieList = new List<string[]>();
            string[] textStrings = new string[maxIndex + 2];
            string[] colorStrings = new string[maxIndex + 2];
            string[] t1 = new string[6]{ "超纲词汇", "未处理词汇", "高中大纲", "基本要求", "较高要求", "更高要求"};
            string[] c1=new string[6] { "#FF0000", "#1C1C1C", "#7FFF00", "#FFFF00", "#FF7F50", "#4876FF" };
            for (int i = 0; i < maxIndex + 2; i++)
            {
                textStrings[i] = t1[i];
                colorStrings[i] = c1[i];
            }
            //if (maxIndex == 1)
            //{
            //    textStrings = new string[3] { "超纲词汇", "高中大纲", "未处理词汇" };
            //    colorStrings = new string[3] { "#FF0000", "#7FFF00", "#1C1C1C" };
            //}
            //else if (maxIndex == 2)
            //{
            //    textStrings = new string[4] { "超纲词汇", "高中大纲", "基本要求", "未处理词汇" };
            //    colorStrings = new string[4] { "#FF0000", "#7FFF00", "#FFFF00", "#1C1C1C" };
            //}
            //else if (maxIndex == 3)
            //{
            //    textStrings = new string[5] { "超纲词汇", "高中大纲","基本要求", "较高要求", "未处理词汇" };
            //    colorStrings = new string[5] { "#FF0000", "#7FFF00", "#FFFF00", "#FF7F50", "#1C1C1C" };
            //}
            //else if (maxIndex == 4)
            //{
            //    textStrings = new string[6] { "超纲词汇", "高中大纲", "基本要求", "较高要求","更高要求", "未处理词汇" };
            //    colorStrings = new string[6] { "#FF0000", "#7FFF00", "#FFFF00", "#FF7F50" , "#4876FF", "#1C1C1C" };
            //}
            pieList.Add(textStrings);
            pieList.Add(colorStrings);
            return pieList;
        }
        /// <summary>
        /// 绘制饼图
        /// </summary>
        /// <param name="dt">绘图元数据表</param>
        public static void ResultChart(DataTable dt)
        {
            int rCount = dt.Rows.Count;
            int cCount = dt.Columns.Count;
            string tableHtml;
            List<string> tableHeader = new List<string>();
            for (int i = 1; i < cCount; i++)
            {
                tableHeader.Add(dt.Rows[0][i].ToString());
            }
            List<int> values = new List<int>();
            for (int i = 0; i < tableHeader.Count; i++)
            {
                //values.Add(int.Parse(dt.Compute("Count(*)", tableHeader[i].ToString()="√")));
            }
            //long dt.Compute("Count(*)", "基本=√");
        }

        public static DataSet ResultDataSet(ArrayList myList)
        {
            DataSet ds = new DataSet();
            return ds;
        }
        #endregion

        #region 输出彩色文本与图例
        /// <summary>
        /// 输出彩色文本
        /// </summary>
        /// <param name="showWordsList">原词汇包含级别标记的列表</param>
        /// <param name="maxIndex">选定的最高等级</param>
        /// <returns>含有色彩标记的字符串构造</returns>
        public static StringBuilder ResultDiv(IEnumerable<List<string>> showWordsList, int maxIndex) //返回彩色文本
        {
            StringBuilder sb = new StringBuilder();
            //string divString = string.Empty;
            //int i = 0;//计数器

            foreach (List<string> sList in showWordsList)
            {
                string sword = sList[0]; //文章中出现的单词
                int stags = int.Parse(sList[1]); //文章中单词对应的级别序号-1,1,2,3...
                //if (i==10)//每10个单词加一个换行
                //{
                //    divString = divString + "<br/>";
                //    i = 0;
                //}
                if (stags > maxIndex + 4 || stags == 0) //没有选择的级别或者没有确定级别的基础词汇,即超纲词汇
                {
                    sb.Append(Colored(sword, 0));
                    //divString = divString + Colored(sword,0);
                    //i++;
                }
                else //不在基础词汇表中或者是已经指定级别的词汇
                {
                    //divString = divString + Colored(sword,stags);
                    if (stags >= 5)
                        sb.Append(Colored(sword, stags - 4));
                    else
                        sb.Append(Colored(sword, stags));
                    //i++;
                }
                sb.Append(" ");
            }
            return sb;
        }
        /// <summary>
        /// 输出单词等级颜色示例
        /// </summary>
        /// <param name="wList">词汇表</param>
        /// <param name="maxIndex">词库级别从5开始，这里做-5处理，降低为从1开始</param>
        /// <param name="symbolFile">符号库文本文件</param>
        /// <returns>图例Html字符串</returns>
        public static string Tuli(IEnumerable<List<string>> wList, int maxIndex, string symbolFile)
        {
            var wordCount = WordCount(wList, maxIndex, symbolFile);
            var jibie = new string[5] { "超纲词汇", "高中大纲", "基本要求", "较高要求", "更高要求" };
            var colorstr = Colored("忽略处理(" + wordCount[maxIndex + 2] + ") ", -2);
            colorstr = colorstr + Colored(" 未处理(" + wordCount[maxIndex + 1] + ")", -1);
            StringBuilder sb = new StringBuilder();
            sb.Append(colorstr);
            //colorstr = colorstr + Colored(" 超纲词汇(" + wordCount[0] + ") ", 0);
            for (var i = 0; i <= maxIndex; i++)//级别从开始，对应 "超纲词汇","高中大纲", "基本要求", "较高要求", "更高要求"
            {
                sb.Append(Colored(jibie[i] + "(" + wordCount[i] + ") ", i));
            }
            colorstr = sb.ToString();
            //Drawpie(wordCount, maxIndex);
            return colorstr;
        }

        public static DataTable InitWordsAnalysisTable(IEnumerable<List<string>> wList, int maxIndex, string symbolFile)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("级别");
            dt.Columns.Add("词汇量");
            dt.Columns.Add("级别号");
            var wordCount = WordCount(wList, maxIndex, symbolFile);
            var jibie = new string[5] { "超纲词汇", "高中大纲", "基本要求", "较高要求", "更高要求" };
            DataRow dr = dt.NewRow();
            dr[0] = "忽略处理";
            dr[1] = wordCount[maxIndex + 2];
            dr[2] = -2;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "未处理";
            dr[1]=wordCount[maxIndex + 1];
            dr[2] = -1;
            dt.Rows.Add(dr);
            for (int i = 0; i <= maxIndex; i++)//级别从开始，对应 "超纲词汇","高中大纲", "基本要求", "较高要求", "更高要求"
            {
                dr=dt.NewRow();
                dr[0] = jibie[i];
                dr[1] = wordCount[i];
                dr[2] =i;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 对字符串着色
        /// </summary>
        /// <param name="str">要着色的字符串</param>
        /// <param name="tags">着色标记</param>
        /// <returns>含有色彩样式的html字符串</returns>
        public static string Colored(string str, int tags)
        {
            //****************单词分级颜色定义*********************/
            //-2:忽略处理的词汇
            //-1:;无法在基础词汇表中找到其原型,即单词属于无法处理词汇
            //0:;不确定词汇级别,即单词属于超纲词汇
            //1:;单词属于基本要求词汇
            //2:;单词属于较高要求词汇
            //3:;单词属于更高要求词汇
            string colorstr;
            var colorArray = new string[7] { "#ff0000", "#00CD66", "#1E90FF", "#008B8B", "#C71585", "#33ffff", "#ff00ff" };//级别从低到高（0起）：超纲词汇，高中大纲，基本要求，较高要求，更高要求
            if (tags >= 0 & tags <= 3)//正常处理的各级别词汇对应的颜色表（截至2016年10月现阶段词汇级别有：超纲词汇，高中大纲，基本要求，较高要求，更高要求）
            {
                colorstr = "<span style='color:" + colorArray[tags] + ";'>" + str + "</span>";
            }
            else if (tags == -1)//-1,特殊处理
            {
                colorstr = "<span style='color:#fff305;'>" + str + "</span>";
            }
            else//-2，忽略处理
            {
                colorstr = "<span style='color:#ffffff;'>" + str + "</span>";//-2,未处理
            }
            return colorstr;
        }
        #endregion

        #region 词汇词频统计


        /// <summary>
        /// 统计变形词汇表中的各级词汇数量
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public static int[] WordProfiles(DataTable dt, int maxIndex)
        {

            int[] profileNo = new int[maxIndex + 2]; //各类词汇数目统计
            int n1 = int.Parse(dt.Compute("Count(level)", "level = '-1'").ToString());
            profileNo[0] = n1;
            int k = 0;
            for (int i = 0; i <= maxIndex; i++)
            {
                profileNo[i] = int.Parse(dt.Compute("Count(level)", "level= '" + i.ToString() + "'").ToString());//选定级别内的词汇//level<=maxIndex
            }
            for (int i = 3; i > maxIndex; i--)
            {
                k = k + int.Parse(dt.Compute("Count(level)", "level= '" + i.ToString() + "'").ToString());
            }
            profileNo[0] = profileNo[0] + k;//超纲词汇数
            profileNo[maxIndex + 1] = int.Parse(dt.Compute("Count(level)", "level = '-1'").ToString());//未处理词汇数
            return profileNo;
        }

        /// <summary>
        /// 处理得到每个级别（包括特殊处理和未处理的）的词汇量
        /// </summary>
        /// <param name="showWordsList"></param>
        /// <param name="maxIndex">最大级别的索引号</param>
        /// <param name="symbolFile">符号词库文件</param>
        /// <returns></returns>
        public static int[] WordCount(IEnumerable<List<string>> showWordsList, int maxIndex,string symbolFile)
        {
            string[] symbols =File.ReadAllLines(symbolFile);//标点符号数组
            var wordCount=new int[maxIndex+3];
            for (var i = 0; i < maxIndex + 3; i++)//初始化整个词频数组
            {
                wordCount[i] = 0;
            }

            //根据词汇处理结果填充数组
            foreach (List<string> sList in showWordsList)//sList单词和对应级别数组
            {
                int stags = int.Parse(sList[1]); //文章中单词对应的级别序号-1,1,2,3...
                if (stags >=5 && stags <= maxIndex+4)//有对应等级的正常处理的词汇，0为超纲词汇(5-9)
                {
                    wordCount[stags-4]++;
                }
                else if (stags == -1)//无法处理的词汇
                {
                    wordCount[maxIndex+1]++;
                }
                else if (stags == -2 && !TextInput.IsInArray(sList[0].ToString(),symbols))
                {
                    wordCount[maxIndex+2]++;
                }
                else if (stags>maxIndex || stags ==0)//超纲词汇,words本身没有级别
                {
                    wordCount[0]++;
                }
            }
            return wordCount;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 随机获取颜色
        /// </summary>
        /// <returns>颜色字符串RGB表示法</returns>
        public static string GetRandomColor()
        {
            Random randomNumFirst = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(randomNumFirst.Next(50));
            Random randomNumSencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int intRed = randomNumFirst.Next(256);
            int intGreen = randomNumSencond.Next(256);
            int intBlue = (intRed + intGreen > 400) ? 0 : 400 - intRed - intGreen;
            intBlue = (intBlue > 255) ? 255 : intBlue;

            return Color.FromArgb(intRed, intGreen, intBlue).Name;
        }
        public static void VisibleOrNot(Control yesId, Control noId)
        {
            yesId.Visible = true;
            noId.Visible = false;
        }
        #endregion
    }
}
