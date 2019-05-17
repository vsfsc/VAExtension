using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;


namespace SP2013.SPOrderWebPart
{
    public partial class SPOrderWebPartUserControl : UserControl
    {
        #region 变量
        private DataTable order;
        private SPWeb web;
        private DataTable _plan;
        private string _planListName = "面试计划";
        private string _orderListName = "预约面试指导";
        private Button btnOrder;


        private SPList _planList;

        public SPList PlanList
        {
            get
            {
                if (_planList == null)
                {
                    _planList = Web.Lists.TryGetList(_planListName);
                }



                return _planList;
            }

        }
        private SPList _orderList;

        public SPList OrderList
        {
            get
            {
                if (_orderList == null)
                {
                    _orderList = Web.Lists.TryGetList(_orderListName);
                }


                return _orderList;
            }

        }
        #endregion
        #region 属性
        /// <summary>
        /// spWeb
        /// </summary>
        public SPWeb Web
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
        /// 计划表
        /// </summary>
        public DataTable Plan
        {
            get
            {
                if (_plan == null)
                {
                    _plan = new DataTable();
                    _plan.Columns.Add("ID", typeof(long));
                    _plan.Columns.Add("address", typeof(string));
                    _plan.Columns.Add("start", typeof(DateTime));
                    _plan.Columns.Add("end", typeof(DateTime));
                    _plan.Columns.Add("teacher", typeof(string));
                    _plan.Columns.Add("num", typeof(int));
                    _plan.Columns.Add("IsOrder", typeof(string));

                }
                return _plan;
            }
        }
        /// <summary>
        /// 预约表
        /// </summary>
        public DataTable Order
        {
            get
            {
                if (order == null)
                {
                    order = new DataTable();
                    order.Columns.Add("ID", typeof(string));
                    order.Columns.Add("User", typeof(string));
                    order.Columns.Add("Time", typeof(string));
                }
                return order;
            }
        }
        #endregion
        #region 事件
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (JudgeUser())
                {

                    lblMsg.Text = "您已经完成预约，请不要重复预约！";
                }
                else
                {
                    BindOrder();
                }
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e4e3f7'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                if (e.Row.Cells[6].Text == "True")
                {
                    btnOrder = ((Button)e.Row.FindControl("btnOrder"));
                    btnOrder.Text = "已满";
                    btnOrder.Enabled = false;

                }
            }
        }
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Order")
            {
                GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent));
                SPWeb spWeb = SPContext.Current.Web;
                int ID = int.Parse(drv.Cells[0].Text);
                int count=int.Parse(drv.Cells[5].Text);
                if (JudgeTime(ID,count))
                {
                    lblMsg.Text = "当前预约已满，请重新选择时间段进行预约！";
                    SPListItem item = PlanList.Items.GetItemById(ID);
                    item["已满"] = "True";
                    item.Update();
                    BindOrder();
                }
                else
                {
                    AddData(ID);
                    Response.Redirect(OrderList.DefaultViewUrl);
                }
            }
        }
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrder.PageIndex = e.NewPageIndex;
            gvOrder.DataSource = (DataTable)ViewState["Order"];
            gvOrder.DataBind();
        }
        #endregion
        #region 方法
        public DataTable GetData()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    foreach (SPListItem item in PlanList.Items)
                    {
                        DataRow dr = Plan.NewRow();
                        dr["ID"] = long.Parse(item["ID"].ToString());
                        dr["address"] = item["地点"];
                        dr["start"] = DateTime.Parse(item["开始时间"].ToString());
                        dr["end"] = DateTime.Parse(item["结束时间"].ToString());
                        dr["teacher"] = item["面试教师"].ToString().Split('#')[1].ToString();
                        dr["num"] = int.Parse(item["限制人数"].ToString());
                        dr["IsOrder"] = item["已满"].ToString();
                        Plan.Rows.Add(dr.ItemArray);
                    }
                }
                catch
                {


                }
            });
            ViewState["Order"] = Plan;
            return Plan;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="listID"></param>
        public void AddData(long listID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    SPListItem item = OrderList.Items.Add();
                    item["预约人"] = Web.CurrentUser;
                    item["预约时间"] = listID;
                    item.Update();
                }
                catch
                {


                }
            });
        }
        /// <summary>
        /// 判断用户
        /// </summary>
        /// <returns></returns>
        public bool JudgeUser()
        {
            bool isHave = false;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {

                    if (OrderList != null)
                    {
                        SPQuery qry = new SPQuery();
                        qry.ViewFields = @"<FieldRef Name='_x9884__x7ea6__x4eba_' />";
                        qry.Query =
                        @"   <Where>
                             <Eq>
                             <FieldRef Name='_x9884__x7ea6__x4eba_' />
                             <Value Type='Integer'>
                             <UserID />
                             </Value>
                             </Eq>
                             </Where>";

                        SPListItemCollection listItems = OrderList.GetItems(qry);
                        if (listItems.Count > 0)
                        {
                            isHave = true;
                        }
                    }
                }
                catch
                {
                }
            });

            return isHave;
        }
        /// <summary>
        /// 判断时间
        /// </summary>
        /// <param name="timeID"></param>
        /// <returns></returns>
        public bool JudgeTime(int timeID,int ucount)
        {
            bool isMore = false;           
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    if (OrderList != null)
                    {
                        SPQuery qry = new SPQuery();
                        qry.ViewFields = @" <FieldRef Name='_x9884__x7ea6__x65f6__x95f4__x002' />";
                        qry.Query =
                           @"<Where>
                             <Eq>
                             <FieldRef Name='_x9884__x7ea6__x65f6__x95f4_' LookupId='True' />
                             <Value Type='Lookup'>" + timeID +
                           @"</Value>
                             </Eq>
                             </Where>";
                        SPListItemCollection listItems = OrderList.GetItems(qry);
                        if (listItems.Count > 0)
                        {
                            //num = (int)float.Parse(listItems[0][0].ToString().Split('#')[1].ToString());
                            if (listItems.Count >= ucount)
                            {
                                isMore = true;
                            }
                        }
                    }
                }
                catch
                {
                }
            });
            return isMore;



        }
        #endregion
        /// <summary>
        /// 
        /// 绑定
        /// </summary>
        public void BindOrder()
        {
            gvOrder.DataSource = GetData();
            gvOrder.DataBind();
        }
    }
}
