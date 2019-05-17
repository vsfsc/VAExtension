using System;
using System.Web;
using System.Web.UI;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace lemmatizer.Layouts.lemmatizer
{
    public partial class test : LayoutsPageBase 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
//            string strPostBackCode =
//ClientScript.GetPostBackEventReference(btnDownload, "");
//            //然后把Button的客户事件与生成的回发事件脚本进行关联：
//            this.btnDownload.Attributes["onclick"] = strPostBackCode;
            //this.btnDownload.OnClientClick = "this.form.onsubmit = function() {return true;}";
            //this.btnOk.Attributes.Add("onclick", "if (typeof(Page_ClientValidate)=='function' && Page_ClientValidate() == false)return false;document.all('" + btnOk.ClientID + "').disabled=true;if(typeof(__doPostBack)=='function'){" + ClientScript.GetPostBackEventReference(btnOk, "") + ";}");
        }
        /// <summary>
        /// 去掉文件名中的特殊符号
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFileName(string fileName)
        {
            string retDate = System.Text.RegularExpressions.Regex.Replace(fileName, @"[.#：]", "").TrimEnd('-');
            return retDate;

        }
        protected void btnRun_Click(object sender, EventArgs e)
        {
            string fileName = ImportTxt();
            if (fileName.Length >0)
            {
                string txt = ReadTxt(fileName);
                txtShow.Text = txt;
            }
        }
        private string GetDbPath()
        {
            string txtPath = Server.MapPath("");
            txtPath =txtPath.Substring (0,txtPath.LastIndexOf ("\\"))+@"\db\";
            return txtPath;
        }
        /// <summary>
        /// 导入txt文件
        /// </summary>
        protected string ImportTxt()
        {
            string txtPath = GetDbPath()+"import/" ; //@"C:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\TEMPLATE\LAYOUTS\db\import\";
            string retTxt = "";
            if (fileUpload.HasFile == false)
            {
                lblMsg.Text = "选择导入的文件";
                retTxt ="";
            }
            else
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension != ".txt")
                    lblMsg.Text = "只能导入文本(.txt)文件";
                else
                {
                    //lblMsg.Text = "导入的文件：" + fileUpload.PostedFile.FileName;
                    string fileName =txtPath  + GetFileName(fileUpload.FileName.Replace(fileExtension, "")) + fileExtension;
                    try
                    {
                        fileUpload.PostedFile.SaveAs(fileName);
                        retTxt= fileName;
                    }
                    catch
                    {
                        lblMsg.Text = "文件上传失败";
                        retTxt = "";
                    }

                }
            }
            return retTxt;
        }
        private string  ReadTxt(string fileName)
        {
            FileStream aFile = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile, System.Text.Encoding.Default);
            string txt= sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            aFile.Close();
            aFile.Dispose();
            System.IO.File.Delete(fileName);
            return txt;
        }
        /// <summary>
        ///  下载查看文件方法
        /// </summary>
        /// <param name="fileserverURL">文件的相对路径(上传到服务器中的虚拟路径)。如：E:\User\\aa\\a.doc</param>
        /// <param name="page">所操作的页面名称</param>
        /// <returns>下载文件成功返回true,否则返回flase</returns>
        public bool FilesDownload(string fileserverURL, System.Web.UI.Page page)
        {
            try
            {
                string fileserverpath = page.Server.MapPath(fileserverURL);
                System.IO.FileInfo fi = new System.IO.FileInfo(fileserverpath);
                fi.Attributes = System.IO.FileAttributes.Normal;
                System.IO.FileStream filestream = new System.IO.FileStream(fileserverpath, System.IO.FileMode.Open);
                long filesize = filestream.Length;
                int i = Convert.ToInt32(filesize);

                page.Response.ContentType = "application/octet-stream";
                page.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(fileserverpath, System.Text.Encoding.UTF8));
                page.Response.AddHeader("Content-Length", filesize.ToString());
                byte[] fileBuffer = new byte[i];
                filestream.Read(fileBuffer, 0, i);
                filestream.Close();
                page.Response.BinaryWrite(fileBuffer);
                page.Response.Buffer = true;
                page.Response.Flush();
                //page.Response.Clear();表示不会清除缓冲区里的内容
                page.Response.Close();
                page.Response.End();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //download
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string fileName = "../db/export/Afghanistan(qq)_201512011624282858.txt";
            fileName = Server.MapPath(fileName);
            WriteOutFile(fileName);
            //Response.Redirect("test.aspx");
            //FilesDownload(fileName, Page);
            //DownloadFile(fileName);
            //ClientScript.RegisterClientScriptBlock(this.GetType (),"postBack","<script language=javascript>this.form.onsubmit = function() {return true;};");

        }
        private void DownloadFile(string filePath)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            this.Page.Response.Clear();
            this.Page.Response.Buffer = true;
            this.Page.Response.Charset = "gb2312";
            this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            this.Page.Response.AppendHeader("content-disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(file.Name , System.Text.Encoding.UTF8) + "\"");
            this.Page.Response.ContentType = "text/plain";
            //添加头信息，为“文件下载/另存为”对话框指定默认文件名
            Page.Response.AddHeader("Content-Disposition", "attachment;filename=" + Page.Server.UrlEncode(file.Name));// 
            Page.Response.AddHeader("Content-Length", file.Length.ToString());
            Page.Response.ContentType = "text/plain";
            Page.Response.Filter.Close();
            Page.Response.WriteFile(file.FullName);
            //Page.Response.Flush();
            //Page.Response.Close();
            //Page.Response.End();
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        /// <summary>
        /// 使用WriteFile下载文件  
        /// </summary>
        /// <param name="filePath">相对路径</param>
        public void WriteFile(string filePath)
        {
            try
            {
                //filePath = Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + Server.UrlEncode(info.FullName));
                    //指定文件大小   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.WriteFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }
            ////文件下载后重新刷新数据
            //Page.RegisterClientScriptBlock("", "<script language=javascript>window.location.href=window.location.href;</script>");
        }

        protected void cblist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region 写出文件
        /// <summary>
        /// 写出文件
        /// </summary>
        /// <param name="pdfFile">文件对象</param>
        /// <returns>返回是否成功</returns>
        protected bool WriteOutFile(string filePath)
        {
            System.IO.FileInfo pdfFile = new System.IO.FileInfo(filePath);
            //写出文件
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
            HttpResponse response = HttpContext.Current.Response;
            response.HeaderEncoding = encoding;
            response.Charset = encoding.HeaderName;
            response.Clear();
            response.ContentEncoding = encoding;
            response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(pdfFile.Name));
            response.AddHeader("Content-Length", pdfFile.Length.ToString());
            response.ContentType = "text/plain";// "application/pdf";
            response.WriteFile(pdfFile.FullName);
            response.Flush();
            response.Close();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            return true;
        }
        #endregion
        protected void btnOk_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "exported", "document.getElementById('" + btnOk.ClientID + "').disabled=false;document.getElementById('exporting').style.display='none';document.getElementById('" + btnDownload.ClientID + "').click();", true);
        }
    }
}