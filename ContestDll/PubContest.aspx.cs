using System;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace ContestDll
{
    public partial class PubContest : LayoutsPageBase
    {
        #region 变量

        /// <summary>
        /// 控件定义
        /// </summary>
        protected Button btnAdd;
        protected GridView gvPeriod;
        protected Button btnSave;
        protected DropDownList ddlOneWorksType;
        protected DateTimeControl dateTimeStartSubmit;
        protected Label error;
        protected TextBox txtName;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected DropDownList ddlTwoWorksType;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtNum;
        protected RequiredFieldValidator RequiredFieldValidator4;
        protected RangeValidator RangeValidator1;
        protected InputFormTextBox txtRequire;
        protected DateTimeControl dateTimeEndSubmit;
        protected DateTimeControl dateTimeStartScore;
        protected DateTimeControl dateTimeEndScore;
        protected DateTimeControl dateTimeStartPublic;
        protected DateTimeControl dateTimeEndPublic;
        protected HiddenField hfID;
        /// <summary>
        /// 作品
        /// </summary>
        public Periods   WorksDt
        {
            get
            {
                if (worksDt == null)
                {
                    worksDt = new Periods();
                }
                return worksDt;
            }

        }
        #endregion
        #region 变量
        private List<ContestDll.WorksType>  WorksTypeDt;//作品类别
        /// <summary>
        /// 作品
        /// </summary>
        private Periods  worksDt;
        protected List<WorksType> WorksType
        {
            get
            {
                if (WorksTypeDt == null)
                    WorksTypeDt = DAL.Works.GetWorksType();
                return WorksTypeDt;
            }
            set
            {
                WorksTypeDt = value;
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
            btnAdd.Click += btnAdd_Click;
            gvPeriod.RowCommand += gvPeriod_RowCommand;
            btnSave.Click += btnSave_Click;
            ddlOneWorksType.SelectedIndexChanged += ddlOneWorksType_SelectedIndexChanged;
            dateTimeStartSubmit.DateChanged += dateTimeStartSubmit_DateChanged;
            gvPeriod.PageIndexChanging += gvPeriod_PageIndexChanging;
            gvPeriod.RowDataBound += gvPeriod_RowDataBound;

        }

        void gvPeriod_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((HyperLink)e.Row.Cells[5].FindControl("lnkStandard")).NavigateUrl = "ScoreStandard.aspx?PeriodID=" + e.Row.Cells[0].Text;
                ((HyperLink)e.Row.Cells[4].FindControl("lnkUpload")).NavigateUrl = "OnlineEnroll.aspx?IsSample=1&&PeriodID=" + e.Row.Cells[0].Text;
            }

        }

        void gvPeriod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPeriod.PageIndex = e.NewPageIndex;

            BindGvDate();
        }


        void dateTimeStartSubmit_DateChanged(object sender, EventArgs e)
        {
            SetTimChangeControl(dateTimeStartSubmit.SelectedDate);

        }
     
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!BLL.User.JudgeUserRight())
                {
                    error.Text = "";
                    try
                    {
                        long courseID = BLL.Course.GetCourseID();
                        if (courseID != 0)
                        {

                            ViewState["CourseID"] = courseID;
                            WorksType  = DAL.Works.GetWorksType();
                            SetValue();
                            SetControls(1);

                            BindOneWorksType(WorksType);
                            BindTwoWorksType(WorksType, ddlOneWorksType.SelectedValue);
                            BindGvDate();
                        }
                        else
                        {
                            SetControls(3);
                            error.Text = "请联系管理员设置竞赛网站！";

                        }
                    }
                    catch (Exception ex)
                    {
                        SetControls(3);
                        error.Text = "请联系管理员设置竞赛网站！";
                    }
                }
                else
                {
                    SetControls(3);
                    error.Text = "当前用户不可操作";
                }

            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSave_Click(object sender, EventArgs e)
        {
            //验证时间
            if (JudgeTime())
            {
                btnSave.Visible = false;
                btnAdd.Visible = true;
                Periods  dr = WorksDt;
                dr.PeriodID = long.Parse(hfID.Value);
                dr.PeriodTitle = txtName.Text;

                dr.Require = txtRequire.Text;
                dr.CourseID = long.Parse(ViewState["CourseID"].ToString());
                //dr["Type"] = rblIsUrl.SelectedValue;
                //添加作品类别
                if (long.Parse(ddlTwoWorksType.SelectedValue) == 0)
                {
                    dr.WorksTypeID = int.Parse(ddlOneWorksType.SelectedValue);
                }
                else
                {
                    dr.WorksTypeID = int.Parse(ddlTwoWorksType.SelectedValue);
                }

                if (txtNum.Text != "")
                {
                    dr.Number = int.Parse(txtNum.Text);
                }

                if (!dateTimeStartSubmit.IsDateEmpty)
                {
                    dr.StartSubmit = dateTimeStartSubmit.SelectedDate;
                }
                if (!dateTimeEndSubmit.IsDateEmpty)
                {
                    dr.EndSubmit = dateTimeEndSubmit.SelectedDate;
                }

                if (!dateTimeStartScore.IsDateEmpty)
                {
                    dr.StartScore = dateTimeStartScore.SelectedDate;
                }

                if (!dateTimeEndScore.IsDateEmpty)
                {
                    dr.EndScore = dateTimeEndScore.SelectedDate;
                }

                if (!dateTimeStartPublic.IsDateEmpty)
                {
                    dr.StartPublic = dateTimeStartPublic.SelectedDate;
                }

                if (!dateTimeEndPublic.IsDateEmpty)
                {
                    dr.EndPublic = dateTimeEndPublic.SelectedDate;
                }
                //时间和人
                dr.CreatedBy = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                dr.Created = DateTime.Now;
                dr.Flag = 1;

                DAL.Periods.UpdatePeriodsByID(dr);
                BindGvDate();
                ClearDateTime(dateTimeStartSubmit);
                ClearDateTime(dateTimeEndSubmit);
                ClearDateTime(dateTimeStartScore);
                ClearDateTime(dateTimeEndScore);
                ClearDateTime(dateTimeStartPublic);
                ClearDateTime(dateTimeEndPublic);
                SetValue();
                SetControls(1);
                error.Text = "";
                // DAL.Common.ShowMessage(this.Page, this.GetType(), "保存成功！");
                Response.Redirect( "pubContest.aspx");
                //Response.Redirect(DAL.Common.SPWeb.Url + "/_layouts/15/ContestWeb/pubContest.aspx");
            }
            else
            {
                error.Text = "请注意设置的提交、评分、公示的开放时间和截止时间为必填，开放时间必须早于截止时间！";


            }
        }
        /// <summary>
        /// 判断
        /// </summary>
        /// <returns></returns>
        private bool JudgeTime()
        {
            bool isRight = true;

            if (dateTimeStartSubmit.IsDateEmpty)
            {

                isRight = false;
            }
            if (dateTimeEndSubmit.IsDateEmpty)
            {

                isRight = false;
            }
            if (dateTimeStartScore.IsDateEmpty)
            {

                isRight = false;
            }
            if (dateTimeEndScore.IsDateEmpty)
            {

                isRight = false;
            }
            if (dateTimeStartPublic.IsDateEmpty)
            {

                isRight = false;
            }
            if (dateTimeEndPublic.IsDateEmpty)
            {

                isRight = false;
            }
            //

            if (DateTime.Compare(dateTimeStartSubmit.SelectedDate, dateTimeEndSubmit.SelectedDate) > 0)
            {
                isRight = false;
            }
            if (DateTime.Compare(dateTimeStartScore.SelectedDate, dateTimeEndScore.SelectedDate) > 0)
            {
                isRight = false;
            }
            if (DateTime.Compare(dateTimeStartPublic.SelectedDate, dateTimeEndPublic.SelectedDate) > 0)
            {
                isRight = false;
            }
            //
            if (DateTime.Compare(dateTimeEndSubmit.SelectedDate, dateTimeStartScore.SelectedDate) >= 0)
            {
                isRight = false;
            }

            if (DateTime.Compare(dateTimeEndScore.SelectedDate, dateTimeEndPublic.SelectedDate) >= 0)
            {
                isRight = false;
            }
            return isRight;

        }
        /// <summary>
        /// 组别改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ddlOneWorksType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTwoWorksType(WorksType , ddlOneWorksType.SelectedValue);
        }
        /// <summary>
        /// 行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            long userId = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
            if (e.CommandName == "Del")
            {
                GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                Periods dr = new Periods();
                dr.PeriodID = long.Parse(drv.Cells[0].Text);
                DateTime dtNow = DateTime.Now;
                DAL.Periods.DelPeriodsByID(dr, userId, dtNow);
                BindGvDate();
                ClearDateTime(dateTimeStartSubmit);
                ClearDateTime(dateTimeEndSubmit);
                ClearDateTime(dateTimeStartScore);
                ClearDateTime(dateTimeEndScore);
                ClearDateTime(dateTimeStartPublic);
                ClearDateTime(dateTimeEndPublic);

                SetValue();
                SetControls(1);

            }
           
            if (e.CommandName == "Down")
            {
                ClearDateTime(dateTimeStartSubmit);
                ClearDateTime(dateTimeEndSubmit);
                ClearDateTime(dateTimeStartScore);
                ClearDateTime(dateTimeEndScore);
                ClearDateTime(dateTimeStartPublic);
                ClearDateTime(dateTimeEndPublic);
                //
                GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                CSPeriodsWorksType   dt = DAL.Periods.GetPeriodsByID(long.Parse(drv.Cells[0].Text));
                txtName.Text = dt.PeriodTitle;

                txtRequire.Text = dt.Require;


                txtNum.Text = dt.Number.ToString ();

                if (dt.StartSubmit.ToString ()  != "")
                {
                    dateTimeStartSubmit.SelectedDate = DateTime.Parse(dt.StartSubmit.ToString());
                }
                if (dt.EndSubmit.ToString()  != "")
                {
                    dateTimeEndSubmit.SelectedDate = DateTime.Parse(dt.EndSubmit.ToString());
                }
                //
                if (dt.StartScore.ToString ()!= "")
                {
                    dateTimeStartScore.SelectedDate = DateTime.Parse(dt.StartScore.ToString());
                }
                if (dt.EndScore.ToString ()!= "")
                {
                    dateTimeEndScore.SelectedDate = DateTime.Parse(dt.EndScore.ToString());
                }
                //
                if (dt.StartPublic.ToString () != "")
                {
                    dateTimeStartPublic.SelectedDate = DateTime.Parse(dt.StartPublic.ToString());
                }
                if (dt.EndPublic.ToString()!= "")
                {
                    dateTimeEndPublic.SelectedDate = DateTime.Parse(dt.EndPublic.ToString());
                }
                //当存的不是不限时
                if (dt.LevelID.ToString ()!= "")
                {
                    //有小类
                    if (dt.LevelID.ToString () != "0")
                    {
                        BindOneWorksType( WorksType );

                        //加载作品类别
                        ddlOneWorksType.SelectedIndex = -1;
                        ddlOneWorksType.Items.FindByValue(dt.ParentID.ToString()).Selected = true;
                        BindTwoWorksType(WorksType , ddlOneWorksType.SelectedValue);
                        ddlTwoWorksType.SelectedIndex = -1;
                        ddlTwoWorksType.Items.FindByValue(dt.WorksTypeID.ToString()).Selected = true;

                    }
                    else
                    {
                        BindOneWorksType(WorksTypeDt );
                        //加载作品类别

                        ddlOneWorksType.SelectedIndex = -1;
                        ddlOneWorksType.Items.FindByValue(dt.WorksTypeID.ToString()).Selected = true;
                        BindTwoWorksType(WorksType , ddlOneWorksType.SelectedValue);
                        ddlTwoWorksType.SelectedIndex = -1;
                        ddlTwoWorksType.Items.FindByValue("0").Selected = true;
                    }
                }
                else
                {

                    BindOneWorksType(WorksTypeDt);
                    //加载作品类别
                    BindTwoWorksType(WorksTypeDt, ddlOneWorksType.SelectedValue);

                }
                hfID.Value = drv.Cells[0].Text;
                BindGvDate();
                SetControls(2);
            }
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnAdd_Click(object sender, EventArgs e)
        {
            //验证时间
            if (JudgeTime())
            {
                Periods  ds = DAL.Periods.GetPeriodsByTitle(txtName.Text.ToString());
                if (ds!=null)
                {
                    error.Text = "竞赛名称已经存在";
                    txtName.Focus();
                    return;
                }
                Periods  dr = WorksDt;
                dr.PeriodID = 1;
                dr.PeriodTitle = txtName.Text;
                
                dr.Require = txtRequire.Text;
                dr.CourseID = long.Parse(ViewState["CourseID"].ToString());
                //添加作品类别
                //当小类不限时
                if (long.Parse(ddlTwoWorksType.SelectedValue) == 0)
                {
                    //当大类有值时
                    if (long.Parse(ddlOneWorksType.SelectedValue) != 0)
                    {
                        dr.WorksTypeID = int.Parse(ddlOneWorksType.SelectedValue);
                    }
                    else
                    {

                        dr.WorksTypeID = 0;
                    }
                }
                else
                {
                    //当小类有值时
                    dr.WorksTypeID = int.Parse(ddlTwoWorksType.SelectedValue);
                }

                string dateStr = DateTime.Now.ToString("yyyyMMdd");

                //dr["Type"] = rblIsUrl.SelectedValue;
                if (txtNum.Text != "")
                {
                    dr.Number = int.Parse(txtNum.Text);
                }

                if (!dateTimeStartSubmit.IsDateEmpty)
                {
                    dr.StartSubmit = dateTimeStartSubmit.SelectedDate;
                }
                if (!dateTimeEndSubmit.IsDateEmpty)
                {
                    dr.EndSubmit = dateTimeEndSubmit.SelectedDate;
                }

                if (!dateTimeStartScore.IsDateEmpty)
                {
                    dr.StartScore = dateTimeStartScore.SelectedDate;
                }

                if (!dateTimeEndScore.IsDateEmpty)
                {
                    dr.EndScore = dateTimeEndScore.SelectedDate;
                }

                if (!dateTimeStartPublic.IsDateEmpty)
                {
                    dr.StartPublic = dateTimeStartPublic.SelectedDate;
                }

                if (!dateTimeEndPublic.IsDateEmpty)
                {
                    dr.EndPublic = dateTimeEndPublic.SelectedDate;
                }
                //时间和人
                dr.CreatedBy = BLL.User.GetUserID(SPContext.Current.Web.CurrentUser);
                dr.Created = DateTime.Now;
                dr.Flag = 1;

                DAL.Periods.InsertPeriods(dr);
                BindGvDate();
                ClearDateTime(dateTimeStartSubmit);
                ClearDateTime(dateTimeEndSubmit);
                ClearDateTime(dateTimeStartScore);
                ClearDateTime(dateTimeEndScore);
                ClearDateTime(dateTimeStartPublic);
                ClearDateTime(dateTimeEndPublic);
                SetValue();
                DAL.Common.ShowMessage(this.Page, this.GetType(), "添加成功！");
                Response.Redirect(DAL.Common.SPWeb.Url + "/_layouts/15/ContestWeb/pubContest.aspx");
            }
            else
            {
                error.Text = "请注意设置的提交、评分、公示的开放时间和截止时间，开放时间必须早于截止时间！";

            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 设置时间控件
        /// </summary>
        /// <param name="dt"></param>
        protected void SetTimeControl(DateTime dt)
        {

            if (dateTimeStartSubmit.IsDateEmpty)
            {
                dateTimeStartSubmit.SelectedDate = dt;
            }
            if (dateTimeEndSubmit.IsDateEmpty)
            {
                dateTimeEndSubmit.SelectedDate = dt.AddDays(7);
            }
            if (dateTimeStartScore.IsDateEmpty)
            {
                dateTimeStartScore.SelectedDate = dt.AddDays(8);
            }
            if (dateTimeEndScore.IsDateEmpty)
            {
                dateTimeEndScore.SelectedDate = dt.AddDays(15);
            }
            if (dateTimeStartPublic.IsDateEmpty)
            {
                dateTimeStartPublic.SelectedDate = dt.AddDays(16);
            }
            if (dateTimeEndPublic.IsDateEmpty)
            {
                dateTimeEndPublic.SelectedDate = dt.AddDays(23);
            }
        }
        /// <summary>
        /// 更改时间控件
        /// </summary>
        /// <param name="dt"></param>

        protected void SetTimChangeControl(DateTime dt)
        {
            dateTimeStartSubmit.ClearSelection();
            dateTimeEndSubmit.ClearSelection();
            dateTimeStartScore.ClearSelection();
            dateTimeEndScore.ClearSelection();
            dateTimeStartPublic.ClearSelection();
            dateTimeEndPublic.ClearSelection();


            dateTimeStartSubmit.SelectedDate = dt;
            dateTimeEndSubmit.SelectedDate = dt.AddDays(7);
            dateTimeStartScore.SelectedDate = dt.AddDays(8);
            dateTimeEndScore.SelectedDate = dt.AddDays(15);
            dateTimeStartPublic.SelectedDate = dt.AddDays(16);
            dateTimeEndPublic.SelectedDate = dt.AddDays(23);
        }
        /// <summary>
        /// 设置控件值
        /// </summary>
        protected void SetValue()
        {
            txtName.Text = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DAL.Common.SPWeb.Title;
            txtNum.Text = "1";
            txtRequire.Text = "";
            SetTimeControl(DateTime.Now);
        }
        /// <summary>
        /// 绑定组别第一级
        /// </summary>
        /// <param name="dt"></param>
        public void BindOneWorksType(List<WorksType>  dt)
        {
            List<WorksType> dts = dt.Where(p => p.LevelID == 0).ToList (); 
            ddlOneWorksType.DataSource = dts;
            ddlOneWorksType.DataTextField = "WorksTypeName";
            ddlOneWorksType.DataValueField = "WorksTypeID";
            ddlOneWorksType.DataBind();
        }
        /// <summary>
        /// 绑定组别第二级
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="WorksTypeID"></param>
        public void BindTwoWorksType(List<WorksType> dt, string WorksTypeID)
        {
            List<WorksType> dts = dt.Where(p=> p.ParentID ==int.Parse(WorksTypeID )).ToList ();
            ddlTwoWorksType.DataSource = dts;
            ddlTwoWorksType.DataTextField = "WorksTypeName";
            ddlTwoWorksType.DataValueField = "WorksTypeID";
            ddlTwoWorksType.DataBind();
        }
        /// <summary>
        /// 绑定gridview
        /// </summary>
        private void BindGvDate()
        {

            List<CSPeriodsWorksType >  dt = DAL.Periods.GetPeriodByCourseID();
            BindGridView(dt, gvPeriod);
        }
        /// <summary>
        /// 绑定works
        /// </summary>
        private void BindGridView(List<CSPeriodsWorksType> dt, GridView gv)
        {
            gv.DataSource = dt;
            gv.DataBind();
        }
        /// <summary>
        /// 控制按钮显示
        /// </summary>
        /// <param name="i"></param>
        private void SetControls(int i)
        {
            if (i == 1)
            {
                btnSave.Visible = false;
                btnAdd.Visible = true;
                hfID.Value = "0";
            }
            else if (i == 2)
            {
                btnSave.Visible = true;
                btnAdd.Visible = false;
            }
            else if (i == 3)
            {
                btnAdd.Enabled = false;
                btnSave.Enabled = false;

            }
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="dtc"></param>
        private void ClearDateTime(DateTimeControl dtc)
        {
            dtc.ClearSelection();
        }
        #endregion
    
    }
}
