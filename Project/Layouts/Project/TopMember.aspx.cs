using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Project.Layouts.Project
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using ProjectDll;

    public partial class TopMember : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            showProjTopMan();
        }

        private void showProjTopMan()
        {
            HtmlContainerControl htmlLi;
            htmlLi = new HtmlGenericControl("li");
            HtmlContainerControl htmlUl = new HtmlGenericControl("ul");
            long[] dt = ProjectDll.DAL.ProjectDal.GetProjUser();  //得到参加过项目的UserID

            List<Proj_VUserScore> pslist = new List<Proj_VUserScore>();
            DataTable dtSumScore = CreateUserScoreTable();
            double fSumScore = 0.0;

            Proj_VUserScore userInfo = new Proj_VUserScore();



            for (int i = 0; i < dt.Length; i++)   //将用户的信息和总积分存储到dataTable dtSumScore中
            {

                DataRow dr = dtSumScore.NewRow();
                pslist = ProjectDll.DAL.ProjectDal.GetProjMemScoreById(dt[i]);
                userInfo = pslist.FirstOrDefault();
                foreach (var item in pslist)
                {
                    fSumScore += (double)item.Score * (double)item.PScore;
                }
                fSumScore = fSumScore / 100.0;
                dr["UserID"] = dt[i];
                dr["SumScore"] = fSumScore;
                dr["Name"] = userInfo.Name;
                dr["SchoolName"] = userInfo.SchoolName;
                dr["Account"] = userInfo.Account;
                dtSumScore.Rows.Add(dr);
                fSumScore = 0.0;
            }
            /*
            对总分进行排序，得到DataTable t
            */

            DataRow[] rows = dtSumScore.Select("", "SumScore desc");
            DataTable t = dtSumScore.Clone();
            t.Clear();
            foreach (DataRow row in rows)
            {
                t.ImportRow(row);
            }

            int temp = 100;
            if (dtSumScore.Rows.Count < 100)
            {
                temp = t.Rows.Count;
            }

            for (int i = 0; i < temp; i++)
            {
                string jifen = string.Format("{0:###.##}", t.Rows[i][1]);
                string str = "<li><a href ='MemberDetail.aspx?userId=" + t.Rows[i][0] + "&sumScore=" + jifen + "&Rank=" + (i + 1).ToString() + "' target = '_blank'>";


                string imgurl = "./images/headgif.gif";
                string headImg = "http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/" + t.Rows[i][4] + "_LThumb.jpg";

                //if (ProjectDll.DAL.Common.UrlCheck(headImg))
                //{
                //    imgurl = headImg;
                //}
                str += "<img width = '157' height = '203' src = '" + headImg + "' border = 'none'></a>";

                str += "<div>";

                str += "<h3><a href = 'MemberDetail.aspx?userId=" + t.Rows[i][0] + "&sumScore=" + jifen + "&Rank=" + (i + 1).ToString() + "' target = '_blank'>" + t.Rows[i][2] + "</a></h3>";

                str += "<p><span class='rating-txt rank'>排名:<em>" + (i + 1).ToString() + "</em>/" + t.Rows.Count + "</span><br/>";
                str += "<span class='rating-txt score'>积分:<em>" + jifen + "</em> </span><br/>";
                str += "<span class='rating-txt org'><abbr title='所在学校/单位' rel='tooltip'>" + t.Rows[i][3] + "</span><br/>";
                str += "</p></li>";

                htmlUl.Controls.Add(new LiteralControl(str));
                htmlUl.Attributes.Add("class", "item1");
            }

            wrap.Controls.Add(htmlUl);
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
    }
}
