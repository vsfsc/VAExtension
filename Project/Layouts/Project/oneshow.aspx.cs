using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace Project.Layouts.Project
{
    using System.Web.UI.WebControls;

    public partial class oneshow : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgvTaskList("我的任务");
                BindgvProjectFile();
                
                //StringBuilder sb = FindChanges();
                //myChanges.InnerHtml = sb.ToString();
            }
        }
        private static string CutWebUrl(string myurl)
        {
            string url = "";
            string weburl = SPContext.Current.Web.Url;
            if (myurl.Contains(weburl))
            {
                url = myurl.Replace(weburl, "");
            }
            return url;
        }
        private void BindgvProjectFile()
        {
            var cellStrings = new[] { "IconUrl", "Name", "fileUrl", "fileSize", "fileAuthor", "TimeCreated", "fileExName" };
            DataTable dt = ProjectDll.DAL.SharePointFileHelper.BindDoclib("项目文档", cellStrings);
            gvmyFiles.DataSource = dt;
            gvmyFiles.DataBind();
        }

        protected void gvmyFiles_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var img = (Image)e.Row.FindControl("iconImg");//文件类型图标
                var imgUrl = DataBinder.Eval(e.Row.DataItem, "IconUrl").ToString();
                img.ImageUrl = SPContext.Current.Web.Url + "/" + imgUrl;

                var fileUrl = CutWebUrl(DataBinder.Eval(e.Row.DataItem, "fileUrl").ToString());//文件地址
                var elink = (HyperLink)e.Row.FindControl("viewDoc");
                elink.NavigateUrl = String.Format(SPContext.Current.Web.Url + "/_layouts/15/WopiFrame.aspx?sourcedoc=" + fileUrl + "&action=VIEW");
                elink.ForeColor = System.Drawing.Color.Blue;
            }
        }
        public static StringBuilder FindChanges()
        {
            StringBuilder sb=new StringBuilder();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPSiteDataQuery query1 = new SPSiteDataQuery();
                //query1.ViewFields ="<OrderBy><FieldRef Name=\"Modified\" Ascending=\"False\" /></OrderBy><Where><Gt><FieldRef Name='Modified' /><Value Type='DateTime'><Today OffsetDays='-7' /></Value></Gt></Where>";
                query1.RowLimit = 100;
                //query1.Webs = "<Webs Scope=\"Recursive\" />";
                query1.Lists = "<Lists BaseType=\"0\" />";
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    DataTable dt = site.RootWeb.GetSiteData(query1);
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("dt[" + column.ColumnName + "]: " + row[column]);
                        }
                    }
                }
            });
            return sb;
        }

        private void BindgvTaskList(string lTitle)
        {
            //string[] cellStrings = new[] { "Title", "_Level", "Priority", "Status", "PercentComplete", "AssignedTo", "StartDate", "DueDate" };
            //DataTable dt = BindTasks("任务", cellStrings);
            string url = SPContext.Current.Web.Url;
            #region 提升权限运行,将SPList列表数据绑定到DataTable

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        int i = 0;
                        foreach (SPList splist in web.Lists)
                        {
                            if (splist.Title == lTitle)
                            {
                                i++;
                            }
                        }
                        if (i>0)
                        {
                            SPList list = web.Lists[lTitle];
                            SPQuery spQuery = new SPQuery();
                            spQuery.Query = "";// @"<Where><Eq><FieldRef Name='Status'/>" +"<Value Type='Text'>已完成</Value></Eq></Where>";
                            //SPListItemCollection collListItems = list.GetItems(spQuery);
                            gvTasks.DataSource = list.GetItems(spQuery).GetDataTable();
                            gvTasks.DataBind();
                        }
                        else
                        {
                            myChanges.InnerHtml = "网站中没有名称为:\"" + lTitle + "\"的列表,你无法查看对应的信息!";
                        }
                    }
                }
            });
            #endregion
            
        }
        /// <summary>
        /// Binds the tasks.
        /// </summary>
        /// <param name="taskList">The task list.</param>
        /// <param name="dtCells">The dt cells.</param>
        /// <returns>DataTable.</returns>
        public static DataTable BindTasks(string taskList, string[] dtCells)
        {
            DataTable dt=new DataTable();
            string url = SPContext.Current.Web.Url;
            #region 提升权限运行,将SPList列表数据绑定到DataTable
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[taskList];
                        SPQuery spQuery=new SPQuery();
                        spQuery.Query = @"<Where><Eq><FieldRef Name='Status'/>" +"<Value Type='Text'>已完成</Value></Eq></Where>";
                        SPListItemCollection collListItems = list.GetItems(spQuery);
                        //for (int i = 0; i < list.ItemCount; i++)
                        //{
                        //    DataRow dr = dt.NewRow();
                        //    SPListItem item = list.Items[i];
                        //    dr["Title"] = item["Title"].ToString();//名称
                        //    dr["_Level"] = item["_Level"].ToString();//级别
                        //    dr["Priority"] = item["Priority"].ToString();//优先级
                        //    dr["Status"] = item["Status"].ToString();//状态
                        //    dr["PercentComplete"] = item["PercentComplete"].ToString();//完成百分比
                        //    dr["AssignedTo"] = item["AssignedTo"].ToString();//分配对象
                        //    dr["StartDate"] = item["StartDate"].ToString();//开始日期
                        //    dr["DueDate"] = item["DueDate"].ToString();//截止日期
                        //    dt.Rows.Add(dr);
                        //}
                        //foreach (SPListItem item in collListItems)
                        //{
                        //    DataRow dr = dt.NewRow();
                        //    string tempStr = "";
                        //    dr["Title"] = item["Title"].ToString();//名称
                        //    dr["_Level"] = item["_Level"].ToString();//级别
                        //    dr["Priority"] = item["Priority"].ToString();//优先级
                        //    dr["Status"] = item["Status"].ToString();//状态
                        //    dr["PercentComplete"] = ((Double)item["PercentComplete"]*100).ToString()+"%";//完成百分比
                        //    tempStr = item["AssignedTo"].ToString();
                        //    tempStr = tempStr.Remove(tempStr.Length - 2);
                        //    dr["AssignedTo"] =tempStr ;//分配对象
                        //    dr["StartDate"] = item["StartDate"].ToString();//开始日期
                        //    dr["DueDate"] = item["DueDate"].ToString();//截止日期
                        //    dt.Rows.Add(dr);
                        //}
                        dt= collListItems.GetDataTable();
                    }
                }
            });
            return dt;
            #endregion
        }

        protected static StringBuilder SpListToDiv(string listTitle)
        {
            var sb=new StringBuilder();
            SPSite site = SPContext.Current.Site;
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists[listTitle];
            var query = from SPListItem item in list.Items orderby item.ID ascending select item;
            sb.Append("<div class='Default_left_part1_left_part1_5'>");
            sb.Append("<div class='Default_left_part1_left_part1_4'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr>");
            int i = 1;
            foreach (SPListItem item in query)
            {
                sb.Append("<td class='Default_left_part1_left_part1_4_1'><div><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td class='Default_left_part1_left_part1_4_1_1'><img alt='' src='");
                sb.Append(item["图标"] == null ? "" : item["图标"].ToString());
                sb.Append("' /></td><td class='Default_left_part1_left_part1_4_1_2'><div><a href='/Lists/List/DispForm.aspx?ID=");
                sb.Append(item["ID"].ToString());
                sb.Append("' class='Default_mylink1_2'><b>");
                sb.Append(item["标题"] == null ? "" : item["标题"].ToString());
                sb.Append("</b></a></div><div>");
                if (item["副标题"] != null)
                {
                    sb.Append(item["副标题"].ToString().Length > 15 ? item["副标题"].ToString().Substring(0, 15) + "…" : item["副标题"].ToString());
                }
                else
                {
                    sb.Append("");
                }
                sb.Append("</div></td></tr></table></div></td>");
                if (i % 2 == 0)
                {
                    sb.Append("</tr><tr>");
                }
                i++;
            }
            sb.Append("</tr></table></div>");
            sb.Append("</div>");
            return sb;
        }

    }
}
