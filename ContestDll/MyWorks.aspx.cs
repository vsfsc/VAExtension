using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ContestDll
{
    public partial class MyWorks : LayoutsPageBase
    {
        #region 控件定义
        protected GridView gvMyWorks;
        protected DropDownList ddlCourses;
        protected DropDownList ddlPeriods;
        protected Button clearSets;
        protected Label showperiod;
        protected Label error;
        #endregion
        #region 变量
        /// <summary>
        /// 变量与控件定义
        /// </summary>
        private SPWeb web; //当前网站
        #endregion
        #region 属性
        private long userID=0;
        private long UserID
        {
            get
            {
                if (userID == 0)
                    userID = DAL.Common.LoginID;
                return userID;
            }
        }
        private List<CSMyWorks> dtMyWorks ;
        private List<CSMyWorks> GetMyWorks
        {
            get
            {
                if (dtMyWorks == null)
                    dtMyWorks = DAL.Works.GetMyWorks(UserID);
                return dtMyWorks;
            }
            set
            {
                dtMyWorks = value;
            }
        }
        #endregion
        #region 事件
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }
        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            //初始化"我的作品列表" gvMyWorks组件事件
            gvMyWorks.PageIndexChanging += new GridViewPageEventHandler(gvMyWorks_PageIndexChanging);
            gvMyWorks.RowDataBound += new GridViewRowEventHandler(gvMyWorks_RowDataBound);
            gvMyWorks.RowCommand += new GridViewCommandEventHandler(gvMyWorks_RowCommand);

        }

        void gvMyWorks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
            long worksId =long.Parse ( drv.Cells[0].Text.Trim());
            List<CSMyWorks>  dt = DAL.Works.GetMyWorks(UserID );
            int nn = dt.Count;
            List<CSMyWorks> drbyWorksId = dt.Where (p=>p.WorksID== worksId  ).ToList ();
            nn = drbyWorksId.Count ;
            long courseId = (long)drbyWorksId[0].CourseID;
            List<Course > dt2 = DAL.Course.GetCourses() ;
            List<Course> drbyCourseId = dt2.Where (p=>p.CourseID== courseId  ).ToList ();
            string courseUrl = drbyCourseId[0].Url;
            if (e.CommandName == "ViewWorks")
            {
                Response.Redirect("Comments.aspx?View=1&&WorksID=" + worksId);
            }
            if (e.CommandName == "EditWorks")
            {
                Response.Redirect("OnlineEnroll.aspx");
            }
        }
        void gvMyWorks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)// || e.Row.RowType == DataControlRowType.Header)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e4e3f7'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                e.Row.Attributes.Add("style", "height:35px;");
                string worksState = DataBinder.Eval(e.Row.DataItem, "StateName").ToString();
                Button btnEditWorks = (Button)e.Row.FindControl("btnEdit");
                if (worksState == "已保存")
                {
                    //e.Row.BackColor = System.Drawing.Color.Azure;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.CornflowerBlue;
                }
                else if (worksState == "已提交")
                {
                    //e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.DarkBlue;
                    btnEditWorks.Enabled = false;
                    btnEditWorks.ToolTip = "作品已提交成功，禁止修改";
                }
                else if (worksState == "评分中")
                {
                    //e.Row.BackColor = System.Drawing.Color.LightPink;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.CadetBlue;
                    btnEditWorks.Enabled = false;
                    btnEditWorks.ToolTip = "作品已在评分中，禁止修改";
                }
                else if (worksState == "评分完成")
                {
                    //e.Row.BackColor = System.Drawing.Color.LightSlateGray;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.DeepPink;
                    btnEditWorks.Enabled = false;
                    btnEditWorks.ToolTip = "作品评分已完成，禁止修改";
                }
                else if (worksState == "公示中")
                {
                    //e.Row.BackColor = System.Drawing.Color.LightGray;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.DarkOrange;
                    btnEditWorks.Enabled = false;
                    btnEditWorks.ToolTip = "作品已在公示中，禁止修改";
                }
                for (int i = 0; i <= 7; i++)
                {
                    //方法一： 
                    e.Row.Cells[i].Text = " " + e.Row.Cells[i].Text + " ";
                    e.Row.Cells[i].Wrap = false;

                    //方法二：
                    //e.Row.Cells[i].Text = "<nobr>#nbsp; " + e.Row.Cells[i].Text + " </nobr>"; 
                }
                if (e.Row.Cells[5].Text.Trim() == "0" || string.IsNullOrEmpty(e.Row.Cells[5].Text.Trim()))
                {
                    e.Row.Cells[5].Text = "尚未公布";
                }
                string relationship = DataBinder.Eval(e.Row.DataItem, "Relationship").ToString();
                //int relationship = Convert.ToInt32(e.Row.Cells[6].Text);
                switch (relationship)
                {
                    case "0":
                        e.Row.Cells[6].Text = "★ 独创作者 ★";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                        break;
                    case "1":
                        e.Row.Cells[6].Text = "☆ 团队队长 ☆";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.SeaGreen;
                        break;
                    case "2":
                        e.Row.Cells[6].Text = "△ 团队成员 △";
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkSlateBlue;
                        break;
                }

            }


        }

        void gvMyWorks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyWorks.PageIndex = e.NewPageIndex;
            BindWorks(GetMyWorks );
        }
        protected void gvMyWorks_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "Desc")
                    ViewState["OrderDire"] = "ASC";
                else
                    ViewState["OrderDire"] = "Desc";
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            BindWorks(GetMyWorks  );
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                //我的作品gvMyWorks事件
                gvMyWorks.PagerSettings.Mode = PagerButtons.NumericFirstLast;
                gvMyWorks.PagerSettings.FirstPageText = "1";
                gvMyWorks.DataKeyNames = new string[] { "WorksID" };
                ViewState["SortOrder"] = "Score";
                ViewState["OrderDire"] = "ASC";
                //ddl竞赛和期次绑定
                BindCourses(DAL.Course.GetCourses());
                ddlPeriods.Visible = false;
                //BindPeriods(Convert.ToInt64(ddlCourses.SelectedValue));
                if (SPContext.Current.Web.CurrentUser  == null)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "message", "alert('请先登录');window.opener ='';window.open('','_self');window.close()", true);
                    //return;
                }
                else
                {
                    List<CSMyWorks > dtMyWorks = DAL.Works.GetMyWorks(UserID);
                    GetMyWorks = dtMyWorks;
                    BindWorks(dtMyWorks);
                }
            }
        }

        #endregion


        #region 方法
        /// <summary>
        /// 获取当站网站Url
        /// </summary>
        public SPWeb myWeb
        {
            get
            {
                if (web == null)
                {
                    web = SPContext.Current.Web;
                }
                return web;
            }
        }
        /// <summary>
        /// 我的作品列表
        /// </summary>
        /// <param name="dt"></param>
        public void BindWorks(List<CSMyWorks > dt)
        {
            gvMyWorks.DataSource = dt;
            gvMyWorks.DataBind();
        }

        /// <summary>
        /// 绑定竞赛下拉列表级
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CourseID"></param>
        public void BindCourses(List<Course>  dt)
        {
            ddlCourses.Items.Clear();
            ddlCourses.Items.Add(new ListItem("请选择所属竞赛", "0"));
            //DataRow[] drs = dt.Select("LevelID=0");
            foreach (Course  dr in dt )
            {
                ddlCourses.Items.Add(new ListItem(dr.CourseName, dr.CourseID.ToString()));
            }
        }
        /// <summary>
        /// 绑定期次下拉列表级
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="courseId"></param>
        public void BindPeriods(long courseId)
        {
            List<Periods > dt = DAL.Periods.GetPeriods();
            ddlPeriods.Items.Clear();
            ddlPeriods.Items.Add(new ListItem("请选择所属期次", "0"));
            if (courseId > 0)
            {
                ddlPeriods.Visible = true;
                clearSets.Visible = true;
                List<Periods> drs = dt.Where (p=>p.CourseID== courseId ).ToList();
                long nnn = drs.Count ;
                if (drs.Count  <= 0)
                {
                    ddlPeriods.Visible = false;
                    showperiod.Visible = true;
                }
                else
                {
                    showperiod.Visible = false;
                    foreach (Periods  dr in drs)
                    {
                        ddlPeriods.Items.Add(new ListItem(dr.PeriodTitle, dr.PeriodID.ToString()));
                    }
                }
            }
        }
        #endregion

        protected void ddlCourses_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            long userID = DAL.Common.LoginID;
            if (userID  == 0)
            {
                error.Text = "请先登录！";
            }
            else
            {
                long courseId = Convert.ToInt64(ddlCourses.SelectedValue);
                BindPeriods(courseId);
                if (courseId > 0)
                {
                    List<CSMyWorks> dt = DAL.Works.GetMyWorks(userID);
                    int nn = dt.Count;
                    List<CSMyWorks> drbyCourse = dt.Where(p=>p.CourseID== courseId).ToList ();
                    GetMyWorks  = dtMyWorks;
                    BindWorks(dtMyWorks);
                }
            }

        }
        protected void ddlPeriods_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            long userID = UserID;
            if (userID  == 0)
            {
                error.Text = "请先登录！";
            }
            else
            {
                long periodId = Convert.ToInt64(ddlPeriods.SelectedValue);
                if (periodId > 0)
                {
                    List<CSMyWorks> dt = DAL.Works.GetMyWorks(UserID);
                    int nn = dt.Count;
                    List<CSMyWorks> drbyPeriod = dt.Where (p=>p.PeriodID== periodId ).ToList ();
                    GetMyWorks = dtMyWorks;
                    BindWorks(dtMyWorks);
                }
            }
        }
        /// <summary>
        /// 重新选择筛选条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clearSets_OnClick(object sender, EventArgs e)
        {

            //我的作品gvMyWorks事件
            gvMyWorks.PagerSettings.Mode = PagerButtons.NumericFirstLast;
            gvMyWorks.PagerSettings.FirstPageText = "1";
            gvMyWorks.DataKeyNames = new string[] { "WorksID" };

            //ddl竞赛和期次绑定
            BindCourses(DAL.Course.GetCourses() );
            ddlPeriods.Visible = false;
            List <CSMyWorks > dtMyWorks = DAL.Works.GetMyWorks(UserID );
            GetMyWorks = dtMyWorks;
            BindWorks(dtMyWorks);
            showperiod.Visible = false;
            clearSets.Visible = false;
        }
    }
}
