using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.HtmlControls;
using ContestDll;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace ContestWeb.Layouts.ContestWeb
{
    public partial class MemberShow : LayoutsPageBase
    {
        #region 参数
        private long memberId
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
                {
                    return long.Parse(Request.QueryString["userId"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        private string memberScore
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["sumScore"]))
                {
                    return Request.QueryString["sumScore"];
                }
                else
                {
                    return "";
                }
            }
        }

        private float memberRank
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Rank"]))
                {
                    return float.Parse(Request.QueryString["Rank"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            ShowMemeberDetail();
            //}
        }
        private void ShowMemeberDetail()
        {
            long[] dt = ContestDll.BLL.User.GetContestUser();
            List<CSVUserSocre> pslist = ContestDll.BLL.User.GetUserScoreByUserId(memberId);
            CSVUserSocre userInfo = pslist.FirstOrDefault();
            if (userInfo != null)
            {
                imageUser.ImageUrl = "./images/headgif.gif";
                string headImg = "http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/" + userInfo.Account + "_MThumb.jpg";

                if (ContestDll.DAL.Common.UrlCheck(headImg))
                {
                    imageUser.ImageUrl = headImg;
                }
                //imageUser.ImageUrl = "http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/" +userInfo.Account + "_MThumb.jpg";
                userName.InnerText = userInfo.Name;
                if (memberScore == "")
                {
                    pSumScore.Visible = false;
                }
                else
                {
                    pSumScore.InnerText = "总积分：" + memberScore;
                }

                ddSchoolName.InnerText = "单位/学校：" + userInfo.SchoolName;

                if (memberRank == 0)
                {
                    ddRank.Visible = false;
                }
                else
                {
                    ddRank.InnerText = "总排名：" + memberRank + "/" + (dt.Length.ToString());  //头像及一些基本信息
                }
            }
            else
            {
                Response.Write("<script>alert('你要查看的用户不存在！');window.open('','_self');window.close();'</script>");
                return;
            }




            //HtmlContainerControl htmlLi;
            // htmlLi = new HtmlGenericControl("li");
            HtmlContainerControl htmlUl = new HtmlGenericControl("ul");
            foreach (var item in pslist)
            {
                string str = "";
                str += "<li><dl class='clearfix'><dd class='science-competition-title'>";

                str += "<a href = 'PDetails.aspx?PeriodID=" + item.PeriodID + "&pageTypeId=0' target ='_blank'><h3>" + item.PeriodTitle + "</h3></a>";
                str += "<span class='status-over' style='padding-left:25px;'>" + ContestDll.BLL.Period.GetPeridState(item.PeriodID, DateTime.Now) + "</span>";

                long[] uids = ContestDll.BLL.User.GetUserScoreByPeriodId(item.PeriodID).Select(us => us.UserID).Distinct().ToArray();
                string csTime = string.Format("{0:D}", item.Created);
                str += "<div class='cp-box'><span class='competition' style='padding-boottom:5px;'>参赛时间:" + csTime + "</span>";
                str += "<span class='competition' style='padding-boottom:5px;'>本赛得分:" + item.Score + "</span>";
                str += "<span class='competition' style='padding-boottom:5px;'>本赛排行：" + (Array.IndexOf(uids, item.UserID) + 1).ToString() + " / " + uids.Length + "</span>";
                str += "<span class='competition'  style='padding-boottom:5px;'>参赛作品：<a href='Comments.aspx?View=1&&WorksID=" + item.WorksID + "' target ='_blank'>" + item.WorksName + ">></a></span>";
                str += "</div></dd></dl></li>";
                htmlUl.Controls.Add(new LiteralControl(str));
                //htmlUl.Attributes.Add("class", "science-list clearfix");
            }
            divContestList.Controls.Add(htmlUl);
        }
    }
}
