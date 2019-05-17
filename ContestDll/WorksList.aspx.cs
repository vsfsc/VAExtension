using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace ContestDll
{
    public partial class WorksList : LayoutsPageBase
    {
        #region 控件定义
        protected  DropDownList ddlQiCi;
        protected GridView gvWorks;
        #endregion
        #region 属性
        /// <summary>
        /// 1、评价训练 2、评分 3、公示点评
        /// </summary>
        private int PeriodTime
        {
            get
            {
                if (ViewState["periodTime"] == null)
                {
                    ViewState["periodTime"] = BLL.Course.JudgeDate(PeriodID);
                }
                return (int)ViewState["periodTime"];
            }
        }
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
        List<CSWorksWorksType> dsSearch;
        /// <summary>
        /// 当前期次的待评作品
        /// </summary>
        private List<CSWorksWorksType> GetWorksByPeriod
        {
            get
            {
                if (dsSearch == null)
                {

                    dsSearch = DAL.Works.GetWorksByPeriodID(PeriodID);
                }
                return dsSearch ;
            }
        }
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            gvWorks.PageIndexChanging += new GridViewPageEventHandler(gvWorks_PageIndexChanging);
            gvWorks.RowCommand += new GridViewCommandEventHandler(gvWorks_RowCommand);
            gvWorks.RowDataBound += gvWorks_RowDataBound;
            ddlQiCi.SelectedIndexChanged += ddlQiCi_SelectedIndexChanged;
            if (!Page.IsPostBack)
            {
                LoadData();
                if (PeriodTime < 3)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "message", "alert('作品未到公示期');document.location.replace('" + DAL.Common.SPWeb.Url + "');", true);
                    return;
                }

                gvWorks.DataKeyNames = new string[] { "WorksID" };
                gvWorks.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);

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
                GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值 
                string worksID = gvWorks.DataKeys[drv.RowIndex].Value.ToString();
                //Response.Redirect("Comments.aspx?WorksID=" + worksID + "&&PeriodID=" + PeriodID, false);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>window.open('Comments.aspx?WorksID=" + worksID + "&&PeriodID=" + PeriodID + "')</script>");
                //LinkButton lnk = (LinkButton)(e.CommandSource);
                //lnk.PostBackUrl = "Comments.aspx?WorksID=" + worksID + "&&PeriodID=" + PeriodID;

            }
        }
        void gvWorks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorks.PageIndex = e.NewPageIndex;
            List<CSWorksWorksType>   dt = GetWorksByPeriod;
            GridBind(dt);

        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            List<CSWorksWorksType > dsSearch = GetWorksByPeriod;
            GridBind(dsSearch );
        }
        #endregion
        #region 方法

        private void LoadData()
        {
            List<CSPeriodsWorksType>  ds = DAL.Periods.GetPeriodByCourseID();
            ddlQiCi.DataSource = ds;
            ddlQiCi.DataTextField = "PeriodTitle";
            ddlQiCi.DataValueField = "PeriodID";
            ddlQiCi.DataBind();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GridBind(List<CSWorksWorksType> dv)
        {
            gvWorks.DataSource = dv;
            gvWorks.DataBind();
        }
        #endregion
    }
}
