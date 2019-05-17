using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Text;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using ProjectDll;
using System.Collections.Generic;

namespace Project.Layouts.Project
{
    public partial class Hot : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            showHotProjects();
        }
        private void showHotProjects()
        {
            HtmlContainerControl htmlLi;
            htmlLi = new HtmlGenericControl("li");
            HtmlContainerControl htmlUl= new HtmlGenericControl("ul");
           
            List<Proj_CountProjMember> dt = ProjectDll.DAL.ProjectDal.GetProjMemberCount();
            dt = dt.Where(ps => ps.MemberCount != 0).ToList();

            foreach(var item in dt)
            {
                string str = "<li class='item-blue item-normal'> ";
               
                str += "<div class='list-main'><span class='mark'></span>";

                str += "<div class='main-title'><span class='span-1'> </span><span class='span-2'>状态</span><span class='span-3'>负责人</span><span class='span-4'>所属学科</span><span class='span-5'>对接情况</span><span class='span-6'>参赛人数</span></div>";
                            
                str += "<div class='main-cont'><span class='span-1'><p title='"+ item.PName +"'><a target='_blank' href='PDetails.aspx?ProjectID="+item.ProjectID+ "&pageTypeId=0'>"+item.PName+"</a></p></span>";                             //显示项目名称
                                                                                                                                                                                                      
                str += "<span class='span-2 status-on'>" + item.StateName + "</span>";  //获取项目状态，-进行中

                str += "<span class='span-3'>" + item.Name + "</span> ";             //项目负责人

                str += "<span class='span-4'>" + item.SubjectName + "</span>";     //项目状态

                if (item.IsMatch == 2)
                {
                    str += "<span class='span-5'>对接成功</span>";
                }

                else if (item.IsMatch == 1)
                {
                    str += "<span class='span-5'>待对接</span>";
                }
                else
                {
                    str += "<span class='span-5'>未开放对接</span>";
                }
                str += "<span class='span-6'>"+item.MemberCount+"</span>";
                str += "</div></div>";

                //htmlLi.Controls.Add(new LiteralControl(str));
                string introStr = item.Introduce;
                if (string.IsNullOrEmpty(introStr))
                {
                    introStr = "无";
                }
                else
                {
                    if (introStr.Length>150)//项目简介多余100字
                    {
                        introStr = introStr.Substring(0,149) + "......";
                    }

                }
                
                str += "<div class='list-info'><p><a target = '_blank' href = 'PDetails.aspx?ProjectID=" + item.ProjectID + "&pageTypeId=0'><span>项目简介：</span >" + introStr + " </a ></p ><p><a target = '_blank' href = 'PDetails.aspx?ProjectID=" + item.ProjectID + "&pageTypeId=0' class='more'>了解更多 &gt;</a></p></div>";

                str += "</li>";
                //htmlLi.Controls.Add(new LiteralControl(str));

                //htmlLi.Attributes.Add("class", "item-blue item-normal");

                htmlUl.Controls.Add(new LiteralControl(str));
                htmlUl.Attributes.Add("class", "competition-list");

            }
           

            divList.Controls.Add(htmlUl);
        }
    }
}
