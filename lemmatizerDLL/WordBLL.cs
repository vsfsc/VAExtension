using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace lemmatizerDLL
{
     public class WordBLL
    {
        #region 进行排查，排除的单词写入此表,高中词表中没有的
        /// <summary>
        /// 按照级别筛选，存在直接返回单词与标记列表，没有通过高中词库进行筛查
        /// </summary>
        /// <param name="wordLevel"></param>
        /// <returns></returns>
        public static List<string[]> GetWordLookup( string wordLevel)
        {
            List<string[]> newWords = new List<string[]>();

            DataSet ds = DAL.WordDAL.GetWordByLookupLevel(wordLevel);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string[] wds = new string[2];
                wds[0] = dr["Word"].ToString();
                wds[1] = dr["Signs"].ToString();
                newWords.Add(wds);
            }
            return newWords;
        }
        /// <summary>
        /// 写入数据库再返回
        /// </summary>
        /// <param name="wordInfo"></param>
        /// <param name="wordLevel"></param>
        /// <returns></returns>
        public static List<string[]> GetWordLookup(List<string[]> wordInfo,string wordLevel)
        {
            List<string[]> newWords = new List<string[]>();

            DataSet ds=new DataSet ();
            foreach ( string[] word in  wordInfo )
            {
                ds = DAL.WordDAL.GetWordByLookupName(word[0]);
                if (ds.Tables[0].Rows.Count == 0)
                    newWords.Add(word);
            }
            InsertLookupWord(newWords, wordLevel, ds);
            return newWords;
        }
        //新词写入数据库
        public static bool InsertLookupWord(List<string[]> wordInfo, string wordLevel,DataSet ds)
        {
            using (SqlTransaction trans = DAL.DataProvider.CurrentTransactionEx)
            {
                try
                {
                    DataRow dr ;
                    foreach (string[] word in wordInfo)
                    {
                        dr = ds.Tables[0].NewRow();
                        dr["Word"] = word[0];
                        dr["WordLevel"] = wordLevel;
                        dr["Signs"] = word[1];
                        dr["Description"] = DateTime.Now.ToString();
                        dr["Flag"] = 1;
                        DAL.WordDAL.InsertWordLookup(trans, dr);
                    }
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        /// <summary>
        /// 筛查文章中超纲生词
        /// </summary>
        /// <param name="txtlist"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> GetWordInText(List<List<string>> txtlist,string fileName)
        {
            List<string> wordList = new List<string>();
            Dictionary<string, string> allWordsProp = new Dictionary<string, string>();
            Dictionary<string, int> allWordsLevel = new Dictionary<string, int>();
            string[] words = TextToArray(fileName);//将包含原型与变形及级别的词库文本文件转化为词库数组
            foreach (string word in words)
            {
                string[] wordsProp = Regex.Split(word, "\t");//将一行单词分三段：原型、变型、级别
                //如果原形所对应的变形为空，则变形显示为原形,级别为空，则改为0,key:变形，value:原形
                try
                {
                    int level = 0;
                    if (wordsProp[0] == "")
                        wordsProp[0] = wordsProp[1];
                    level = int.Parse(wordsProp[2]);
                    allWordsProp.Add(wordsProp[0], wordsProp[1]);
                    if (!allWordsLevel.ContainsKey(wordsProp[1]))
                        allWordsLevel.Add(wordsProp[1], level);
                }
                catch
                { }
            }
            return wordList;
        }
        #endregion
        #region 导入数据
        /// <summary>
        /// 0 word 1 wordLevel 2 wordVariation
        /// num==1 1个原型对于多个变形增加新的词汇
        /// nnum=2 对于存在的单词，只更新级别，小级别为主
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool InsertWord(ArrayList wordInfo, int num)
        {
            using (SqlTransaction trans = DAL.DataProvider.CurrentTransactionEx)
            {
                try
                {
                    foreach (List<string> wordProp in wordInfo)
                    {
                        string word = wordProp[0];//原型

                        DataSet ds = DAL.WordDAL.GetWordByName(word);
                        long wordID;
                        bool isNew;
                        DataRow dr;
                        if (ds.Tables[0].Rows.Count == 0 || num == 1)//没有这个单词
                        {
                            isNew = true;
                            foreach (DataRow dr1 in ds.Tables[0].Rows)
                            {
                                if (dr1["WordVariation"].ToString() == wordProp[2].ToString())
                                {
                                    isNew = false;
                                    break;
                                }
                            }
                            if (isNew)
                            {
                                dr = ds.Tables[0].NewRow();
                                dr["Word"] = word;
                                if (wordProp[1] == "")
                                    wordProp[1] = "0";
                                dr["WordLevel"] = int.Parse(wordProp[1]);
                                dr["WordVariation"] = wordProp[2];
                                dr["Flag"] = 1;
                                wordID = DAL.WordDAL.InsertWord( dr);
                            }
                        }
                        else
                        {
                            dr = ds.Tables[0].Rows[0];
                            int tmpWordLevel = 0;
                            int wordLevel = int.Parse(wordProp[1]);//传递级别与保存级别比较

                            int drLevel = int.Parse(dr["WordLevel"].ToString());
                            if (wordLevel < drLevel || drLevel == 0)//只属于一个级别，低级的
                                tmpWordLevel = wordLevel;
                            else
                                tmpWordLevel = drLevel;

                            foreach (DataRow dr1 in ds.Tables[0].Rows)
                            {
                                dr1["WordLevel"] = tmpWordLevel;
                                DAL.WordDAL.UpdateWord( dr1);

                            }
                        }
                    }
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        #endregion
        #region 读取文本进行解析
        /// <summary>
        /// 以表格获取带频次的单词
        /// </summary>
        /// <param name="dWords"></param>
        /// <param name="wordsTimes"></param>
        /// <returns></returns>
        private static DataTable GetWordResultByTxt(Dictionary<string, int> dWords, Dictionary<string, int> wordsTimes)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Word", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("Level", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("Frequency", typeof(int));
            dt.Columns.Add(dc);
            DataRow dr;
            foreach (string eKey in dWords.Keys)
            {
                try
                {
                    dr = dt.NewRow();
                    dr["Word"] = eKey;
                    dr["Level"] = dWords[eKey];
                    dr["Frequency"] = wordsTimes[eKey];
                    dt.Rows.Add(dr);
                }
                catch
                {

                }
            }
            DataRow[] drs = dt.Select("", "Level");// "高中 desc ,基本 desc ,较高 desc,更高 desc");
            DataSet ds = new DataSet();
            ds.Merge(drs);
            return ds.Tables[0];
        }

        /// <summary>
        /// 将按行分布的文本文档转化为字符串数组
        /// </summary>
        /// <param name="fileName">文本文件路径</param>
        /// <returns>字符串数组</returns>
        public static string[] TextToArray(string fileName)
        {
            FileStream aFile = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);

            string str = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            aFile.Close();
            aFile.Dispose();
            string[] words = Regex.Split(str, "\r\n");
            return words;
        }

        /// <summary>
        /// 通过读取词库文本文件，进行词汇原型查找，并确定词汇的级别
        /// </summary>
        /// <param name="srcWords">录入的文本生成的词汇表</param>
        /// <param name="wLevel">选中的最大级别</param>
        /// <param name="fileName">包含原型与变形及级别的词库文件</param>
        /// <param name="isEurope">是否处理欧框词汇</param>
        /// <returns></returns>
        public static Dictionary<int, object> SearchWordsWithTxt(List<List<string>> srcWords,  string fileName,int isEurope)
        {
            string[] words = TextToArray(fileName);//将包含原型与变形及级别的词库文本文件转化为词库数组
            Dictionary<int, object> retWords = new Dictionary<int, object>();//初始化返回值
            Dictionary<string, string> allWordsProp = new Dictionary<string, string>();//
            Dictionary<string, int> allWordsLevel = new Dictionary<string, int>();
            foreach (string word in words)
            {
                string[] wordsProp = Regex.Split(word, "\t");
                //如果原形所对应的变形为空，则变形显示为原形,级别为空，则改为0,key:变形，value:原形
                //try
                //{
                if (wordsProp.Length == 1) break;
                int level = 0;
                if (wordsProp[0] == "")
                    wordsProp[0] = wordsProp[1];
                level = int.Parse(wordsProp[2]);
                if (!allWordsProp.ContainsKey(wordsProp[0]))
                    allWordsProp.Add(wordsProp[0], wordsProp[1]);
                if (!allWordsLevel.ContainsKey(wordsProp[1]))
                    allWordsLevel.Add(wordsProp[1], level);
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
            }

            Dictionary<string, int> wordsTimes = new Dictionary<string, int>();//单词以及出现的次数

            List<string> rWord;
            List<List<string>> sWords = new List<List<string>>();//原词带级别
            Dictionary<string, int> dWords = new Dictionary<string, int>(); ;//不重复的原型
            #region 不处理欧框
            if (isEurope==0)
            {
                foreach (List<string> eWord in srcWords)//遍历文档处理后生成的词汇表（数据项格式为List<string>，共两位：首位保存词汇，二位保存处理要求（0不处理，1处理））
                {
                    string wordInfo = eWord[0];
                    if (eWord[1] == "0")//不处理的，直接返回
                    {
                        rWord = new List<string>();
                        rWord.Add(wordInfo);
                        rWord.Add("-2");
                        rWord.Add("-1");
                    }
                    else
                    {
                        rWord = new List<string>();
                        if (allWordsProp.ContainsKey(wordInfo))//通过变形进行查找
                        {
                            string keyValue;
                            allWordsProp.TryGetValue(wordInfo, out keyValue);
                            rWord.Add(keyValue);
                            rWord.Add(allWordsLevel[keyValue].ToString());
                            rWord.Add("-1");
                        }
                        else if (allWordsProp.ContainsValue(wordInfo))
                        {
                            rWord.Add(wordInfo);
                            rWord.Add(allWordsLevel[wordInfo].ToString());
                            rWord.Add("-1");
                        }
                        else
                        {
                            rWord.AddRange(new string[] { wordInfo, "-1", "-1" });
                        }
                        string wordLevel = rWord[1];
                        if (!wordsTimes.ContainsKey(rWord[0]))
                        {
                            int ciji = int.Parse(rWord[1]);
                            dWords.Add(rWord[0], ciji);//原型
                            wordsTimes.Add(rWord[0], 1);//单词出现的次数，首次为1
                        }
                        else
                        {
                            int wCount = wordsTimes[rWord[0]];
                            wordsTimes[rWord[0]] = wCount + 1;
                        }
                        rWord = new List<string>();
                        rWord.Add(wordInfo);
                        rWord.Add(wordLevel);
                        rWord.Add("-1");
                    }
                    sWords.Add(rWord);

                }
            }
            #endregion
            #region 处理欧框
            else
            {
                foreach (List<string> eWord in srcWords)//遍历文档处理后生成的词汇表（数据项格式为List<string>，共两位：首位保存词汇，二位保存处理要求（0不处理，1处理））
                {
                    string wordInfo = eWord[0];
                    if (eWord[1] == "0")//不处理的，直接返回
                    {
                        rWord = new List<string>();
                        rWord.Add(wordInfo);
                        rWord.Add("-2");
                    }
                    else
                    {
                        rWord = new List<string>();
                        if (allWordsProp.ContainsKey(wordInfo))//通过变形进行查找
                        {
                            string keyValue;
                            allWordsProp.TryGetValue(wordInfo, out keyValue);
                            rWord.Add(keyValue);
                            rWord.Add(allWordsLevel[keyValue].ToString());
                        }
                        else if (allWordsProp.ContainsValue(wordInfo))
                        {
                            rWord.Add(wordInfo);
                            rWord.Add(allWordsLevel[wordInfo].ToString());
                        }
                        else
                            rWord.AddRange(new string[] { wordInfo, "-1" });
                        string wordLevel = rWord[1];
                        if (!wordsTimes.ContainsKey(rWord[0]))
                        {
                            int ciji = int.Parse(rWord[1]);
                            dWords.Add(rWord[0], ciji);//原型
                            wordsTimes.Add(rWord[0], 1);//单词出现的次数，首次为1
                        }
                        else
                        {
                            int wCount = wordsTimes[rWord[0]];
                            wordsTimes[rWord[0]] = wCount + 1;
                        }
                        rWord = new List<string>();
                        rWord.Add(wordInfo);
                        rWord.Add(wordLevel);
                    }
                    sWords.Add(rWord);

                }
            }
            #endregion
            retWords.Add(0, sWords);//返回retWords键值对中第一个数据集：输入文本中所有原词对应级别的列表，数据格式是List，内部存储的数据还是一个两位的List，分别对应文本中的单词和级别
            DataTable dt = GetWordResultByTxt(dWords, wordsTimes);
            retWords.Add(1, dt);//返回retWords键值对中第二个数据集：要输出的单词与对应频次，格式是数据表DataTable
            retWords.Add(2, wordsTimes);//返回retWords键值对中第三个数据集：文本中所有词汇经处理后的原型及对应级别，格式是键值对Dictionary<string, int>，键是单词原型，值是词汇级别
            //Dictionary<string, int> wordsTimes =(Dictionary<string, int>) ret[2];
            //WriteIntoDB(wordsTimes);
            return retWords;
        }

        /// <summary>
        /// 对字符串按照给定的字符串分割，然后读取每个分段的第一行当作该段文本的标题标记，其余内容作为内容，存入数据库,并返回本次处理的批次号
        /// </summary>
        /// <param name="texts">输入的文本</param>
        /// <param name="splits">分隔符</param>
        /// <param name="splitstr">todo: describe splitstr parameter on SplitTextToDB</param>
        /// <returns></returns>
        public static int SplitTextToDB(string texts,string splitstr)
        {
            string tempStr = "";
            string strRet = "";
            List<string> tempList = new List<string>();
            List<string[]> lists = new List<string[]>();
            string[] resultStr = Regex.Split(texts, "\r\n", RegexOptions.IgnoreCase);
            //第一步：除去空行
            for (int i = 0; i < resultStr.Length; i++)
            {
                string s = resultStr[i];
                if (s.Trim().Length > 0)
                {
                    tempList.Add(s);
                }
            }
            Dictionary<string, string> alltxt = new Dictionary<string, string>();
            tempStr = tempList[0];
            //第二步：分割文本
            for (int i = 0; i < tempList.Count; i++)
            {
                string s = tempList[i];
                if (s.Equals(splitstr))
                {
                    strRet = strRet.Replace(tempStr, "");
                    alltxt.Add(tempStr, strRet);
                    if (i != tempList.Count - 1)
                    {
                        tempStr = tempList[i + 1];
                    }
                    //tempList.Add(strRet);
                    strRet = "";
                }
                else
                {
                    strRet += s + " ";
                }

            }
            //第三步：写入数据库
            int k=InsertNewTxt(alltxt);

            //第四步，做生词筛查处理

            //第五步，输出生词表
            return k;
        }

        /// <summary>
        /// 将键值对中的数据写入数据库，并返回处理批次号
        /// </summary>
        /// <param name="dict">要处理的键值对</param>
        /// <returns></returns>
        public static int InsertNewTxt(Dictionary<string,string> txts)
        {
            using (SqlTransaction trans = DAL.DataProvider.CurrentTransactionEx)
            {
                try
                {
                    DataSet ds = DAL.WordDAL.GetTxts();
                    DataTable dt = ds.Tables[0];
                    int marker = 0;
                    if (dt.Rows.Count > 0)
                    {
                        marker = int.Parse(dt.Compute("Max(Marker)", "").ToString());
                    }

                    DataRow dr;
                    foreach (KeyValuePair<string, string> txt in txts)
                    {
                        dr = dt.NewRow();
                        dr["Title"] = txt.Key;
                        dr["Contents"] = txt.Value;
                        dr["Marker"] = marker + 1;
                        dr["CreatedDate"] = DateTime.Now;
                        dr["Flag"] = 1;
                        DAL.WordDAL.InsertTxts(trans, dr);
                    }
                    trans.Commit();
                    return marker + 1;
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        public static string leveltoSigns(int level)
        {
            string signs;
            switch (level)
            {
                case 6:
                    signs ="";//基本要求
                    break;
                case 7:
                    signs ="★";//较高要求
                    break;
                case 8:
                    signs = "▲";//更高要求
                    break;
                default:
                    signs = "◆";//超纲词汇
                    break;
            }
            return signs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ArrayList cibiaoku(string fileName)
        {
            ArrayList arr = new ArrayList();
            FileStream aFile = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            Dictionary<int, object> retWords = new Dictionary<int, object>();
            string str = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            aFile.Close();
            aFile.Dispose();
            string[] words = Regex.Split(str, "\r\n");
            Dictionary<string, string> allWordsProp = new Dictionary<string, string>();
            Dictionary<string, int> allWordsLevel = new Dictionary<string, int>();
            foreach (string word in words)
            {
                string[] wordsProp = Regex.Split(word, "\t");
                //如果原形所对应的变形为空，则变形显示为原形,级别为空，则改为0,key:变形，value:原形
                try
                {
                    int level = 0;
                    if (wordsProp[0] == "")
                    {
                        wordsProp[0] = wordsProp[1];
                    }
                    level = int.Parse(wordsProp[2]);
                    allWordsProp.Add(wordsProp[0], wordsProp[1]);
                    if (!allWordsLevel.ContainsKey(wordsProp[1]))
                    {
                        allWordsLevel.Add(wordsProp[1], level);
                    }
                }
                catch
                { }
            }
            arr.Add(allWordsLevel);
            arr.Add(allWordsProp);
            return arr;
        }
        #endregion
        #region 通过数据库解析给定文本
        /// <summary>
        /// 以表格形式返回不重复的单词原形，结果直接输出
        /// </summary>
        /// <param name="dWords"></param>
        /// <returns></returns>
        private static DataTable GetWordResult(ArrayList dWords)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("词汇", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("高中", typeof(string));//2016-10-17 15:34:16 增补高中大纲词汇表
            dt.Columns.Add(dc);
            dc = new DataColumn("基本", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("较高", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("更高", typeof(string));
            dt.Columns.Add(dc);
            DataRow dr;
            foreach (List<string> eWord in dWords)
            {
                try
                {
                    dr = dt.NewRow();
                    dr["词汇"] = eWord[0];
                    int dLevel = int.Parse(eWord[1].ToString());
                    //////////////////////////////////////////////////////////////////////////
                    //词汇级别变更：2016-10-17 15:35:20 增补高中大纲词汇表，级别序号从原有1基本、2较高、3更高 三级改为 5高中、6基本、7较高、8更高 四级。
                    //////////////////////////////////////////////////////////////////////////
                    if (dLevel == 5)
                        dr["高中"] = "√";
                    if (dLevel == 6)
                        dr["基本"] = "√";
                    if (dLevel == 7)
                        dr["较高"] = "√";
                    if (dLevel == 8)
                        dr["更高"] = "√";
                    dt.Rows.Add(dr);
                }
                catch
                {

                }
            }
            DataRow[] drs = dt.Select("", "高中 desc ,基本 desc ,较高 desc,更高 desc");
            DataSet ds = new DataSet();
            ds.Merge(drs);
            return ds.Tables[0];
        }
        /// <summary>
        /// 将数据表的中的数据一次全读到内存中
        /// </summary>
        /// <param name="srcWords"></param>
        /// <returns></returns>
        public static ArrayList SearchWordsNew(List<string> srcWords, int wLevel)
        {
            ArrayList sWords = new ArrayList();//返回源给定单词的值
            ArrayList dWords = new ArrayList();//
            ArrayList allWords = new ArrayList();
            List<string> rWord = new List<string>();
            DataSet ds = DAL.WordDAL.GetAllWords();
            foreach (string wordInfo in srcWords)
            {

                rWord = GetWordInfoNew(ds.Tables[0], wordInfo);
                int gLevel = int.Parse(rWord[1].ToString());
                if (gLevel <= wLevel)
                {
                    if (!dWords.Contains(rWord))
                        dWords.Add(rWord);
                    string wordLevel = rWord[1];
                    rWord = new List<string>();
                    rWord.Add(wordInfo);
                    rWord.Add(wordLevel);
                    sWords.Add(rWord);
                }
            }
            allWords.Add(sWords);
            DataTable dt = GetWordResult(dWords);
            allWords.Add(dt);
            return allWords;
        }
        /// <summary>
        /// 多次访问数据表
        /// </summary>
        /// <param name="srcWords"></param>
        /// <returns></returns>
        public static ArrayList SearchWords(List<string> srcWords, int wLevel)
        {
            ArrayList sWords = new ArrayList();//返回源给定单词的值
            ArrayList dWords = new ArrayList();//
            Dictionary<string, string> quChongWords = new Dictionary<string, string>();
            ArrayList allWords = new ArrayList();
            List<string> rWord = new List<string>();

            foreach (string wordInfo in srcWords)
            {
                if (wordInfo != "")
                {
                    rWord = GetWordInfo(wordInfo);
                    int gLevel = int.Parse(rWord[1].ToString());
                    if (gLevel <= wLevel)
                    {
                        if (!quChongWords.ContainsKey(rWord[0]))//去重
                        {
                            quChongWords.Add(rWord[0], rWord[1]);
                            dWords.Add(rWord);
                        }

                        string wordLevel = rWord[1];
                        rWord = new List<string>();
                        rWord.Add(wordInfo);
                        rWord.Add(wordLevel);
                        sWords.Add(rWord);
                    }
                }
            }
            allWords.Add(sWords);
            DataTable dt = GetWordResult(dWords);
            allWords.Add(dt);
            return allWords;
        }
        private static List<string> GetWordInfoNew(DataTable dt, string wordName)
        {
            List<string> rWord = new List<string>();
            DataRow[] drs = dt.Select("WordVariation='" + wordName + "'");
            if (drs.Length == 0)
            {
                drs = dt.Select("Word='" + wordName + "'");
            }
            if (drs.Length > 0)
            {
                DataRow dr = drs[0];
                string wordInfo = dr["Word"].ToString();
                if (!dr.IsNull("WordLevel") && !string.IsNullOrEmpty(dr["WordLevel"].ToString()))
                {
                    rWord.Add(wordInfo);
                    rWord.Add(dr["WordLevel"].ToString());
                }
                else
                {
                    rWord.Add(wordInfo);
                    rWord.Add("0");
                }
            }
            else
            {
                rWord.Add(wordName);
                rWord.Add("-1");
            }
            return rWord;
        }
        #endregion
        #region 解析给定文本
        private static List<string> GetWordInfo(string wordName)
        {
            DataSet ds = DAL.WordDAL.GetWordVariationByVariation(wordName);
            List<string> rWord = new List<string>();
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds = DAL.WordDAL.GetWordByName(wordName);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string wordInfo = dr["Word"].ToString();
                if (!dr.IsNull("WordLevel") && !string.IsNullOrEmpty(dr["WordLevel"].ToString()))
                {
                    rWord.Add(wordInfo);
                    rWord.Add(dr["WordLevel"].ToString());
                }
                else
                {
                    rWord.Add(wordInfo);
                    rWord.Add("0");
                }
            }
            else
            {
                rWord.Add(wordName);
                rWord.Add("-1");
            }
            return rWord;
        }
        private string CountWords(string txts)//这是要统计的文本
        {
            int digitCount = 0;//数字个数
            int letterCount = 0;//字母个数
            int chineseCount = 0;//中文个数
            int otherCount = 0;//其他个数
            char[] ch = txts.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (char.IsDigit(ch[i]))//判断数字
                {
                    digitCount++;
                }
                else if ((ch[i] >= 65 && ch[i] <= 90) || (ch[i] >= 97 && ch[i] <= 122))//判断字母
                {
                    letterCount++;
                }
                else if (ch[i] >= 0x4E00 && ch[i] <= 0x9FA5)//判断汉字
                {
                    chineseCount++;
                }
                else//其他……
                {
                    otherCount++;
                }
            }
            string msg = string.Empty;
            msg = "汉字个数：" + chineseCount.ToString() + "字母个数：" + letterCount.ToString() + "其它字符个数：" + otherCount.ToString();
            return msg;
        }
        #endregion
        #region 将词频写入数据库
        public static  bool  WriteIntoDB( Dictionary<string, int> wordsTimes)
        {

                DataSet ds;
                DataRow dr;
                try
                {
                    foreach (KeyValuePair<string, int> wKey in wordsTimes)
                    {
                        ds = DAL.WordDAL.GetWordFrequencyByName( wKey.Key);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr = ds.Tables[0].Rows[0];
                            dr["Frequency"] = (int)dr["Frequency"] + wKey.Value;
                            DAL.WordDAL.UpdateWordFrequency( dr);
                        }
                        else
                        {
                            dr = ds.Tables[0].NewRow();
                            dr["WordName"] = wKey.Key;
                            dr["Frequency"] = wKey.Value;
                            DAL.WordDAL.InsertWordFrequency( dr);
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        #endregion
    }
}