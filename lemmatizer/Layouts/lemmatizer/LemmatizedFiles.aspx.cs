using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Ionic.Zip;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;


namespace lemmatizer.Layouts.lemmatizer
{
    public partial class LemmatizedFiles : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["SortOrder"] = "FileName";
                ViewState["OrderDire"] = "ASC";
                BindFilesList();
            }
        }

        private string covUnit(double oData)
        {
            string cData = "0 KB";
            if (oData<103)
            {
                cData =  "0.01 KB";
            }
            else if (oData >= 103 && oData < 1024 * 1024)
            {
                cData = (oData / 1024).ToString("0.00") + " KB";
            }
            else
            {
                cData = (oData / (1024 * 1024)).ToString("0.00") + " MB";
            }
            return cData;
        }
        void BindFilesList()
        {
            //List<System.IO.FileInfo> lstFI = new List<System.IO.FileInfo>();
            string dirPath = HttpContext.Current.Server.MapPath("../db/export/");
            //string[] files = System.IO.Directory.GetFiles(dirPath);
            //foreach (var s in files)
            //{
            //    lstFI.Add(new System.IO.FileInfo(s));
            //}
            //获得目录信息
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            //获得目录文件列表
            FileInfo[] filesinfo = dir.GetFiles("*.*");
            //string[] fileNames = new string[filesinfo.Length];
            //临时数据表
            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("Creator");
            dt.Columns.Add("CreationTime");
            dt.Columns.Add("Size");
            
            //int i = 1;
            foreach (FileInfo fileInfo in filesinfo)
            {
                DataRow dr = dt.NewRow();
                //fileNames[i] = fileInfo.Name;
                //i++;
                dr["FileName"] = fileInfo.Name;
                
                var m = fileInfo.Name.IndexOf('(');
                var n = fileInfo.Name.IndexOf(')');
                dr["Creator"] = fileInfo.Name.Substring(m+1, n - m-1);

                dr["CreationTime"] = fileInfo.CreationTime;
                dr["Size"] = covUnit(fileInfo.Length);
                
                dt.Rows.Add(dr);
            }
            DataView dv = dt.DefaultView;
            string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
            dv.Sort = sort;
            gvFiles.DataSource = dv;// lstFI;
            gvFiles.DataBind();
        }

        protected void PackDown_Click(object sender, EventArgs e)
        {
            string dirPath = HttpContext.Current.Server.MapPath("../db/export/");
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=DotNetZip.zip");
            using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))//解决中文乱码问题
            {
                int k = 0;
                foreach (GridViewRow gvr in gvFiles.Rows)
                {
                    if (((CheckBox)gvr.Cells[1].Controls[1]).Checked)//checkbox列
                    {
                        zip.AddFile(dirPath +gvr.Cells[4].Text, "");//filename列
                        k++;
                    }
                }
                if (k>0)
                {
                    zip.Save(Response.OutputStream);
                }
                else
                {
                    PageAlert("You have not selected any files that need package download", this);
                    return;
                }
                
            }
            
            //加载js代码，调用GetResultFromServer()方法  
            //string sjs = "GetResultFromServer();";

            //Page.RegisterClientScriptBlock( "", sjs);
            
            Response.End();
        }
        protected void gvFiles_Sorting(object sender, GridViewSortEventArgs e)
        {
            //得到分页数据源
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
            BindFilesList();
        }
        protected void lbtnView_Command(object sender, CommandEventArgs e)
        {
            string fileName = "../db/export/" + e.CommandArgument.ToString();
            WriteFile(fileName);
            Response.Write("<script language=javascript>windows.location=windows.location.href;</script>");
            BindFilesList();
            
        }
        /// <summary>
        /// 使用OutputStream.Write分块下载文件  
        /// </summary>
        /// <param name="filePath"></param>
        public void WriteFileBlock(string filePath)
        {
            filePath = Server.MapPath(filePath);
            if (!File.Exists(filePath))
            {
                return;
            }
            FileInfo info = new FileInfo(filePath);
            //指定块大小   
            long chunkSize = 4096;
            //建立一个4K的缓冲区   
            byte[] buffer = new byte[chunkSize];
            //剩余的字节数   
            long dataToRead = 0;
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                dataToRead = stream.Length;

                //添加Http头   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + Server.UrlEncode(info.FullName));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //防止client失去连接   
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.Close();
            }

        }
        /// <summary>
        /// 使用WriteFile下载文件  
        /// </summary>
        /// <param name="filePath">相对路径</param>
        public void WriteFile(string filePath)
        {
            try
            {
                filePath = Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + Server.UrlEncode(info.Name));
                    //指定文件大小   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.WriteFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                    //Page.RegisterClientScriptBlock("", "<script language=javascript>window.location.href=window.location.href;</script>");
                }
            }
            catch (Exception)
            {
                
            }
            finally
            {
                HttpContext.Current.Response.Close();
            }
            //文件下载后重新刷新数据
            //Page.RegisterClientScriptBlock("", "<script language=javascript>window.location.href=window.location.href;</script>");
        }

        private static void PageAlert(string info, Page p)
        {
            string script = "<script>alert('" + info + "')</script>";
            p.ClientScript.RegisterStartupScript(p.GetType(), "", script);
        }
        /// <summary>
        /// 使用微软的TransmitFile下载文件
        /// </summary>
        /// <param name="filePath">服务器相对路径</param>
        public void TransmitFile(string filePath)
        {
            try
            {
                filePath = Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();

                    //指定Http Mime格式为压缩包
                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";

                    // Http 协议中有专门的指令来告知浏览器, 本次响应的是一个需要下载的文件. 格式如下:
                    // Content-Disposition: attachment;filename=filename.txt
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(info.FullName));
                    //不指明Content-Length用Flush的话不会显示下载进度   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.TransmitFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }

        }

        /// <summary>   
        /// 移动目录内的文件到另一目录   
        /// </summary>   
        /// <param name="sorDir">源目录，如：Server.MapPath("~/product_image/44/8813/")</param>   
        /// <param name="desDir">目标目录，如：Server.MapPath("~/product_image/141/8813/")</param>   
        public static void MoveDirFile(string sorDir, string desDir)
        {
            if (!Directory.Exists(sorDir))
            {
                return;
            }
            if (!Directory.Exists(desDir))
            {
                Directory.CreateDirectory(desDir);
            }
            foreach (string item in Directory.GetFiles(sorDir))
            {
                try
                {
                    FileInfo fi = new FileInfo(item);
                    string tmp = desDir + fi.Name;
                    if (File.Exists(tmp))
                    {
                        File.Delete(tmp);
                    }
                    fi.MoveTo(tmp);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            Directory.Delete(sorDir, true);
        } 

        protected void gvFiles_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow ||
                e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
            }
        }
        protected void lbtnDel_Command(object sender, CommandEventArgs e)
        {
            string fileName = "../db/export/" + e.CommandArgument.ToString();
            string path = Request.MapPath(fileName);
            
            //var file = new FileInfo(fileName);
            //string temppath = "../db/exporttemp/"+file.Name;
            //if (file.Exists)
            //{
            //   File.Move(fileName, temppath);
            //}
            FileInfo downloadExist = new FileInfo(path);

            // 方法一
            if (downloadExist.Exists)
            {
                File.Delete(path);
            }
            BindFilesList();
        }

       
    }
}
