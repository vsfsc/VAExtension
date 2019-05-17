using System;
using System.Web;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections;

namespace ProjectDll.DAL
{
    public class Common
    {
        #region SP网站的操作
        public static long LoginID
        {
            get
            {
                long loginID = 0;
                try
                {
                    List<ProjectDll.User> dsLogin = LoginUser;
                    if (dsLogin != null && dsLogin.Count  > 0)
                        loginID = dsLogin[0].UserID  ;
                }
                catch
                { }
                return loginID;
            }
        }
        public static List<ProjectDll.User> LoginUser
        {
            get
            {
                List<ProjectDll.User> dsLogin = null;
                try
                {
                    dsLogin = DAL.User.GetUserByAccount(GetLoginAccount);
                }
                catch
                { }
                return dsLogin;
            }
        }
        /// <summary>
        /// 获取用户登录账号
        /// </summary>
        /// <returns></returns>
        public static string GetLoginAccount
        {
            get
            {
                SPUser us = SPContext.Current.Web.CurrentUser;
                string accountName = "";
                if (us != null)
                {
                    accountName = SPContext.Current.Web.CurrentUser.LoginName;
                    accountName = accountName.Substring(accountName.LastIndexOf("\\") + 1);
                }
                return accountName;
            }
        }
        public static SPWeb SPWeb
        {
            get
            {
                return SPContext.Current.Web;
            }
        }
        /// <summary>
        /// 当前网站的批次ID
        /// </summary>
        /// <summary>
        /// 当前用户是否当前网站管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsWebAdmin
        {
            get
            {
                bool right = SPWeb.DoesUserHavePermissions(SPBasePermissions.FullMask);
                return right;
            }
        }
        /// <summary>
        /// 信息提示
        /// </summary>
        /// <returns></returns>
        public static bool ShowMessage(System.Web.UI.Page page, Type type, string message)
        {
            page.ClientScript.RegisterStartupScript(type, "message", "<script defer>alert('" + message + "')</script>");
            return true;
        }

        /// <summary>
        /// 打开新的窗口(改成模式窗口)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fileName"></param>
        public static void OpenWindow(Page page, string fileName)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "open", "<script defer>window.open('" + fileName + "','_blank');</script>");
            //page.ClientScript.RegisterStartupScript(page.GetType(), "open", "<script defer>window.showModalDialog('" + fileName + "','_blank','dialogWidth=1002px;dialogHeight=600px');</script>");
        }
        public static bool JudgeRight(string RoleID, string CurrRoleID)
        {
            bool isRight = false;
            if (CurrRoleID == RoleID)
            {
                isRight = true;

            }
            return isRight;
        }

        /// <summary>
        /// 记入日志文件
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLog(string content)
        {
            using (StreamWriter write = new StreamWriter("reglog.txt"))
            {
                write.WriteLine(DateTime.Now.ToString());
                write.WriteLine(content);

            }

        }
      
        #endregion

        #region 拆分字符串
        /// <summary>
        /// 根据字符串拆分字符串
        /// </summary>
        /// <param name="strSource">要拆分的字符串</param>
        /// <param name="strSplit">拆分符</param>
        /// <returns></returns>
        public static string[] StringSplit(string strSource, string strSplit)
        {
            string[] strtmp = new string[1];
            int index = strSource.IndexOf(strSplit, 0, StringComparison.Ordinal);
            if (index < 0)
            {
                strtmp[0] = strSource;
                return strtmp;
            }
            else
            {
                strtmp[0] = strSource.Substring(0, index);
                return StringSplit(strSource.Substring(index + strSplit.Length), strSplit, strtmp);
            }
        }

        /// <summary>
        /// 采用递归将字符串分割成数组
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strSplit"></param>
        /// <param name="attachArray"></param>
        /// <returns></returns>
        private static string[] StringSplit(string strSource, string strSplit, string[] attachArray)
        {
            string[] strtmp = new string[attachArray.Length + 1];
            attachArray.CopyTo(strtmp, 0);


            int index = strSource.IndexOf(strSplit, 0, StringComparison.Ordinal);
            if (index < 0)
            {
                strtmp[attachArray.Length] = strSource;
                return strtmp;
            }
            else
            {
                strtmp[attachArray.Length] = strSource.Substring(0, index);
                return StringSplit(strSource.Substring(index + strSplit.Length), strSplit, strtmp);
            }
        }
        #endregion

        #region EntityFramework调用存储过程
        /// <summary>  
        /// 执行原始SQL命令  
        /// </summary>  
        /// <param name="commandText">SQL命令</param>  
        /// <param name="parameters">参数</param>  
        /// <returns>影响的记录数</returns>  
        public Object[] ExecuteSqlNonQuery<T>(string commandText, params Object[] parameters)
        {
            using (ProjectEntities  context =new ProjectEntities ())
            {
                var results = context.Database.SqlQuery<T>(commandText, parameters);
                results.Single();
                return parameters;
            }
        }
        #endregion
        #region 外加的方法

        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static  void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

       
        /// <summary>
        /// 以指定的邮箱向多个用户发送邮件
        /// </summary>
        /// <param name="fromEmail">发送邮件的源</param>
        /// <param name="fromDisplayName">显示名称</param>
        /// <param name="pwd">发送源的邮箱密码</param>
        /// <param name="toMail">发送的目标邮箱</param>
        /// <param name="toSubject">发送的主题</param>
        /// <param name="toBody">发送的内容</param>
        /// <returns></returns>
        public static bool SendMail(string fromEmail, string fromDisplayName, string pwd, string[] toMail, string toSubject, string toBody)
        {
            ////设置发件人信箱,及显示名字
            MailAddress from = new MailAddress(fromEmail, fromDisplayName);
            //设置收件人信箱,及显示名字 
            //MailAddress to = new MailAddress(TextBox1.Text, "");


            //创建一个MailMessage对象
            MailMessage oMail = new MailMessage();

            oMail.From = from;
            for (int i = 0; i < toMail.Length; i++)
            {
                oMail.To.Add(toMail[i].ToString());
            }


            oMail.Subject = toSubject; //邮件标题 
            oMail.Body = toBody; //邮件内容

            oMail.IsBodyHtml = true; //指定邮件格式,支持HTML格式 
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");//邮件采用的编码 
            //oMail.Priority = MailPriority.High;//设置邮件的优先级为高
            //Attachment oAttach = new Attachment("");//上传附件
            //oMail.Attachments.Add(oAttach); 

            //发送邮件服务器 +
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.neu.edu.cn";// fromEmail.Substring(fromEmail.IndexOf("@") + 1); //163.com指定邮件服务器smtp.sina.com
            client.Credentials = new NetworkCredential(fromEmail, pwd);//指定服务器邮件,及密码

            //发送
            try
            {
                client.Send(oMail); //发送邮件
                oMail.Dispose(); //释放资源
                return true;// "恭喜你！邮件发送成功。";
            }
            catch
            {
                oMail.Dispose(); //释放资源
                return false;// "邮件发送失败，检查网络及信箱是否可用。" + e.Message;
            }


        }
        #endregion

        #region download
        public static void DownLoadFileByStream(SPFile file, HttpResponse Response, string fileName)
        {
            var bytes = file.OpenBinary();
            Stream stream = new MemoryStream(bytes);

            long dataToRead = stream.Length;
            int length;

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //
            bytes = new byte[10000];

            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).Replace("+", "%20"));
            //
            try
            {
                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        length = stream.Read(bytes, 0, bytes.Length);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        bytes = new byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
                Response.End();
            }
            catch
            {

            }

        }
        /// <summary>
        /// 以字符流的形式下载文件之所以转换成 UTF8 是为了支持中文文件名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="Response"></param>
        public static void DownLoadFileByStream(string filePath, string fileName, HttpResponse Response)
        {

            FileInfo fi = new FileInfo(filePath);//fullpath指的是文件的物理路径 
            if (!fi.Exists) return;

            FileStream fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            //
            long dataToRead = fs.Length;
            int length;

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //
            byte[] bytes = new byte[10000];

            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
             var urlEncode = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
             if (urlEncode != null)
             {
                 Response.AddHeader("Content-Disposition", "attachment;  filename=" + urlEncode.Replace("+", "%20"));
             }

            //
            try
            {
                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        length = fs.Read(bytes, 0, bytes.Length);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        bytes = new byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
                fs.Close();
                Response.End();
            }
            catch
            {

            }
            finally {

                if (fs != null)
                {
                    fs.Close();
                }
            }

        }       
        #endregion
        #region 分配作品方法

        /// <summary>
        /// 将数据表中某一列生成数组
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colname"></param>
        /// <returns></returns>
        public static string[] ColumnTostrArray(DataTable dt, string colname)
        {
            string[] arrayA = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                arrayA[i] = Convert.ToString(dr["colname"]);
            }
            return arrayA;
        }

        /// <summary>
        /// 从一个数组中筛选N个不重复随机数组成新的数组
        /// </summary>
        /// <param name="n">随机数个数</param>
        /// <param name="arrayA">原有数组</param>
        /// <returns></returns>
        /// 
        public static string[] GetRandomsArray(long n, params string[] arrayA) //获取随机的n个作品ID
        {
            int aLength = arrayA.Length - 1;
            string value = "";
            Random rd = new Random();//定义生成数组
            if (aLength >= n)
            {
                string[] sortAl = new string[n];
                //n = n > aLength ? aLength : n;//当筛选个数大于数组长度时,筛选个数置为数组长度
                for (long i = 0; i < n; i++)
                {
                    int index = rd.Next(0, aLength);
                    //Thread.Sleep(1000);
                    sortAl[i] = arrayA[index];
                    value += arrayA[index] + ","; //跟踪监视生成的随机数组
                    arrayA[index] = arrayA[aLength];
                    arrayA[aLength] = sortAl[i];
                    aLength--;
                }
                return sortAl;
            }
            else
            {

                n = aLength + 1;
                string[] sortAl = new string[n];
                for (int i = 0; i < n; i++)
                {
                    sortAl[i] = arrayA[i];
                }
                return sortAl;
            }
        }
        #endregion
        #region 列表与表格相互转化
        /// <summary>  
        /// 将集合类转换成DataTable  
        /// </summary>  
        /// <param name="list">集合</param>  
        /// <returns></returns>  
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }



        /// <summary>
        /// 将数据表中某一列生成数组
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnname"></param>
        /// <returns></returns>
        public static string[] TableTostrArray(DataTable dt, string columnname)
        {
            string[] arrayA = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                arrayA[i] = Convert.ToString(dr[columnname]);
            }
            return arrayA;
        }
        /// <summary>
        /// 将数据表行数组转化为数据表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="drRows">数据表行数组</param>
        /// <returns></returns>
        public static DataTable ToDataTable(DataRow[] drRows)
        {
            if (drRows == null || drRows.Length == 0)
            {
                return null;
            }
            DataTable tmp = drRows[0].Table.Clone();  // 复制DataRow的表结构
            foreach (DataRow dr in drRows)
            {
                tmp.Rows.Add(dr.ItemArray);  // 将DataRow添加到DataTable中
            }
            return tmp;
        }
        #endregion

        /// <summary>
        /// 字节表示文件大小转化为易于阅读的单位表示的字符串
        /// </summary>
        /// <param name="size">文件大小字节数</param>
        /// <returns></returns>
        public static string HumanReadableFilesize(double size)
        {
            string[] units = new string[]{ "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
        public static bool UrlCheck(string strUrl)
        {
            if (!strUrl.Contains("http://") && !strUrl.Contains("https://"))
            {
                strUrl = "http://" + strUrl;
            }
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                myRequest.Method = "HEAD";
                myRequest.Timeout = 10000;  //超时时间10秒
                HttpWebResponse res = (HttpWebResponse)myRequest.GetResponse();
                return (res.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
    }

}
