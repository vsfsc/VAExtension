using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace ProjectDll.DAL
{
    public class SharePointFileHelper
    {
        // 上传文件
        // 
        // 参数
        // 上传的文件在SharePoint上的位置，要上传的本地文件的路径名，用户名，密码，域        
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="strDestUrl">The dest URL.</param>
        /// <param name="strFilePathName">Name of the file path.</param>
        /// <param name="strUserName">Name of the user.</param>
        /// <param name="strPassword">The password.</param>
        /// <param name="strDomain">The domain.</param>
        /// <returns>System.String.</returns>
        public static string UploadFile(string strDestUrl, string strFilePathName, string strUserName, string strPassword, string strDomain)
        {
            string strResult = "Success";

            try
            {
                string strFileName = Path.GetFileName(strFilePathName);
                string strCopiedFilePathName = Path.GetTempPath() + strFileName;

                // 将文件拷贝到临时文件夹
                // 目的是可以在文件在被打开状态下还可以上传
                File.Copy(strFilePathName, strCopiedFilePathName, true);

                // 打开拷贝到临时目录下的文件
                FileStream fs = new FileStream(strCopiedFilePathName, FileMode.Open, FileAccess.Read);

                // 读文件
                BinaryReader br = new BinaryReader(fs);
                Byte[] filecontents = br.ReadBytes((int)fs.Length);

                br.Close();
                fs.Close();

                WebClient webclient = CreateWebClient(strUserName, strPassword, strDomain);

                // 上传
                webclient.UploadData(strDestUrl + strFileName, "PUT", filecontents);
            }
            catch (Exception ex)
            {
                strResult = "Failed! " + ex.Message;
            }

            return strResult;
        }
        
        // 下载文件
        // 
        // 参数
        // 下载的文件在SharePoint上的位置，文件下载后存放的本地文件夹路径，用户名，密码，域        
        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="strSourceUrl">The source URL.</param>
        /// <param name="strDestFolder">The dest folder.</param>
        /// <param name="strUserName">Name of the user.</param>
        /// <param name="strPassword">The password.</param>
        /// <param name="strDomain">The domain.</param>
        /// <returns>System.String.</returns>
        public static string DownloadFile(string strSourceUrl, string strDestFolder, string strUserName, string strPassword, string strDomain)
        {
            string strResult = "Success";

            try
            {
                WebClient webclient = CreateWebClient(strUserName, strPassword, strDomain);

                // 下载
                Byte[] filecontents = webclient.DownloadData(strSourceUrl);

                string strFileName = Path.GetFileName(strSourceUrl);

                // 创建文件
                FileStream fs = new FileStream(strDestFolder + strFileName, FileMode.Create, FileAccess.Write);
                // 写文件
                fs.Write(filecontents, 0, filecontents.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                strResult = "failed! " + ex.Message;
            }

            return strResult;
        }

        // 创建WebClient
        // 参数：用户名，密码，域（用来登陆SharePoint）        
        /// <summary>
        /// Creates the web client.
        /// </summary>
        /// <param name="strUserName">Name of the user.</param>
        /// <param name="strPassword">The password.</param>
        /// <param name="strDomain">The domain.</param>
        /// <returns>WebClient.</returns>
        private static WebClient CreateWebClient(string strUserName, string strPassword, string strDomain)
        {
            WebClient webclient = new WebClient();

            if (String.IsNullOrEmpty(strUserName))
            {
                webclient.UseDefaultCredentials = true;
            }
            else
            {
                NetworkCredential credential = new NetworkCredential(strUserName, strPassword, strDomain);
                webclient.Credentials = credential;
            }

            return webclient;
        }

        public static DataTable CreaTable(string[] dtCells)
        {
            var dt=new DataTable("ListInfo");
            for (int i = 0; i < dtCells.Count(); i++)
            {
                var dc = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    Caption = dtCells[i],
                    ColumnName = dtCells[i]
                }; //创建一个列对象
                dt.Columns.Add(dc);
            }
            return dt;
        }

        public static string GetDocLibListName(string docLibTitle)
        {
            string listcode = "";
            string url = SPContext.Current.Web.Url;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        foreach (SPList list in web.Lists)
                        {
                            if (list.BaseTemplate != SPListTemplateType.DocumentLibrary) //找到文档库 
                            {
                                continue;
                            }
                            else
                            {
                                if (list.Title == docLibTitle)
                                {
                                    //list.Direction;
                                }
                            }
                        }
                    }
                }
            });
            return listcode;
        }
       #region
        /// <summary>
        /// 绑定文档库中文件到DataTable
        /// </summary>
        /// <param name="docLibTitle">The document library title.</param>
        /// <param name="dtCells"></param>
        public static DataTable BindDoclib(string docLibTitle, string[] dtCells)
        {
            DataTable newdt = CreaTable(dtCells);
            string url = SPContext.Current.Web.Url;
            
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (var site = new SPSite(url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        foreach (SPList list in web.Lists)
                        {
                            if (list.BaseTemplate != SPListTemplateType.DocumentLibrary || list.Title != docLibTitle)
                                continue;
                            newdt = BindDocFolder(list.RootFolder, dtCells);
                            break;
                        }
                    }
                }
            });
            return newdt;
            
        }
#endregion
        /// <summary>
        /// 递归方法读取文件目录中的文件信息绑定到DatTable
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="dtCells"></param>
        private static DataTable BindDocFolder(SPFolder folder, string[] dtCells)
        {
            DataTable dt = CreaTable(dtCells);
            //int fCount = 0;
            string url = SPContext.Current.Web.Url;
            if (folder.SubFolders.Count == 0)// //如果不存在子文件夹就返回，并把文件夹中的文件加到dt中 
            {
                //fCount = folder.Files.Count;
                foreach (SPFile file in folder.Files)//显示子文件
                {
                    DataRow dr = dt.NewRow();
                    dr[0] =  url+"_layouts/images/" + file.IconUrl.ToString();//文件图标
                    string fname = file.Name;
                    string[] fnStrings = Common.StringSplit(fname,".");
                    dr[1] = fnStrings[0];//文件名称
                    dr[6] = fnStrings[1].ToUpper();//扩展名
                    dr[2] =url + "/" + file.Url.ToString();//文件Url
                    dr[3] = HumanReadableFilesize(file.Length).ToString();//文件大小
                    string author = file.Author.ToString();
                    dr[4] = author.Substring(author.LastIndexOf("\\", StringComparison.Ordinal) + 1).ToString();//文件作者
                    dr[5] = file.TimeCreated.ToString();
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                //fCount = folder.Files.Count;
                foreach (SPFile file in folder.Files)//添加当前文件夹中的文件到dt中 
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "_layouts/images/" + file.IconUrl;//文件图标
                    dr[0] = "_layouts/images/" + file.IconUrl.ToString();//文件图标
                    string fname = file.Name;
                    string[] fnStrings = Common.StringSplit(fname, ".");
                    dr[1] = fnStrings[0];//文件名称
                    dr[6] = fnStrings[1];//扩展名
                    dr[2] = url + "/" + file.Url;//文件Url
                    dr[3] = HumanReadableFilesize(file.Length);//文件大小
                    string author = file.Author.ToString();
                    dr[4] = author.Substring(author.LastIndexOf("\\", StringComparison.Ordinal) + 1).ToString();//文件作者
                    dr[5] = file.TimeCreated.ToString();
                    dt.Rows.Add(dr);
                }
                //foreach (SPFolder spfolder in folder.SubFolders)
                //{
                //    if (spfolder.Name == "Forms")//去掉默认的系统文件夹 
                //    {
                //        continue;
                //    }
                    
                //}
                
            }
            return dt;
            
        }

        private static String HumanReadableFilesize(double size)
        {
            var units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            const double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
    }
}
