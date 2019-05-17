using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using Microsoft.SharePoint;

namespace lemmatizerDLL
{
    public class TextInput
    {
        #region 读取文档转化为含标记的字符串列表


        /// <summary>
        /// 原文字符串转化为单词与分隔符列表
        /// </summary>
        /// <param name="articleStr">原文字符串</param>
        /// <param name="ignoreWordsFile">忽略处理词汇文档</param>
        /// <param name="ordWordsFile">合成词词汇文档</param>
        /// <param name="sumbolFile">标点符号文档</param>
        /// <returns >存储有单词和标记的列表的列表</returns>
        public static List<List<string>> ArticleToList(string articleStr, string ignoreWordsFile, string ordWordsFile, string sumbolFile)
        {
            var allWords = new Dictionary<string, int>();//键值对,string存单词,int存标记
            char[] chars = articleStr.ToCharArray();
            //var articleList = new ArrayList();
            var allwordsls = new List<List<string>>();
            var tempStr = string.Empty;
            string[] ignoreWords = File.ReadAllLines(ignoreWordsFile);//忽略词汇表
            string[] ordinalWords = File.ReadAllLines(ordWordsFile);//专有名词或者常见词(即不做lemma处理的词汇)
            //const string tagsStr = ",，.。:：;；@（）()“”?？!！#&\"/_—=+";//标点符号字符串
            var tagsReg = new Regex(@"\p{P}");//标点符号正则表达式
            string[] symbols = File.ReadAllLines(sumbolFile);//标点符号数组
            for (long i = 0; i < chars.Length; i++)
            {
                if (IsInArray(chars[i].ToString(), symbols) || (int)chars[i] == 10 || (int)chars[i] == 13 || (int)chars[i] == 32) //是标点符号类字符
                {
                    if (tempStr.Length != 0 || tempStr != "")//tempStr变量已有单词存入,需要先将其加入数组列表
                    {
                        if (tempStr.Length == 1 && tempStr != "a")//只有一个字母,但不是"a"的单个字母的单词不做处理
                        {
                            var wdList = new List<string> { tempStr, "0" };//单字母且不是a的单词
                            allwordsls.Add(wdList);
                            //articleList.Add(wdList);
                        }
                        else if (tempStr.IndexOf("'", StringComparison.Ordinal) > -1 || tempStr.IndexOf("’", StringComparison.Ordinal) > -1 || tempStr.IndexOf("‘", StringComparison.Ordinal) > -1 || HasNumber(tempStr) || Regex.IsMatch(tempStr, "-") || Array.IndexOf<string>(ordinalWords, tempStr) != -1 || Regex.IsMatch(tempStr, "[A-Z]") || IsInArray(tempStr, ignoreWords))
                        //单词串中包含’‘',即为合成词（中英文状态的单引号都算）或者包含数字、或者是罗马数字,或者属于标记为不做lemma处理的常见词汇,或者包含大写字母,或者跳过不用处理的单词
                        {
                            var wdList = new List<string> { tempStr, "0" };
                            allwordsls.Add(wdList);
                        }
                        else //需正常处理的单词
                        {
                            var wdList = new List<string> { tempStr, "1" };
                            allwordsls.Add(wdList);
                            //articleList.Add(wdList);
                        }
                    }
                    //var signList = new List<string> { chars[i].ToString(), "0" };//符号
                    //allwordsls.Add(signList);
                    tempStr = string.Empty;//缓存字符串置空,等待下次填充
                }
                else//字符是字母
                {
                    tempStr = tempStr + chars[i].ToString();
                }
            }
            //处理最后一个单词外面没有标点符号或者全文只有一个单词的情况
            var tmList = new List<string>();
            if (allwordsls.Count > 0)//2017-1-4
                tmList = (List<string>)allwordsls[allwordsls.Count - 1];
            if (tempStr.Length > 0 && (allwordsls.Count == 0 || tmList[0] != tempStr))
            {
                var signList = new List<string> { tempStr, "1" };
                allwordsls.Add(signList);
            }
            return allwordsls;
        }

        /// <summary>
        /// 判断指定字符是否包含于给定字符串数组中
        /// </summary>
        /// <param name="str">判断字符</param>
        /// <param name="markArr">字符串数组</param>
        /// <returns>exists是否包含于</returns>
        public static bool IsInArray(string str, string[] markArr)
        {
            bool exists = Array.Exists(markArr, element => element == str);
            return exists;
        }

        /// <summary>
        /// 从文件直接读取文本,支持格式:rtf,txt,doc,docx
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string FileRead(string filePath)
        {
            string line = string.Empty;
            string tempDataStr = string.Empty;
            //try
            //{
            //{
            //FileStream aFile = new FileStream(filePath, FileMode.Open);
            //StreamReader sr = new StreamReader(aFile);
            //string str = sr.ReadToEnd();
            //sr.Close();
            SPSecurity.RunWithElevatedPrivileges(delegate()
           {
               if (filePath.Length > filePath.LastIndexOf(".", System.StringComparison.Ordinal))
               {
                   string fileTypeStr = filePath.Substring(filePath.LastIndexOf(".", System.StringComparison.Ordinal));
                   if (fileTypeStr == ".txt")//txt文件
                   {
                       foreach (string lines in File.ReadLines(filePath, Encoding.Default))
                       {
                           tempDataStr = tempDataStr + lines; //按行依次读取文件中数据
                       }
                   }
                   //else if (fileTypeStr == ".rtf")//rtf文件
                   //{
                   //    tempDataStr = File.ReadAllText(filePath, Encoding.Default);
                   //}
                   //else if (fileTypeStr == ".doc" || fileTypeStr == ".docx")//word文件
                   //{
                   //    try
                   //    {
                   //        Word.Application wApp = new Word.Application();
                   //        Word.Document doc = null;
                   //        object unknow = Type.Missing;
                   //        wApp.Visible = false;
                   //        object file = filePath;
                   //        doc = wApp.Documents.Open(ref file,
                   //            ref unknow, ref unknow, ref unknow, ref unknow,
                   //            ref unknow, ref unknow, ref unknow, ref unknow,
                   //            ref unknow, ref unknow, ref unknow, ref unknow,
                   //            ref unknow, ref unknow, ref unknow);
                   //        for (int i = 0; i < doc.Paragraphs.Count; i++)
                   //        {
                   //            tempDataStr = tempDataStr + doc.Paragraphs[i].Range.Text.Trim();
                   //        }
                   //        CloseFile(wApp);
                   //        QuitApp(wApp);

                   //    }
                   //    catch (Exception ex)
                   //    {

                   //    }
                   //}
                   else
                   {
                       tempDataStr = "你选择导入的文件格式不正确,仅支持导入.txt文件！";
                   }
               }
               else
               {
                   tempDataStr = "文件路径格式不正确!";
               }
               //}
               //}
               //catch
               //{

               //}

           });
            return tempDataStr;
        }
        #endregion

        #region 文本文档读写

        //private static void CloseFile(Word.Application wordApp)
        //{
        //    try
        //    {
        //        object unknow = Type.Missing;
        //        object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
        //        wordApp.ActiveDocument.Close(ref saveChanges, ref unknow, ref unknow);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private static void  QuitApp(Word.Application app)
        //{
        //    try
        //    {
        //        object unknow = Type.Missing;
        //        object saveChanges = Word.WdSaveOptions.wdSaveChanges;
        //        app.Quit(ref saveChanges, ref unknow, ref unknow);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        /// <summary>
        /// 去除特殊字符
        /// </summary>
        /// <param name="strHtml">原字符串</param>
        /// <param name="tag">特殊字符</param>
        /// <returns>处理后字符串</returns>
        public static string FilterSpecial(string strHtml,string tag)
        {

            if (string.Empty == strHtml)
            {
                return strHtml;
            }
            string[] aryReg = { "'", "'delete", "?", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "_", ";", "||", "[", "]", "&", "/", "-", "|", " ", "''" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                strHtml = strHtml.Replace(aryReg[i], tag);
            }
            return strHtml;
        }

        /// <summary>
        /// 文本文件保存
        /// </summary>
        /// <param name="txtTitle">文档标题</param>
        /// <param name="txtStr">文档内容</param>
        /// <param name="txtPath">文档路径</param>
        public static void  FileWrite(string txtTitle,string txtStr,string txtPath)
        {
            if (!Directory.Exists(txtPath))
            {
                Directory.CreateDirectory(txtPath);
            }
            txtPath = txtPath + txtTitle;
            // DateTime.Today.ToString("yyyyMMddHHmmssffff") + ".txt";//文章名+操作者姓名+年月日时分秒4位毫秒作为文件名
            FileStream fs = new FileStream(txtPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(txtStr);
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 读取文本文件生成文本字符串
        /// </summary>
        /// <param name="fileName">文本文件全路径</param>
        /// <returns>文本字符串</returns>
        public static string ReadTxt(string fileName)
        {
            FileStream aFile = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile, System.Text.Encoding.Default);
            string txt = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            aFile.Close();
            aFile.Dispose();
            return txt;
        }
        #endregion

        #region 字符串处理

        /// <summary>
        /// 弹出提示框脚本
        /// </summary>
        /// <param name="info">提示信息内容</param>
        /// <param name="p">执行对象</param>
        public static void PageAlert(string info, Page p)
        {
            string script = "<script>alert('" + info + "')</script>";
            p.ClientScript.RegisterStartupScript(p.GetType(), "", script);
        }

        /// <summary>
        /// 根据已有的组合词词汇文档拆分组合单词
        /// </summary>
        /// <param name="myWord">组合词</param>
        /// <param name="txtlines">组合词文档行数组</param>
        /// <returns>拆分结构列表</returns>
        public static List<string> WordSplit(string myWord, string[] txtlines)
        {
            List<string> wordList=new List<string>();

            int n=txtlines.Length;
            string[] wordLines=new string[n];
            string[] splitLines = new string[n];
            for (int i = 0; i < n; i++)
            {
                string wordStr = txtlines[i].ToString();
                if(wordStr.Split(':').Length>1)
                {
                    wordLines[i] = wordStr.Split(':')[0];//获取:前的子字符串,即组合词汇
                    splitLines[i] = wordStr.Split(':')[1];//获取:后的子字符串,即词汇的拆分形式
                }

            }
            bool bl = false;
            for(int j=0;j<n;j++)
            {
                if (wordLines[j] == myWord)//找到了合成词的拆分形式,生成拆分数组
                {
                    string[] sArray = splitLines[j].Split(',');
                    wordList = new List<string>(sArray);
                    bl = true;
                    break;
                }
            }
            if(bl==false)
            {
                wordList.Add(myWord);//没找到该合成词的拆分形式,直接输出
                //string[] sArray =new string[1]{myWord};
                //wordList =new List<string>(sArray);
            }
            return wordList;
        }

        /// <summary>
        /// 判断字符串是否包含阿拉伯数字或者罗马数字
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns>T or F</returns>
        public static bool HasNumber(string str)
        {
            bool hasnumber = false;
            Regex r = new Regex(@"^m*(d?c{0,3}|c[dm])" + "(l?x{0,3}|x[lc])(v?i{0,3}|i[vx])$");//罗马数字
            if (r.Match(str).Success) //包含罗马数字
            {
                hasnumber = true;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] > 47 && str[i] < 58)//是阿拉伯数字
                    {
                        hasnumber = true;
                        break;
                    }
                }
            }
            return hasnumber;
        }
        #endregion

        #region 统计文本中单词的个数
        /// <summary>
        /// 统计文本中单词的个数
        /// </summary>
        /// <param name="value">被统计的值</param>
        /// <returns>结果long</returns>
        public static long countWords(string value)
        {
            long len = 0;
                        // 替换中文字符为空格
            value = value.Replace(@"/[\u4e00-\u9fa5]+/g", " ");
                        // 将换行符，前后空格不计算为单词数
            value = value.Replace(@"/\n|\r|^\s+|\s+$/gi", "");
                        // 多个空格替换成一个空格
            value = value.Replace(@"/\s+/gi", " ");
                        // 更新计数
            Regex rx=new Regex(@"/\s/g", RegexOptions.IgnoreCase);
            MatchCollection ms = rx.Matches(value);
            foreach (Match ma in ms)
            {
                if (ma.Success)
                {
                    len = len + 1;
                }

            }
            if (ms.Count==0)
            {
                len = 0;
            }
            return len;
        }
        #endregion
    }
}
