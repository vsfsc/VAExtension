using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ContestDll.BLL
{
    public class Pager
    {
        ///<summary>
        ///分页控件
        ///</summary>
        ///<param name="recordCount">记录总数</param>
        ///<param name="pageIndex">分页索引</param>
        ///<param name="pageSize">分页大小</param>        
        /// <returns>返回分页的Html代码</returns>
        public static string Html(int recordCount, int pageIndex, int pageSize)
        {
            string result = string.Empty;
            pageSize = pageSize < 1 ? 10 : pageSize;
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            recordCount = recordCount < 1 ? 1 : recordCount;
            if (recordCount > pageSize)
            {
                int pageCount = (recordCount % pageSize) == 0 ? (recordCount / pageSize) : (recordCount / pageSize) + 1;

                string pageLink = PageLinkConstruct();

                if (pageIndex > pageCount)
                {
                    pageIndex = pageCount;
                }

                string htmlLeftPage = string.Empty;
                string htmlRightPage = string.Empty;

                //前两页
                for (int i = 2; i > 0; i--)
                {
                    if (pageIndex - i > 0)
                    {
                        htmlLeftPage += string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, (pageIndex - i)), pageIndex - i);
                    }
                }
                //后两页
                for (int j = 1; j < 3; j++)
                {
                    if (pageIndex + j <= pageCount)
                    {
                        htmlRightPage += string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, (pageIndex + j)), pageIndex + j);
                    }
                }

                int prevPage = pageIndex - 1;

                if (prevPage < 1)
                {
                    prevPage = 1;
                }

                int nextPage = pageIndex + 1;

                if (nextPage > pageCount)
                {
                    nextPage = pageCount;
                }

                string leftHtml = string.Empty;

                if (pageIndex > 1)
                {
                    leftHtml = string.Format("<a {0}>{1}</a><a {2}>{3}</a>{4}", string.Format("href=\"{0}\"", string.Format(pageLink, 1)), "首页", string.Format("href=\"{0}\"", string.Format(pageLink, prevPage)), "上一页", htmlLeftPage);
                }

                string rightHtml = string.Empty;

                if (pageIndex < pageCount)
                {
                    rightHtml = string.Format("{0}<a {1}>{2}</a><a {3}>{4}</a>", htmlRightPage, string.Format("href=\"{0}\"", string.Format(pageLink, nextPage)), "下一页", string.Format("href=\"{0}\"", string.Format(pageLink, pageCount)), "尾页");
                }

                result = string.Format("<div class=\"htmlpager\">{0}<b>{1}</b>{2}</div>", leftHtml, pageIndex, rightHtml);
            }

            return result;

        }

        /// <summary>
        /// 构造翻页Url
        /// </summary>
        /// <returns></returns>
        private static string PageLinkConstruct()
        {
            HttpContext hc = HttpContext.Current;
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(hc.Request.QueryString["page"]))
            {
                for (int i = 0; i < hc.Request.QueryString.Count; i++)
                {
                    if (hc.Request.QueryString.Keys[i] != "page")
                    {
                        sb.AppendFormat("{0}={1}", hc.Request.QueryString.Keys[i], hc.Request.QueryString[i].ToString());
                        sb.Append("&");
                    }
                    else
                    {
                        sb.Append("page={0}&");
                    }
                }
            }
            else
            {
                for (int i = 0; i < hc.Request.QueryString.Count; i++)
                {
                    sb.AppendFormat("{0}={1}", hc.Request.QueryString.Keys[i], hc.Request.QueryString[i].ToString());
                    sb.Append("&");
                }

                sb.Append("page={0}&");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            var url = new UriBuilder();

            url.Scheme = hc.Request.Url.Scheme;
            url.Host = hc.Request.Url.Host;
            url.Port = hc.Request.Url.Port;
            url.Path = hc.Request.Url.AbsolutePath;
            url.Query = sb.ToString();

            return url.ToString();
        }
        
        #region DataTable筛选，排序返回符合条件行组成的新DataTable或直接用DefaultView按条件返回
        /// <summary>
        /// DataTable筛选，排序返回符合条件行组成的新DataTable或直接用DefaultView按条件返回
        /// eg:SortExprDataTable(dt,"Sex='男'","Time Desc",1)
        /// </summary>
        /// <param name="dt">传入的DataTable</param>
        /// <param name="strExpr">筛选条件</param>
        /// <param name="strSort">排序条件</param>
        /// <param name="mode">1,直接用DefaultView按条件返回,效率较高;2,DataTable筛选，排序返回符合条件行组成的新DataTable</param>
        public static DataTable SortDataTable(DataTable dt, string strExpr, string strSort, int mode)
        {
            switch (mode)
            {
                case 1:
                    //方法一　直接用DefaultView按条件返回
                    dt.DefaultView.RowFilter = strExpr;
                    dt.DefaultView.Sort = strSort;
                    return dt;
                case 2:
                    //方法二　DataTable筛选，排序返回符合条件行组成的新DataTable
                    DataTable dt1 = new DataTable();
                    DataRow[] GetRows = dt.Select(strExpr, strSort);
                    //复制DataTable dt结构不包含数据
                    dt1 = dt.Clone();
                    foreach (DataRow row in GetRows)
                    {
                        dt1.Rows.Add(row.ItemArray);
                    }
                    return dt1;
                default:
                    return dt;
            }
        }
        #endregion

        #region 获取DataTable从指定索引号startIndex开始的RowCount条数的数据
        /// <summary>
        /// 获取DataTable从指定索引号startIndex开始的RowCount条数的数据
        /// </summary>
        /// <param name="startIndex">指定索引号</param>
        /// <param name="RowCount">指定选取条数</param>
        /// <param name="oDT">源DataTable</param>
        /// <returns></returns>
        public static DataTable DtSelectRows(int startIndex,int RowCount, DataTable oDT)
        {
            if (oDT.Rows.Count < startIndex|| oDT.Rows.Count < startIndex+RowCount)
            {
                return oDT;
            }

            DataTable NewTable = oDT.Clone();
            DataRow[] rows = oDT.Select("1=1");
            for (int i = startIndex; i < startIndex + RowCount; i++)
            {
                NewTable.ImportRow((DataRow)rows[i]);
            }
            return NewTable;
        }
        #endregion

        #region 获取DataTable中指定列的数据
        /// <summary>
        /// 获取DataTable中指定列的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tableName">新的DataTable的名词</param>
        /// <param name="strColumns">指定的列名集合</param>
        /// <returns>返回新的DataTable</returns>
        public static DataTable GetTableColumn(DataTable dt, string tableName, params string[] strColumns)
        {
            DataTable dtn = new DataTable();
            if (dt == null)
            {
                throw new ArgumentNullException("参数dt不能为null");
            }
            try
            {
                dtn = dt.DefaultView.ToTable(tableName, true, strColumns);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dtn;
        }
        #endregion
    }
}
