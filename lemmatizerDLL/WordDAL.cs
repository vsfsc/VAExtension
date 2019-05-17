using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace lemmatizerDLL.DAL
{
    public class WordDAL
    {
        public static long InsertWord(DataRow dr)
        {
            return ((long)DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(DAL.DataProvider.ConnectionString, "InsertWord", dr)[0].Value);
        }
        public static long InsertWordLookup(SqlTransaction trans, DataRow dr)
        {
            return ((long)DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(trans, "InsertWordLookup", dr)[0].Value);
        }
        public static long InsertWord(SqlTransaction trans, DataRow dr)
        {
            return ((long)DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(trans, "InsertWord", dr)[0].Value);
        }
        /// <summary>
        /// 文本数据写入数据库
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertTxts(SqlTransaction trans, DataRow dr)
        {
            return ((long)DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(trans, "InsertTxts", dr)[0].Value);
        }
        public static long InsertWordFrequency(DataRow dr)
        {
            return ((long)DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(DAL.DataProvider.ConnectionString, "InsertWordFrequency", dr)[0].Value);
        }
        public static DataSet GetWordFrequencyByName(string word)
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetWordFrequencyByName", word);
            return ds;
        }
        public static DataSet GetTxts()
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetTxts");
            return ds;
        }
        public static DataSet GetWordByName(string word)
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetWordByName", word);
            return ds;
        }
        public static DataSet GetWordByLookupName(string word)
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetWordByLookupName", word);
            return ds;
        }
        public static DataSet GetAllWords()
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetAllWords");
            return ds;
        }
        //新通用教材单词排除
        public static DataSet GetWordByLookupLevel(string wordLevel)
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetWordByLookupLevel", wordLevel);
            return ds;
        }
        public static DataSet GetWordLevel()
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetWordLevel");
            return ds;
        }
        public static DataSet GetWordVariationByVariation(string variation)
        {
            DataSet ds = DAL.SqlHelper.ExecuteDataset(DAL.DataProvider.ConnectionString, "GetWordVariationByVariation", variation);
            return ds;
        }
        public static int UpdateWord(DataRow dr)
        {
            return (DAL.SqlHelper.ExecuteAppointedParameters(DAL.DataProvider.ConnectionString, "UpdateWord", dr));
        }
        public static int UpdateWord(SqlTransaction trans, DataRow dr)
        {
            return (DAL.SqlHelper.ExecuteAppointedParameters(trans, "UpdateWord", dr));
        }
        public static int UpdateWordFrequency(DataRow dr)
        {
            return (DAL.SqlHelper.ExecuteAppointedParameters(DAL.DataProvider.ConnectionString, "UpdateWordFrequency", dr));
        }
    }
}
