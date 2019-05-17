using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectDll;
using System.Linq;
using System.Collections.Generic;

namespace Project.Layouts
{
    public partial class MyProjectsMatch : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //gvMyProjMatch.RowCreated += gvMyProjMatch_RowCreated;
            gvMyProjMatch.RowCommand += gvMyProjMatch_RowCommand;
            if (!IsPostBack)
            {
                BindvMyProjMatch(gvMyProjMatch);
            }

        }

        void gvMyProjMatch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                GridViewRow gvr = ((Control)e.CommandSource).BindingContainer as GridViewRow; 
                string matchID =gvMyProjMatch.DataKeys[gvr.RowIndex ].Value.ToString () ;//  DataBinder.Eval(gvMyProjMatch.Rows[gvr.RowIndex].DataItem, "MatchID").ToString();
                Proj_Match dr = new Proj_Match();
                hdMatchID.Value = matchID;
                List<Proj_Match> dt = ProjectDll.DAL.ProjectDal.GetProjectsMatch();  //从表获取详细信息
                dt = dt.Where(ps => ps.MatchID == long.Parse(matchID)).ToList();
                divMatchDetail.Visible = true;
                matcheDetails.DataSource = dt; //绑定到控件
                matcheDetails.DataBind();
            }
        }
        private void BindvMyProjMatch(GridView gv)
        {
            // string projName = SPContext.Current.Web.Title;  //获取项目的名称
            string projName = "大数据分析";
            Proj_Project dt = ProjectDll.DAL.ProjectDal.GetProjectByTitle(projName);
            List<Proj_Match> psList = ProjectDll.BLL.ProjectBll.GetMatchByProjID(dt.ProjectID);
            gvMyProjMatch.DataSource = psList;
            gvMyProjMatch.DataKeyNames = new string[] { "MatchID" }; 

            gvMyProjMatch.DataBind();
        }

      
        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    var button = sender as Button;
        //    GridViewRow gvr = (GridViewRow)button.Parent.Parent;
        //    string matchID = DataBinder.Eval(gvMyProjMatch.Rows[gvr.RowIndex].DataItem, "MatchID").ToString();
        //    Proj_Match dr = new Proj_Match();
        //    hdMatchID.Value = matchID;
        //    List<Proj_Match> dt = ProjectDll.DAL.ProjectDal.GetProjectsMatch();  //从表获取详细信息
        //    dt = dt.Where(ps => ps.MatchID == long.Parse(matchID)).ToList();
        //    divMatchDetail.Visible = true;
        //    matcheDetails.DataSource = dt; //绑定到控件
        //    matcheDetails.DataBind();
        //}

        //protected void gvMyProjMatch_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.FindControl("btnView") != null)
        //        {
        //            var ButtonAdd = (Button)e.Row.FindControl("btnView");
        //            ButtonAdd.Click += btnView_Click;
        //        }
        //    }            
        //}

        protected void btnAccepted_Click(object sender, EventArgs e)
        {
            string strMatchID = hdMatchID.Value;
            long matchID = Convert.ToInt64(strMatchID);
            int IsAccetped = 1;
            ProjectDll.DAL.ProjectDal.UpdateProjectsMatch(matchID, IsAccetped);
            Response.Write("<script>alert('您已接受对方的项目对接，祝合作愉快！');location.href='MyProjectsMatch.aspx';</script>");
            //List<Proj_Match> dr = ProjectDll.DAL.ProjectDal.GetProjectsMatch();
            //dr = dr.Where(ps => ps.MatchID == matchID).ToList();
            //foreach (var item in dr)
            //{
            //    item.AcceptedTime = DateTime.Now;
            //    item.IsAccepted = 1;
            //}
        }

        protected void btnRefuse_Click(object sender, EventArgs e)
        {
            string strMatchID = hdMatchID.Value;
            long matchID = Convert.ToInt64(strMatchID);
            int IsAccetped = -1;
            ProjectDll.DAL.ProjectDal.UpdateProjectsMatch(matchID, IsAccetped);
            Response.Write("<script>alert('您已拒绝对方的项目对接！');location.href='MyProjectsMatch.aspx';</script>");
        }

        protected void gvMyProjMatch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string matchState = e.Row.Cells[4].Text;
                int state = Int32.Parse(matchState);
                switch(state)
                {
                    case 0: { e.Row.Cells[4].Text = "等待对方回应"; }break;
                    case 1: { e.Row.Cells[4].Text = "接受对接"; }break;
                    case -1: { e.Row.Cells[4].Text = "拒绝对接"; }break;
                }
            }
        }

        //protected void gvMyProjMatch_SelectedIndexChanged(object sender, EventArgs e)
        //{


        //}
    }
}
