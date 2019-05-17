using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Data;
namespace ContestDll
{
    public class WorksPartition : LayoutsPageBase
    {
        #region 控件定义
        protected TextBox txtWorksName;
        protected DropDownList ddlWorksType;
        protected GridView gvWorks;
        protected GridView gvExpert;
        protected Button btnSearch;
        protected Button btnGroup;
        protected Button btnOk;
        #endregion
        #region 属性
        private List<WorksType> GetDSWorksType
        {
            get
            {
                List<WorksType> ds = DAL.Works.GetWorksType();
                return ds;
            }
        }
        //获取所有专家
        private List<CSUserWithSchoolAndRole> GetDSExperts
        {
            get
            {
                List<CSUserWithSchoolAndRole> dsExpert = DAL.User.GetUserBySchoolAndRole(0, 0,7, 1,"","");
                return dsExpert;
            }
        }
        private List<CSExpertWorksTypeDetail> GetDSExpertWorksType
        {
            get
            {
                    List<CSExpertWorksTypeDetail>  dsExpertWorksType = DAL.User.GetExpertWorksTypeDetail(4);
                    return dsExpertWorksType;
            }
        }
        //所有待分组作品
        private List<CSWorksPartition> GetWorksPartition(string worksName)
        {
            long contestID = DAL.Common.GetContestID(Page);
            List<CSWorksPartition> dsSearch = DAL.Works.GetWorksPartition(contestID, worksName);
            return dsSearch;

        }
        //获取选择的专家
        private List<long> GetExperts
        {
            get
            {
                List<long> experts = new List<long>();
                foreach (GridViewRow row in gvExpert.Rows)
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("ckb0");
                    if (myCheckBox.Checked)
                    {
                        long userID = (long)gvExpert.DataKeys[row.RowIndex].Value;
                        experts.Add(userID);

                    }
                }
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
                    //组委会用户操作
                    if (!DAL.Common. IsWebAdmin && !DAL.Common.LoginRole.Contains(5))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('没有权限');top.location.href='" +DAL.Common. SPWeb.Url + "'</script>");
                        return;
                    }
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
                GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值 
                string worksID = gvWorks.DataKeys[drv.RowIndex].Value.ToString();
                DAL.Common.OpenWindow(Page, "OnshowWorksSubmit.aspx?WorksID=" + worksID);
            }
        }

        //作品分组
        void btnOk_Click(object sender, EventArgs e)
        {
            int totalCount = 0;
            ViewState["SelectedExperts"] = null;
            List<long> lstExperts = GetExperts;
            foreach (GridViewRow row in gvWorks.Rows)
            {
                CheckBox myCheckBox = (CheckBox)row.FindControl("ckb");
                if (myCheckBox.Checked)
                {
                    long worksID =long.Parse ( gvWorks.DataKeys[row.RowIndex].Value.ToString () );
                    try
                    {
                        if (BLL.Works.InsertWorksExpert(worksID, lstExperts))
                            totalCount += 1;
                    }
                    catch
                    {
                    }
                }
            }
            if (totalCount > 0)
            {
               DAL. Common.ShowMessage(Page, this.GetType(), "成功分组作品" + totalCount.ToString() + "个");
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
            string sql = "";
                string txtName = txtWorksName.Text;
            if (txtWorksName.Text.Length > 0)
            {
                txtName = txtName.Replace("'", "''");
                txtName = txtName.Replace("%", "[%]");
                txtName = txtName.Replace("*", "[*]");
                sql = "WorksName like '%" + txtName + "%'";
            }
            List<CSWorksPartition> dsSearch = GetWorksPartition(txtName );
            List<CSWorksPartition> dsResult = dsSearch.ToList ();
            if (ddlWorksType.SelectedValue != "0")
            {
                dsResult = dsResult.Where(p => p.WorksTypeID == int.Parse(ddlWorksType.SelectedValue)).ToList();
                SearchWorksTypeSublevel(int.Parse (ddlWorksType.SelectedValue), ref dsResult ,dsSearch );

            }
         

            try
            {
                ViewState["dsSearch"] = dsResult;
                GridBind(dsResult);
            }
            catch
            {
               DAL. Common.ShowMessage(Page, this.GetType(), "查询出错！");
            }
        }
        private void SearchWorksTypeSublevel(int topLevl, ref  List<CSWorksPartition> csResult, List<CSWorksPartition> dsSearch)
        {
            List<WorksType> dsWorkType = GetDSWorksType;
            List<WorksType> dv = dsWorkType.Where(p => p.ParentID == topLevl).ToList();
            List<CSWorksPartition> dsTmp = new List<CSWorksPartition>();
            for (int i = 0; i < dv.Count; i++)
            {
                dsTmp = dsSearch.Where(p => p.WorksTypeID == dv[i].WorksTypeID).ToList();
                csResult.AddRange(dsTmp);
                SearchWorksTypeSublevel(dv[i].WorksTypeID, ref csResult, dsSearch);
            }
        }
        //页码更改事件
        void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorks.PageIndex = e.NewPageIndex;
            List<CSWorksPartition> dt =  (List<CSWorksPartition>)ViewState["dsSearch"] ;
            GridBind(dt );
        }

        #endregion
        #region 事件
        /// <summary>
        /// 获取带有级别的数据集
        /// </summary>
        /// <returns></returns>
        public static void GetWorksTypeByLevel(ref List<WorksType>  dsResult, List<WorksType>  dsWorksType, int parentID, int level)
        {
            List<WorksType> dt = dsWorksType.ToList ();
            List<WorksType> drs = dt.Where (p=>p.ParentID== parentID).ToList ();
            foreach (WorksType dr in drs)
            {
                WorksType drTmp = new WorksType();
                drTmp.LevelID  = dr.LevelID ;
                drTmp.WorksTypeID = dr.WorksTypeID;
                int len = dr.WorksTypeName .Length;
                drTmp.WorksTypeName=dr.WorksTypeName.PadLeft(len + level * 6, '-');

                dsResult.Add(drTmp);
                GetWorksTypeByLevel(ref dsResult, dsWorksType,  dr.WorksTypeID, level + 1);
            }

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            ////学校
            //List<CSSchoolWorksNum > dsSchool = DAL.User.GetSchoolWorksNum(DAL.Common.ContestID(Page) );
            //List<CSSchoolWorksNum > dsTmp = new List<CSSchoolWorksNum >();
            //CSSchoolWorksNum  dr = new CSSchoolWorksNum ();
            //dr.SchoolID = 0;
            //dr.SchoolName  = "全部";
            //dsTmp.Add(dr);
            //dsTmp.AddRange (dsSchool);
            //dsSchool = dsTmp.ToList ();
            //ddlSchool.DataSource = dsSchool;
            //ddlSchool.DataTextField = "SchoolName";
            //ddlSchool.DataValueField = "SchoolID";
            //ddlSchool.DataBind();
            //作品分类
            List<WorksType>  dsWorksType1 = GetDSWorksType;
            List<WorksType> dsWorksType = new List<WorksType>();  
            GetWorksTypeByLevel(ref dsWorksType, dsWorksType1, 0, 0);
            List<WorksType> efdsTmp = new List<WorksType>(); 
            WorksType efdr = new WorksType();
            efdr.WorksTypeID  = 0;
            efdr.WorksTypeName  = "全部";
            efdsTmp.Add(efdr);
            efdsTmp.AddRange(dsWorksType);
            dsWorksType = efdsTmp.ToList ();
            ddlWorksType.DataSource = dsWorksType;
            ddlWorksType.DataTextField = "WorksTypeName";
            ddlWorksType.DataValueField = "WorksTypeID";
            ddlWorksType.DataBind();
            //专家
            List<CSUserWithSchoolAndRole>  dsExpert = GetDSExperts;
            List<CSExpertWorksTypeDetail>  dsExpertWorksType = GetDSExpertWorksType;
            foreach (CSUserWithSchoolAndRole  drExprt in dsExpert)
            {
                string worksType = "";
                List<CSExpertWorksTypeDetail> drs = dsExpertWorksType.Where(p => p.UserID == drExprt.UserID).ToList ();
                if (drs.Count > 0)
                    worksType = drs[0].WorksTypeName;
                drExprt.WorksType = worksType;

            }
            gvExpert.DataSource = dsExpert;
            gvExpert.DataBind();

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GridBind(List<CSWorksPartition > dv)
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
