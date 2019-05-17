using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ContestDll.DAL ;

namespace ContestDll
{
    public partial class ScoreStandard : LayoutsPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadData();
            //ViewState["dsStandard"] = null;
            ShowStandard();
            ddlQiCi.SelectedIndexChanged += ddlQiCi_SelectedIndexChanged;
            ddlWorksType.SelectedIndexChanged += ddlQiCi_SelectedIndexChanged;
            btnSave.Click += btnSave_Click;
        }

        void ddlQiCi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ViewState["dsStandardByType"] = null;
            //ViewState["dsStandard"] = null;
            ShowStandard();
        }
        #region 初始控件及方法

        protected Button btnSave;
        protected HtmlGenericControl divEditContent;
        protected DropDownList ddlQiCi;
        protected DropDownList ddlWorksType;
        private void LoadData()
        {
            List<CSPeriodsWorksType> ds = DAL.Periods.GetPeriodByCourseID();
            CSPeriodsWorksType cs = new CSPeriodsWorksType();
            cs.PeriodTitle = "";
            cs.PeriodID = 0;
            List<CSPeriodsWorksType> dsTmp = new List<CSPeriodsWorksType>();
            dsTmp.Add(cs);
            dsTmp.AddRange(ds);
            ddlQiCi.DataSource = dsTmp;
            ddlQiCi.DataTextField = "PeriodTitle";
            ddlQiCi.DataValueField = "PeriodID";
            ddlQiCi.DataBind();
            if (dsTmp.Count > 1)
                if (Request.QueryString["PeriodID"] != null)
                {
                    ListItem lstItem = ddlQiCi.Items.FindByValue(Request.QueryString["PeriodID"]);
                    if (lstItem == null)
                        ddlQiCi.SelectedIndex = 1;
                    else
                        ddlQiCi.SelectedIndex = ddlQiCi.Items.IndexOf(lstItem);
                }
                else
                    ddlQiCi.SelectedIndex = 1;
            List<WorksType> lstTmp = BLL.WorksType.GetWorksTypeTopLevel();
            ddlWorksType.DataSource = lstTmp;
            ddlWorksType.DataTextField = "WorksTypeName";
            ddlWorksType.DataValueField = "WorksTypeID";
            ddlWorksType.DataBind();
        }
        private void ShowStandard()
        {
            List<CSPeriodScoreStandard > dsScoreStandard = GetPeriodStandarad;
            ShowEditContent(dsScoreStandard);
        }
        private void InitControlDetail(int i, CSPeriodScoreStandard  drStandard1,CSWorksTypeScoreStandard drStandard2,ScoreStandard drStandard3, string score, string des, ref Table tbl)
        {
            string sName;
            string sID;
            string sDes;
            if (drStandard1 == null)
            {
                if (drStandard2 ==null)
                {
                    sName = drStandard3.StandardName;
                    sID = drStandard3.StandardID.ToString();
                    sDes = drStandard3.Description;

                }
                else
                {
                    sName = drStandard2.StandardName;
                    sID = drStandard2.StandardID.ToString();
                    sDes = drStandard2.StandardDescription;
                }

            }
            else
            {
                 sName = drStandard1.StandardName;
                 sID = drStandard1.StandardID.ToString();
                 sDes = drStandard1.StandardDescription;
            }
            TableRow tRow;
            TableCell tCell;
            Label lstStandard;
            TextBox txtContent;
            tRow = new TableRow();
            tCell = new TableCell();
            lstStandard = new Label();
            lstStandard.Width = 100;
            lstStandard.ID = "lst" + i.ToString();
            lstStandard.Text = sName;
            tCell.Controls.Add(lstStandard);

            tRow.Cells.Add(tCell);
            txtContent = new TextBox();
            txtContent.ID = "txtScore" + i.ToString() + "_" + sID;
            txtContent.Width = 20;
            if (score != "")
                txtContent.Text = score;
            tCell = new TableCell();
            tCell.Controls.Add(txtContent);
            tCell.Controls.Add(new LiteralControl("分（设置指标分数）"));

            tRow.Cells.Add(tCell);
            tbl.Rows.Add(tRow);

            lstStandard = new Label();
            lstStandard.Width = 400;
            lstStandard.ID = "des" + i.ToString();
            lstStandard.Font.Size = 8;
            lstStandard.ForeColor = System.Drawing.Color.Gray;
            lstStandard.Text = sDes ;
            tRow = new TableRow();
            tCell = new TableCell();
            tCell.ColumnSpan = 2;
            tCell.Controls.Add(lstStandard);
            tRow.Cells.Add(tCell);
            tbl.Rows.Add(tRow);

            txtContent = new TextBox();
            txtContent.ID = "txtDes" + i.ToString();
            txtContent.TextMode = TextBoxMode.MultiLine;
            txtContent.Rows = 5;
            txtContent.Width = 400;
            if (des != "")
                txtContent.Text = des;
            else
                txtContent.Text = "分条描述指标的具体评分标准";
            tRow = new TableRow();
            tCell = new TableCell();
            tCell.ColumnSpan = 2;
            tCell.Controls.Add(txtContent);
            tRow.Cells.Add(tCell);
            tbl.Rows.Add(tRow);
        }
        private void ShowEditContent(List<CSPeriodScoreStandard > dsScoreStandard)
        {
            divEditContent.Controls.Clear();
            int i = 1;
            Table tbl = new Table();
            tbl.CellSpacing = 2;
            if (dsScoreStandard.Count >0)
            {
                foreach (CSPeriodScoreStandard  dr in dsScoreStandard)
                {
                    InitControlDetail(i, dr,null,null, dr.Score.ToString (), dr.StandardDescription, ref tbl);
                    i += 1;
                }
            }
            else
            {
                List<CSWorksTypeScoreStandard > dsStandardByType = GetScoreStandardByType;
                if (dsStandardByType.Count > 0)
                {
                    foreach (CSWorksTypeScoreStandard dr in dsStandardByType)
                    {
                        InitControlDetail(i,null, dr,null, dr.Score.ToString (), dr.StandardDescription, ref tbl);
                        i += 1;
                    }
                }
                else
                {
                    List<ScoreStandard>   dsStandard = DSAllStandard;
                    i = 1;
                    foreach (ScoreStandard  dr in dsStandard)
                    {
                        InitControlDetail(i,null,null, dr, "", "", ref tbl);
                        i += 1;
                    }
                }
            }
            divEditContent.Controls.Add(tbl);
        }
        /// <summary>
        /// 五个指标共90分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSave_Click(object sender, EventArgs e)
        {
            Table tbl = (Table)divEditContent.Controls[0];
            List<CSPeriodScoreStandard> ds = GetPeriodStandarad;
            List<CSWorksTypeScoreStandard> ds1=new List<CSWorksTypeScoreStandard> ();
            string txtScore;
            string lstValue;
            string txtDes;
            List<CSPeriodScoreStandard >  drs;
            if (PeriodID == 0)
                ds1 = GetScoreStandardByType;
            int result = 0;
            int i = 0;
            while (i < DSAllStandard.Count * 3)
            {
                lstValue = ((TextBox)tbl.Rows[i].Cells[1].Controls[0]).ID;
                lstValue = lstValue.Substring(lstValue.IndexOf("_") + 1);
                txtScore = ((TextBox)tbl.Rows[i].Cells[1].Controls[0]).Text;
                txtDes = ((TextBox)tbl.Rows[i + 2].Cells[0].Controls[0]).Text;
                if (txtScore == "")
                    txtScore = "0";
                    ContestEntities db = new ContestEntities();
                    PeriodStandard dr;
                if (PeriodID > 0)
                {
                    int standID=int.Parse(lstValue);
                    drs = ds.Where(p => p.StandardID == standID).ToList();
                    //drs = ds.Tables[0].Select("StandardID=" + lstValue);
                    if (drs.Count >0)
                    {
                        drs[0].StandardDescription = txtDes.Trim();
                        drs[0].Score = int.Parse(txtScore.Trim());
                        drs[0].Modified = DateTime.Now;
                        drs[0].ModifiedBy = DAL.Common.LoginID;
                        DAL.Standard.UpdatePeriodStandard(drs[0]);
                        result += 1;
                    }
                    else
                    {
                        if (txtScore != "")
                        {
                            dr = new ContestDll.PeriodStandard();
                            dr.PeriodID = PeriodID;
                            dr.WorkTypeID  = WorksTypeID;
                            dr.StandardID = int.Parse(lstValue);
                            dr.StandardDescription = txtDes.Trim();
                            dr.Score  = int.Parse(txtScore.Trim());
                            dr.Created  = DateTime.Now;
                            dr.CreatedBy = DAL.Common.LoginID;
                            dr.Flag  = 1;
                            DAL.Standard.InsertPeriodStandard(dr);
                            result += 1;
                        }
                    }
                }
                else
                {

                    CSWorksTypeScoreStandard  dr1 = ds1.SingleOrDefault(p => p.StandardID == int.Parse(lstValue));
                    if (dr1!=null)
                    {

                        dr1.Score  = int.Parse(txtScore.Trim());
                        dr1.StandardDescription  = txtDes.Trim();
                        DAL.WorksType.UpdateWorksTypeScoreStandard(dr1);
                        result += 1;
                    }
                    else
                    {
                        dr1 = new CSWorksTypeScoreStandard();
                        dr1.WorkTypeID  = WorksTypeID;
                        dr1.StandardID  = int.Parse (lstValue);
                        dr1.StandardDescription  = txtDes.Trim();
                        dr1.Score  = int.Parse(txtScore.Trim());
                        dr1.Flag  = 1;
                        DAL.WorksType.InsertWorksTypeScoreStandard(dr1);
                        result += 1;
                    }
                }
                //保存或更新
                i += 3;
            }
            if (result > 0)
            {
                //ViewState["dsStandard"] = null;
                DAL.Common.ShowMessage(this.Page, this.GetType(), "保存成功");

            }
            else
                DAL.Common.ShowMessage(this.Page, this.GetType(), "分数不能空");

        }
        /// <summary>
        /// 指标评分
        /// </summary>
        /// <param name="dr"></param>
        #endregion
        #region 属性
        private long periodID;
        private int workTypeID;
        private long PeriodID
        {
            get
            {
                periodID = long.Parse(ddlQiCi.SelectedValue);
                return periodID;
            }
        }
        private int WorksTypeID
        {
            get
            {
                workTypeID = int.Parse(ddlWorksType.SelectedValue);
                return workTypeID;
            }
        }
        private List<CSWorksTypeScoreStandard> GetScoreStandardByType
        {
            get
            {
                //if (ViewState["dsStandardByType"] == null)
                //{
                    List<CSWorksTypeScoreStandard>   ds = DAL.WorksType.GetWorksTypeScoreStandardByTypeID(WorksTypeID);
                    return ds;
                    //ViewState["dsStandardByType"] = ds;
                //}
                //return (List<CSWorksTypeScoreStandard>)ViewState["dsStandardByType"];
            }
        }
        private List<CSPeriodScoreStandard> GetPeriodStandarad
        {
            get
            {
                //if (ViewState["dsStandard"] == null)
                //{
                List<CSPeriodScoreStandard> ds = DAL.Works.GetScoreStandardByWorksType(WorksTypeID, PeriodID);
                return ds;
                //    ViewState["dsStandard"] = ds;
                //}
                //return (List<CSPeriodScoreStandard>)ViewState["dsStandard"];
            }
        }
        //获取所有的指标,所有类别共用一个指标，分值和描述不同。
        private List<ScoreStandard> DSAllStandard
        {
            get
            {
                //if (ViewState["dsSublevel"] == null)
                //{
                    List<ScoreStandard >   ds = DAL.Works.GetScoreStandardSubLevel();
                    return ds;
                //    ViewState["dsSublevel"] = ds;
                //}
                //return (List<ScoreStandard>)ViewState["dsSublevel"];
            }
        }
        #endregion
    }
}
