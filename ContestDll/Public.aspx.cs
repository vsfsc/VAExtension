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
    public partial class Public : LayoutsPageBase
    {
        #region 控件定义
        protected TextBox txtWorksName;
        protected TextBox txtWorksCode;
        protected GridView gvWorks;
        protected Button btnSearch;
        protected DropDownList ddlWorksType;
        #endregion
        #region 属性
        /// <summary>
        /// 当前网站的比赛ID
        /// </summary>
        public long ContestID
        {
            get
            {
                if (ViewState["ContestID"] == null)
                {
                    ViewState["ContestID"] = System.Configuration.ConfigurationManager.AppSettings["contestID"];
                }
                return Convert.ToInt64(ViewState["ContestID"]);
            }

        }
        List<WorksType> ds;
        private List<WorksType> GetDSWorksType
        {
            get
            {
                if (ds == null)
                    ds = ContestDll.DAL.Works.GetWorksType();
                return ds;
            }
        }
        List<CSWorksPublic> dsSearch;
        //所有待分组作品
        private List<CSWorksPublic> GetWorksPublic
        {
            get
            {
                if (dsSearch == null)
                {
                    long contestID = ContestID;
                    if ((int)ViewState["Para"] > 0)
                        contestID = contestID - 1;
                    dsSearch = ContestDll.DAL.Works.GetWorksPublic(contestID);
                }
                return dsSearch;
            }
        }
        #endregion
        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //try
                //{
                //    //所有登录用户
                //    if (SPUser == null)
                //    {
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script defer>alert('请先登录');top.location.href='" + SPWeb.Url + "'</script>");
                //        return;
                //    }
                //}
                //catch
                //{
                //}
                ViewState["Para"] = Request.QueryString.Count;
                gvWorks.DataKeyNames = new string[] { "WorksID" };
                gvWorks.PageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
                InitControl();
                btnSearch_Click(null, null);

            }
            ////组委会用户操作
            //if (!IsWebAdmin && LoginRole != 5)
            //{
            //    gvWorks.Columns[gvWorks.Columns.Count - 1].Visible = false; 
            //}

            btnSearch.Click += new EventHandler(btnSearch_Click);
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
            if (e.CommandName == "ViewDetail" || e.CommandName == "ViewDetailScore")
            {
                GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值 
                string worksID = gvWorks.DataKeys[drv.RowIndex].Value.ToString();
                List<CSWorksPublic> dsTmp = (List<CSWorksPublic>)ViewState["dsSearch"];
                List<CSWorksPublic> dataView = dsTmp.Where(p => p.WorksID == long.Parse(worksID)).ToList() ;
                //Session["CurrentWorks"] = dataView;

                if (e.CommandName == "ViewDetail")
                    Common.OpenWindow(Page, "OnshowWorksSubmit.aspx?WorksID=" + worksID);//OnshowWorksSubmit.aspx
                else
                {
                    //还没有进行初评
                    if (dataView[0].ExpertScore == 0)
                    {
                        Common.ShowMessage(Page, this.GetType(), "还未进行评审！");
                        return;
                    }
                    Common.OpenWindow(Page, "WorksScoreDetail.aspx");
                }
            }
        }
        //页码更改事件
        void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorks.PageIndex = e.NewPageIndex;
            List<CSWorksPublic> dt = (List<CSWorksPublic>)ViewState["dsSearch"];
            GridBind(dt);
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            List<CSWorksPublic> dsSearch = GetWorksPublic;
            string txtName;
            if (txtWorksName.Text.Length > 0)
            {
                txtName = txtWorksName.Text;
                txtName = txtName.Replace("'", "''");
                txtName = txtName.Replace("%", "[%]");
                txtName = txtName.Replace("*", "[*]");
                dsSearch =dsSearch.Where (p=>p.WorksName.Contains( txtName)).ToList ();
            }
            if (txtWorksCode.Text.Length > 0)
            {
                txtName = txtWorksCode.Text;
                txtName = txtName.Replace("'", "''");
                txtName = txtName.Replace("%", "[%]");
                txtName = txtName.Replace("*", "[*]");
                dsSearch = dsSearch.Where(p => p.WorksCode.Contains(txtName)).ToList();

            }
            if (ddlWorksType.SelectedValue != "0")
            {
                List<CSWorksPublic> dsResult = dsSearch.ToList();
                List<CSWorksPublic> dsTmp = dsSearch.Where(p => p.WorksTypeID == int.Parse(ddlWorksType.SelectedValue)).ToList();
                //dsSearch.AddRange(dsTmp);
                
                SearchWorksTypeSublevel(int.Parse (ddlWorksType.SelectedValue),dsResult , ref dsTmp );

            }

             try
            {
                ViewState["dsSearch"] = dsSearch ;
                GridBind(dsSearch);
            }
            catch
            {
                Common.ShowMessage(Page, this.GetType(), "查询出错！");
            }
        }
        #endregion
        #region 方法
        private void SearchWorksTypeSublevel(int topLevl,  List<CSWorksPublic> dsSearch,  ref  List<CSWorksPublic>  dsResult)
        {
            List<WorksType>  dsWorkType = GetDSWorksType;
            List<WorksType> dv = dsWorkType.Where(p=>p.ParentID == topLevl  ).ToList ();
            for (int i = 0; i < dv.Count; i++)
            {
                List<CSWorksPublic> dsTmp = dsSearch.Where(p => p.WorksTypeID == dv[i].WorksTypeID).ToList();
                dsResult.AddRange(dsTmp);
                SearchWorksTypeSublevel(dv[i].WorksTypeID, dsSearch, ref dsResult);
            }
        }
        /// <summary>
        /// 获取带有级别的数据集
        /// </summary>
        /// <returns></returns>
        private void GetWorksTypeByLevel(ref List<WorksType> dsResult, List<WorksType> dsWorksType, int parentID, int level)
        {
            List<WorksType> drs = dsWorksType.Where (p=>p.ParentID==  parentID).ToList ();
            foreach (WorksType  dr in drs)
            {
                WorksType drTmp = new WorksType();
                drTmp.WorksTypeID = dr.WorksTypeID;
                int len = dr.WorksTypeName.Length;
                drTmp.WorksTypeName  = dr.WorksTypeName .PadLeft(len + level * 6, '-');
                dsResult.Add(drTmp);
                GetWorksTypeByLevel(ref dsResult, dsWorksType,  dr.WorksTypeID, level + 1);
            }

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            //作品分类
            List<WorksType>  dsWorksType1 = GetDSWorksType;
            List<WorksType> dsWorksType = new List<WorksType>();
            GetWorksTypeByLevel(ref dsWorksType, dsWorksType1, 0, 0);
            List<WorksType> dsTmp = new List<WorksType>();
            WorksType dr = new WorksType();
            dr.WorksTypeID = 0;
            dr.WorksTypeName = "全部";
            dsTmp.Add(dr);
            dsTmp.AddRange(dsWorksType);
            dsWorksType = dsTmp ;
            ddlWorksType.DataSource = dsWorksType;
            ddlWorksType.DataTextField = "WorksTypeName";
            ddlWorksType.DataValueField = "WorksTypeID";
            ddlWorksType.DataBind();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GridBind(List<CSWorksPublic> dv)
        {
            gvWorks.DataSource = dv;
            gvWorks.DataBind();
        }
        #endregion
    }
}
