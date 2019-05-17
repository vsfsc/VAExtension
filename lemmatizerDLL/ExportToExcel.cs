using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
//using Microsoft.Office.Interop.Excel;
using System.Web.UI.WebControls;

namespace lemmatizerDLL
{
    //导出结果到excel文件
    public class ExportToExcel
    {
        // <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public static  void ExportExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;
        }
        #region
        // 用于生成Excel表头的函数块
        // 时间: 23:07 2008/9/28    Author: Andy Lu 
        // 文件: AndyGridViewTHeaderHepler.cs 
        // 功能: 动态生成复合表头帮助类 
        // 相邻父列头之间用'#'分隔,父列头与子列头用空格(' ')分隔,相邻子列头用逗号分隔(','). 
        // 两行：序号#分公司#组别#本日成功签约单数 预警,续约,流失,合计#累计成功签约单数 预警,续约,流失,合计#任务数#完成比例#排名 
        // 三行：等级#级别#上期结存 件数,重量,比例#本期调入 收购调入 件数,重量,比例#本期发出 车间投料 件数,重量,比例#本期发出 产品外销百分比 件数,重量,比例#平均值 
        // 三行时请注意：列头要重复
        // 文件出处：http://www.cnblogs.com/mzoylee/archive/2009/09/24/1573504.html
        public void SplitTableHeader(GridViewRow targetHeader, string newHeaderNames)
        {
            //获得表头元素的实例
            TableCellCollection tcl = targetHeader.Cells;
            //清除元素 
            tcl.Clear();
            int row = GetRowCount(newHeaderNames);
            int col = GetColCount(newHeaderNames);
            string[,] nameList = ConvertList(newHeaderNames, row, col);
            int RowSpan = 0;
            int ColSpan = 0;
            for (int k = 0; k < row; k++)
            {
                string LastFName = "";
                for (int i = 0; i < col; i++)
                {
                    if (LastFName == nameList[i, k] && k != row - 1)
                    {
                        LastFName = nameList[i, k];
                        continue;
                    }
                    else
                    {
                        LastFName = nameList[i, k];
                    }
                    int bFlag = IsVisible(nameList, k, i, LastFName);
                    switch (bFlag)
                    {
                        case 0:
                            break;
                        case 1:
                            RowSpan = GetSpanRowCount(nameList, row, k, i);
                            ColSpan = GetSpanColCount(nameList, row, col, k, i);
                            //添加表头控件 
                            tcl.Add(new TableHeaderCell());
                            tcl[tcl.Count - 1].RowSpan = RowSpan;
                            tcl[tcl.Count - 1].ColumnSpan = ColSpan;
                            tcl[tcl.Count - 1].HorizontalAlign = HorizontalAlign.Center;
                            tcl[tcl.Count - 1].Text = LastFName;
                            break;
                        case -1:
                            string[] EndColName = LastFName.Split(new char[] { ',' });
                            foreach (string eName in EndColName)
                            {
                                //添加表头控件 
                                tcl.Add(new TableHeaderCell());
                                tcl[tcl.Count - 1].HorizontalAlign = HorizontalAlign.Center;
                                tcl[tcl.Count - 1].Text = eName;
                            }
                            break;
                    }
                }
                if (k != row - 1)
                {//不是起始行,加入新行标签 
                    tcl[tcl.Count - 1].Text = tcl[tcl.Count - 1].Text + "</th></tr><tr class=" + targetHeader.CssClass + ">";
                }
            }
        }
        /// <summary> 
        /// 如果上一行已经输出和当前内容相同的列头，则不显示 
        /// </summary> 
        /// <param name="ColumnList">表头集合</param> 
        /// <param name="rowIndex">行索引</param> 
        /// <param name="colIndex">列索引</param> 
        /// <returns>1:显示,-1:含','分隔符,0:不显示</returns> 
        private int IsVisible(string[,] ColumnList, int rowIndex, int colIndex, string CurrName)
        {
            if (rowIndex != 0)
            {
                if (ColumnList[colIndex, rowIndex - 1] == CurrName)
                {
                    return 0;
                }
                else
                {
                    if (ColumnList[colIndex, rowIndex].Contains(","))
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 1;
        }
        /// <summary> 
        /// 取得和当前索引行及列对应的下级的内容所跨的行数 
        /// </summary> 
        /// <param name="ColumnList">表头集合</param> 
        /// <param name="row">行数</param> 
        /// <param name="rowIndex">行索引</param> 
        /// <param name="colIndex">列索引</param> 
        /// <returns>行数</returns> 
        private int GetSpanRowCount(string[,] ColumnList, int row, int rowIndex, int colIndex)
        {
            string LastName = "";
            int RowSpan = 1;
            for (int k = rowIndex; k < row; k++)
            {
                if (ColumnList[colIndex, k] == LastName)
                {
                    RowSpan++;
                }
                else
                {
                    LastName = ColumnList[colIndex, k];
                }
            }
            return RowSpan;
        }
        /// <summary> 
        /// 取得和当前索引行及列对应的下级的内容所跨的列数 
        /// </summary> 
        /// <param name="ColumnList">表头集合</param> 
        /// <param name="row">行数</param> 
        /// <param name="col">列数</param> 
        /// <param name="rowIndex">行索引</param> 
        /// <param name="colIndex">列索引</param> 
        /// <returns>列数</returns> 
        private int GetSpanColCount(string[,] ColumnList, int row, int col, int rowIndex, int colIndex)
        {
            string LastName = ColumnList[colIndex, rowIndex];
            int ColSpan = ColumnList[colIndex, row - 1].Split(new char[] { ',' }).Length;
            ColSpan = ColSpan == 1 ? 0 : ColSpan;
            for (int i = colIndex + 1; i < col; i++)
            {
                if (ColumnList[i, rowIndex] == LastName)
                {
                    ColSpan += ColumnList[i, row - 1].Split(new char[] { ',' }).Length;
                }
                else
                {
                    LastName = ColumnList[i, rowIndex];
                    break;
                }
            }
            return ColSpan;
        }
        /// <summary> 
        /// 将已定义的表头保存到数组 
        /// </summary> 
        /// <param name="newHeaders">新表头</param> 
        /// <param name="row">行数</param> 
        /// <param name="col">列数</param> 
        /// <returns>表头数组</returns> 
        private string[,] ConvertList(string newHeaders, int row, int col)
        {
            string[] ColumnNames = newHeaders.Split(new char[] { '#' });
            string[,] news = new string[col, row];
            string Name = "";
            for (int i = 0; i < col; i++)
            {
                string[] CurrColNames = ColumnNames[i].ToString().Split(new char[] { ' ' });
                for (int k = 0; k < row; k++)
                {
                    if (CurrColNames.Length - 1 >= k)
                    {
                        if (CurrColNames[k].Contains(","))
                        {
                            if (CurrColNames.Length != row)
                            {
                                if (Name == "")
                                {
                                    news[i, k] = news[i, k - 1];
                                    Name = CurrColNames[k].ToString();
                                }
                                else
                                {
                                    news[i, k + 1] = Name;
                                    Name = "";
                                }
                            }
                            else
                            {
                                news[i, k] = CurrColNames[k].ToString();
                            }
                        }
                        else
                        {
                            news[i, k] = CurrColNames[k].ToString();
                        }
                    }
                    else
                    {
                        if (Name == "")
                        {
                            news[i, k] = news[i, k - 1];
                        }
                        else
                        {
                            news[i, k] = Name;
                            Name = "";
                        }
                    }
                }
            }
            return news;
        }
        /// <summary> 
        /// 取得复合表头的行数 
        /// </summary> 
        /// <param name="newHeaders">新表头</param> 
        /// <returns>行数</returns> 
        private int GetRowCount(string newHeaders)
        {
            string[] ColumnNames = newHeaders.Split(new char[] { '#' });
            int Count = 0;
            foreach (string name in ColumnNames)
            {
                int TempCount = name.Split(new char[] { ' ' }).Length;
                if (TempCount > Count)
                    Count = TempCount;
            }
            return Count;
        }
        /// <summary> 
        /// 取得复合表头的列数 
        /// </summary> 
        /// <param name="newHeaders">新表头</param> 
        /// <returns>列数</returns> 
        private int GetColCount(string newHeaders)
        {
            return newHeaders.Split(new char[] { '#' }).Length;
        }
        #endregion

    }
}
