using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using lemmatizerDLL;

namespace lemmatizer.Layouts.lemmatizer
{
    public partial class vblist : LayoutsPageBase
    {
        public ArrayList CiLib;
        #region 页面事件
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnUploadFile.ServerClick += new EventHandler(btnUploadFile_ServerClick);
            if (!IsPostBack)
            {
                inputDiv.Visible = true;
                outputDiv.Visible = false;
            }
            string WordsFile = GetDbPath() + "words/AllWords.txt";
            CiLib = lemmatizerDLL.WordBLL.cibiaoku(WordsFile);
        }
        /// <summary>
        /// 页面提醒
        /// </summary>
        /// <param name="info"></param>
        /// <param name="p"></param>
        private static void PageAlert(string info, Page p)
        {
            string script = "<script>alert('" + info + "')</script>";
            p.ClientScript.RegisterStartupScript(p.GetType(), "", script);
        }

        private bool isSelected()
        {
            bool isSelected = false;
            if (ddlBook.SelectedIndex!=0&ddlUnit.SelectedIndex!=0&ddlText.SelectedIndex!=0)
            {
                isSelected = true;
            }
            return isSelected;
        }

        /// <summary>
        /// Lemma操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lemmanew_Click(object sender, EventArgs e)
        {
            /********变量**********/
            string titleStr;//标题
            string nameStr;//用户名
            string txtStr = txtcontent.Value.Trim();//正文文本

            //检验文本册次，单元，篇章属性选择完毕
            if (!isSelected())
            {
                PageAlert("你尚未选择本次筛查的文章册次、单元或篇章！", this);
                return;
            }

            if (string.IsNullOrEmpty(txtcontent.Value)) //处理的文本还未输入
            {
                PageAlert("你还未输入或导入需要处理的文本,请确认后再试！", this);
                txtcontent.Focus();
                lemmanew.Enabled = true;
                return;
            }
            else
            {
                string regEx = @"((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";
                txtStr = Regex.Replace(txtcontent.Value, regEx, ";");//正则表达式排除文中的网址
            }

            /***********1、过滤文本判断是否包含有英文单词*************/
            string ignoreWordsFile = GetDbPath() + "words/ignoreWords.txt";
            string ordinalWordsFile = GetDbPath() + "words/OrdinalWords.txt";
            string symbolFile = GetDbPath() + "words/symbol.txt";
            var txtlist = TextInput.ArticleToList(txtStr, ignoreWordsFile, ordinalWordsFile, symbolFile);//文本转化为字符串数组,将需要处理的单词存到数组中

            //PageAlert(timeRecord, this);
            if (txtlist.Count == 0)//文本中不包含有英文单词
            {
                PageAlert("文本中不包含英文单词！", this);
                txtcontent.Focus();
                lemmanew.Enabled = true;
                return;
            }

            string wordLevel = "";
            int maxIndex = 0;
            maxIndex = 5;//ddlBook.SelectedIndex + 4;
            int selectIndex = ddlBook.SelectedIndex + ddlUnit.SelectedIndex + ddlText.SelectedIndex;
            if (maxIndex == 0||selectIndex<3)
            {
                PageAlert("你还未选定课文对应的册、单元或篇目!", this);
                lemmanew.Enabled = true;
                return;
            }
            else
            {
                string unitValue = ddlUnit.SelectedValue;
                if (int.Parse(unitValue)< 10)
                {
                    unitValue = "0" + unitValue;
                }
                wordLevel = ddlBook.SelectedValue + unitValue + ddlText.SelectedValue;//单词选定的级别为当前选择的册、单元、篇章的序号连接组成,其中单元以补0处理
            }
            /*********2、查询是否已经处理过本篇课文************/
            titleStr = ddlBook.SelectedItem.Text + ddlUnit.SelectedItem.Text + ddlText.SelectedItem.Text;
            List<string[]> oldwordsList = new List<string[]>();
            List<words> newWordsList = new List<words>();

            oldwordsList=WordBLL.GetWordLookup(wordLevel);
            if (oldwordsList.Count>0)
            {
                PageAlert("“"+titleStr+"”课文已经处理过，系统将为您直接输出生词表！", this);
                for (int i = 0; i < oldwordsList.Count; i++)
                {
                    newWordsList.Add(new words(i + 1, oldwordsList[i][0].ToString(), oldwordsList[i][1].ToString()));
                }
                wordgv.DataSource = newWordsList;
                wordgv.DataBind();
                totalW.Text = "你所筛查的本篇课文共有 " + newWordsList.Count.ToString() + " 生词";
                inputDiv.Visible = false;
                outputDiv.Visible = true;
                Titlelb.Text = "生词表输出";
            }
            else
            {

                oldwordsList = new List<string[]>();
                /**********3、保存要处理的文本************/
                SPUser currentUser = SPContext.Current.Web.CurrentUser;
                nameStr = currentUser.Name;

                titleStr = TextInput.FilterSpecial(titleStr, "");
                string filePath = GetDbPath() + @"export/";//txt文件保存的路径
                string nowStr = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);//时间格式字符串：年月日时分秒4位毫秒
                string fileTitle = titleStr + "(" + nameStr + ")" + nowStr + ".txt"; ;//文章标题+ _ + 处理人姓名 + 处理人所属院校（登录名）+ 当前时间
                TextInput.FileWrite(fileTitle, txtStr, filePath);//将即将处理的文本保存到服务器上的指定目录中;

                inputDiv.Visible = false;

                /***************4、词汇与级别表筛查*******************/
                string wordLib = GetDbPath() + "words/AllWords.txt";//包含原型与变型以及对应等级的词汇表文本
                Dictionary<int, object> allwordsList = WordBLL.SearchWordsWithTxt(txtlist, wordLib,0);
                DataTable wordsTable = (DataTable)allwordsList[1];//包含要输出的单词的元词汇、级别、频次

                //string europeLib = GetDbPath() + "words/EuropeLib.txt";//包含原型与变型以及对应等级的词汇表文本
                //Dictionary<int, object> europeList = WordBLL.SearchWordsWithTxt(txtlist, europeLib, 0);
                //DataTable europeTable = (DataTable)europeList[1];//包含要输出的单词的元词汇、级别、频次

                if (wordsTable.Rows.Count>0)//有生词
                {
                    DataRow[] drs = wordsTable.Select("[level]>'"+maxIndex+ "' or [level]=0");//生词词汇的原词、级别、频次
                    for (int i = 0; i < drs.Length; i++)
                    {
                        int level = (int)drs[i][1];
                        string signs= WordBLL.leveltoSigns(level);//将数字级别转换为级别标记☆\△\◇等
                        string[] wds = new string[2] {drs[i][0].ToString(),signs};
                        oldwordsList.Add(wds);
                    }
                    oldwordsList = WordBLL.GetWordLookup(oldwordsList, wordLevel);//与动态词汇表对比筛查
                    /***************5、输出本文生词词汇表********************/

                    for (int i = 0; i < oldwordsList.Count; i++)
                    {
                        newWordsList.Add(new words(i+1,oldwordsList[i][0].ToString(), oldwordsList[i][1].ToString()));
                    }
                    wordgv.DataSource = newWordsList;
                    wordgv.DataBind();
                    totalW.Text = "你所筛查的本篇课文共有 " + newWordsList.Count + " 生词";


                    outputDiv.Visible = true;
                    Titlelb.Text = "生词表输出";
                }

            }
            lemmanew.Enabled = true;
        }
        /// <summary>
        /// 生词序列
        /// </summary>
        public class words
        {
            public int ID { get; set; }
            public string Words { get; set; }
            public string Signs { get; set; }

            public words(int id, string name,string signs)
            {
                this.ID = id;
                this.Words = name;
                this.Signs = signs;
            }
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void closeBtn_OnClick(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>document.getElementById('outputDiv').style.display = '';document.getElementById('inputDiv').style.display = 'none';</script>", true);
            txtcontent.Value = "";//清空正文文本
            Titlelb.Text = "课文录入";
            lemmanew.Enabled = true;
            outputDiv.Visible = false;
            inputDiv.Visible = true;
        }

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void backBtn_OnClick(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>document.getElementById('outputDiv').style.display = '';document.getElementById('inputDiv').style.display = 'none';</script>", true);
            outputDiv.Visible = false;
            inputDiv.Visible = true;
            Titlelb.Text = "课文录入";
        }

        /// <summary>
        /// 词汇表数据行创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wordgv_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow ||
                e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            }
        }

        /// <summary>
        /// 词汇表数据行绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wordgv_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == -1)
            {
                e.Row.Cells[0].Text = "NO.";
                e.Row.Cells[1].Text = "Word";
                e.Row.Cells[2].Text = "Level";
                e.Row.Cells[3].Text = "Frequency";

            }
            else
            {
                int indexId = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexId.ToString();
            }

        }

        /// <summary>
        /// 清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clearBtn_OnClick(object sender, EventArgs e)
        {
            this.txtcontent.Value = "";
        }

        /// <summary>
        /// 词汇表排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wordgv_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "Desc")
                {
                    ViewState["OrderDire"] = "ASC";
                }
                else
                {
                    ViewState["OrderDire"] = "Desc";
                }
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            wordgv.DataBind();
        }

        /// <summary>
        /// 文本导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtImport_Click(object sender, EventArgs e)
        {
            string fileName = ImportTxt();
            if (fileName.Length > 0)
            {
                string txt = TextInput.ReadTxt(fileName);
                txtcontent.Value = txt;
            }
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>wordStatic(txtcontent);</script>", true);
        }
        #endregion

        #region 方法


        private string GetDbPath()
        {
            string txtPath = Server.MapPath("");
            txtPath = txtPath.Substring(0, txtPath.LastIndexOf("\\", StringComparison.Ordinal)) + @"\db\";
            return txtPath;
        }
        private static string TimeSpend(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts = endTime - startTime;
            string rtime = ts.Days.ToString() + ":" + ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString() + " : " + ts.Milliseconds.ToString() + ":";
            return rtime;
        }


        /// <summary>
        /// 去掉文件名中的特殊符号
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFileName(string fileName)
        {
            string retDate = Regex.Replace(fileName, @"[.#：]", "").TrimEnd('-');
            return retDate;

        }

        /// <summary>
        /// 导入txt文件
        /// </summary>
        private string ImportTxt()
        {
            string txtPath = GetDbPath() + "import/";
            string retTxt = "";
            if (txtUpload.HasFile == false)
            {
                PageAlert("选择导入的文件", this);
                retTxt = "";
            }
            else
            {
                var extension = Path.GetExtension(txtUpload.FileName);
                if (extension != null)
                {
                    string fileExtension = extension.ToLower();
                    if (fileExtension != ".txt")
                        PageAlert("只能导入文本(.txt)文件", this);
                    else
                    {
                        //lblMsg.Text = "导入的文件：" + fileUpload.PostedFile.FileName;
                        string fileName = txtPath + GetFileName(txtUpload.FileName.Replace(fileExtension, "")) + fileExtension;
                        try
                        {
                            txtUpload.PostedFile.SaveAs(fileName);
                            retTxt = fileName;
                        }
                        catch(Exception)
                        {
                            PageAlert("文件导入失败，请检查后再试！",this);
                            retTxt = "";
                        }


                    }
                }
            }
            return retTxt;

        }

        /// <summary>
        /// 导出Excel功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Excel_Click(object sender, EventArgs e)
        {
            string title = ddlBook.SelectedItem.Text + ddlUnit.SelectedItem.Text + ddlText.SelectedItem.Text + DateTime.Now.Date.ToString("yyyyMMdd");
            //导出全部数据，取消分页
            wordgv.AllowPaging = false;
            wordgv.ShowFooter = false;

            //BindGridView();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + title + ".xls");
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");//设置输出流为简体中文

            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。
            this.EnableViewState = false;
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            StringWriter oStringWriter = new StringWriter(myCItrad);
            HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);

            ClearControls(wordgv);
            this.wordgv.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.End();

            //还原分页显示
            wordgv.AllowPaging = true;
            wordgv.ShowFooter = true;
            //BindGridView();
        }

        /// <summary>
        /// GridView如果需要实现导出Excel功能，则该函数需要重载
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for
        }

        /// <summary>
        /// 清除控件中的所有控件，以便导出Excel
        /// </summary>
        /// <param name="control"></param>
        private void ClearControls(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }

            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
            }
            return;
        }

        protected void exExcel_OnClick(object sender, EventArgs e)
        {
            string title = ddlBook.SelectedItem.Text+"-"+ddlUnit.SelectedItem.Text + "-" + ddlText.SelectedItem.Text;
            DateTime dt = DateTime.Now;
            string excelTitle = title + "_"+ dt.ToString("yyyyMMddhhmmss");
            excelTitle = excelTitle + ".xls";
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;//注意编码
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + excelTitle);//HttpUtility.UrlEncode(excelTitle, Encoding.UTF8).ToString());
            HttpContext.Current.Response.ContentType = excelTitle;//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
            wordgv.Page.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            wordgv.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
            //GridViewToExcel(wordgv, "application/ms-excel", excelTitle);
            // Export(gvRecord, "application/ms-excel", str);
        }

        /// <summary>
        /// 将网格数据导出到Excel
        /// </summary>
        /// <param name="ctrl">网格名称(如GridView1)</param>
        /// <param name="fileType">要导出的文件类型(Excel:application/ms-excel)</param>
        /// <param name="fileName">要保存的文件名</param>
        public static void GridViewToExcel(Control ctrl, string fileType, string fileName)
        {
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;//注意编码
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
            HttpContext.Current.Response.ContentType = fileType;//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
            ctrl.Page.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            ctrl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }
        #endregion


        /// <summary>
        /// 篇目选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlText.SelectedIndex==0)
            {
                PageAlert("请选择你要筛查的篇目", this);
                lbTextTitle.Visible = false;
            }
            else
            {
                if (ddlBook.SelectedIndex != 0 & ddlUnit.SelectedIndex != 0)
                {
                    string texttitle = ddlBook.SelectedItem.Text + "   " + ddlUnit.SelectedItem.Text + "   " + ddlText.SelectedItem.Text;
                    lbTextTitle.Text = "你即将处理的文章是：" + texttitle;
                    lbTextTitle.Visible = true;
                }
                else
                {
                    PageAlert("你尚未选择本次筛查的文章 册 或 单元", this);
                    lemmanew.Enabled = true;
                    lbTextTitle.Visible = false;
                }
            }
        }

        /// <summary>
        /// 单元选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnit.SelectedIndex == 0)
            {
                PageAlert("请选择你要筛查的 单元", this);
                lbTextTitle.Visible = false;
            }
            else
            {
                string texttitle = ddlBook.SelectedItem.Text + "   " + ddlUnit.SelectedItem.Text + "   " + ddlText.SelectedItem.Text;
                lbTextTitle.Text = "你即将处理的文章是：" + texttitle + "";
                lbTextTitle.Visible = true;
            }

        }

        /// <summary>
        /// 册次选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBook.SelectedIndex == 0)
            {
                PageAlert("请选择你要筛查的 册", this);
                lbTextTitle.Visible = false;
            }
            else
            {
                string texttitle = ddlBook.SelectedItem.Text + "   " + ddlUnit.SelectedItem.Text + "   " + ddlText.SelectedItem.Text;
                lbTextTitle.Text = "你即将处理的文章是：" + texttitle + "";
                lbTextTitle.Visible = true;
            }
        }
    }
}
