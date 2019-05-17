using System;
using System.Web.UI.HtmlControls;
using LearningActivity.DAL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LearningActivity.Layouts.LearningActivity
{
    public partial class LA_Public : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string divHtml = "";
                long currentUserId = UserDAL.GetUserId();
                if (currentUserId == 65 ||currentUserId== 39)
                {
                   
                    var laList = DAL.LearningActivityDal.GetLaCountsbyUsers();
                    if (laList.Count > 0)
                    {
                        HtmlContainerControl myDiv;
                        int aIndex = 0;
                        //divHtml += "<div>";
                        for (int i = 0; i < laList.Count; i++)
                        {
                            
                            string userName = laList[i].Name;
                            string userId = laList[i].UserID.ToString();
                            string counts = laList[i].counts.ToString();
                            divHtml += "<div class='div_InputModel'><a class='myLink' href='la_my.aspx?ud=" + userId + "'><h3>" + userName + " ( " + counts + " )</h3></a></div>";
                            //if ((i + 1) % 4 == 0)
                            //{
                            //    divHtml += "</div><div>";
                            //}
                        }
                        //divHtml += "</div>";
                        myat.InnerHtml = divHtml;
                    }
                    else
                    {
                        DAL.Common.Alert("尚未有用户在本系统记录活动,网页将关闭");
                        Response.Write("<script>window.opener=null;window.close();</script>");
                    }
                }
                else
                {
                    DAL.Common.Alert("你无权查看本页面,网页将关闭!");
                    Response.Write("<script>window.opener=null;window.close();</script>");
                }
                
            }
        }
    }
}
