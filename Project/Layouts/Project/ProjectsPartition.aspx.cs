using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ProjectDll;

namespace Project.Layouts.Project
{
    public partial class ProjectsPartition : LayoutsPageBase
    {
        #region 属性
        private List<ProjectDll.Subject> projSubject;
        private List<ProjectDll.Subject> GetDSWorksType
        {
            get
            {
                if (projSubject == null)
                {
                    projSubject = ProjectDll.DAL.SubjectDal.GetSubjects();
                }
                return projSubject;
            }
        }
        //获取所有专家
        private List<ProjectDll.Proj_VUserWithRole> dsExpert;
        private List<ProjectDll.Proj_VUserWithRole> GetDSExperts
        {
            get
            {
                if (dsExpert == null)
                {
                     dsExpert = ProjectDll.DAL.User.GetUserBySchoolandRole(3);//(0, 0, 3, 1);
                }
                return dsExpert;
            }
        }
        private DataSet GetDSExpertWorksType
        {
            get
            {
                if (ViewState["dsExpertWorksType"] == null)
                {
                    DataSet dsExpertWorksType = null;// ProjectDll.DAL.User.GetExpertWorksTypeDetail();
                    ViewState["dsExpertWorksType"] = dsExpertWorksType;
                }
                return (DataSet)ViewState["dsExpertWorksType"];
            }
        }
        //所有待分组作品
        private DataSet GetWorksPartition
        {
            get
            {
                if (ViewState["WorksAll"] == null)
                {

                    DataSet dsSearch = null;// DAL.Works.GetWorksPartition(ContestID);
                    ViewState["WorksAll"] = dsSearch;
                }
                return (DataSet)ViewState["WorksAll"];
            }
        }
        //获取选择的专家
        private List<object> GetExperts
        {
            get
            {
                //if (ViewState["SelectedExperts"] == null)
                //{
                List<object> experts = new List<object>();
                foreach (GridViewRow row in gvExpert.Rows)
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("ckb0");
                    if (myCheckBox.Checked)
                    {
                        object userID = gvExpert.DataKeys[row.RowIndex].Value;
                        experts.Add(userID);

                    }
                }
                //ViewState["SelectedExperts"] = experts;
                //}
                return experts;
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
                    ////组委会用户操作
                    //if (!IsWebAdmin && !LoginRole.Contains(5))
                    //{
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('没有权限');top.location.href='" + SPWeb.Url + "'</script>");
                    //    return;
                    //}
                }
                catch
                {
                }
                gvWorks.DataKeyNames = new string[] { "WorksID" };
                gvWorks.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
                gvExpert.DataKeyNames = new string[] { "UserID" };
                InitControl();
                btnSearch_Click(null, null);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script defer>ShowNo();</script>");


            }
            btnSearch.Click += new EventHandler(btnSearch_Click);
            btnGroup.Click += new EventHandler(btnGroup_Click);
            btnOk.Click += new EventHandler(btnOk_Click);
            gvWorks.PageIndexChanging += new GridViewPageEventHandler(gvUser_PageIndexChanging);
            gvWorks.RowCommand += new GridViewCommandEventHandler(gvWorks_RowCommand);
        }


        /// <summary>
        /// 查看提交作品详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvWorks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
            //    GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值 
            //    string worksID = gvWorks.DataKeys[drv.RowIndex].Value.ToString();
            //    DataSet dsTmp = (DataSet)ViewState["dsSearch"];
            //    DataView dataView = dsTmp.Tables[0].DefaultView;
            //    dataView.RowFilter = "WorksID=" + worksID;
            //    DataSet ds = dsTmp.Clone();
            //    ds.Merge(new DataRow[] { dataView[0].Row });
            //    Session["CurrentWorks"] = ds;
            //    Common.OpenWindow(Page, "OnshowWorksSubmit.aspx");
            //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>window.open('OnshowWorksSubmit.aspx?WorksID="+worksID +")</script>");
            }
        }

        //作品分组
        void btnOk_Click(object sender, EventArgs e)
        {
            int totalCount = 0;
            ViewState["SelectedExperts"] = null;
            List<object> lstExperts = GetExperts;
            foreach (GridViewRow row in gvWorks.Rows)
            {
                CheckBox myCheckBox = (CheckBox)row.FindControl("ckb");
                if (myCheckBox.Checked)
                {
                    object worksID = gvWorks.DataKeys[row.RowIndex].Value;
                    try
                    {
                        //if (BLL.Works.InsertWorksExpert(worksID, lstExperts))
                        //    totalCount += 1;
                    }
                    catch
                    {
                    }
                }
            }
            if (totalCount > 0)
            {
                //Common.ShowMessage(Page, this.GetType(), "成功分组作品" + totalCount.ToString() + "个");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script defer>ShowNo();</script>");
                ClearExpert();
                //重新查询
                ViewState["WorksAll"] = null;
                btnSearch_Click(null, null);

            }
        }

        void btnGroup_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script defer>ShowYes();</script>");
        }
        //查询(处理特殊符号)
        void btnSearch_Click(object sender, EventArgs e)
        {
            return;
            DataSet dsSearch = GetWorksPartition;
            string sql = "";
            if (txtWorksName.Text.Length > 0)
            {
                string txtName = txtWorksName.Text;
                txtName = txtName.Replace("'", "''");
                txtName = txtName.Replace("%", "[%]");
                txtName = txtName.Replace("*", "[*]");
                sql = "WorksName like '%" + txtName + "%'";
            }
            if (ddlWorksType.SelectedValue != "0")
            {
                string tmpSql = "";
                SearchWorksTypeSublevel(ddlWorksType.SelectedValue, ref tmpSql);
                if (sql.Length > 0)
                    sql = sql + " and (WorksTypeID=" + ddlWorksType.SelectedValue + tmpSql + ")";
                else
                    sql = "(WorksTypeID=" + ddlWorksType.SelectedValue + tmpSql + ")";
            }
            if (ddlSchool.SelectedValue != "0")
            {
                if (sql.Length > 0)
                    sql = sql + " and SchoolID=" + ddlSchool.SelectedValue;
                else
                    sql = "SchoolID=" + ddlSchool.SelectedValue;
            }

            DataView dv = new DataView(dsSearch.Tables[0]);
            try
            {
                dv.RowFilter = sql;
                DataSet ds = new DataSet();
                ds.Tables.Add(dv.ToTable());
                ViewState["dsSearch"] = ds;
                GridBind(dv);
            }
            catch
            {
                //Common.ShowMessage(Page, this.GetType(), "查询出错！");
            }
        }
        private void SearchWorksTypeSublevel(string topLevl, ref  string strSql)
        {
            //DataSet dsWorkType = GetDSWorksType;
            //DataView dv = new DataView(dsWorkType.Tables[0]);
            //dv.RowFilter = "ParentID=" + topLevl;
            //for (int i = 0; i < dv.Count; i++)
            //{
            //    strSql += " or WorksTypeID=" + dv[i]["WorksTypeID"];
            //    SearchWorksTypeSublevel(dv[i]["WorksTypeID"].ToString(), ref strSql);
            //}
        }
        //页码更改事件
        void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorks.PageIndex = e.NewPageIndex;
            DataTable dt = ((DataSet)ViewState["dsSearch"]).Tables[0];
            GridBind(dt.DefaultView);
        }

        #endregion
        #region 事件
        /// <summary>
        /// 获取带有级别的数据集
        /// </summary>
        /// <returns></returns>
        public static void GetWorksTypeByLevel(ref DataSet dsResult, DataSet dsWorksType, int parentID, int level)
        {
            DataTable dt = dsWorksType.Copy().Tables[0];
            DataRow[] drs = dt.Select("ParentID=" + parentID);
            foreach (DataRow dr in drs)
            {
                DataTable dtTmp = dt.Clone();
                DataRow drTmp = dtTmp.NewRow();
                drTmp[0] = dr[0];
                int len = dr[1].ToString().Length;
                drTmp[1] = dr[1].ToString().PadLeft(len + level * 6, '-');
                dtTmp.Rows.Add(drTmp);
                dsResult.Merge(dtTmp);
                GetWorksTypeByLevel(ref dsResult, dsWorksType, int.Parse(dr[0].ToString()), level + 1);
            }

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            //学校
            //DataSet dsSchool =DAL.School.GetSchoolWorksNum(ContestID);
            //DataSet dsTmp = dsSchool.Clone();
            //DataRow dr = dsTmp.Tables[0].NewRow();
            //dr["SchoolID"] = 0;
            //dr["SchoolName"] = "全部";
            //dsTmp.Tables[0].Rows.Add(dr);
            //dsTmp.Merge(dsSchool);
            //dsSchool = dsTmp.Copy();
            //ddlSchool.DataSource = dsSchool;
            //ddlSchool.DataTextField = "SchoolName";
            //ddlSchool.DataValueField = "SchoolID";
            //ddlSchool.DataBind();
            ////作品分类
            //DataSet dsWorksType1 = null;// GetDSWorksType;
            //DataSet dsWorksType = dsWorksType1.Clone();
            //GetWorksTypeByLevel(ref dsWorksType, dsWorksType1, 0, 0);
            //dsTmp = dsWorksType.Clone();
            //dr = dsTmp.Tables[0].NewRow();
            //dr["WorksTypeID"] = 0;
            //dr["WorksTypeName"] = "全部";
            //dsTmp.Tables[0].Rows.Add(dr);
            //dsTmp.Merge(dsWorksType);
            //dsWorksType = dsTmp.Copy();
            //ddlWorksType.DataSource = dsWorksType;
            //ddlWorksType.DataTextField = "WorksTypeName";
            //ddlWorksType.DataValueField = "WorksTypeID";
            //ddlWorksType.DataBind();
            ////专家
            //DataSet dsExpert = null;// GetDSExperts;
            //DataSet dsExpertWorksType = null;// GetDSExpertWorksType;
            //dsExpert.Tables[0].Columns.Add(new DataColumn("WorksType", typeof(string)));
            //foreach (DataRow drExprt in dsExpert.Tables[0].Rows)
            //{
            //    string worksType = "";
            //    DataRow[] drs = dsExpertWorksType.Tables[0].Select("UserID=" + drExprt["UserID"].ToString());
            //    //foreach (DataRow dr1 in drs)
            //    //    worksType += dr1["WorksTypeName"].ToString() + "--";
            //    //if (worksType.Length > 0)
            //    //    worksType = worksType.Substring(0, worksType.Length - 2);
            //    if (drs.Length > 0)
            //        worksType = drs[0]["WorksTypeName"].ToString();
            //    drExprt["WorksType"] = worksType;

            //}
            //dsExpert.AcceptChanges();
            //gvExpert.DataSource = dsExpert.Tables[0].DefaultView;
            //gvExpert.DataBind();

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GridBind(DataView dv)
        {
            gvWorks.DataSource = dv;
            gvWorks.DataBind();
        }
        #endregion
        #region 方法
        /// <summary>
        /// 分组后，清空选择的专家
        /// </summary>
        private void ClearExpert()
        {
            foreach (GridViewRow row in gvExpert.Rows)
            {
                CheckBox myCheckBox = (CheckBox)row.FindControl("ckb0");
                if (myCheckBox.Checked)
                    myCheckBox.Checked = false;
            }
        }
        #endregion
    }
}
