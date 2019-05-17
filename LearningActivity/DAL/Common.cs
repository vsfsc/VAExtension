using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;

namespace LearningActivity.DAL
{
    /// <summary>
    /// 扩展通用功能
    /// </summary>
    public class Common
    {
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
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        static public void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
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
        public static SPWeb SPWeb
        {
            get
            {
                return SPContext.Current.Web;
            }
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
        /// <summary>
        /// 计算两个日期之间相差的天数
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns></returns>
        public static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days;
        }
        /// <summary>
        /// 判断输入的内容是否是数字
        /// </summary>
        /// <param name="strIn">输入的内容</param>
        /// <returns></returns>
        public static bool IsInteger(string strIn)
        {
            bool bolResult = true;
            if (strIn == "")
            {
                bolResult = false;
            }
            else
            {
                foreach (char Char in strIn)
                {
                    if (char.IsNumber(Char))
                        continue;
                    else
                    {
                        bolResult = false;
                        break;
                    }
                }
            }
            return bolResult;
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
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).Replace("+", "%20"));

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
            finally
            {

                if (fs != null)
                {
                    fs.Close();
                }
            }

        }
        #region 外加的方法
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

        public static double TimeToSeconds(string dtString)
        {
            double seconds = 0;
            string[] ss = dtString.Split(':');
            //for (int i = 0; i < ss.Length; i++)
            //{
            //    double 
            //    if (i==0)
            //    {
            //        int
            //    }
            //    seconds += double.Parse(ss[i]);
            //}
            seconds = double.Parse(ss[0])*3600 + double.Parse(ss[1])*60 + double.Parse(ss[2]);
            return seconds;
        }
        public static ArrayList MakelList()
        {
            string[] hrList=new string[24];
            string[] minList=new string[60];
            
            for (int i = 0; i < 60; i++)
            {
                minList[i]=i.ToString("00");
            }
            for (int i =7; i < 24; i++)
            {
                hrList[i - 7] = i.ToString("00");
            }
            for (int i =0; i < 7; i++)
            {
                hrList[i + 17] = i.ToString("00");
            }
            ArrayList array = new ArrayList();
            array.Add(hrList);
            array.Add(minList);
            return array;
        }
        #endregion
        /// <summary>
        /// 生成两个日期之间的日期数组
        /// </summary>
        /// <param name="dtStart">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns></returns>
        public static string[] DtStrings(DateTime dtStart,DateTime dtEnd)
        {
            int days = DateDiff(dtStart, dtEnd);
            string[] dtStrings = new string[days];
            for (int i = 0; i < days; i++)
            {
                dtStrings[i] =string.Format("{0:d}",dtStart.AddDays(i));
            }
            return dtStrings;
        }
    }

}
