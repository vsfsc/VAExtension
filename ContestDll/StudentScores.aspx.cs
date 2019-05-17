using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace ContestDll
{
    public class StudentScores : LayoutsPageBase
    {
        #region 控件定义
        protected global::System.Web.UI.WebControls.DropDownList ddlQiCi;
        protected global::System.Web.UI.WebControls.GridView gvWorks;
        #endregion
        #region 属性
        private long PeriodID
        {
            get
            {
                try
                {
                    return long.Parse(ddlQiCi.SelectedValue);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 当前期次的待评作品
        /// </summary>
        private List<CSWorksToEvaluate> GetWorksToEvaluate
        {
            get
            {
                if (ViewState["WorksAll"] == null)
                {
                    List<CSWorksToEvaluate> dsSearch = DAL.Works.GetWorksToEvaluate(DAL.Common.LoginID, PeriodID, 0);
                    ViewState["WorksAll"] = dsSearch;
                }
                return (List<CSWorksToEvaluate>)ViewState["WorksAll"];
            }
        }
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DAL.Common.SPWeb.CurrentUser == null)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "message", "alert('请先登录');window.opener ='';window.open('','_self');window.close()", true);
                return;
            }
            gvWorks.PageIndexChanging += new GridViewPageEventHandler(gvWorks_PageIndexChanging);
            gvWorks.RowCommand += new GridViewCommandEventHandler(gvWorks_RowCommand);
            gvWorks.RowDataBound += gvWorks_RowDataBound;
            ddlQiCi.SelectedIndexChanged += ddlQiCi_SelectedIndexChanged;
            if (!Page.IsPostBack)
            {
                gvWorks.DataKeyNames = new string[] { "WorksID" };
                gvWorks.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
                LoadData();
                btnSearch_Click(null, null);
            }

        }

        void gvWorks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[i].ForeColor = ColorTranslator.FromHtml("#000000");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //模板列
                ((HyperLink)e.Row.Cells[4].FindControl("lnkScore")).NavigateUrl = "Comments.aspx?WorksID=" + e.Row.Cells[0].Text + "&&PeriodID=" + PeriodID;
            }

        }

        void ddlQiCi_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);

        }

        void gvWorks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail" || e.CommandName == "WorksScore")
            {
                GridViewRow drv = (GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent); //此得出的值是表示那行被选中的索引值 
                string worksID = gvWorks.DataKeys[drv.RowIndex].Value.ToString();
                Response.Redirect("Comments.aspx?WorksID=" + worksID + "&&PeriodID=" + PeriodID, false);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>window.open('Comments.aspx?WorksID=" + worksID + "&&PeriodID="+PeriodID +"')</script>");
                //LinkButton lnk = (LinkButton)(e.CommandSource);
                //    lnk.PostBackUrl = "Comments.aspx?WorksID=" + worksID + "&&PeriodID=" + PeriodID;

            }
        }
        void gvWorks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorks.PageIndex = e.NewPageIndex;
            List<CSWorksToEvaluate> dt = (List<CSWorksToEvaluate>)ViewState["dsSearch"];
            GridBind(dt);

        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            List<CSWorksToEvaluate>  dsSearch = GetWorksToEvaluate;
            ViewState["dsSearch"] = dsSearch;
            GridBind(dsSearch);
        }
        #endregion
        #region 方法
        private void LoadData()
        {
            List<CSPeriodsWorksType> ds = DAL.Periods.GetPeriodByCourseID();
            ddlQiCi.DataSource = ds;
            ddlQiCi.DataTextField = "PeriodTitle";
            ddlQiCi.DataValueField = "PeriodID";
            ddlQiCi.DataBind();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GridBind(List<CSWorksToEvaluate> dv)
        {
            gvWorks.DataSource = dv;
            gvWorks.DataBind();
        }
        #endregion
    }
}
