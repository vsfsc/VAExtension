using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using ContestDll.DAL;

namespace ContestDll
{
    public class PreliminaryEvaluate : LayoutsPageBase
    {
        #region 控件定义
        protected GridView gvWorks;
        protected Button btnSearch;
        protected TextBox txtWorksName;
        protected DropDownList ddlWorksType;
        #endregion
        #region 属性
        private List<WorksType> GetDSWorksType
        {
            get
            {
                if (ViewState["dsWorksType"] == null)
                {
                    List<WorksType>  ds =  DAL.Works.GetWorksType( );
                    ViewState["dsWorksType"] = ds;
                }
                return (List<WorksType>)ViewState["dsWorksType"];
            }
        }
        private List<CSWorksToEvaluate> GetWorksToEvaluate
        {
            get
            {
                if (ViewState["WorksAll"] == null)
                {
                    long contestID = DAL.Common.GetContestID(Page);
                    List<CSWorksToEvaluate> dsSearch = DAL.Works.GetWorksToEvaluate(DAL.Common.LoginID, contestID,0);
                    ViewState["WorksAll"] = dsSearch;
                }
                return (List<CSWorksToEvaluate>)ViewState["WorksAll"];
            }
        }
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //专家评分操作
                    if (!DAL.Common. IsWebAdmin && !DAL.Common. LoginRole.Contains(7))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('没有权限');top.location.href='" +DAL.Common.SPWeb.Url + "'</script>");
                        return;
                    }
                }
                catch
                {
                }
                gvWorks.DataKeyNames = new string[] { "WorksID" };
                gvWorks.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
                InitControl();
                btnSearch_Click(null, null);
            }
            btnSearch.Click += new EventHandler(btnSearch_Click);
            gvWorks.PageIndexChanging += new GridViewPageEventHandler(gvWorks_PageIndexChanging);
            gvWorks.RowCommand += new GridViewCommandEventHandler(gvWorks_RowCommand);
        }

        void gvWorks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail" || e.CommandName == "WorksScore")
            {
                GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值 
                string worksID = gvWorks.DataKeys[drv.RowIndex].Value.ToString();
                //List<CSWorksToEvaluate> dsTmp = (List<CSWorksToEvaluate>)ViewState["dsSearch"];
                //DataView dataView = dsTmp.Tables[0].DefaultView;
                //dataView.RowFilter = "WorksID=" + worksID;
                //DataSet ds = dsTmp.Clone();
                //ds.Merge(new DataRow[] { dataView[0].Row });
                //Session["CurrentWorks"] = ds;
                if (e.CommandName == "ViewDetail")
                    DAL.Common.OpenWindow(Page, "OnshowWorksSubmit.aspx?WorksID=" + worksID);
                else
                {
                    if (Request.QueryString["PeriodID"] != null)
                        DAL.Common.OpenWindow(Page, "Comments.aspx?WorksID=" + worksID + "&&PeriodID=" + Request.QueryString["PeriodID"]);
                    else
                        DAL.Common.OpenWindow(Page, "Comments.aspx?WorksID=" + worksID);
               }
            }
        }

        void gvWorks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorks.PageIndex = e.NewPageIndex;
            List<CSWorksToEvaluate> dt = ((List<CSWorksToEvaluate>)ViewState["dsSearch"]);
            GridBind(dt);

        }

        void btnSearch_Click(object sender, EventArgs e)
        {
          
            List<CSWorksToEvaluate>  dsSearch = GetWorksToEvaluate;
            List<CSWorksToEvaluate> dsResult=dsSearch.ToList();
            string sql = "";
            if (txtWorksName.Text.Length > 0)
            {
                string txtName = txtWorksName.Text;
                txtName = txtName.Replace("'", "''");
                txtName = txtName.Replace("%", "[%]");
                txtName = txtName.Replace("*", "[*]");

                sql = "WorksName like '%" + txtName + "%'";
                dsResult =dsResult .Where(p => p.WorksName.Contains(txtName)).ToList();
            }
            if (ddlWorksType.SelectedValue != "0")
            {
                List<CSWorksToEvaluate> dsTmp = dsResult.ToList();
                dsResult = dsTmp.Where(p=>p.WorksTypeID ==int.Parse (ddlWorksType.SelectedValue)).ToList ();
                SearchWorksTypeSublevel(int.Parse (ddlWorksType.SelectedValue), ref dsResult ,dsTmp  );
            }
            try
            {
                ViewState["dsSearch"] = dsResult ;
                GridBind(dsResult);
            }
            catch
            {
                Common.ShowMessage(Page, this.GetType(), "查询出错！");
            }
        }
        #endregion
        #region 方法
        private void SearchWorksTypeSublevel(int topLevl, ref    List<CSWorksToEvaluate> dsResult,List<CSWorksToEvaluate> dsSearch  )
        {
            List<WorksType>  dsWorkType = GetDSWorksType;
            List<WorksType> dv = dsWorkType.Where(p=>p.ParentID ==topLevl ).ToList ();
            List<CSWorksToEvaluate> dsTmp; 
            for (int i = 0; i < dv.Count; i++)
            {
                dsTmp  =dsSearch.Where(p=>p.WorksTypeID==dv[i].WorksTypeID ).ToList ();
                dsResult.AddRange(dsTmp);
                SearchWorksTypeSublevel(dv[i].WorksTypeID  , ref dsResult ,dsSearch );
            }
        }
        private void InitControl()
        {
            //作品分类
            List<WorksType>  dsWorksType1 = GetDSWorksType;
            List<WorksType> dsWorksType = new List<WorksType>();
            WorksPartition.GetWorksTypeByLevel(ref dsWorksType, dsWorksType1, 0, 0);
            List<WorksType> dsTmp = new List<WorksType>();
            WorksType  dr = new WorksType ();
            dr.WorksTypeID  = 0;
            dr.WorksTypeName  = "全部";
            dsTmp.Add(dr);
            dsTmp.AddRange(dsWorksType);
            dsWorksType = dsTmp.ToList();
            ddlWorksType.DataSource = dsWorksType;
            ddlWorksType.DataTextField = "WorksTypeName";
            ddlWorksType.DataValueField = "WorksTypeID";
            ddlWorksType.DataBind();
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
