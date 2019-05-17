using System;
using System.Collections.Generic;
using System.Linq;
using ProjectDll;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project.Layouts.Project
{
    using System.Web.UI.HtmlControls;
    using ProjectDll;

    public partial class MemberShow : Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            showMemeberDetail();
        }

        private void showMemeberDetail()
        {
            long[] dt = ProjectDll.DAL.ProjectDal.GetProjUser();
            List<Proj_VUserScore> pslist = ProjectDll.DAL.ProjectDal.GetProjMemScoreById(memberId);
            Proj_VUserScore userInfo = new Proj_VUserScore();
            userInfo = pslist.FirstOrDefault();
            imageUser.ImageUrl = "http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/" + userInfo.Account + "_MThumb.jpg";
            userName.InnerText = userInfo.Name;
            if (memberScore == "")
            {
                pSumScore.Visible = false;
            }
            else
            {
                pSumScore.InnerText = "积分：" + memberScore;
            }


            ddSchoolName.InnerText = "单位：" + userInfo.SchoolName;
            if (memberRank == 0)
            {
                ddRank.Visible = false;
            }
            else
            {
                ddRank.InnerText = "排名：" + memberRank + "/" + (dt.Length.ToString());  //头像及一些基本信息
            }



            //HtmlContainerControl htmlLi;
            // htmlLi = new HtmlGenericControl("li");
            HtmlContainerControl htmlUl = new HtmlGenericControl("ul");
            foreach (var item in pslist)
            {
                string str = "";
                str += "<li><dl class='clearfix'><dd class='science-competition-title'>";

                str += "<strong><a href = 'PDetails.aspx?ProjectID=" + item.ProjectID + "&pageTypeId=0' target ='_blank'>" + item.PName + "</a></strong>";

                str += "<div class='competition-info clearfix'><span class='status-over'>加入时间:" + item.ApplyInTime + "</span>";
                str += "<div class='cp-box'><span class='competition'>项目状态:" + item.StateName + "</span>";
                str += "<span class='competition'>个人得分:" + item.Score + "</span>";
                str += "<span class='competition'>项目得分：" + item.PScore + "</span>";
                str += "</div></div></dd></dl></li>";
                htmlUl.Controls.Add(new LiteralControl(str));
                //htmlUl.Attributes.Add("class", "science-list clearfix");
            }

            divProjectList.Controls.Add(htmlUl);




        }
    }
}