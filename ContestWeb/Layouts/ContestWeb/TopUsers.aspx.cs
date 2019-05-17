using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using ContestDll;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace ContestWeb.Layouts.ContestWeb
{
    using System.Web.UI.WebControls;

    public partial class TopUsers : LayoutsPageBase
    {
        #region 参数
        private int pageIndex
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["page"]))
                {
                    return int.Parse(Request.QueryString["page"]);
                }
                else
                {
                    return 1;
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //showTopUsers(pageIndex);
            //showPager();
            BindgvUSinfo();
        }

        private void BindgvUSinfo()
        {
            long[] userIdS = ContestDll.BLL.User.GetContestUser();
            gvUInfo.DataSource = GetUserinfo(userIdS);
            gvUInfo.DataBind();
        }
        /// <summary>
        /// 生成用户数据列表
        /// </summary>
        /// <param name="userIdS">The user identifiers.</param>
        /// <returns>DataTable.</returns>
        private DataTable GetUserinfo(long[] userIdS)
        {
            DataTable dtSumScore = CreateUserScoreTable();
            double fSumScore = 0.0;
            for (int i = 0; i < userIdS.Length; i++)   //将用户的信息和总积分存储到dataTable dtSumScore中
            {
                DataRow dr = dtSumScore.NewRow();
                List<CSVUserSocre> uslist = ContestDll.BLL.User.GetUserScoreByUserId(userIdS[i]);
                fSumScore += uslist.Where(item => item.Score != null).Sum(item => (double) item.Score);
                string sumScore = fSumScore.ToString("##.00");
                fSumScore=Convert.ToDouble(sumScore);
                dr["UserID"] = userIdS[i];
                dr["SumScore"] = fSumScore;
                CSVUserSocre userInfo = uslist.FirstOrDefault();
                if (userInfo != null)
                {
                    dr["Name"] = userInfo.Name;
                    dr["SchoolName"] = userInfo.SchoolName;
                    dr["Account"] = userInfo.Account;
                }
                dtSumScore.Rows.Add(dr);
                fSumScore = 0.0;
            }
            return DtAddXuHao(SortDataTable(dtSumScore, "SumScore desc"));            
        }
        /// <summary>
        /// Sorts the data table.
        /// </summary>
        /// <param name="oldDt">The old dt.</param>
        /// <param name="sortStr">The sort string.</param>
        /// <returns>DataTable.</returns>
        private DataTable SortDataTable(DataTable oldDt,string sortStr)
        {
            DataTable newDt = oldDt.Clone();
            DataRow[] rows = oldDt.Select("", sortStr);
            newDt.Clear();
            foreach (DataRow row in rows)
            {
                newDt.ImportRow(row);
            }
            return newDt;
        }
        /// <summary>
        /// 给DataTable添加序号
        /// </summary>
        /// <param name="dt"></param>
        private static DataTable DtAddXuHao(DataTable dt)
        {
            dt.Columns.Add("XuHao", typeof(int));
            dt.Columns["XuHao"].DefaultValue = 0;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["XuHao"] = i + 1;

            }
            return dt;
        }
        private void ShowTopUsers(int currentPage)
        {
            HtmlContainerControl htmlLi;
            htmlLi = new HtmlGenericControl("li");
            long[] userIdS = ContestDll.BLL.User.GetContestUser();  //得到参加过项目的UserID
            DataTable dt=GetUserinfo(userIdS);            
            int rCount =userIdS.Length;
            //计算分页
            int pageMth =(int) Math.Ceiling((float)rCount /25);//向上取整，不够1补1            
            int rowCount = 25;
            int startIndex = (currentPage - 1) * rowCount;
            if (currentPage<1)
            {
                ContestDll.DAL.Common.Alert("已是第一页了");
            }
            else if(currentPage > pageMth)
            {
                ContestDll.DAL.Common.Alert("已是最后一页了");
            }
            else
            {
                if (currentPage==pageMth)//最后一页
                {
                    rowCount = rCount - startIndex;                   
                }               
                DataTable nDt = ContestDll.BLL.Pager.DtSelectRows(startIndex, rowCount, dt);
                BindUserInfoList(nDt, startIndex, rCount);
            }
        }
        private void showInfo(DataTable dt,int myrank,int sumrank)
        {
            HtmlContainerControl htmlLi;
            htmlLi = new HtmlGenericControl("li");
            HtmlContainerControl htmlUl = new HtmlGenericControl("ul");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jifen = string.Format("{0:###.##}", dt.Rows[i][1]);
                string str = "<li><dl><dt><a href ='MemberShow.aspx?userId=" + dt.Rows[i][0] + "&sumScore=" + jifen + "&Rank=" + (i + 1).ToString() + "' target = '_blank'>";

                string headImg = "http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/" + dt.Rows[i][4] + "_LThumb.jpg";//用户头像地址
                //if (!ContestDll.DAL.Common.UrlCheck(headImg))//判断该用户的头像是否存在且可访问
                //{
                //    headImg = "./images/headgif.gif";
                //}
                str += "<img width = '160' height = '160' src = '" + headImg + "' border = 'none'></a></dt>";
                str += "<dd class='science-list-rank-text'>";
                str += "<a href = 'MemberShow.aspx?userId=" + dt.Rows[i][0] + "&sumScore=" + jifen + "&Rank=" + (i + 1).ToString() + "' target = '_blank'>" + dt.Rows[i][2] + "</a>";
                str += "<span>排名:<em>" + (myrank+i + 1).ToString() + "</em>/" + sumrank + "</span></dd>";
                str += "<dd class='science-school'> <abbr title='学校名称' rel='tooltip'>" + dt.Rows[i][3] + "</abbr>";
                str += "<span style='padding-left:60px;'>积分:<em>" + jifen + "</em> </span>";
                str += "</dd></dl></li>";
                htmlUl.Controls.Add(new LiteralControl(str));
                htmlUl.Attributes.Add("class", "science-list clearfix");
            }
            divTopman.Controls.Add(htmlUl);
        }

        private void BindUserInfoList(DataTable dt, int myrank, int sumrank)
        {
            HtmlContainerControl htmlUl = new HtmlGenericControl("table");
            string str = "";
            str += "<tr><th>头像</th><th>用户名</th><th>个人积分</th><th>个人排行</th><th>所在院校</th></tr>";
            htmlUl.Controls.Add(new LiteralControl(str));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jifen = string.Format("{0:###.##}", dt.Rows[i][1]);
                str = "<tr><td><a href ='MemberShow.aspx?userId=" + dt.Rows[i][0] + "&sumScore=" + jifen + "&Rank=" + (i + 1).ToString() + "' target = '_blank'>";

                string headImg = "http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/" + dt.Rows[i][4] + "_SThumb.jpg";//用户头像地址
                //if (!ContestDll.DAL.Common.UrlCheck(headImg))//判断该用户的头像是否存在且可访问
                //{
                //    headImg = "./images/headgif.gif";
                //}
                str += "<img width = '48' height = '48' src = '" + headImg + "' border = 'none'></a></td>";
                str += "<td><a href = 'MemberShow.aspx?userId=" + dt.Rows[i][0] + "&sumScore=" + jifen + "&Rank=" + (i + 1).ToString() + "' target = '_blank'>" + dt.Rows[i][2] + "</a></td>";
                str += "<td><em>" + jifen + "</em> </td>";
                str += "<td><em>" + (myrank + i + 1).ToString() + "</em>/" + sumrank + "</td>";
                str += "<td>" + dt.Rows[i][3] + "</td>";
                str += "</tr>";
                htmlUl.Controls.Add(new LiteralControl(str));
            }
            uInfoDiv.Controls.Add(htmlUl);
        }
        private void showPager()
        {
            //HtmlContainerControl htmlPager;
            //htmlPager = new HtmlGenericControl("div");
            long[] userIdS = ContestDll.BLL.User.GetContestUser();  //得到参加过项目的UserID
            string pagerHtml = ContestDll.BLL.Pager.Html(userIdS.Length, 1, 25);
            pagerDiv.InnerHtml = pagerHtml;
        }
        private static DataTable CreateUserScoreTable()
        {
            DataTable dt = new DataTable("UserSumScore");
            dt.Columns.Add("UserID", Type.GetType("System.Int64"), "");
            dt.Columns.Add("SumScore", Type.GetType("System.Double"), "");
            dt.Columns.Add("Name", Type.GetType("System.String"), "");
            dt.Columns.Add("SchoolName", Type.GetType("System.String"), "");
            dt.Columns.Add("Account", Type.GetType("System.String"), "");
            return dt;
        }

        protected void gvUInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUInfo.PageIndex = e.NewPageIndex;
            BindgvUSinfo();
        }
    }
}
