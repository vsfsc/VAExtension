using System;
using System.Data;
using System.Web.UI.WebControls;
using CourseChooseList.Bll;
using CourseDll;
using CourseDll.Bll;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace CourseChooseList.Layouts.CourseChooseList
{
    public partial class CourseChoose : LayoutsPageBase
    {
        #region 变量
        private SPWeb web;
        private string currentSchoolYear;
        private SPUser currentUser;
        private string studentNO;
        private string grade;
        private static int dgNO;
        private string currentAccount;

        private int modulenameL;
        private int startL;
        private int endL;
        private int xueshiL;
        private int totallong;
        #endregion
        #region 属性
        public string Grade
        {
            get
            {
                if (grade == null)
                {
                    grade = "";
                    int xn = 0;
                    int i = 0;
                    try
                    {
                        string[] bj = null;
                        SPSecurity.RunWithElevatedPrivileges(delegate()
                        {
                            bj = ADHelper.GetUserGroups(StudentNO);
                        });
                        if (bj[0] != "")
                        {
                            for (; i < bj[0].Length; i++)
                            {
                                if (Char.IsNumber(bj[0], i))
                                    break;
                            }
                        }
                        xn = int.Parse("20" + bj[0].Substring(i, 2));

                    }
                    catch (Exception e)
                    {
                    }
                    int year = DateTime.Today.Year;
                    //根据学号获得学生的当前年级
                    switch (year - xn)
                    {
                        case 0: grade = "一年级"; break;
                        case 1: grade = "二年级"; break;
                        case 2: grade = "三年级"; break;
                        case 3: grade = "四年级"; break;
                        case 4: grade = "五年级"; break;
                    }
                }
                return grade;
            }
        }
        public SPUser CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    currentUser = SPWeb.CurrentUser;
                }
                return currentUser;
            }
        }
        public string StudentNO
        {
            get
            {
                if (studentNO == null)
                {
                    studentNO = SPWeb.CurrentUser.LoginName.Replace("CCC\\", "");
                }
                return studentNO;
            }
        }
        public string CurrentAccount
        {
            get
            {
                if (currentAccount == null)
                {
                    currentAccount = SPWeb.CurrentUser.LoginName;

                }
                return currentAccount;
            }

        }
        public string CurrentSchoolYear
        {
            get
            {
                if (currentSchoolYear == null)
                {
                    currentSchoolYear = currentSchoolYear = DateTime.Today.Year.ToString() + "-" + (DateTime.Today.Year + 1).ToString();
                }
                return currentSchoolYear;
            }
        }


        /// <summary>
        /// 获取当前网页
        /// </summary>
        public SPWeb SPWeb
        {
            get
            {
                web = SPContext.Current.Web;
                return web;
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
            gvCourse.RowCommand += gvCourse_RowCommand;
            gvCourse.RowDataBound += gvCourse_RowDataBound;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void gvCourse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e4e3f7'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                if (e.Row.Cells[7].Text == "1" || e.Row.Cells[8].Text != "&nbsp;")
                {
                    ((CheckBox)e.Row.FindControl("cbChoose")).Enabled = false;
                    ((CheckBox)e.Row.FindControl("cbChoose")).Checked = true;
                }
                if (e.Row.Cells[7].Text != "1" && e.Row.Cells[8].Text != "&nbsp;")
                {
                    ((Button)e.Row.FindControl("btnCancelCourse")).Enabled = true;
                }
                else
                {
                    ((Button)e.Row.FindControl("btnCancelCourse")).Enabled = false;
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void gvCourse_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelCourse")
            {
                GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                CourseManage cm = new CourseManage();
                DataRow dr = cm.CourseChooseDt.NewRow();
                dr["ID"] = long.Parse(drv.Cells[9].Text);
                dr["Flag"] = 0;
                cm.DeleteStudentCourse(dr);
                bindDG();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //bindDG();
            }
        }

        /// <summary>
        /// 获取字段长度
        /// </summary>
        //protected void Getfieldlongth()
        //{
        //    CourseManage cm = new CourseDll.Bll.CourseManage();
        //    string tableName = "Modules";
        //    DataTable dt = cm.GetFieldLongth(tableName);
        //    if (dt.Rows.Count > 0)
        //    {
        //        modulenameL=int.Parse(dt.Rows[1]["长度"].ToString());
        //        xueshiL = int.Parse(dt.Rows[4]["长度"].ToString());
        //        startL = int.Parse(dt.Rows[5]["长度"].ToString());
        //        endL = int.Parse(dt.Rows[6]["长度"].ToString());
        //        totallong = modulenameL + xueshiL + startL + endL;
        //        gvCourse.Columns[1].HeaderStyle.Width = (int)Math.Round((double)(modulenameL / totallong * 80)) + '%';
        //        gvCourse.Columns[2].HeaderStyle.Width = (int)Math.Round((double)(modulenameL / totallong * 80)) + '%';
        //        gvCourse.Columns[3].HeaderStyle.Width = (int)Math.Round((double)(modulenameL / totallong * 80)) + '%';
        //        gvCourse.Columns[4].HeaderStyle.Width = (int)Math.Round((double)(modulenameL / totallong * 80)) + '%';
        //    }
        //}
        
        #endregion

        #region 方法
        /// <summary>
        /// 绑定
        /// </summary>
        public void bindDG()
        {
            //判断当前用户是否选课


            CourseManage cm = new CourseManage();
            UserManage um = new UserManage();
            long StudentID = um.GetOrAddUserIDByAccount(StudentNO);
            DataTable dt = cm.GetCourseManage(StudentID);
            ViewState["StudentID"] = StudentID;
            gvCourse.DataSource = dt;
            gvCourse.DataBind();
            //
            int i = 0;
            int sum = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SCID"].ToString() != "")
                {
                    i++;
                    sum += int.Parse(dr["ClassHours"].ToString());


                }

            }
            int studyHour = int.Parse(System.Configuration.ConfigurationManager.AppSettings["StudyHour"]);
            string des = "";
            if (sum >= studyHour)
            {
                des = "您已经达到学时要求";
            }
            else
            {
                des = "您还差" + (studyHour - sum) + "学时";
            }
            Des.Text = "亲，您已经选择了" + i + "个模块的课程，共" + sum + "学时,本课程要求" + studyHour + "学时," + des;
        }
        #endregion

        protected void btnOk_Click(object sender, EventArgs e)
        {
            CourseManage cm = new CourseManage();
            DataRow drr;
            int sum = 0;
            bool IsAdd = true;
            foreach (GridViewRow dr in gvCourse.Rows)
            {
                if (((CheckBox)dr.FindControl("cbChoose")).Checked == true)
                {
                    sum += int.Parse(dr.Cells[6].Text);
                }
                if (((CheckBox)dr.FindControl("cbChoose")).Checked == true && dr.Cells[8].Text == "&nbsp;")
                {
                    IsAdd = cm.IsAddCourse(dr.Cells[5].Text, sum);
                    if (IsAdd)
                    {
                        drr = cm.CourseChooseDt.NewRow();
                        drr["ID"] = 1;
                        drr["ModuleID"] = long.Parse(dr.Cells[1].Text);
                        drr["StudentID"] = long.Parse(ViewState["StudentID"].ToString());
                        cm.AddCourseChoose(drr);
                    }
                }
            }
            bindDG();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CourseManage cm = new CourseManage();
            DataRow drr;
            int sum = 0;
            bool IsAdd = true;
            foreach (GridViewRow dr in gvCourse.Rows)
            {
                if (((CheckBox)dr.FindControl("cbChoose")).Checked == true)
                {
                    sum += int.Parse(dr.Cells[6].Text);
                }
                if (((CheckBox)dr.FindControl("cbChoose")).Checked == true && dr.Cells[8].Text == "&nbsp;")
                {
                    IsAdd = cm.IsAddCourse(dr.Cells[5].Text, sum);
                    if (IsAdd)
                    {
                        drr = cm.CourseChooseDt.NewRow();
                        drr["ID"] = 1;
                        drr["ModuleID"] = long.Parse(dr.Cells[1].Text);
                        drr["StudentID"] = long.Parse(ViewState["StudentID"].ToString());
                        cm.AddCourseChoose(drr);
                    }
                }
            }
            bindDG();

        }

        protected string GetNameL()
        {
            return Math.Round((double)(modulenameL/totallong*80))+"%";
            
        }
        protected string GetxueshiL()
        {
            return Math.Round((double)(xueshiL / totallong * 80)) + "%";
        }
        protected string GetstartL()
        {
            return Math.Round((double)(startL / totallong * 80)) + "%";
        }
        protected string GetendL()
        {
            return Math.Round((double)(endL / totallong * 80)) + "%";
        }
    }
}
